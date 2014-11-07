using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {

	Vector3 initialPos;
	Vector3 finalPos;
	bool canMove = true;
	public float speed = 1f;
	public bool Horizontal;
	private float posx;
	//int originalPosition = gameObject.transform.position;
	// Use this for initialization
	void Start () {
		//initialPos = gameObject.transform.position;
	//	print ("A posiçao iniccil da nuvem " + transform.name + " e : " + transform.position);
		if (Horizontal) {
						posx = Random.Range (-15f, 15f);
						initialPos = new Vector3 (posx, Random.Range (12f, 25f), 0);
						finalPos = new Vector3 (posx, -15f, 0);
				} else {
						initialPos = new Vector3 (Random.Range (-15f, 15f), Random.Range (12f, 25f), 0);
						finalPos = new Vector3 (Random.Range (-15f, 15f), -15f, 0);
				}
		transform.position = initialPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			Move();
		}
	}

	void Move(){
	
		var step = speed * Time.deltaTime;
	
		transform.position = Vector3.MoveTowards (transform.position, finalPos, step);

		if (Vector3.Distance (transform.position, finalPos) <= 0) {

			if (Horizontal) {
				posx = Random.Range (-15f, 15f);
				initialPos = new Vector3 (posx, Random.Range (12f, 25f), 0);
				finalPos = new Vector3 (posx, -15f, 0);
			} else {
				initialPos = new Vector3 (Random.Range (-15f, 15f), Random.Range (12f, 25f), 0);
				finalPos = new Vector3 (Random.Range (-15f, 15f), -15f, 0);
			}

			transform.position = initialPos;
		}
	}

}
