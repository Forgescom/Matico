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
	public static int CURRENT_LIVES;



	//XML VALUES
	public static List<Dictionary<string,string>> houses = new List<Dictionary<string,string>>();
	public static List<Dictionary<string,string>> questions = new List<Dictionary<string, string>>();

	void Awake()
	{
		if (controller == null)
		{
			DontDestroyOnLoad(gameObject);
			controller = this;
		}
		else if (controller != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
		if (PlayerPrefs.GetString (PREFS_PLAYER_NAME) == "")
			PLAYER_NAME = "Nome";	
		else 
			PLAYER_NAME = PlayerPrefs.GetString (PREFS_PLAYER_NAME);
	
	
		PLAYER_FACE = PlayerPrefs.GetInt (PREFS_PLAYER_AVATAR);


		CheckUnlocked ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


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


	public static void ChangeScene(string newScene)
	{

	}

	void CheckUnlocked()
	{
		for (int i = 0; i<houses.Count; i ++) {
			string block;
			houses[i].TryGetValue("Blocked",out block);

			if(block == "false")
			{
				CURRENT_LEVEL ++;
			}
		}

	}

	public static void MiniGamelEnd(string outCome)
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

	
	
	
	
}
