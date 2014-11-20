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
	//Baloon
	public Animator baloon32;
	public Animator baloon21;

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

	void OnTriggerEnter2D(Collider2D col){

		switch(col.name){
			case "raio": 
				delSprite32();
				delSprite21();
				face.GetComponent<SpriteRenderer>().active = false;
				thisSprite.sprite = burnmfkr;
				Brain.lives--;
				StartCoroutine(WaitFotIt());
				break;
			case "ave_head":
				Brain.lives--;
				changeSprite();
				if (Brain.lives == 2){
					baloon32.GetComponent<SpriteRenderer> ().active = true;
					baloon32.SetBool("pop",true);
					Invoke ("delSprite32", 1);
				}
				if (Brain.lives == 1){
					baloon21.GetComponent<SpriteRenderer> ().active = true;
					baloon21.SetBool("pop2",true);
					Invoke ("delSprite21",1);
				}
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
	void delSprite32(){
		baloon32.GetComponent<SpriteRenderer> ().active = false;
	}
	void delSprite21(){
		baloon21.GetComponent<SpriteRenderer> ().active = false;
	}
	IEnumerator WaitFotIt() {
		yield return new WaitForSeconds(1);
		changeSprite();
		face.GetComponent<SpriteRenderer>().active = true;
	}
}
