using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Cannon : MonoBehaviour
{
	
	//a vector that points in the middle between left and right parts of the slingshot
	private Vector3 CannonMiddleVector;
	
	[HideInInspector]
	public CannonState CannonState;
	
	//the left and right parts of the slingshot
	public Transform CannonOrigin;
//	public Transform LeftCannonOrigin, RightCannonOrigin;
	
	//two line renderers to simulate the "strings" of the slingshot
	public LineRenderer CannonLineRenderer;

//	public LineRenderer CannonLineRenderer1;
//	public LineRenderer CannonLineRenderer2;
	
	//this linerenderer will draw the projected trajectory of the thrown panda
	public LineRenderer TrajectoryLineRenderer;
	
	[HideInInspector]
	//the panda to throw
	public GameObject PandaToThrow;
	
	//the position of the panda tied to the slingshot
	public Transform PandaWaitPosition;
	
	public float ThrowSpeed;
	
	[HideInInspector]
	public float TimeSinceThrown;

	public float RotationSpeed = 5;
	
	// Use this for initialization
	void Start()
	{
		//set the sorting layer name for the line renderers
		//for the slingshot renderers this did not work so I
		//set the z on the background sprites to 10
		//hope there's a better way around that!
		CannonLineRenderer.sortingLayerName = "Foreground";

//		CannonLineRenderer1.sortingLayerName = "Foreground";
//		CannonLineRenderer2.sortingLayerName = "Foreground";
		TrajectoryLineRenderer.sortingLayerName = "Foreground";
		
		CannonState = CannonState.Idle;

//		CannonLineRenderer.SetPosition(0, CannonOrigin.position);
//		CannonLineRenderer1.SetPosition(0, LeftCannonOrigin.position);
//		CannonLineRenderer2.SetPosition(0, RightCannonOrigin.position);
		
		//pointing at the middle position of the two vectors
		CannonMiddleVector = new Vector3((CannonOrigin.position.x * 1.5f) / 2, (CannonOrigin.position.y * 1.5f) / 2, 0);

/*		CannonMiddleVector = new Vector3((LeftCannonOrigin.position.x + RightCannonOrigin.position.x) / 2,
		                                 (LeftCannonOrigin.position.y + RightCannonOrigin.position.y) / 2, 0);
*/
}
	
	// Update is called once per frame
	void Update()
	{
		switch (CannonState)
		{
			case CannonState.Idle:
				//fix panda's position
				InitializePanda();
				//display the cannon "trigger"
//				DisplayCannonLineRenderer();
				if (Input.GetMouseButtonDown(0))
				{
					//get the point on screen user has tapped
					Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					//if user has tapped onto the panda
					if (PandaToThrow.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
					{
						CannonState = CannonState.UserPulling;
					}
				}
				break;
			case CannonState.UserPulling:
//				DisplayCannonLineRenderer();
				
				if (Input.GetMouseButton(0))
				{
					//get where user is tapping
					Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					location.z = 0;
					
					transform.LookAt(location);

					transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.z * 2f));


					//we will let the user pull the panda up to a maximum distance
					if (Vector3.Distance(location, CannonMiddleVector) > 1.5f)
					{
						//basic vector maths :)
						var maxPosition = (location - CannonMiddleVector).normalized * 1.5f + CannonMiddleVector;
						
						PandaToThrow.transform.position = maxPosition;
					}
					else
					{
						PandaToThrow.transform.position = location;
					}
					float distance = Vector3.Distance(CannonMiddleVector, PandaToThrow.transform.position);
					//display projected trajectory based on the distance
					DisplayTrajectoryLineRenderer(distance);
				}
				else//user has removed the tap 
				{
					SetTrajectoryLineRenderesActive(false);
					//throw the panda!!!
					TimeSinceThrown = Time.time;
					float distance = Vector3.Distance(CannonMiddleVector, PandaToThrow.transform.position);
					if (distance > 1)
					{
						SetCannonLineRendererActive(false);
						CannonState = CannonState.PandaFlying;
						ThrowPanda(distance);
					}
					else//not pulled long enough, so reinitiate it
					{
						//distance/10 was found with trial and error :)
						//animate the panda to the wait position
						PandaToThrow.transform.positionTo(distance / 10, //duration
						                                  PandaWaitPosition.transform.position). //final position
							setOnCompleteHandler((x) =>
							                     {
								x.complete();
								x.destroy();
								InitializePanda();
							});
					}
				}
				break;
			case CannonState.PandaFlying:
				break;
			default:
				break;
		}

	}

	private void ThrowPanda(float distance)
	{
		transform.GetComponent<Animator> ().SetBool ("Fire", true);

/*
		animation.PlayQueued("Fire");
		WaitForAnimation(animation);
*/
		//get velocity
		Vector3 velocity = CannonMiddleVector - PandaToThrow.transform.position;
		PandaToThrow.GetComponent<Panda>().OnThrow(); //make the panda aware of it

		PandaToThrow.GetComponent<Panda>().transform.GetComponent<Animator> ().SetBool ("Mooving", true);

		//old and alternative way
		//PandaToThrow.GetComponent<Rigidbody2D>().AddForce
		//    (new Vector2(v2.x, v2.y) * ThrowSpeed * distance * 300 * Time.deltaTime);
		//set the velocity
		PandaToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, -velocity.y) * ThrowSpeed * distance;
		
		
		//notify interested parties that the panda was thrown
		if (PandaThrown != null)
			PandaThrown(this, EventArgs.Empty);
	}
	
	public event EventHandler PandaThrown;

	private IEnumerator WaitForAnimation (Animation animation)
	{
		do
		{
			yield return null;
		} while (animation.isPlaying);
	}

	private void InitializePanda()
	{
		//initialization of the ready to be thrown panda
		PandaToThrow.transform.position = PandaWaitPosition.position;

		CannonState = CannonState.Idle;
		SetCannonLineRendererActive(true);
	}
	
	void DisplayCannonLineRenderer()
	{
//		CannonLineRenderer.SetPosition(100, PandaToThrow.transform.position);

//		CannonLineRenderer1.SetPosition(1, PandaToThrow.transform.position);
//		CannonLineRenderer2.SetPosition(1, PandaToThrow.transform.position);
	}
	
	void SetCannonLineRendererActive(bool active)
	{
		CannonLineRenderer.enabled = active;

//		CannonLineRenderer1.enabled = active;
//		CannonLineRenderer2.enabled = active;
	}
	
	void SetTrajectoryLineRenderesActive(bool active)
	{
		TrajectoryLineRenderer.enabled = active;
	}
	
	
	/// <summary>
	/// Another solution (a great one) can be found here
	/// http://wiki.unity3d.com/index.php?title=Trajectory_Simulation
	/// </summary>
	/// <param name="distance"></param>
	void DisplayTrajectoryLineRenderer(float distance)
	{
		SetTrajectoryLineRenderesActive(true);
		Vector3 v2 = CannonMiddleVector - PandaToThrow.transform.position;

		int segmentCount = 15;
		float segmentScale = 2;
		Vector2[] segments = new Vector2[segmentCount];
		
		// The first line point is wherever the player's cannon, etc is
		segments[0] = PandaToThrow.transform.position;
		
		// The initial velocity
		Vector2 segVelocity = new Vector2(v2.x, -v2.y) * ThrowSpeed * distance;
		
		float angle = Vector2.Angle(segVelocity, new Vector2(1, 0));
		float time = segmentScale / segVelocity.magnitude;

		transform.Rotate(transform.position.x, transform.position.y, transform.position.z + 0.5f*angle);

		for (int i = 1; i < segmentCount; i++)
		{
			//x axis: spaceX = initialSpaceX + velocityX * time
			//y axis: spaceY = initialSpaceY + velocityY * time + 1/2 * accelerationY * time ^ 2
			//both (vector) space = initialSpace + velocity * time + 1/2 * acceleration * time ^ 2
			float time2 = i * Time.fixedDeltaTime * 5;
			segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
		}
		
		TrajectoryLineRenderer.SetVertexCount(segmentCount);
		for (int i = 0; i < segmentCount; i++)
			TrajectoryLineRenderer.SetPosition(i, segments[i]);
	}
}