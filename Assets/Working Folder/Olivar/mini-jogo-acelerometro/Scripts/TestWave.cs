using UnityEngine;
using System.Collections;

public class TestWave : MonoBehaviour {

	Vector3 startPos;
	Vector3 endPos;

	float speed = 5f;
	// Use this for initialization
	void Start () {
		FindNewPosition (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, endPos) > 1f){
			
			transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
		}
		else 
		{
			if(transform.position.x > 0)
				FindNewPosition(true);
			else
				FindNewPosition(false);
		}

	}

	void FindNewPosition(bool left )
	{
		float endRandomY = Random.Range (5, -5);
		if (left == true) {
			endPos = new Vector3 (-14, endRandomY, transform.position.z);
		}
		else
		{
			endPos = new Vector3 (14, endRandomY, transform.position.z);
		}

	}
}
