using UnityEngine;
using System.Collections;

public class AcelerometerBrain : MonoBehaviour {

	//TEXTO NAS BOLHAS 
	public TextMesh[] bolhas = new TextMesh[10];
	public TextMesh calculoText;	
	public TextMesh finalMessage;
	public TextMesh vidasText;

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject accelerometer;




	int currentScreen = 0;

	int num1;
	int num2;
	int num3;
	int result;

	public float hoverForce;

	public GameObject boia;
	public GameObject sharkfin;
	public GameObject waves;

	int vidas = 3;


	//EVENTS
	public delegate void StartGameDelegate();
	public static event StartGameDelegate startGame;
	public delegate void EndGameDelegate(string outcome);
	public static event EndGameDelegate endGame;


	// Use this for initialization
	void Start () {
		/*introScreen.SetActive (true);
		explanationScreen.SetActive (false);
		accelerometer.SetActive(false);*/



		if(startGame !=null)
		{
			startGame();
		}
		
		set_values ();
		vidasText.text = "Vidas: " + vidas.ToString();
		//bloquear objectos antes de ecras desaparecerem

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel(1);
		}

	}

	void ChangeScreen()
	{
		if (currentScreen == 0) {
			explanationScreen.SetActive(true);
			explanationScreen.animation.Play("Explanation");
			currentScreen ++;
		}
		else if(currentScreen == 1)
		{
			accelerometer.SetActive(true);
			currentScreen ++;
			if(startGame !=null)
			{
				startGame();
			}
		}
	}





	void AnswerHit(bool correct)
	{
		Handheld.Vibrate ();

		if(correct)
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
		vidas --;

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

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
//		AccelerometerBox.finishEvent += AccelerometerFinish;
	}
	
	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
//		AccelerometerBox.finishEvent -= AccelerometerFinish;
	}


	void set_values()
	{
		num1 = Random.Range(1, 20);
		num2 = Random.Range(1, 20);
		num3 = Random.Range(1, 20);
		
		calculoText.text = "Calculo: " + num1.ToString() +" + "+ num2.ToString() +" + "+ num3.ToString() + " =? ";
		
		result = num1 + num2 + num3;
		
		int i = 0;
		int j = 1;
		
		int randomPositionForCorrect = Random.Range (0, 10);
		bolhas[randomPositionForCorrect].text = result.ToString ();
		bolhas[randomPositionForCorrect].transform.parent.tag = "Certo";
		
		for(i = 0; i < bolhas.Length; i++)
		{
			if(bolhas[i].text != result.ToString()){
				bolhas[i].transform.parent.tag = "Errado";
				if (i % 2 != 0) {
					bolhas[i].text = (result + j).ToString();
				}
				else {
					bolhas[i].text = (result - j).ToString();
				}
				j++;			
			}
		}
	}
}

