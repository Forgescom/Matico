using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public GameObject brain;
	public GameObject boia;

	public float speed = 1.5f;
	public float speedAc = 5;

	int xVelocity = 3;
	int yVelocity = 4;

	// Coordenadas limite
	public float[] boundaries;
	public bool canMove = true;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update()
	{
		if(canMove)
			HandleMovement ();
	}

	void HandleMovement()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			
			var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			transform.position += move * speed * Time.deltaTime;
			
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x,boundaries[0], boundaries[1]),
			                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
			                                  transform.position.z);
				
		}
		else 
		{
			transform.Translate(Input.acceleration.x, Input.acceleration.y, 0);
			
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x,boundaries[0], boundaries[1]),
			                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
			                                  transform.position.z);
		}
	}

	public void CollisionOccur (GameObject collisionWith)
	{

		if (collisionWith.tag == "North")
		{

		}
		else if (collisionWith.tag == "South") 
		{

		}
		else if (collisionWith.tag == "East") 
		{

		}
		else if (collisionWith.tag == "West") 
		{

		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		brain.SendMessage("CollisionOccur",col.gameObject);

	}
}



