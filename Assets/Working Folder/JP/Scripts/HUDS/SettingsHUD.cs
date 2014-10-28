using UnityEngine;
using System.Collections;

public class SettingsHUD : MonoBehaviour {

	bool open = false;

	AudioClip backGroundMusic;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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
			case "somFx":
			GameController.SwitchOnOffSound("Music");
			break;
		
		}



	}
}
