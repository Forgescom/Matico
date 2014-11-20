﻿using UnityEngine;
using System.Collections;

public class Tilt_brain : MonoBehaviour {

	public int lives;
	// Use this for initialization
	//contadores
	public countMenorScr countMenor;
	public int Menor;
	public countIgualSrc countIgual;
	public int Igual;
	public countMaiorSrc countMaior;
	public int Maior;
	//timer
	public float cntTime;
	public Vector2 timePos;
	public Vector2 timeSize;

	//Over restart
	public GUIText RestartText;
	public GUIText GameOverText;
	private bool gameOver;
	private bool restart;

	//win
	public string winText;

	public bool paused;

	void Start(){
		gameOver = false;
		restart = false;
		RestartText.text = "";
		GameOverText.text = "";
	}

	void Update(){
		countMaior.barDisplay = Maior;
		countIgual.barDisplay = Igual;
		countMenor.barDisplay = Menor;

		//GAMEOVER
		//time
		cntTime -= Time.deltaTime;

		if (cntTime <= 0){
			GameOver ();
		}
		//lives
		if (lives == 0) {
			ToLose();
		}
		//PAUSE
		if(Input.GetKeyDown("p") && !paused)
		{
			print("p");
			Time.timeScale = 0.0f;
			paused = true;
		}else if(Input.GetKeyDown("p") && paused)
		{
			print("Unpaused");
			Time.timeScale = 1.0f;
			paused = false;    
		} 
		//RESTART
		if (restart){
			if (Input.GetKeyDown (KeyCode.R)) {
				Time.timeScale = 1.0f;
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	public void GameOver (){
		Time.timeScale = 0.0f;

		bool empate = false;
		int maiorValor = 0;

		if (Maior > maiorValor) {
			maiorValor = Maior;
		}
		if (Menor >= maiorValor) {
			if(maiorValor == Menor){
				empate = true;
			}
			else
			{
				maiorValor = Menor;
				empate = false;
			}
		}
		if (Igual >= maiorValor) {
			if(maiorValor == Igual){
				empate = true;
			}
			else
			{
				maiorValor = Igual;
				empate = false;
			}
		}

		if (empate) {
			ToLose();		
		}else{
			checkWin (maiorValor);
		}
	}

	void checkWin (int value){
		if (value == Maior) {
			if (winText == "Maior"){
				GameOverText.text = "YOU WIN!!!";
			}else{
				ToLose();	
			}
		}
		if (value == Igual) {
			if (winText == "Igual"){
				GameOverText.text = "YOU WIN!!!";
			}else{
				ToLose();	
			}
		}
		if (value == Menor) {
			if (winText == "Menor"){
				GameOverText.text = "YOU WIN!!!";
			}else{
				ToLose();	
			}
		}
	}

	void ToLose() {
		GameOverText.text = "YOU LOSE!";
		RestartText.text = "Press 'R' for Restart";
		restart = true;	
	}

	void OnGUI() {
		GUI.Box (new Rect(timePos.x, timePos.y, timeSize.x, timeSize.y), "" + cntTime.ToString("0"));
	}
}
