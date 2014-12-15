using UnityEngine;
using System.Collections;

public class SettingsHUD : MonoBehaviour {

	bool open = false;

	AudioClip backGroundMusic;
	public HUDClick somFx;
	public HUDClick somMusica;
	
	// Use this for initialization
	void Start () {
		TurnOffOnSound ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void TurnOffOnSound()
	{

		somMusica.buttonOn = GameController.BG_SOUND;
		somMusica.UpdateButton ();
	}

	void btClick(GameObject clickedBt){


		switch (clickedBt.name) {
			case "openclose":
				if (open == false) {
					animation.Play("SettingsIn");
					open = true;
				}
				else
				{
					animation.Play("SettingsOut");
					open = false;
				}
			break;
			case "somMusica":
				GameController.SwitchOnOffSound("Music");
			break;
			case "somFx":
				GameController.SwitchOnOffSound("FX");
			break;
			case "sair":
				Application.LoadLevel("Menu");
			break;
		
		}



	}
}
