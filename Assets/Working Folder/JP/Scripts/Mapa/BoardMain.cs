using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoardMain : MonoBehaviour {

	public GameObject intro;
	public GameObject bg;

	public GameObject [] housesGameObject;

	public CameraMovesHandler cameraScript;



	void Start(){
		housesGameObject =   GameObject.FindGameObjectsWithTag("House").OrderBy( go => go.name ).ToArray();

		ShowIntro ();
		UnlockHouses ();
		AssignHousesSettings ();

		print ("BU");

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

	void UnlockHouses()
	{
		/*for (int i = 0; i<GameController.CURRENT_LEVEL; i ++) {
			housesGameObject[i].GetComponent<CasaController>().UnlockButton();
			if(i == (GameController.CURRENT_LEVEL -1))
			{
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = true;
			}
		}*/
		for (int i = 0; i<GameController.houses.Count; i ++) {

			string locked;
			GameController.houses[i].TryGetValue("Blocked",out locked);

			if(locked == "false")
			{
				housesGameObject[i].GetComponent<CasaController>().UnlockButton();
			}


			if(i == (GameController.CURRENT_LEVEL -1))
			{
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = true;
			}
		}

	}

	void AssignHousesSettings()
	{	
		for (int i = 0; i < GameController.houses.Count; i++) {
			string houseName;
			GameController.houses[i].TryGetValue("HouseName",out houseName);
	
			string locked;
			GameController.houses[i].TryGetValue("Blocked",out locked);
			housesGameObject[i].GetComponent<CasaValues>().locked = (locked == "false") ? false:true;


			if(houseName == housesGameObject[i].name)
			{
				string typeOfGame;
				GameController.houses[i].TryGetValue("Typeofgame",out typeOfGame);
				switch(typeOfGame)
				{
					case "shooter": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.shooter; break;
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
	}

	public void StartLevel(Transform houseCliked)
	{
		string gameToOpen = houseCliked.GetComponent<CasaValues> ().gameType.ToString();

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

	public void UnlockNextLevel()
	{
		print (housesGameObject.Length);	


		for (int i = 0; i < housesGameObject.Length; i++) {			

			if(	housesGameObject[i].GetComponent<CasaValues>().locked == true)
			{
				housesGameObject[i].GetComponent<CasaValues>().locked = false;
				GameController.CURRENT_LEVEL = i;
				break;
			}
		}


	}


	void EnableMap()
	{
		if (Application.loadedLevelName == "Board") {
			bg.GetComponent<BackgroundTouch> ().enabled = true;
			cameraScript.startAnimBoard (housesGameObject [GameController.CURRENT_LEVEL].transform.position);
		}
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += EnableMap;
		CasaController.throwGame += StartLevel;
		//GameController.loadNewLevel += UnlockNextLevel;
	}

	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= EnableMap;
		CasaController.throwGame -= StartLevel;
		//GameController.loadNewLevel += UnlockNextLevel;
	}
}
