using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour 
{
	GameObject brain;

	public float speed = 0.5f;

	// Coordenadas limite
	public float[] boundaries;
	public bool canMove = false;
	public Sprite [] skins;

//	public GameObject questionArea;
	GameObject questionArea;

	int currentState = 0;


	void Start () {
		brain = GameObject.Find ("GameBrain");
	
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
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x,boundaries[0], boundaries[1]),
		                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
		                                  transform.position.z);
	}



	void OnTriggerEnter2D(Collider2D col)
	{
	
		if(col.name.Contains("hip"))
		{
			col.SendMessage("AnimAndDestroy");


			if(col.tag == "Errado") {

				brain.SendMessage("AnswerHit", false);
			}
			else if (col.tag == "Certo") {
			
				brain.SendMessage("AnswerHit", true);
			}
		}
		else if (col.tag =="Shark")
		{
			brain.SendMessage("ObjectHit");
			Destroy(gameObject);

		}
		else if (col.tag =="Wave")
		{
			AddWaveForce();
		}

	}

	public void ChangeSkin(int index)
	{
		print ("INDEX  " + index);
		transform.GetComponent<SpriteRenderer> ().sprite = skins [index];

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

	void StartMovement()
	{
		canMove = true;
		transform.GetComponent<SpriteRenderer> ().sprite = skins [0];

	}

	void StopMovement(string outcome){
		transform.rigidbody2D.velocity  = new Vector2(0,0);
		//transform.position = Vector3.zero;
		canMove = false;

	}

	void RestartValues(){
		currentState = 0;
		transform.position = Vector3.zero;
		transform.GetComponent<SpriteRenderer> ().sprite = skins [0];
	}

	void OnEnable()
	{
		AcelerometerBrain.restartGame += RestartValues;
		AcelerometerBrain.startGame += StartMovement;
		AcelerometerBrain.endGame += StopMovement;
	}

	void OnDisable()
	{
		AcelerometerBrain.restartGame -= RestartValues;
		AcelerometerBrain.startGame -= StartMovement;
		AcelerometerBrain.endGame -= StopMovement;
	}
}
