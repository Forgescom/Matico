using UnityEngine;
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

		cntTime -= Time.deltaTime;
		if (cntTime <= 0 || lives == 0) {
						//FIM
			GameOver ();
			RestartText.text = "Press 'R' for Restart";
			restart = true;
		}

		if (restart){
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}

	}

	public void GameOver (){
		GameOverText.text = "Game Over!";
		gameOver = true;
	}

	void OnGUI() {
		GUI.Box (new Rect(timePos.x, timePos.y, timeSize.x, timeSize.y), "" + cntTime.ToString("0"));
	}



}
