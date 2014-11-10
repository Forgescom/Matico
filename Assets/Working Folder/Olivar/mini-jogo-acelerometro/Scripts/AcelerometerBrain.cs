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


	//GAMEOBJECTS	
	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject accelerometer;
	public GameObject questionHolder;
	public GameObject screenLock;
	public GameObject sharkFin;
	public GameObject boia;

	//STARTING POINT
	public GUITexture [] vidasTexture;
	public static int vidas;
	int currentScreen;	

	//STATIC VARIABLES FOR GAME OBJECTS
	//public static int CURRENT_SKIN_INDEX = 0;

	

	// Use this for initialization
	void Start () {
		currentScreen = (GameController.ACELEROMETER_TUT == true) ? 0 : 1;
		introScreen.SetActive (true);
		explanationScreen.SetActive (false);
		accelerometer.SetActive(false);
		screenLock.SetActive(false);
		questionHolder.SetActive (false);

		TurnOffOnSound ();

	}

	void Init(){

		//CURRENT_SKIN_INDEX = 0;
		vidas = 3;
		vidasTexture [vidas].gameObject.SetActive (false);

		foreach (GUITexture obj in vidasTexture) {
			obj.gameObject.SetActive (true);
		}



		//TELL OTHER OBJECTS TO START GAME
		if (startGame != null) {
			startGame();
		}
		CreateNewBoia ();
	}

	void TurnOffOnSound()
	{
		
		AudioSource audio = transform.GetComponent<AudioSource> ();
		audio.enabled = GameController.BG_SOUND;
		
		if (audio.enabled)
			audio.Play ();
		else {
			audio.Stop();
		}
	}

	// Update is called once per frame
	void Update () {
	

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
			questionHolder.SetActive (true);
			questionHolder.animation.Play("QuestionIn");
			accelerometer.SetActive(true);
			currentScreen ++;
		}
	}


	void PlayerHit(string colObj, bool outCome)
	{
		//print ("Colidi com : " + colObj + " e foi este o resultado" + outCome);
		//Handheld.Vibrate ();

		switch (colObj) {
			case "Bubble":
				if(outCome == true)
				{
					//print("FIM DO JOGO   ACERTEI");
					if(endGame != null)
					{
						endGame("Certo");						
					}
				}
				else
				{
					//print("FIM DO JOGO ERREI");
					if(endGame != null)
					{
						endGame("Errado");						
					}
				}

				foreach (GUITexture obj in vidasTexture) {
					obj.gameObject.SetActive (false);
				}


			break;

			case "Shark":
				
		
				if (vidas != 0) {
					Invoke("CreateNewBoia",3);					
				}
				else if (vidas == 0) {
					if(endGame != null)
					{
						endGame("Errado");	
					}
					foreach (GUITexture obj in vidasTexture) {
						obj.gameObject.SetActive (false);
					}
				}	
				vidas--;

			break;
		}
	}

	void CreateNewBoia()
	{
	
		GameObject novaBoia = Instantiate (boia, Vector3.zero, Quaternion.identity) as GameObject;
		novaBoia.transform.parent = accelerometer.transform;
		novaBoia.tag = "Player";
		novaBoia.SendMessage ("ChangeSkin");		
		novaBoia.GetComponent<PlayerController> ().canMove = true;

		vidasTexture [vidas].gameObject.SetActive (false);
		sharkFin.GetComponent<SharkFin> ().target = novaBoia.transform;

	}


	
	IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (1.5F);
		transform.SendMessage ("SetQuestionValues");
		questionHolder.animation.Play("QuestionIn");
		screenLock.SetActive(true);
	}

	void RestartGameAndQuestion()
	{
		questionHolder.animation.Play("QuestionOut");
		StartCoroutine ("ChangeQuestion");
	}

	void OnEnable()
	{
		PlayerController.boiaHit += PlayerHit;
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		FailureScreen.RestartGame += RestartGameAndQuestion;
		ClickToUnlock.unlockScreen += Init;
		GameController.RestartGame += Init;

	}


	
	void OnDisable()
	{
		PlayerController.boiaHit -= PlayerHit;
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= RestartGameAndQuestion;
		ClickToUnlock.unlockScreen -= Init;
		GameController.RestartGame -= Init;
	}
}

