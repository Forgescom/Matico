using UnityEngine;
using System.Collections;

public class ScratchController : MonoBehaviour {

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject scratchCard;
	public GameObject sucessScreen;
	public GameObject failureScreen;

	int currentScreen = 0;

	// Use this for initialization
	void Start () {
		explanationScreen.SetActive (false);
		introScreen.SetActive (true);
		scratchCard.SetActive(false);
		sucessScreen.SetActive (false);
		failureScreen.SetActive(false);
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
			scratchCard.SetActive(true);
			currentScreen ++;
		}
	}

	void ScratchFinish(string inParam)
	{
		if (inParam == "Certo")
		{
			//GameController.MiniGamelEnd("Win");
			sucessScreen.SetActive(true);

		}				
		else{
			//GameController.MiniGamelEnd("Loose");
			failureScreen.SetActive(true);
		}
		
	}

	void btClick(GameObject btClicked)
	{
		switch (btClicked.name) {
		case "BtMap":
			GameController.MiniGamelEnd("Won");
			break;
		case "BtRepeat":
			//GameController.MiniGamelEnd("Won");
			break;
		case "BtNext":
			GameController.MiniGamelEnd("NextLevel");
			break;
		}
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		ScratchBox.finishEvent += ScratchFinish;
	}

	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		ScratchBox.finishEvent -= ScratchFinish;
	}

}
