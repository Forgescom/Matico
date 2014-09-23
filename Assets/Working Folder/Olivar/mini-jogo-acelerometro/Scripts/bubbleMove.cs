using UnityEngine;
using System.Collections;

public class bubbleMove : MonoBehaviour {

/*	Vector3 start = new Vector3(0,-4,0);
	Vector3 target = new Vector3(2,-4,0);
*/


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 start = new Vector3 (0, 0, 0);
		Vector3 target = new Vector3 (Random.Range (6.3f, -6.3f), Random.Range (-6.5f, -4.5f), 0);

		Vector3 pos = transform.position;

		if (pos != target) 
		{
			transform.position = Vector3.MoveTowards(pos, target, 0.04f);
		}

		else
		{
			target = start;
			if (pos == start)
			{
				target = new Vector3 (Random.Range (6.3f, -6.3f), Random.Range (-6.5f, -4.5f), 0);
			}
		}
	}

}
