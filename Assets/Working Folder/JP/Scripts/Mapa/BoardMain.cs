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
	public List<Dictionary<string,string>> houses = new List<Dictionary<string,string>>();

	public static string currentLevelType;
	public static int currentLevel;
	public static int currentLevelDificulty;
	public static int energies = 4;

	public static BoardMain control;

	void Awake(){
		print ("BU");
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}
		
		housesGameObject =   GameObject.FindGameObjectsWithTag("House").OrderBy( go => go.name ).ToArray();
		
		ShowObjects ();
		UnlockHouses ();
		LoadHousesSettings ();
	}

	void Start(){
		print ("BU2");
		/*housesGameObject =   GameObject.FindGameObjectsWithTag("House").OrderBy( go => go.name ).ToArray();

		ShowObjects ();
		UnlockHouses ();
		LoadHousesSettings ();

*/
	}

	void ShowObjects(){
		Main.CURRENT_LEVEL = 2;
		if (Main.CURRENT_LEVEL <= 1) {
			intro.SetActive(true);
			bg.GetComponent<BackgroundTouch>().enabled = false;
		}
		else
		{
			intro.SetActive(false);
			bg.SetActive(true);
		}
	}

	void UnlockHouses()
	{
		for (int i = 0; i<Main.CURRENT_LEVEL; i ++) {
			housesGameObject[i].GetComponent<CasaController>().UnlockButton();
			if(i == (Main.CURRENT_LEVEL -1))
			{
				housesGameObject[i].GetComponent<CasaController>().isHighLighted = true;
			}
		}
	}

	void LoadHousesSettings()
	{	
		for (int i = 0; i < houses.Count; i++) {
			string houseName;
			houses[i].TryGetValue("HouseName",out houseName);
	
			if(houseName == housesGameObject[i].name)
			{
				string typeOfGame;
				houses[i].TryGetValue("Typeofgame",out typeOfGame);
				switch(typeOfGame)
				{
					case "shooter": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.shooter; break;
					case "accelerometer": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.accelerometer; break;
					case "scratchcard": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.scratchcard; break;
					case "tilt": housesGameObject[i].GetComponent<CasaValues>().gameType = TypeOfGames.tilt; break;
				}

				currentLevelType = typeOfGame;

				string energiesSpent;
				houses[i].TryGetValue("EnergiesSpent",out energiesSpent);
				housesGameObject[i].GetComponent<CasaValues>().energiesSpent = int.Parse(energiesSpent);
				
				string dificulty;
				houses[i].TryGetValue("Dificulty",out dificulty);
				housesGameObject[i].GetComponent<CasaValues>().dificulty = int.Parse(dificulty);
			}
		}
	}

	void StartLevel(Transform houseCliked)
	{

		currentLevelType = houseCliked.GetComponent<CasaValues> ().gameType.ToString();
		currentLevel = int.Parse(houseCliked.name.Substring (4, 2));

		currentLevelDificulty = houseCliked.GetComponent<CasaValues> ().dificulty;

		switch (currentLevelType) {
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
			cameraScript.startAnimBoard (housesGameObject [Main.CURRENT_LEVEL].transform.position);
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
