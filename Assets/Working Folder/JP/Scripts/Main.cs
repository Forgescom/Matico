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


	// Use this for initialization
	void Start () {

		//SHOW MENUS
		ShowMenu ();

	}
	
	// Update is called once per frame
	void Update () {
	
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

	void ShowMenu()
	{
		//IF WE ALREADY HAVE A NAME GO FOR INITIAL SCREEN
		//OTHERWISE SHOW CHARATCER SELECTION MENU
		if (PlayerPrefs.GetString (GameController.PREFS_PLAYER_NAME) == "") {
			mainMenu.SetActive (false);
			avatarMenu.SetActive(true);
			activeMenu = avatarMenu;
		}
		else
		{
			mainMenu.SetActive (true);
			avatarMenu.SetActive(false);
			activeMenu = mainMenu;
		}
	}


}
