using UnityEngine;
using System.Collections;

public class SettingsHandler : MonoBehaviour {

	//SOUNDS
	public BtHandler btSoundFx;
	public BtHandler btSoundAmbiente;
	public BtHandler btSoundMatico;


	// Use this for initialization
	void Start () {


		btSoundFx.buttonOn = GameController.fxSoundOn;
		btSoundFx.updateButton ();

		btSoundAmbiente.buttonOn = GameController.musicSoundOn;
		btSoundAmbiente.updateButton ();


		bool soundMaticoOn;

		 

		int soundMaticoPrefs = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_SOUNDMATICO);
		soundMaticoOn = (soundMaticoPrefs == 1) ? true : false;
		btSoundMatico.buttonOn = soundMaticoOn;
		btSoundMatico.updateButton ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	void btClick(GameObject bt)
	{
		bool btValue = bt.GetComponent<BtHandler>().buttonOn;
		int boolInt = btValue ? 1 : 0;


		switch (bt.name) {
			case "BtSoundMatico":

			PlayerPrefs.SetInt(GameController.PREFS_PLAYER_SOUNDMATICO, boolInt);				
				
				break;
			case "BtSoundAmbiente":
			GameController.SwitchOnOffSound("Music");
			PlayerPrefs.SetInt(GameController.PREFS_PLAYER_SOUNDAMBIENTE, boolInt);	
				break;
			case "BtSoundFx":
			GameController.SwitchOnOffSound("FX");
			PlayerPrefs.SetInt(GameController.PREFS_PLAYER_SOUNDFX, boolInt);	
				break;
			
		}


	}

	
	
}
