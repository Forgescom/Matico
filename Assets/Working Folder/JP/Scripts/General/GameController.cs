using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameController : MonoBehaviour {
	public static GameController controller;

	//PLAYER PREF KEYS
	public static string PREFS_PLAYER_NAME = "PlayerName";
	public static string PREFS_PLAYER_AVATAR = "PlayerAvatar";
	public static string PREFS_PLAYER_CURRENTLEVEL = "PlayerLevel";
	public static string PREFS_PLAYER_FXSOUND = "SoundFx";
	public static string PREFS_PLAYER_BGSOUND = "SoundAmbiente";
	public static string SOUND_BG = "Background";
	public static string SOUND_FX = "FX";

	//SOUND
	public static bool FX_SOUND = true;
	public static bool BG_SOUND = true;

	//PLAYER VALUES
	public static string PLAYER_NAME;
	public static int PLAYER_FACE;
	public static int CURRENT_LEVEL;
	public static int CURRENT_LEVEL_DIFICULTY;
	public static string CURRENT_LEVEL_TYPE;
	public static int CURRENT_LIVES_LOST;
	public static int CURRENT_LIVES = 4;


	//MINI GAMES FINAL SCREEN
	public GameObject SUCCESS_SCREEN;
	public GameObject FAILURE_SCREEN;

	//TUTORIALS
	public static bool SCRATCHCARD_TUT = true;
	public static bool ACELEROMETER_TUT = true;
	public static bool FLIP_TUT = false;
	public static bool SHOOTER_TUT = false;


	//RESTARTING VAR
	public static bool SHOOTER_RESTARTING = false;


	//XML VALUES
	public static List<Dictionary<string,string>> houses = new List<Dictionary<string,string>>();
	public static List<Dictionary<string,string>> questions = new List<Dictionary<string, string>>();



	//EVENTS AND DELEGATES
	public delegate void SoundDelegate(string type);
	public static event SoundDelegate updateSoundVolume;
	public delegate void RestartDelegate();
	public static event RestartDelegate RestartGame;


	public delegate void UnlockEvent(bool FromMatico);
	public static event UnlockEvent unlockHouse;





	void Awake()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (controller == null){
			DontDestroyOnLoad(gameObject);
			controller = this;
		}
		else if (controller != this){
			Destroy(gameObject);
		}


		//THROW EVENT TO TURN ON/OFF SOUNDS
		if (updateSoundVolume != null) {
			updateSoundVolume(SOUND_BG);
			updateSoundVolume(SOUND_FX);
		}

	}

	// Use this for initialization
	void Start () {

		CURRENT_LEVEL = 0;
		Init ();
	}
	void Init(){

		SetSound ();
		SetAvatar ();

	}


	// Update is called once per frame
	void Update () {	

	}

	public void SetAvatar(){
		if (PlayerPrefs.GetString (PREFS_PLAYER_NAME) == "")
			PLAYER_NAME = "Nome";	
		else 
			PLAYER_NAME = PlayerPrefs.GetString (PREFS_PLAYER_NAME);		
		
		PLAYER_FACE = PlayerPrefs.GetInt (PREFS_PLAYER_AVATAR);

	}
	public void SetSound(){
		int musicSoundOnINT = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_BGSOUND);
		BG_SOUND = (musicSoundOnINT == 0) ? false : true;
		int fxSoundOnINT = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_FXSOUND);
		FX_SOUND = (fxSoundOnINT == 0) ? false : true;


		
		//THROW EVENT TO TURN ON/OFF SOUNDS
		if (updateSoundVolume != null) {
			updateSoundVolume(SOUND_BG);
			updateSoundVolume(SOUND_FX);
		}
	}

	public static void SwitchOnOffSound(string sound)
	{
		bool currentValue;
		int boolInt;

		if (sound == "FX") {
			FX_SOUND = !FX_SOUND;

			currentValue = FX_SOUND;
			boolInt = currentValue ? 1 : 0;

			PlayerPrefs.SetInt(PREFS_PLAYER_FXSOUND,boolInt);
			if (updateSoundVolume != null) {
				updateSoundVolume(SOUND_FX);
			}
		}
		else if(sound == "Music"){
			BG_SOUND = !BG_SOUND;

			currentValue = BG_SOUND;
			boolInt = currentValue ? 1 : 0;

			PlayerPrefs.SetInt(PREFS_PLAYER_BGSOUND,boolInt);
			
			if (updateSoundVolume != null) {
				updateSoundVolume(SOUND_BG);
			}

		}
	}

	//SAVE PLAYER VALUES FROM MAIN MENU
	public static void SavePlayerPref(string nome = null, int avatarNumber = 99)
	{
		if(nome != null)
			PlayerPrefs.SetString (GameController.PREFS_PLAYER_NAME, nome);
			PLAYER_NAME = nome;
		if (avatarNumber != 99)
			PLAYER_FACE = avatarNumber;
			PlayerPrefs.SetInt (GameController.PREFS_PLAYER_AVATAR, avatarNumber);
		
	}

	//HANDLE EVENTS FROM MINI GAMES
	public void ShowMiniGameFinalScreen(string outComeIn)
	{
		if (outComeIn == "Certo") {
			GameObject successScreen = Instantiate (SUCCESS_SCREEN,new Vector3(0,0,-9),Quaternion.identity) as GameObject;
			successScreen.transform.parent = Camera.main.transform;
		}
		else if (outComeIn == "Errado"){
			Invoke("RemoveLife",2);
			GameObject failScreen = Instantiate (FAILURE_SCREEN,new Vector3(0,0,-9),Quaternion.identity) as GameObject;
			failScreen.transform.parent = Camera.main.transform;
			LifesHandler scriptEnergies = failScreen.transform.FindChild("EnergyHUD").GetComponent<LifesHandler>() as LifesHandler;
			scriptEnergies.AnimateLifeLoose();
		}
		houses[CURRENT_LEVEL-1]["EnergiesSpent"] = CURRENT_LIVES_LOST.ToString();
	}

	public static void NoLifesHandler()
	{
		Application.LoadLevel ("Board");
	}

	void RemoveLife(){
		CURRENT_LIVES --;
		CURRENT_LIVES_LOST ++;
	}

	//HANDLE CLICK FROM SUCCESS SCREEN
	void btClick(GameObject bt)
	{

		switch (bt.name) {
		case "BtRepeat":
	
			if(RestartGame!=null)
			{
				RestartGame();
			}
		
			Destroy(bt.transform.parent.gameObject);
			break;
		case "BtNext":

			Application.LoadLevel("Board");
			StartCoroutine("WaitForBoardLoad");

			break;
		}
		
	}

	IEnumerator WaitForBoardLoad()
	{
		yield return new WaitForSeconds (2f);
		houses[CURRENT_LEVEL-1]["Played"] = "true";
		houses[CURRENT_LEVEL]["Blocked"] = "false";

		if(unlockHouse != null)
		{
			unlockHouse(false);
		}
		transform.SendMessage ("WriteToXml");

	}
	
	void OnEnable()
	{
		ScratchController.GameEnded += ShowMiniGameFinalScreen;
		AcelerometerBrain.endGame += ShowMiniGameFinalScreen;
		GameManager.endGame += ShowMiniGameFinalScreen;
	}
	void OnDisable()
	{
		ScratchController.GameEnded -= ShowMiniGameFinalScreen;
		AcelerometerBrain.endGame -= ShowMiniGameFinalScreen;
		GameManager.endGame -= ShowMiniGameFinalScreen;
	}
}
