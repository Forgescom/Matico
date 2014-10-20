using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AcelerometerBrain : MonoBehaviour {

	//TEXTO NAS BOLHAS 	
	public TextMesh vidasText;

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject accelerometer;
	public GameObject sharkFin;
	public GameObject boia;


	int currentScreen = 1;	
	int vidas = 3;
	
	// Iniciar Jogo
	public TextMesh startText;
	bool start = false;

	//EVENTS
	public delegate void StartGameDelegate();
	public static event StartGameDelegate startGame;
	public delegate void EndGameDelegate(string outcome);
	public static event EndGameDelegate endGame;

	public delegate void RestartGameDelegate();
	public static event RestartGameDelegate restartGame;

	//EVENTS FOR GAME END
	public delegate void gameEnd(string test);
	public static event gameEnd GameEnded;
	public delegate void GameNewTry();
	public static event GameNewTry GameTryAgain;


	// Use this for initialization
	void Start () {
		introScreen.SetActive (true);
		explanationScreen.SetActive (false);
		accelerometer.SetActive(false);
		boia.GetComponent<PlayerController>().canMove = false;
		//Init ();
	}

	void Init() 
	{
		if (startGame != null)
		{
			startGame();
			boia.GetComponent<PlayerController>().canMove = true;
		}
		
//		vidasText.text = "Vidas: " + GameController.CURRENT_LIVES.ToString ();
		vidasText.text = "Vidas: " + vidas.ToString ();
		//bloquear objectos antes de ecras desaparecerem
	}
	
	// Update is called once per frame
	void Update () {

		if (start == false) {
			startText.text = "Toca no ecran para o jogo iniciar";
			if(Input.touchCount > 0)
			{
				Init();
				startText.transform.position = new Vector3(100, 0, 0);
				start = true;
			}
		}

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
			accelerometer.SetActive(true);
			currentScreen ++;
			/*if(startGame !=null)
			{
				startGame();
			}*/
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
		vidasText.text = "Vidas: " + vidas.ToString ();

		if (vidas == 0) {
			if(endGame != null)
			{
				endGame("Errado");
			}
		}
	}


	public void GameEnd(string outcome, int vidas)
	{
		//FINAL SCREEN DEPENDING ON LIVES

		/*if ((outcome == "Vitoria") && (vidas == 3)) {
			finalMessage.text = "Resposta certa, excelente!";
			successScreen.SetActive(true);
		}
		else if ((outcome == "Vitoria") && (vidas == 2)) {
			finalMessage.text = "Resposta certa, bom jogo!";
			successScreen.SetActive(true);
		}
		else if ((outcome == "Vitoria") && (vidas == 1)) {
			finalMessage.text = "Resposta certa, foi por pouco!";
			successScreen.SetActive(true);
		}
		else if(outcome == "Derrota")
		{
			finalMessage.text = "Perdeste todas as vidas, tenta outra vez!";
			failureScreen.SetActive (true);
		}
		else if(outcome == "Derrotatubarao")
		{
			finalMessage.text = "Foste apanhado pelo tubarao! Tenta outra vez!";
			failureScreen.SetActive (true);
		}
		boia.GetComponent<PlayerController> ().canMove = false;
		sharkfin.GetComponent<SharkFin> ().enabled = false;
		waves.GetComponent<WaveController> ().enabled = false;*/
	}

	void btClick(GameObject btClicked)
	{
		switch (btClicked.name) {
		case "BtMap":
			GameController.MiniGamelEnd("Won");
			break;
		case "BtRepeat":
			Application.LoadLevel("Accelerometer");
			break;
		case "BtNext":
			GameController.MiniGamelEnd("NextLevel");
			break;
		}
	}

	void RestartGame()
	{
		//		accelerometer.animation.Play("");
		StartCoroutine ("ChangeQuestion");
	}
	
	IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (1.5F);
		if (GameTryAgain != null) {		
			GameTryAgain();			
		}	
		transform.SendMessage ("SetQuestionValues");
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		FailureScreen.RestartGame += RestartGame;
//		AccelerometerBox.finishEvent += AccelerometerFinish;
	}
	
	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= RestartGame;
//		AccelerometerBox.finishEvent -= AccelerometerFinish;
	}
}

