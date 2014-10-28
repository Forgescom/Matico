using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {

	Vector3 initialPos;
	Vector3 finalPos;
	bool canMove = true;
	public float speed = 1f;

	//int originalPosition = gameObject.transform.position;
	// Use this for initialization
	void Start () {
		initialPos = gameObject.transform.position;
		finalPos = new Vector3 (-0.3f, -7.7f, 0);

	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			MoveCoala();
		}
	}

	void MoveCoala(){
	
		var step = speed * Time.deltaTime;
	
		transform.position = Vector3.MoveTowards (transform.position, finalPos, step);

		if (Vector3.Distance (transform.position, finalPos) <= 0) {
			transform.position = initialPos;
		}
	}

}
