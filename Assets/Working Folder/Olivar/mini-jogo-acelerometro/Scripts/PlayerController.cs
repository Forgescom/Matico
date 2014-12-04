using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour 
{
	//EVENT FOR COLLISION WITH OTHER OBJECTS
	public delegate void PlayerHit(string objectHited,bool correct = false);
	public static event PlayerHit boiaHit;

	// Coordenadas limite
	public float[] boundaries;
	public Sprite [] skins;

	//CONSTRAINTS
	public bool canMove = false;
	public float speed = 0.5f;

	public bool hittedOnSwirl = false;
	Vector3 swirlPosition;
	public float unit = 2;
	public float freq = 0.2f;
	float decreaseAmount = 0.005f;
	float angle = 15;

	void Start () {
		//ASSIGN TEXTUR
		//RESET POSITION
		//RESET CONSTRAINTS


	}

	void Init(){
		canMove = true;
		transform.position = Vector3.zero;
		transform.GetComponent<SpriteRenderer> ().sprite = skins [AcelerometerBrain.vidas];
	}

	void Update()
	{
		if (canMove == true)
				HandleMovement ();
		else if (canMove == false && hittedOnSwirl == true)
				SwirlTrap ();

		ClampMovement ();
	}

	void SwirlTrap ()
	{
		float newX = (unit * Mathf.Cos (Time.time * freq)) + swirlPosition.x;
		float newY = (unit * Mathf.Sin (Time.time * freq)) + swirlPosition.y;

		transform.position = new Vector3 (newX,newY, transform.position.z);
		transform.localScale = new Vector3 (transform.localScale.x - 0.005f, transform.localScale.y - 0.005f, 1);
		freq -= 0.0001f;
		unit -= decreaseAmount;	

		print (unit);
		if (unit < 0.07f) {
			if(boiaHit !=null)
			{
				boiaHit("Shark");
			}
			Destroy(gameObject);
		}

	}

	void HandleMovement()
	{

		if (SystemInfo.deviceType == DeviceType.Desktop) {			
			var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			transform.position += move * speed * Time.deltaTime;				
		}
		else 
		{
			float smoothSpeed = 8f * Time.deltaTime;
			transform.Translate(Input.acceleration.x * smoothSpeed, Input.acceleration.y * smoothSpeed, 0);
		}
	}

	void ClampMovement()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x,boundaries[0], boundaries[1]),
		                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
		                                  transform.position.z);
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		//COLLISION WITH BUBBLE
		if(col.name.Contains("hip"))
		{
			col.SendMessage("AnimAndDestroy");


			if(col.tag == "Errado") {
				//THROW EVENT FOR ACCELEROMETER BRAIN
				if(boiaHit !=null)
				{
					boiaHit("Bubble",false);
				}

			}
			else if (col.tag == "Certo") {
				if(boiaHit !=null)
				{
					boiaHit("Bubble",true);
				}
			}
			Invoke("DestroyThis",1);
		}
		//COLLISION WITH SHARK
		else if (col.tag =="Shark")
		{
			if(boiaHit !=null)
			{
				boiaHit(col.tag);
			}
			Destroy(gameObject);

		}
		else if (col.tag =="Wave")
		{
			AddWaveForce();
		}
		else if (col.tag == "Swirl")
		{
			swirlPosition = col.transform.position;
			col.SendMessage("StopAutoDestroy");
			hittedOnSwirl = true;
			canMove = false;

			if(boiaHit !=null)
			{
				boiaHit("Swirl",true);
			}
		}

	}

	public void ChangeSkin()
	{
		transform.GetComponent<SpriteRenderer> ().sprite = skins [AcelerometerBrain.vidas];
	}

	void DestroyThis(){
		Destroy(gameObject);
	}

	void AddWaveForce()
	{
		/*float force = 100;
		 * 
		// VER QUAL O LADO QUE COLIDE E DETERMINAR FORÇA/DIREÇAO
		transform.rigidbody2D.AddForce(Vector3.right * force * Time.deltaTime);

		canMove = false;
		StartCoroutine ("WaveEffect");*/
	}

	IEnumerator WaveEffect()
	{
		yield return new WaitForSeconds (0.75f);
		canMove = true;
	}



	void StopMovement(string outcome){
		transform.rigidbody2D.velocity  = new Vector2(0,0);
		canMove = false;

	}


	void OnEnable()
	{
		AcelerometerBrain.startGame += Init;
		AcelerometerBrain.endGame += StopMovement;
	}

	void OnDisable()
	{
		AcelerometerBrain.startGame -= Init;
		AcelerometerBrain.endGame -= StopMovement;
	}
}
