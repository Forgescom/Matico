using UnityEngine;
using System.Collections;

public class MoveTowards : MonoBehaviour {
	public Transform target;
	float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

	
	}
}
