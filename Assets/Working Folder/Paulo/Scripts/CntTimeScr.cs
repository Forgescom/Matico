using UnityEngine;
using System.Collections;

public class CntTimeScr : MonoBehaviour {

	public float time;
	//public float speedTime = 1f;
	public bool canMoveTime = true;
	Vector3 initialPosTime;
	Vector3 finalPosTime;

	//public Tilt_brain brain;


	void Start () {
		initialPosTime = gameObject.transform.position;
		finalPosTime = new Vector3 (8,3,-1);
	}

	void Update(){
		if (canMoveTime == true) {
						Moving ();
					//ClampMovement ();
				}
	}

	void Moving(){
		var speedTime = 5.8f/time;
		var step = speedTime * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, finalPosTime, step);
	}
}
