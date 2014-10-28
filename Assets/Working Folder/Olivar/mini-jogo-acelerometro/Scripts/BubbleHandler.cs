using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleHandler : MonoBehaviour {

	public GameObject [] bubbles;

	List<GameObject> shownBubbles = new List<GameObject>();



	int maxShownBubbles = 5;

	// Use this for initialization
	void Start () 
	{


	}

	void Init(){
		foreach (GameObject bub in bubbles) {
			bub.SetActive(false);
		}
		InvokeRepeating("ShowBubble", 1,2); 
	}

	
	// Update is called once per frame
	void Update () 
	{

	}

	void ShowBubble()
	{

		int randomBubble = Random.Range (0, bubbles.Length);


		bubbles [randomBubble].SetActive (true);

		if (shownBubbles.Contains (bubbles [randomBubble]) == false) {
			shownBubbles.Add (bubbles [randomBubble]);
		}

		if (shownBubbles.Count > maxShownBubbles) {
			shownBubbles[0].SetActive(false);
			shownBubbles.RemoveAt(0);
		}
	}

	void EndGame(string outCome){
		CancelInvoke ();
	}

	void RestartValues()
	{
		foreach (GameObject bub in bubbles) {
			bub.SetActive(true);
			bub.GetComponent<CircleCollider2D>().enabled = true;
		}
	}

	void OnEnable(){

		AcelerometerBrain.startGame += Init;
		AcelerometerBrain.endGame += EndGame;
	//	AcelerometerBrain.restartGame += RestartValues;

	}

	void OnDisable(){
		AcelerometerBrain.startGame -= Init;
		AcelerometerBrain.endGame -= EndGame;
		//AcelerometerBrain.restartGame -= RestartValues;
	}

}
