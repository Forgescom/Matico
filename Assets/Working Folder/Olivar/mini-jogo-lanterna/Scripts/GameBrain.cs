using UnityEngine;
using System.Collections;

public class GameBrain : MonoBehaviour {
	// Game Objects
	public GameObject scene;
	public GameObject questionHolder;
	public GameObject lantern;
//	public GameObject screenLock;


	//EVENTS FOR GAME END
	public delegate void gameEnd(string test);
	public static event gameEnd GameEnded;
	public delegate void GameNewTry();
	public static event GameNewTry GameTryAgain;

	
	//EVENTS
	public delegate void StartGameDelegate();
	public static event StartGameDelegate startGame;
	public delegate void EndGameDelegate(string outcome);
	public static event EndGameDelegate endGame;
	
	public delegate void RestartGameDelegate();
	public static event RestartGameDelegate restartGame;
	

	public bool gameended;
	private bool won;

	int currentScreen = 1;	
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		currentScreen = (GameController.LANTERN_TUT == true) ? 0 : 1;
//		introScreen.SetActive (true);
//		explanationScreen.SetActive (false);
		lantern.SetActive(true);
//		screenLock.SetActive(false);
		questionHolder.SetActive (true);
	}

	void ChangeScreen()
	{
		if (currentScreen == 0) {
//			explanationScreen.SetActive(true);
//			explanationScreen.animation.Play("Lantern");
			currentScreen ++;
		}
		else if(currentScreen == 1)
		{
//			screenLock.SetActive(true);
			questionHolder.SetActive (true);
			questionHolder.animation.Play("QuestionIn");
			lantern.SetActive(true);
			currentScreen ++;
		}
	}

	void AnswerHit(bool correct)
	{
		Handheld.Vibrate ();
		gameended = true;
		if(correct == true)
		{
			if(endGame != null)
			{
				won = true;
			}
		}
		else
		{
			if(endGame != null)
			{
				won = false;
			}
		}
	}


	IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (1.5F);
		transform.SendMessage ("SetQuestionValues");
		questionHolder.animation.Play("QuestionIn");
//		screenLock.SetActive(true);
	}

}
