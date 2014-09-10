using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour {
	public Sprite btJogarNormal;
	public Sprite btJogarOver;
	public Sprite btProgramaNormal;
	public Sprite btProgramaOver;
	public Sprite btDefNormal;
	public Sprite btDefOver;

	public GameObject menuSettings;
	public GameObject menuPrograma;

	string activePanel = "Main";

	// Use this for initialization
	void Start () {
		menuPrograma.SetActive (false);
		menuSettings.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void btEnter(GameObject bt)
	{
		switch (bt.name) {
			case "BtJogar":
				bt.GetComponent<SpriteRenderer> ().sprite = btJogarOver;				
				break;
			case "BtPrograma":
				bt.GetComponent<SpriteRenderer> ().sprite = btProgramaOver;
				break;
			case "BtDefinicoes":
				bt.GetComponent<SpriteRenderer> ().sprite = btDefOver;
				break;	
			
		}
	}

	void btExit(GameObject bt)
	{
		switch (bt.name) {
			case "BtJogar":
				bt.GetComponent<SpriteRenderer> ().sprite = btJogarNormal;
				break;
			case "BtPrograma":
				bt.GetComponent<SpriteRenderer> ().sprite = btProgramaNormal;
				break;	
			case "BtDefinicoes":
				bt.GetComponent<SpriteRenderer> ().sprite = btDefNormal;
				break;	
		}
	}

	void btClick(GameObject bt)
	{
		switch (bt.name) {
			case "BtJogar":
				Application.LoadLevel (1);	
				break;
			case "BtPrograma":
				activePanel = "Programa";
				menuPrograma.SetActive (true);
				transform.GetComponent<Animation>().Play("MainMenuOut");
				menuPrograma.SendMessage("Activate");
				break;
			case "BtDefinicoes":
				activePanel = "Settings";
				menuSettings.SetActive (true);
				transform.GetComponent<Animation>().Play("MainMenuOut");
				menuSettings.SendMessage("Activate");
				break;				
			case "BtBack":
				GoBackMenu();
				break;
		}
	}

	void GoBackMenu()
	{
		if (activePanel == "Settings") {
			menuSettings.SendMessage("Deactivate");
		}
		else if(activePanel == "Programa")
		{
			menuPrograma.SendMessage("Deactivate");
		}


		transform.GetComponent<Animation>().Play("MainMenuIn");
	}
}
