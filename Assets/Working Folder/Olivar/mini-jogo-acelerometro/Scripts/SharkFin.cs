using UnityEngine;
using System.Collections;

public class SharkFin : MonoBehaviour {
	public GameObject brain;

	bool canMove = false;
	Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
		print (startPos);
	}

	void Update()
	{
		if (canMove == true) {
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
	}
	void StartMovement()
	{
		canMove = true;
		print ("LIGUEI TUBARAO");
	}
	
	void OnEnable()
	{
		AcelerometerBrain.startGame += StartMovement;
		AcelerometerBrain.restartGame += RestartValues;
	}
	
	void OnDisable()
	{
		AcelerometerBrain.startGame -= StartMovement;
		AcelerometerBrain.restartGame -= RestartValues;
	}

	void RestartValues(){
		transform.position = startPos;
		canMove = true;
	}

}