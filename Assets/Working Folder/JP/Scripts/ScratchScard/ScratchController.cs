﻿using UnityEngine;
using System.Collections;

public class ScratchController : MonoBehaviour {

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject scratchCard;


	int currentScreen = 0;

	//EVENTS FOR GAME END
	public delegate void GameEnd(string test);
	public static event GameEnd GameEnded;


	// Use this for initialization
	void Start () {
		currentScreen = (GameController.SCRATCHCARD_TUT == true) ? 0 : 1;

		print (currentScreen);

		explanationScreen.SetActive (false);
		introScreen.SetActive (true);
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
			currentScreen ++;
		}
	}

	void ScratchFinish(string inParam)
	{
		if (GameEnded != null) {		
			GameEnded(inParam);			
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
