using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SharkFin : MonoBehaviour {
//	public GameObject brain;
	public Transform target;
	public bool canMove = false;
	bool pursuit = true;
	Vector3 startPos;

	public SpriteRenderer boiaAnim;
	public SpriteRenderer halfBoiaAnim;

	public Sprite [] skins;
	public Sprite [] halfSkins;

	int currentSkin = 0;

	void Start()
	{
		startPos = transform.position;

	}

	void Update()
	{
		if (canMove == true)
			SharkMovement();

	}

	void SharkMovement() {

		float speed = 1f;


		if(pursuit && target!=null){

			transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
			var dir = target.position - transform.position;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			angle = angle - 180;
			Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, 0.02f);
		}
		else{

			transform.position = Vector3.MoveTowards(transform.position,startPos,5f*Time.deltaTime);
			if(transform.position == startPos)
			{
				pursuit = true;
				transform.animation.Play("Idle");

			}
		}
	}

	void StartMovement()
	{
		canMove = true;
		pursuit = true;
		currentSkin = 0;

	}
	void StopMovement(string result)
	{
		transform.position = startPos;
		canMove = false;
		pursuit = false;
	}

	void AnimEndStopPursuit()
	{
		canMove = true;
		pursuit = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {

			//print ("VOU COMEÇAR A ANIMAR O TUBARAO");
			halfBoiaAnim.sprite = halfSkins[currentSkin];
			boiaAnim.sprite = skins[currentSkin];
			transform.animation.Play("Stuck");
			canMove = false;
			currentSkin ++;


		}
	}




	void OnEnable()
	{
		AcelerometerBrain.startGame += StartMovement;
		AcelerometerBrain.restartGame += RestartValues;
		AcelerometerBrain.endGame += StopMovement;
	}
	
	void OnDisable()
	{
		AcelerometerBrain.startGame -= StartMovement;
		AcelerometerBrain.restartGame -= RestartValues;
		AcelerometerBrain.endGame -= StopMovement;
	}

	void RestartValues(){
		transform.position = startPos;
	}

}