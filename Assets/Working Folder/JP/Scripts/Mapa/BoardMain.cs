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

		TurnOffOnSound (GameController.SOUND_FX);
		TurnOffOnSound (GameController.SOUND_BG);
		AssignHousesSettings ();
		//UnlockHouses ();

		if(showIntro == true)
			ShowIntro ();
		else
			EnableMap();


	}

	void ShowIntro(){
		if (GameController.CURRENT_LEVEL == 0) {
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

			//CHECK IF HOUSE IS LOCK AND ASSIGN	
			string locked;
			GameController.houses[i].TryGetValue("Blocked",out locked);

			if(locked == "true")
				housesGameObject[i].GetComponent<CasaController>().locked = true;
			else{
			
				housesGameObject[i].GetComponent<CasaController>().locked = false;
			}


			//CHECK IF HOUSE WAS PLAYED AND IF IS UNLOCKED TO HIGHLIGHT
			string played;
			GameController.houses[i].TryGetValue("Played",out played);

			if(played == "false" && locked == "false")
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = true;
			else
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = false;


			string typeOfGame;
			GameController.houses[i].TryGetValue("Typeofgame",out typeOfGame);
			switch(typeOfGame)
			{
				case "Shooter": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.shooter; break;
				case "Acelerometer": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.accelerometer; break;
				case "ScratchCard": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.scratchcard; break;
				case "Tilt": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.tilt; break;
			}

			string energiesSpent;
			GameController.houses[i].TryGetValue("EnergiesSpent",out energiesSpent);
			housesGameObject[i].GetComponent<CasaValues>().energiesSpent = int.Parse(energiesSpent);
			
			string dificulty;
			GameController.houses[i].TryGetValue("Dificulty",out dificulty);
			housesGameObject[i].GetComponent<CasaValues>().dificulty = int.Parse(dificulty);

			//UPDATE HOUSE TO ITS STATE WITH VALUES ASSIGNED NOW
			housesGameObject[i].GetComponent<CasaController>().UpdateState();


			//CHECK IF ShOW INTRO OR NOT
			if(i == 0 && played == "true")
			{
				showIntro = false;
			}
		}

	}

	void UnlockNextHouse(bool fromMatico = false)
	{
		CasaController houseController = housesGameObject [GameController.CURRENT_LEVEL].GetComponent<CasaController> ();

		if (GameController.CURRENT_LEVEL % 3 == 0 && fromMatico == false) {
			transform.SendMessage("ShowMatico");
		}
		else
		{
			if(houseController.locked == true){
				houseController.locked = false;
				houseController.UnlockButton ();
				houseController.isHighLighted = true;
				housesGameObject [GameController.CURRENT_LEVEL - 1].GetComponent<CasaController> ().isHighLighted = false;
			}
		}

	}

	void LockLastHouse()
	{

		CasaController houseController = housesGameObject [GameController.CURRENT_LEVEL-1].GetComponent<CasaController> ();


		if(houseController.locked == false){
			houseController.locked = true;
			houseController.LockButton ();
			houseController.isHighLighted = false;
			housesGameObject [GameController.CURRENT_LEVEL - 2].GetComponent<CasaController> ().isHighLighted = true;

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
				GameController.SHOOTER_RESTARTING = false;
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

	void TurnOffOnSound(string soundType)
	{
		if(soundType == GameController.SOUND_BG)
		{
			AudioSource audio = transform.GetComponent<AudioSource> ();
			audio.enabled = GameController.BG_SOUND;
			
			if (audio.enabled)
				audio.Play ();
			else {
				audio.Stop();
			}
		}
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += EnableMap;
		CasaController.throwGame += StartLevel;
		GameController.updateSoundVolume += TurnOffOnSound;
		GameController.unlockHouse += UnlockNextHouse;
		GameController.lockHouse += LockLastHouse;
		MaticoUnlocker.unlockNextHouse += UnlockNextHouse;
	}

	void OnDisable()
	{
		GameController.updateSoundVolume -= TurnOffOnSound;
		DeactivateOnAnimEnd.animationFinish -= EnableMap;
		CasaController.throwGame -= StartLevel;
		GameController.unlockHouse -= UnlockNextHouse;
		GameController.lockHouse -= LockLastHouse;
		MaticoUnlocker.unlockNextHouse -= UnlockNextHouse;
	}
}
