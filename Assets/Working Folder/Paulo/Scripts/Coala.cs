using UnityEngine;
using System.Collections;

public class Coala : MonoBehaviour {

	public bool canMove = true;
	//inicio jogo
	Vector3 initialPos;
	Vector3 finalPos;
	public float speedToPosition = 1f;

	//public GameObject control;	
	public Sprite burnmfkr;
	public Sprite [] arrayLives;

	//FACE
	public Animator face;

	//guardar Sprite;
	SpriteRenderer thisSprite;

	public Tilt_brain Brain;
	//public NumberSrc foodNumber;

	public float speed = 0.5f;

	// Use this for initialization
	void Start () {
		//aceder a sprite
		thisSprite = transform.GetComponent<SpriteRenderer> ();
		initialPos = gameObject.transform.position;
		finalPos = new Vector3 (0,-5f,-2);
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

		switch(col.name){
			case "raio": 
				thisSprite.sprite = burnmfkr;
				Brain.lives--;
				Invoke ("changeSprite", 2);
				break;
			case "ave_head":
				Brain.lives--;
				Invoke ("changeSprite", 2);
				break;
			case "Menor":
				face.SetBool("Eat",true);;
				Brain.Menor = Brain.Menor + 1;
				break;
			case "Igual":
				face.SetBool("Eat",true);
				Brain.Igual = Brain.Igual + 1;
			break;
			case "Maior":
				face.SetBool("Eat",true);
				Brain.Maior = Brain.Maior + 1;
			break;
		}
	}

	void changeSprite(){
		thisSprite.sprite = arrayLives[Brain.lives];
		}
}
