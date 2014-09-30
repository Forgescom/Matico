using UnityEngine;
using System.Collections;

public class SettingsHandler : MonoBehaviour {

	//SOUNDS
	/*bool fxSoundOn = true;
	bool musicSounOn = true;
	bool maticoSoundOn = true;*/
	public BtHandler btSoundFx;
	public BtHandler btSoundAmbiente;
	public BtHandler btSoundMatico;


	// Use this for initialization
	void Start () {
	

		bool soundFxOn;
		bool soundMaticoOn;
		bool soundAmbienteOn;

		int soundMaticoPrefs = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_SOUNDMATICO);
		soundMaticoOn = (soundMaticoPrefs == 0) ? false : true;
		btSoundMatico.buttonOn = soundMaticoOn;
		btSoundMatico.updateButton ();

		int soundFxPrefs = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_SOUNDFX);
		soundFxOn = (soundFxPrefs == 0) ? false : true;
		btSoundFx.buttonOn = soundFxOn;
		btSoundFx.updateButton ();

		int soundAmbientePrefs = PlayerPrefs.GetInt (GameController.PREFS_PLAYER_SOUNDAMBIENTE);
		soundAmbienteOn = (soundAmbientePrefs == 0) ? false : true;
		btSoundAmbiente.buttonOn = soundAmbienteOn;
		btSoundAmbiente.updateButton ();

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
			PlayerPrefs.SetInt(GameController.PREFS_PLAYER_SOUNDAMBIENTE, boolInt);	
				break;
			case "BtSoundFx":
			PlayerPrefs.SetInt(GameController.PREFS_PLAYER_SOUNDFX, boolInt);	
				break;
			
		}
	}

	
	
}
