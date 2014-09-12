using UnityEngine;
using System.Collections;

public class SettingsHandler : MonoBehaviour {
	public Main mainBrain;


	//AVATAR
	string nomeActual;
	int indiceAvatar;
	public TextMesh nameMesh;
	public AvatarPiker avatarSettings;

	//SOUNDS
	bool fxSoundOn = true;
	bool musicSounOn = true;
	bool maticoSoundOn = true;
	public BtHandler btSoundFx;
	public BtHandler btSoundAmbiente;
	public BtHandler btSoundMatico;


	// Use this for initialization
	void Start () {
		nomeActual = PlayerPrefs.GetString (Main.PREFS_PLAYER_NAME);
		indiceAvatar = PlayerPrefs.GetInt (mainBrain.PREFS_PLAYER_AVATAR);
		avatarSettings.SetCurrentFaceIndex (indiceAvatar);
		nameMesh.text = nomeActual;

		bool soundFxOn;
		bool soundMaticoOn;
		bool soundAmbienteOn;

		int soundMaticoPrefs = PlayerPrefs.GetInt (mainBrain.PREFS_PLAYER_SOUNDMATICO);
		soundMaticoOn = (soundMaticoPrefs == 0) ? false : true;
		btSoundMatico.buttonOn = soundMaticoOn;
		btSoundMatico.updateButton ();

		int soundFxPrefs = PlayerPrefs.GetInt (mainBrain.PREFS_PLAYER_SOUNDFX);
		soundFxOn = (soundFxPrefs == 0) ? false : true;
		btSoundFx.buttonOn = soundFxOn;
		btSoundFx.updateButton ();

		int soundAmbientePrefs = PlayerPrefs.GetInt (mainBrain.PREFS_PLAYER_SOUNDAMBIENTE);
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
				PlayerPrefs.SetInt(mainBrain.PREFS_PLAYER_SOUNDMATICO, boolInt);				
				break;
			case "BtSoundAmbiente":
				PlayerPrefs.SetInt(mainBrain.PREFS_PLAYER_SOUNDAMBIENTE, boolInt);	
				break;
			case "BtSoundFx":
				PlayerPrefs.SetInt(mainBrain.PREFS_PLAYER_SOUNDFX, boolInt);	
				break;
			
		}
	}

	
	
}
