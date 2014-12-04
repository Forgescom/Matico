using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {

	Vector3 initialPos;
	Vector3 finalPos;
	bool canMove = true;

	public float speed = 1f;
	public bool Horizontal;
	public bool Rain;
	public float timeToStart = 1f;

	private float posx;

	void Start () {
		StartCoroutine(waitToStart());
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
				posx = Random.Range (-10f, 10f);
				initialPos = new Vector3 (posx, Random.Range (12f, 45f), 0);
				finalPos = new Vector3 (posx, -15f, 0);
			} else if (Rain) {
				initialPos = new Vector3 (0, Random.Range (40f, 50f), 0);
				finalPos = new Vector3 (0, -15f, 0);
			}else{
				initialPos = new Vector3 (Random.Range (-10f, 10f), Random.Range (12f, 45f), 0);
				finalPos = new Vector3 (Random.Range (-10f, 10f), -15f, 0);
			}

			transform.position = initialPos;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.name == "eatingKfood") {
						Start ();//restart
				}
		//transform.position = initialPos;
	}

	IEnumerator waitToStart() {

		yield return new WaitForSeconds (timeToStart);

		if (Horizontal) {
			posx = Random.Range (-10f, 10f);
			initialPos = new Vector3 (posx, Random.Range (12f, 25f), 0);
			finalPos = new Vector3 (posx, -15f, 0);
		} else if (Rain) {
			initialPos = new Vector3 (0, Random.Range (40f, 50f), 0);
			finalPos = new Vector3 (0, -15f, 0);
		}else{
			initialPos = new Vector3 (Random.Range (-10f, 10f), Random.Range (12f, 25f), 0);
			finalPos = new Vector3 (Random.Range (-10f, 10f), -15f, 0);
		}
		transform.position = initialPos;
	}

}
