using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour {


	public Main mainBrain;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void btClick(GameObject bt)
	{
		switch (bt.name) {
			case "BtJogar":
				Application.LoadLevel (1);	
				break;
			case "BtPrograma":
				mainBrain.ChangeMenu(transform.gameObject, mainBrain.programaMenu);
			break;
			case "BtDefinicoes":
				mainBrain.ChangeMenu(transform.gameObject, mainBrain.settingsMenu);
			break;				
			case "BtBack":
				mainBrain.BackMenu();
				break;
		}
	}
}
