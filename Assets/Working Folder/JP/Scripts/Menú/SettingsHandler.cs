using UnityEngine;
using System.Collections;

public class SettingsHandler : MonoBehaviour {

	//SOUNDS
	public BtHandler btSoundFx;
	public BtHandler btSoundAmbiente;
	public BtHandler btSoundMatico;


	// Use this for initialization
	void Start () {


		btSoundFx.buttonOn = GameController.FX_SOUND;
		btSoundFx.updateButton ();

		btSoundAmbiente.buttonOn = GameController.BG_SOUND;
		btSoundAmbiente.updateButton ();


	}
	
	// Update is called once per frame
	void Update () {

	}

	void btClick(GameObject bt)
	{
		switch (bt.name) {
			case "BtSoundAmbiente":
				GameController.SwitchOnOffSound("Music");
				
				break;
			case "BtSoundFx":
				GameController.SwitchOnOffSound("FX");
				
				break;			
		}
	}

	
	
}
