using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AcelerometerBrain : MonoBehaviour {

	//EVENTS
	public delegate void StartGameDelegate();
	public static event StartGameDelegate startGame;
	
	public delegate void EndGameDelegate(string outcome);
	public static event EndGameDelegate endGame;
	
	public delegate void RestartGameDelegate();
	public static event RestartGameDelegate restartGame;


	//GAMEOBJECTS	

	public GUITexture [] vidasTexture;
	int vidas = 4;

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject accelerometer;
	public GameObject screenLock;
	public GameObject sharkFin;
	public GameObject boia;


	int currentScreen = 0;	
	bool lostByShark = false;

	
	// Iniciar Jogo

	bool start = false;

	// Use this for initialization
	void Start () {
		introScreen.SetActive (true);
		explanationScreen.SetActive (false);
		accelerometer.SetActive(false);
		screenLock.SetActive(false);

	}

	void Init(){
	


		foreach (GUITexture obj in vidasTexture) {
			obj.gameObject.SetActive (true);
		}
		vidas = 4;
		vidasTexture [vidas-1].gameObject.SetActive (false);

		if(startGame != null)
		{
			startGame();
		}

	}


	// Update is called once per frame
	void Update () {
	

		if (Input.GetKey (KeyCode.Escape)) {
//			Application.LoadLevel(1);
			RestartGame();
		}

	}

	void ChangeScreen()
	{
		if (currentScreen == 0) {
			explanationScreen.SetActive(true);
			explanationScreen.animation.Play("boiaExplanation");
			currentScreen ++;
		}
		else if(currentScreen == 1)
		{
			screenLock.SetActive(true);
			accelerometer.SetActive(true);

			currentScreen ++;

		}
	}
	
	void AnswerHit(bool correct)
	{
		Handheld.Vibrate ();

		if(correct == true)
		{
			if(endGame != null)
			{
				endGame("Certo");

			}
		}
		else
		{
			if(endGame != null)
			{
				endGame("Errado");

			}
		}
	}

	void ObjectHit()
	{



		vidas--;

		if (vidas != 0) {
			//StartCoroutine ("CreateNewBoia");
			Invoke("CreateNewBoia",2);

		}
		else if (vidas == 0) {
			lostByShark = true;
			if(endGame != null)
			{
				endGame("Errado");
			}
		}



	}

	void CreateNewBoia()
	{
	
		GameObject novaBoia = Instantiate (boia, Vector3.zero, Quaternion.identity) as GameObject;
		novaBoia.transform.parent = accelerometer.transform;
		
		novaBoia.SendMessage ("ChangeSkin",vidas-1);
		
		novaBoia.GetComponent<PlayerController> ().canMove = true;
		novaBoia.tag = "Player";

		vidasTexture [vidas-1].gameObject.SetActive (false);
		sharkFin.GetComponent<SharkFin> ().target = novaBoia.transform;
	}





	void RestartGame()
	{
		//		accelerometer.animation.Play("");
	
		Init ();
		if (lostByShark == true) {
			CreateNewBoia();
		}

		StartCoroutine ("ChangeQuestion");

		screenLock.SetActive(true);

		if (restartGame != null) {
			restartGame();
		}
	}
	
	IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (1.5F);
		/*if (GameTryAgain != null) {		
			GameTryAgain();			
		}	*/
		transform.SendMessage ("SetQuestionValues");
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		FailureScreen.RestartGame += RestartGame;
		ClickToUnlock.unlockScreen += Init;
		GameController.RestartGame += RestartGame;

	}
	
	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= RestartGame;
		ClickToUnlock.unlockScreen -= Init;
		GameController.RestartGame -= RestartGame;
	}
}

