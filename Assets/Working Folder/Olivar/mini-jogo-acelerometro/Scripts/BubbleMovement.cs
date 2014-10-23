using UnityEngine;
using System.Collections;

public class BubbleMovement : MonoBehaviour {

	float startZ;
	// Use this for initialization
	void Start () 
	{
		startZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveBubble ();
	}
	
	void MoveBubble()
	{
		Vector3 start = transform.position;
		Vector3 target = new Vector3 (start.x + Random.Range (2f, -2f), start.y + Random.Range (-2f, 2f), startZ);
		
		Vector3 pos = transform.position;
		
		if (pos != target) 
		{
			
			transform.position = Vector3.MoveTowards(pos, target, 0.01f);
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
	
	void AnimAndDestroy()
	{
		transform.collider2D.enabled = false;
		Animator bubbleAnimator = transform.GetComponent<Animator> ();
		bubbleAnimator.SetBool ("pop", true);
	}
}
