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



	// Use this for initialization
	void Start () {
		settingsMenu.SetActive (false);
		programaMenu.SetActive (false);


		if (PlayerPrefs.GetString (PREFS_PLAYER_NAME) == "") {
			mainMenu.SetActive (false);
			avatarMenu.SetActive(true);

			//CHANGE ACTIVE MENU NAME
			activeMenu = avatarMenu;
		}
		else
		{
			mainMenu.SetActive (true);
			avatarMenu.SetActive(false);

			activeMenu = mainMenu;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveName(string nomeEntrada, int avatarNumber)
	{
		PlayerPrefs.SetString (PREFS_PLAYER_NAME, nomeEntrada);
		PlayerPrefs.SetInt (PREFS_PLAYER_AVATAR, avatarNumber);	

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
}
