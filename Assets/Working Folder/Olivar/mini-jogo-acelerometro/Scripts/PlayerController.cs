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



	void Start () {
		//ASSIGN TEXTUR
		//RESET POSITION
		//RESET CONSTRAINTS


	}

	void Init(){
		canMove = true;
		transform.position = Vector3.zero;
		transform.GetComponent<SpriteRenderer> ().sprite = skins [AcelerometerBrain.CURRENT_SKIN_INDEX];
	}

	void Update()
	{
		if(canMove == true)
			HandleMovement ();

		ClampMovement ();
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

	}

	public void ChangeSkin()
	{
		//print ("ABOUT TO CHANGE SKIN TO THIS INDEX :" + AcelerometerBrain.CURRENT_SKIN_INDEX);
		transform.GetComponent<SpriteRenderer> ().sprite = skins [AcelerometerBrain.CURRENT_SKIN_INDEX];

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
		//AcelerometerBrain.restartGame += RestartValues;
		AcelerometerBrain.startGame += Init;
		AcelerometerBrain.endGame += StopMovement;
	}

	void OnDisable()
	{
		//AcelerometerBrain.restartGame -= RestartValues;
		AcelerometerBrain.startGame -= Init;
		AcelerometerBrain.endGame -= StopMovement;
	}
}
