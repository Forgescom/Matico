using UnityEngine;
using System.Collections;

public class Coala : MonoBehaviour {

	public bool canMove = true;
	//inicio jogo
	Vector3 initialPos;
	Vector3 finalPos;
	public float speedToPosition = 1f;

	public float speed = 0.5f;
	
	// Use this for initialization
	void Start () {
		initialPos = gameObject.transform.position;
		finalPos = new Vector3 (0,-3,0);
	}

	void Update(){
		if (canMove == true) {
			MoveCoalaToPosition ();
			HandleMovement ();
			ClampMovement ();
		}
	}

	void MoveCoalaToPosition(){
		var step = speedToPosition * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, finalPos, step);
	}
	
	void HandleMovement()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
			transform.position += move * speed * Time.deltaTime;
			ClampMovement();
		}
		else 
		{
			float smoothSpeed = 8f * Time.deltaTime;
			transform.Translate(Input.acceleration.x * smoothSpeed,0,0);
			ClampMovement();
		}
	}
	void ClampMovement()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -13f, 13f),transform.position.y, 0);
	}
}
