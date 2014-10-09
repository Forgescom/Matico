using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	public float speed = 3;

	public Vector3 endPos;

	public GameObject brain;

	bool canMove = false;

	// Use this for initialization
	void Start () {
		FindNewPosition (false);
	}

	// Update is called once per frame
	void Update () {

		if(canMove == true){
			if (Vector3.Distance (transform.position, endPos) > 1f){
				
				transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
			}
			else 
			{
				if(transform.position.x > 0) {
					FindNewPosition(true);
					transform.localScale = new Vector3(-0.75f, 0.75f, -1);
				}
				else {
					FindNewPosition(false);
					transform.localScale = new Vector3(0.75f, 0.75f, -1);
				}
			}
		}
	}

	void FindNewPosition(bool left )
	{
		float endRandomY = Random.Range (5, -5);
		if (left == true) {
			endPos = new Vector3 (-14, endRandomY, transform.position.z);
		}
		else
		{
			endPos = new Vector3 (14, endRandomY, transform.position.z);
		}
	}
		
	void StartMovement()
	{
		canMove = true;
		print ("LIGUEI ONDAS");
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
