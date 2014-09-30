using UnityEngine;
using System.Collections;

public class SharkFin : MonoBehaviour {
	void start()
	{
	}

	void Update()
	{

		Transform target;
		float speed = 5f;

		target = GameObject.FindWithTag ("Player").transform;
		Vector3 targetHeading = target.position - transform.position;
		Vector3 targetDirection = targetHeading.normalized;
		
		//rotate to look at the player
		transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 50f, 0);

//		transform.LookAt(targetDirection);
//		transform.Rotate(new Vector3(0,90,0), Space.Self);//correcting the original rotation

		//move towards the player
		transform.position += transform.forward * speed * Time.deltaTime;
	}	
}