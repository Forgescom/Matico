using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	//MENU CONTROLLER
	public GameObject avatarMenu;
	public GameObject mainMenu;
	public GameObject settingsMenu;
	public GameObject programaMenu;
	public GameObject backButton;
	GameObject activeMenu;


	//PLAYER PREFS KEYS
	public static string PREFS_PLAYER_NAME = "PlayerName";
	public static string PREFS_PLAYER_AVATAR = "PlayerAvatar";
	public static string PREFS_PLAYER_CURRENTLEVEL = "PlayerLevel";
	public static string PREFS_PLAYER_SOUNDFX = "SoundFx";
	public static string PREFS_PLAYER_SOUNDAMBIENTE = "SoundAmbiente";
	public static string PREFS_PLAYER_SOUNDMATICO = "SoundMatico";



	//PLAYER VALUES
	public static string PLAYER_NAME;
	public static int CURRENT_LEVEL;


	// Use this for initialization
	void Start () {

		//SHOW/HIDE MENUS
		ShowHideMenus ();

		//LOAD PLAYERPREFS AND CHOOSE WHETER TO SHOW OR NOT AVATAR MENU
		LoadPlayerPrefs ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void SavePlayerPref(string nome = null, int avatarNumber = 99, int newLevel =0)
	{
		if(nome != null)
			PlayerPrefs.SetString (PREFS_PLAYER_NAME, nome);
		if(avatarNumber!=99)
			PlayerPrefs.SetInt (PREFS_PLAYER_AVATAR, avatarNumber);
		if(newLevel !=0)
			PlayerPrefs.SetInt (PREFS_PLAYER_CURRENTLEVEL, newLevel);

	}

	public void ChangeMenu(GameObject closeMenu, GameObject openMenu)
	{
		closeMenu.animation.Play (closeMenu.name + "Out");
		openMenu.SetActive (true);
		openMenu.animation.Play (openMenu.name + "In");
		
		//CHANGE ACTYIVE MENU
		activeMenu = openMenu;

		if (activeMenu != mainMenu) {
			backButton.animation.Play("BtBackIn");
		}
	}

	public void BackMenu()
	{
		backButton.animation.Play("BtBackOut");
		ChangeMenu (activeMenu, mainMenu);
	}

	void LoadPlayerPrefs()
	{

		if (PlayerPrefs.GetString (PREFS_PLAYER_NAME) == "") {
			mainMenu.SetActive (false);
			avatarMenu.SetActive(true);
			PLAYER_NAME = "Nome";
			//CHANGE ACTIVE MENU NAME
			activeMenu = avatarMenu;
		}
		else
		{
			mainMenu.SetActive (true);
			avatarMenu.SetActive(false);
			PLAYER_NAME = PlayerPrefs.GetString (PREFS_PLAYER_NAME) ;
			activeMenu = mainMenu;
		}

		if (PlayerPrefs.HasKey (PREFS_PLAYER_CURRENTLEVEL)) {
			CURRENT_LEVEL = PlayerPrefs.GetInt (PREFS_PLAYER_CURRENTLEVEL);
		}
	}

	void ShowHideMenus()
	{
		settingsMenu.SetActive (false);
		programaMenu.SetActive (false);
	}
}
