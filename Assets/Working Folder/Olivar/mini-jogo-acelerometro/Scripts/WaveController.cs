using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float yStart = Random.Range(-2f, 4f);
		float yEnd = Random.Range(-2f, 4f);
		float y1;
		float y2;

		Vector3 currentPos = transform.position;

		if (currentPos != (new Vector3(10, transform.position.y, transform.position.z))) {
			y1 = yStart;
			y2 = yEnd;

			Vector3 startPos = new Vector3(-10f, y1, 0);
			Vector3 endPos = new Vector3(10f, y2, 0);
			float speed = 3;

			print (y1);
			print (y2);

			if (currentPos.x != endPos.x) {
				transform.localScale = new Vector3(1, 1, 1);
			}
			else {
				transform.localScale = new Vector3(-1, 1, 1);
			}
			transform.position = Vector3.MoveTowards(currentPos, endPos, speed * Time.deltaTime);
		}
	}
}
