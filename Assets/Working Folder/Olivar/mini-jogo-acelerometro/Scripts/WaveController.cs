using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	public float speed = 3;
	float yStart = 0;
	float yEnd = 0;
	float yEnd2 = 0;
	
	public Vector3 startPos;
	public Vector3 startPos1;
	public Vector3 startPos2;
	public Vector3 endPos;
	public Vector3 endPos1;
	public Vector3 endPos2;
	public Vector3 currentPos;

	public float hoverForce;
	public GameObject collisionWith;
	public GameObject wave;

	// Use this for initialization
	void Start () {
		yStart = Random.Range(-4f, 4f);
		yEnd = Random.Range(-4f, 4f);
		yEnd2 = Random.Range(-4f, 4f);
		
		startPos = transform.position = new Vector3(-12f, yStart, -1);
		endPos = endPos1 = new Vector3(12f, yEnd, -1);
		startPos1 = endPos1;
		startPos2 = endPos2;
		endPos2 = new Vector3(-12f, yEnd2, -1);

		print (yStart);
	}

	// Update is called once per frame
	void Update () {
		currentPos = transform.position;

		if ((currentPos.x < endPos.x) && (currentPos.x >= endPos2.x)) {
				endPos = endPos1;
				if (currentPos.x != endPos.x) {
				transform.localScale = new Vector3(0.75f, 0.75f, -1);
			}
			else {
				transform.localScale = new Vector3((-0.75f), 0.75f, -1);
			}
		}

		else if ((currentPos.x <= endPos.x) && (currentPos.x > endPos2.x)) {
			endPos = endPos2;
			if (currentPos.x != endPos.x) {
				transform.localScale = new Vector3((-0.75f), 0.75f, -1);
			}
			else {
				transform.localScale = new Vector3(0.75f, 0.75f, -1);
			}
		}

		transform.position = Vector3.MoveTowards(currentPos, endPos, speed * Time.deltaTime);

//		CollisionOccur (collisionWith);
	}

	void CollisionOccur (GameObject collisionWith)
	{
		if (collisionWith.tag == "Player")
		{
			rigidbody.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
		}
	}

}
