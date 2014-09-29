using UnityEngine;
using System.Collections;

public class SharkFinMove : MonoBehaviour {

	public GameObject boia;

	private float speed;
	//The start and finish positions for the interpolation
	private Vector3 startPosition;
	private Vector3 endPosition;

	/// <summary>
	/// Called to begin the linear interpolation
	/// </summary>
	void Start()
	{
	}

	void Update() {
		startPosition = transform.position;
		endPosition = boia.GetComponent<PlayerController> ().transform.position;
		speed = Random.Range (0.01f, 0.06f);

		if (startPosition != endPosition) {    
			transform.position = new Vector2(transform.position.x + speed , transform.position.y + speed);
		} 
	}
}

