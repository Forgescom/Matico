using UnityEngine;
using System.Collections;

public class Coala : MonoBehaviour {

	public bool canMove = true;
	//inicio jogo
	Vector3 initialPos;
	Vector3 finalPos;
	public float speedToPosition = 1f;

	public GameObject control;
	
	public Sprite burnmfkr;
	public Sprite [] arrayLives;
	//public Sprite [] arrayEat;

	SpriteRenderer thisSprite;
	//Sprite saveSprite;

	public Tilt_brain brainLives;

	public float speed = 0.5f;
	// Use this for initialization
	void Start () {
		//animation.Play("swing");
		//Screen.orientation = ScreenOrientation.Portrait;
		initialPos = gameObject.transform.position;
		finalPos = new Vector3 (0,-5f,-2);
		thisSprite = transform.GetComponent<SpriteRenderer> ();
	}

	void Update()
	{
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
			
			transform.Translate(Input.acceleration.x * smoothSpeed, Input.acceleration.y * smoothSpeed, 0);
			ClampMovement();
			
		}
	}
	void ClampMovement()
	{
		transform.position = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.name == "raio") {
			thisSprite.sprite = burnmfkr;
			brainLives.lives--;
			Invoke ("changeSprite", 2);
		}
		if (col.name == "ave_head") {
			brainLives.lives--;
			Invoke ("changeSprite", 2);
		}
		if (col.name == "Food") {
						//Debug.Log ("esta comendo");

			//animation.Play("Eating");
			//UnityEngine.Animation:Play("eatHappy");
				}
	}

	void changeSprite(){
		Debug.Log ("on");
		thisSprite.sprite = arrayLives[brainLives.lives];
		//JUST A COMMETN

		}
}
