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
	public static string PREFS_PLAYER_SOUNDFX = "SoundFx";
	public static string PREFS_PLAYER_SOUNDAMBIENTE = "SoundAmbiente";
	public static string PREFS_PLAYER_SOUNDMATICO = "SoundMatico";

	//PLAYER VALUES
	public static string PLAYER_NAME;
	public static int PLAYER_FACE;
	public static int CURRENT_LEVEL;
	public static int CURRENT_LEVEL_DIFICULTY;
	public static string CURRENT_LEVEL_TYPE;
	public static int CURRENT_LIVES_LOST =0;


	public static int CURRENT_LIVES = 4;

	//MINI GAMES FINAL SCREEN
	public GameObject SUCCESS_SCREEN;
	public GameObject FAILURE_SCREEN;

	//TUTORIALS
	public static bool SCRATCHCARD_TUT = false;
	public static bool ACELEROMETER_TUT = true;
	public static bool FLIP_TUT = true;
	public static bool SHOOTER_TUT = true;

	//XML VALUES
	public static List<Dictionary<string,string>> houses = new List<Dictionary<string,string>>();
	public static List<Dictionary<string,string>> questions = new List<Dictionary<string, string>>();

	//SOUND VALUES
	public static bool fxSoundOn = true;
	public static bool musicSoundOn = true;

	//EVENTS AND DELEGATES
	public delegate void SoundDelegate();
	public static event SoundDelegate updateSoundVolume;
	public delegate void RestartDelegate();
	public static event RestartDelegate RestartGame;

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
			updateSoundVolume();
		}
		//print ("THROW");
	}

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt (PREFS_PLAYER_SOUNDAMBIENTE, 1);
		PlayerPrefs.SetInt (PREFS_PLAYER_SOUNDFX, 1);
		Init ();





	}
	void Init(){
		if (PlayerPrefs.GetString (PREFS_PLAYER_NAME) == "")
			PLAYER_NAME = "Nome";	
		else 
			PLAYER_NAME = PlayerPrefs.GetString (PREFS_PLAYER_NAME);


		int musicSoundOnINT = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_SOUNDAMBIENTE);
		musicSoundOn = (musicSoundOnINT == 0) ? false : true;
		int fxSoundOnINT = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_SOUNDFX);
		fxSoundOn = (fxSoundOnINT == 0) ? false : true;

	
		//THROW EVENT TO TURN ON/OFF SOUNDS
		if (updateSoundVolume != null) {
			updateSoundVolume();
		}



		PLAYER_FACE = PlayerPrefs.GetInt (PREFS_PLAYER_AVATAR);
	}
	// Update is called once per frame
	void Update () {

	}


	public static void SwitchOnOffSound(string sound)
	{
		if (sound == "FX") {
			fxSoundOn = !fxSoundOn;
		}
		if(sound == "Music"){
			musicSoundOn = !musicSoundOn;
		}

		if (updateSoundVolume != null) {
			updateSoundVolume();
		}
	}

	//SAVE PLAYER VALUES FROM MAIN MENU
	public static void SavePlayerPref(string nome = null, int avatarNumber = 99, int newLevel =0)
	{
		if(nome != null)
			PlayerPrefs.SetString (GameController.PREFS_PLAYER_NAME, nome);
			PLAYER_NAME = nome;
		if (avatarNumber != 99)
			PLAYER_FACE = avatarNumber;
			PlayerPrefs.SetInt (GameController.PREFS_PLAYER_AVATAR, avatarNumber);
		if(newLevel !=0)
			CURRENT_LEVEL = newLevel;
			PlayerPrefs.SetInt (GameController.PREFS_PLAYER_CURRENTLEVEL, newLevel);
		
	}

	//HANDLE EVENTS FROM MINI GAMES
	public void ShowMiniGameFinalScreen(string outComeIn)
	{
		if (outComeIn == "Certo") {
			if(CURRENT_LIVES <4)
				CURRENT_LIVES ++;
			Instantiate (SUCCESS_SCREEN,new Vector3(0,0,9),Quaternion.identity);
			houses[CURRENT_LEVEL]["Blocked"] = "false";
		}
		else if (outComeIn == "Errado"){
			CURRENT_LIVES --;
			CURRENT_LIVES_LOST ++;
			houses[CURRENT_LEVEL-1]["EnergiesSpent"] = CURRENT_LIVES_LOST.ToString();
			Instantiate (FAILURE_SCREEN,new Vector3(0,0,-9),Quaternion.identity);
		}
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
			Destroy(bt.transform.root.gameObject);
			break;
		case "BtNext":
			Application.LoadLevel("Board");
			transform.SendMessage ("WriteToXml");
			break;
		}
		
	}


	void OnEnable()
	{
		ScratchController.GameEnded += ShowMiniGameFinalScreen;
		AcelerometerBrain.endGame += ShowMiniGameFinalScreen;
	}
	void OnDisable()
	{
		ScratchController.GameEnded -= ShowMiniGameFinalScreen;
		AcelerometerBrain.endGame -= ShowMiniGameFinalScreen;
	}
}
