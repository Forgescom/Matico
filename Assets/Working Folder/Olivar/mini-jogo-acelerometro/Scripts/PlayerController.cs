using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public GameObject brain;

	public float speed = 1.5f;
	public float speedAc = 5;

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


	void OnTriggerEnter2D(Collider2D col)
	{
		brain.SendMessage("CollisionOccur",col.gameObject);

	}
}



