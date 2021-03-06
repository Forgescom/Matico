﻿using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour {

	public static bool CANMOVE = true;
	public AudioClip scratchSound;

	Vector3 startPosition;
	bool grabed = false;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		CANMOVE = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(CANMOVE == true){
			if (CheckFingerTouch() == true || grabed == true) {
				DragCoin();
			}
		}
		else{
			transform.position = startPosition;
		}

	}


	bool CheckFingerTouch()
	{

		if (Input.touchCount > 0) {
			
			Vector3 wp  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos  = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			
			if(hit && hit.name == "moeda"){
				print("ACERTEI NA MOEDA");
				grabed = true;
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			transform.audio.Stop();
			transform.position = startPosition;
			grabed = false;
			return false;
		}
	}


	void DragCoin()
	{
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 5));
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(grabed)
		{
			if (Input.touchCount>0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				if(transform.audio.isPlaying == false)
					transform.audio.Play();
				col.SendMessage ("Scratch", true);
			}
			else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary){
				transform.audio.Pause();
				col.SendMessage ("Scratch", false);
			}
		}

	}



	void OnTriggerExit2D(Collider2D col){
		col.SendMessage ("Scratch", false);

		
	}
	void RestartGame()
	{
		CANMOVE = true;
	}

	void GameEnded(string tag)
	{
		CANMOVE = false;
		transform.audio.Stop();
	}
	
	void OnEnable()
	{
		ScratchController.GameTryAgain += RestartGame;
		ScratchBox.finishEvent += GameEnded;
		
	}
	void OnDisable()
	{
		ScratchController.GameTryAgain += RestartGame;
		ScratchBox.finishEvent -= GameEnded;
		
	}


}
