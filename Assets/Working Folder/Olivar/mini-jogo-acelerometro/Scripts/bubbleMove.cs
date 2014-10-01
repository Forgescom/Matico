using UnityEngine;
using System.Collections;

public class bubbleMove : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 start = transform.position;
		Vector3 target = new Vector3 (start.x + Random.Range (2f, -2f), start.y + Random.Range (-2f, 2f), -2);

		Vector3 pos = transform.position;

		if (pos != target) 
		{
			transform.position = Vector3.MoveTowards(pos, target, 0.06f);
		}

		else
		{
			target = start;
			if (pos == start)
			{
				target = new Vector3 (Random.Range (2f, -2f), Random.Range (-2f, 2f), -2);
			}
		}
	}
}
