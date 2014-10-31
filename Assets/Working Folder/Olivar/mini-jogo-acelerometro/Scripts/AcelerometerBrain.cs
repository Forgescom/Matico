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
	
	/*public delegate void RestartGameDelegate();
	public static event RestartGameDelegate restartGame;*/


	//GAMEOBJECTS	
	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject accelerometer;
	public GameObject screenLock;
	public GameObject sharkFin;
	public GameObject boia;

	//STARTING POINT
	public GUITexture [] vidasTexture;
	int vidas;
	int currentScreen;	

	//STATIC VARIABLES FOR GAME OBJECTS
	public static int CURRENT_SKIN_INDEX = 0;

	

	// Use this for initialization
	void Start () {

		introScreen.SetActive (true);
		explanationScreen.SetActive (false);
		accelerometer.SetActive(false);
		screenLock.SetActive(false);
		currentScreen = (GameController.ACELEROMETER_TUT == true) ? 0 : 1;

	}

	void Init(){

		CURRENT_SKIN_INDEX = 0;
		vidas = 4;
		vidasTexture [CURRENT_SKIN_INDEX].gameObject.SetActive (false);

		foreach (GUITexture obj in vidasTexture) {
			obj.gameObject.SetActive (true);
		}

		CreateNewBoia ();

		//TELL OTHER OBJECTS TO START GAME
		if (startGame != null) {
			startGame();
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
			break;

			case "Shark":
				vidas--;
				//print("TENHO ESTAS VIDAS :" + vidas);
				if (vidas != 0) {

				//print("ADICIONA NOVA BOIA");
					//ESPERAR 3 SEGUNDOS E LANÇAR NOVA BOIA
					CURRENT_SKIN_INDEX ++;
					Invoke("CreateNewBoia",3);
					
				}
				else if (vidas == 0) {
				//	print("FIM DO JOGO  FIQUEI SEM BOIAS");

					if(endGame != null)
					{
						Init ();
						//startGame();
						endGame("Errado");	
					}
				}			

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



		vidasTexture [CURRENT_SKIN_INDEX].gameObject.SetActive (false);
		sharkFin.GetComponent<SharkFin> ().target = novaBoia.transform;

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
		PlayerController.boiaHit += PlayerHit;
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		FailureScreen.RestartGame += Init;
		ClickToUnlock.unlockScreen += Init;
		GameController.RestartGame += Init;

	}
	
	void OnDisable()
	{
		PlayerController.boiaHit -= PlayerHit;
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= Init;
		ClickToUnlock.unlockScreen -= Init;
		GameController.RestartGame -= Init;
	}
}

