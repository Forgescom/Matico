using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoardMain : MonoBehaviour {

	public GameObject intro;
	public GameObject bg;
	public GameObject housesHolder;

	public GameObject [] housesGameObject;

	public CameraMovesHandler cameraScript;

	public static BoardMain control;


	void Start(){
		housesGameObject =   GameObject.FindGameObjectsWithTag("House").OrderBy( go => go.name ).ToArray();

		ShowIntro ();
		UnlockHouses ();
		AssignHousesSettings ();


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
		print ("BU" + GameController.CURRENT_LEVEL);
		for (int i = 0; i<GameController.CURRENT_LEVEL; i ++) {
			housesGameObject[i].GetComponent<CasaController>().UnlockButton();
			if(i == (GameController.CURRENT_LEVEL -1))
			{
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = true;
			}
			print ("BU");
		}
	}

	void AssignHousesSettings()
	{	
		for (int i = 0; i < GameController.houses.Count; i++) {
			string houseName;
			GameController.houses[i].TryGetValue("HouseName",out houseName);
	
			if(houseName == housesGameObject[i].name)
			{
				string typeOfGame;
				GameController.houses[i].TryGetValue("Typeofgame",out typeOfGame);
				switch(typeOfGame)
				{
					case "shooter": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.shooter; break;
					case "accelerometer": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.accelerometer; break;
					case "scratchcard": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.scratchcard; break;
					case "tilt": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.tilt; break;
				}

			//	currentLevelType = typeOfGame;

				string energiesSpent;
				GameController.houses[i].TryGetValue("EnergiesSpent",out energiesSpent);
				housesGameObject[i].GetComponent<CasaValues>().energiesSpent = int.Parse(energiesSpent);
				
				string dificulty;
				GameController.houses[i].TryGetValue("Dificulty",out dificulty);
				housesGameObject[i].GetComponent<CasaValues>().dificulty = int.Parse(dificulty);
			}
		}
	}

	void StartLevel(Transform houseCliked)
	{
		string gameToOpen = houseCliked.GetComponent<CasaValues> ().gameType.ToString();
		//currentLevelType = houseCliked.GetComponent<CasaValues> ().gameType.ToString();
		//currentLevel = int.Parse(houseCliked.name.Substring (4, 2));

		//currentLevelDificulty = houseCliked.GetComponent<CasaValues> ().dificulty;

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


	void LevelEnd()
	{
		/*IF WINS 
		 * INCREMENT LEVEL NUMBER
		 * UNLOCK AND HIGHLIGHT NEXT LEVEL
		 * SAVE GAME
		 * 
		 * IF LOOSES
		 * SAVE SPEN ENERGIES ON GAME
		 * REMOVE ENERGI
		 * 
		 * */
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
	}

	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= EnableMap;
		CasaController.throwGame -= StartLevel;
	}
}
