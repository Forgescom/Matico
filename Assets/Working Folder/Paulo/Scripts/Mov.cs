using UnityEngine;
using System.Collections;

public class Mov : MonoBehaviour {

	bool canMove = true;
	//public float speed1 = 1f;
	// Use this for initialization
	void Start () {
	}
	
	void Update () {

		if (canMove) {
			MoveCoala();
		}
	}
	
	void MoveCoala(){
		
	/*	var step = Cloud1.speed1 * Time.deltaTime;
		
		transform.position = Vector3.MoveTowards (transform.position, finalPos, step);
		
		if (Vector3.Distance (transform.position, finalPos) <= 0) {
			transform.position = initialPos;
		}*/
	}
}
