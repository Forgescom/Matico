using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
	public GameObject brain;

	public float speed = 0.5f;

	// Coordenadas limite
	public float[] boundaries;
	public bool canMove = false;

	int currentState = 0;


	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update()
	{
		if(canMove)
			HandleMovement ();
	
	}

	void HandleMovement()
	{

		if (SystemInfo.deviceType == DeviceType.Desktop) {
			
			var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			transform.position += move * speed * Time.deltaTime;
			
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x,boundaries[0], boundaries[1]),
			                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
			                                  transform.position.z);
				
		}
		else 
		{
			float smoothSpeed = 8f * Time.deltaTime;

			transform.Translate(Input.acceleration.x * smoothSpeed, Input.acceleration.y * smoothSpeed, 0);
			
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x,boundaries[0], boundaries[1]),
			                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
			                                  transform.position.z);
		}
	}



	void OnTriggerEnter2D(Collider2D col)
	{

		if(col.name.Contains("hip"))
		{
			col.SendMessage("AnimAndDestroy");
			if(col.tag =="Errado")
				brain.SendMessage("AnswerHit",false);
			else if (col.tag == "Certo")
				brain.SendMessage("AnswerHit",true);
		}
		else if (col.tag =="Shark")
		{
			currentState ++;	
			string animatorKey = "Damage"+ currentState;

			transform.GetComponent<Animator>().SetBool(animatorKey,true);			


			brain.SendMessage("ObjectHit");
		}
		else if (col.tag =="Wave")
		{
			AddWaveForce();
		}
	}

	void AddWaveForce()
	{
		float force = 50;
		// VER QUAL O LADO QUE COLIDE E DETERMINAR FORÇA/DIREÇAO
		transform.rigidbody2D.AddForce(Vector3.right * force * Time.deltaTime);

		canMove = false;
		StartCoroutine ("WaveEffect");
	}

	IEnumerator WaveEffect()
	{
		yield return new WaitForSeconds (0.75f);
		canMove = true;
	}

	void StartMovement()
	{
		canMove = true;
		print ("LIGUEI BOIA");
	}

	void OnEnable()
	{
		AcelerometerBrain.startGame += StartMovement;
	}

	void OnDisable()
	{
		AcelerometerBrain.startGame -= StartMovement;
	}
}
