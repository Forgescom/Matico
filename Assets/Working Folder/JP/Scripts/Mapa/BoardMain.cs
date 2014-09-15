using UnityEngine;
using System.Collections;

public class BoardMain : MonoBehaviour {

	//ANIMATE CAMERA
	public bool canStartCamera = false;

	//HOUSES
	public GameObject [] houses;
	int currentHouse;

	//OBJECTS TO CONTROLL
	public GameObject intro;
	public Camera mainCamera;




	// Use this for initialization
	void Start () {

		currentHouse = PlayerPrefs.GetInt (Main.PREFS_PLAYER_LEVEL);


		//CHECK PLAYER LEVEL IF 0 THEN START ANIMATION ELSE DONT SHOW MATICO
		if (currentHouse == 0) {
			currentHouse = 1;
			PlayerPrefs.SetInt (Main.PREFS_PLAYER_LEVEL,1);
			intro.SetActive(true);
		}
		else
		{
			currentHouse = PlayerPrefs.GetInt (Main.PREFS_PLAYER_LEVEL);
			intro.SetActive(false);
			mainCamera.GetComponent<CameraMovesHandler>().startAnimBoard(houses [currentHouse-1].transform.position);
		}

		//UNLOCKHOUSES
		for (int i = 0; i<currentHouse; i ++) {
			houses[i].GetComponent<CasaController>().UnlockButton();
			if(i == (currentHouse -1))
			{
				houses[i].GetComponent<CasaController>().isHighLighted = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canStartCamera == true) {
			if(intro.animation.isPlaying == false)
			{
				mainCamera.GetComponent<CameraMovesHandler>().startAnimBoard(houses [currentHouse-1].transform.position);
			}		
		}
	}

	void LevelPassed()
	{
		currentHouse ++;
		PlayerPrefs.SetInt (Main.PREFS_PLAYER_LEVEL, currentHouse);

	}


}
