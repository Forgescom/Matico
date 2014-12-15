using UnityEngine;
using System.Collections;

public class MoveTowards : MonoBehaviour {
	public Transform target;
	float speed = 1f;
	Vector3 initialScale = new Vector3(0.1f,0.1f,0.1f);
	Vector3 finalScale = new Vector3(1,1,1);
	public float zoomInTime = 2f;


	// Use this for initialization
	void Start () {
		transform.localScale = initialScale;
		//ZoomIn ();
	}

	public void ZoomIn()
	{
		transform.scaleTo (zoomInTime, finalScale);
	}

	// Update is called once per frame
	void Update () {
		//transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

	}
}
