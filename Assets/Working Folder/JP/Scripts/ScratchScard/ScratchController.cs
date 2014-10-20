using UnityEngine;
using System.Collections;

public class ScratchController : MonoBehaviour {

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject scratchCard;


	int currentScreen = 0;

	//EVENTS FOR GAME END
	public delegate void GameEnd(string test);
	public static event GameEnd GameEnded;
	public delegate void GameNewTry();
	public static event GameNewTry GameTryAgain;

	// Use this for initialization
	void Start () {
		Init ();

	}


	void Init()
	{
		currentScreen = (GameController.SCRATCHCARD_TUT == true) ? 0 : 1;
		introScreen.SetActive (true);
		explanationScreen.SetActive (false);
		scratchCard.SetActive(false);

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
			GameController.SCRATCHCARD_TUT = false;
			currentScreen ++;
		}
		else if(currentScreen == 1)
		{
			scratchCard.SetActive(true);
			scratchCard.animation.Play("ScratchCardIn");
			currentScreen ++;
		}
	}

	void ScratchFinish(string inParam)
	{
		if (GameEnded != null) {		
			GameEnded(inParam);			
		}		
	}

	void RestartGame()
	{
		scratchCard.animation.Play("ScratchCardOut");
		StartCoroutine ("ChangeQuestion");
	}

	IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (1.5F);
		if (GameTryAgain != null) {		
			GameTryAgain();			
		}	
		transform.SendMessage ("SetQuestionValues");
		scratchCard.animation.Play ("ScratchCardIn");
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		ScratchBox.finishEvent += ScratchFinish;
		FailureScreen.RestartGame += RestartGame;
		GameController.RestartGame += RestartGame;
	}

	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		ScratchBox.finishEvent -= ScratchFinish;
		FailureScreen.RestartGame -= RestartGame;
		GameController.RestartGame -= RestartGame;
	}

}
