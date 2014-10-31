using UnityEngine;
using System.Collections;

public class Relampago : MonoBehaviour {

	Vector3 initialPos;
	Vector3 finalPos;
	bool canMove = true;
	public float speed1 = 1f;
	
	//int originalPosition = gameObject.transform.position;
	// Use this for initialization
	void Start () {
		initialPos = gameObject.transform.position;
		finalPos = new Vector3 (-1.2f, -11.35f, -3);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			MoveRelampago();
		}
	}
	
	void MoveRelampago(){
		
		var step = speed1 * Time.deltaTime;
		
		transform.position = Vector3.MoveTowards (transform.position, finalPos, step);
		
		if (Vector3.Distance (transform.position, finalPos) <= 0) {
			transform.position = initialPos;
		}
	}
}
