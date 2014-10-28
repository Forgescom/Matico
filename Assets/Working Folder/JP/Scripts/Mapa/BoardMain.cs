using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoardMain : MonoBehaviour {

	public GameObject intro;
	public GameObject bg;
	public GameObject clouds;

	public GameObject [] housesGameObject;

	public CameraMovesHandler cameraScript;
	public bool showIntro = true;


	void Start(){


		housesGameObject =   GameObject.FindGameObjectsWithTag("House").OrderBy( go => go.name ).ToArray();

		TurnOffOnSound ();
		AssignHousesSettings ();

		if(showIntro == true)
			ShowIntro ();
		else
			EnableMap();


	}

	void ShowIntro(){
		if (GameController.CURRENT_LEVEL <= 1) {
			intro.SetActive(true);
			bg.GetComponent<BackgroundTouch>().enabled = false;
		}
		else
		{
			intro.SetActive(false);
			EnableMap();
		}
	}
	

	void AssignHousesSettings()
	{	

		for (int i = 0; i < GameController.houses.Count; i++) {
			string houseName;
			GameController.houses[i].TryGetValue("HouseName",out houseName);
	
			string locked;
			GameController.houses[i].TryGetValue("Blocked",out locked);

			if(locked == "false")
			{
				housesGameObject[i].GetComponent<CasaValues>().locked = false;
				housesGameObject[i].GetComponent<CasaController>().UnlockButton();
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = true;

				//IF MORE THEN ONE LEVEL IS UNLOCK DONT SHOW INTRO
				if(i>0)
				{
					showIntro = false;
					housesGameObject[i-1].GetComponent<CasaController>().isHighLighted = false;
				}
			}
			else{
				housesGameObject[i].GetComponent<CasaValues>().locked = true;
			}

			string typeOfGame;
			GameController.houses[i].TryGetValue("Typeofgame",out typeOfGame);
			switch(typeOfGame)
			{
				case "Shooter": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.shooter; break;
				case "Acelerometer": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.accelerometer; break;
				case "ScratchCard": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.scratchcard; break;
				case "tilt": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.tilt; break;
			}

			string energiesSpent;
			GameController.houses[i].TryGetValue("EnergiesSpent",out energiesSpent);
			housesGameObject[i].GetComponent<CasaValues>().energiesSpent = int.Parse(energiesSpent);
			
			string dificulty;
			GameController.houses[i].TryGetValue("Dificulty",out dificulty);
			housesGameObject[i].GetComponent<CasaValues>().dificulty = int.Parse(dificulty);
	
		}
	}

	public void StartLevel(Transform houseCliked)
	{
		string gameToOpen = houseCliked.GetComponent<CasaValues> ().gameType.ToString();
		int dificulty = houseCliked.GetComponent<CasaValues> ().dificulty;
		
		int currentLevelNumber =0;
		int.TryParse(houseCliked.name.Substring(4,2),out currentLevelNumber);
		GameController.CURRENT_LEVEL = currentLevelNumber;
		GameController.CURRENT_LEVEL_DIFICULTY = dificulty;
		GameController.CURRENT_LEVEL_TYPE = gameToOpen;
		GameController.CURRENT_LIVES_LOST = 0;

		switch (gameToOpen) {
			case "shooter":
				Application.LoadLevel ("Shooter");			
				break;
			case "accelerometer":
				Application.LoadLevel ("ocean");			
				break;
			case "scratchcard":
				Application.LoadLevel ("Scratchcard");			
				break;
			case "tilt":
				Application.LoadLevel ("TiltGame");			
				break;
		}	
	}
	


	void EnableMap()
	{
		if (Application.loadedLevelName == "Board") {
			bg.GetComponent<BackgroundTouch> ().enabled = true;
			cameraScript.startAnimBoard (housesGameObject [GameController.CURRENT_LEVEL].transform.position);
			clouds.GetComponent<Animation>().Play("CloudOpen");
		}
	}

	void TurnOffOnSound()
	{
		if (GameController.musicSoundOn == false) {
			transform.GetComponent<AudioSource>().Stop();
		}
		else{
			transform.GetComponent<AudioSource>().Play();
			//transform.audio.Play();
		}
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += EnableMap;
		CasaController.throwGame += StartLevel;
		GameController.updateSoundVolume += TurnOffOnSound;
	}

	void OnDisable()
	{
		GameController.updateSoundVolume -= TurnOffOnSound;
		DeactivateOnAnimEnd.animationFinish -= EnableMap;
		CasaController.throwGame -= StartLevel;
	}
}
