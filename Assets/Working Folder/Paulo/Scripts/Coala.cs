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

	//contadores
	private int Menor;
	private int Maior;
	private int Igual;
	public countMenorScr countMenor;
	public countIgualSrc countIgual;
	public countMaiorSrc countMaior;

	//guardar Sprite;
	SpriteRenderer thisSprite;

	public Tilt_brain brainLives;
	//public NumberSrc foodNumber;

	public float speed = 0.5f;
	// Use this for initialization
	void Start () {
		//aceder a sprite
		thisSprite = transform.GetComponent<SpriteRenderer> ();
		//iniciar contadores
		Menor = 0;
		Maior = 0;
		Igual = 0;
		//defenir posiçoes
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
		//GameObject teste;
		switch(col.name){
			case "raio": 
				//col.gameObject.SetActive(false);
				thisSprite.sprite = burnmfkr;
				brainLives.lives--;
				Invoke ("changeSprite", 2);
				break;
			case "ave_head":
				//col.gameObject.SetActive(false);
				brainLives.lives--;
				Invoke ("changeSprite", 2);
				break;
			case "Menor":
				//col.gameObject.SetActive(false);
				Menor = Menor + 1;
				countMenor.barDisplay = Menor;
				break;
			case "Igual":
				//col.gameObject.SetActive(false);
				Igual = Igual + 1;
				countIgual.barDisplay = Igual;
			break;
			case "Maior":
				//col.gameObject.SetActive(false);
				Maior = Maior + 1;
				countMaior.barDisplay = Maior;
			break;
		}
	}

	void changeSprite(){
		Debug.Log ("on");
		thisSprite.sprite = arrayLives[brainLives.lives];
		//JUST A COMMETN

		}
}
