using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SharkFin : MonoBehaviour {
//	public GameObject brain;

	public bool canMove = false;
	Vector3 startPos;

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
		Transform target;
		float speed = 1f;
		
		target = GameObject.FindWithTag ("Player").transform;
		
		if (target.position.x < transform.position.x) {
			transform.localScale = new Vector3(0.75f,0.75f,1);
		}
		else {
			transform.localScale = new Vector3(-0.75f,0.75f,1);
		}
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}

	void StartMovement()
	{
		canMove = true;

	}
	void StopMovement(string result)
	{
		canMove = false;
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