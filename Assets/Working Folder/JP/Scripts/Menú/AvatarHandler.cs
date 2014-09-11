using UnityEngine;
using System.Collections;

public class AvatarHandler : MonoBehaviour {

	SpriteRenderer faceHolder;
	public Sprite [] faces;
	int currentFaceIndex = 0;


	public GameObject mainMenu;
	public GameObject settingsMenu;

	// Use this for initialization
	void Start () {
		faceHolder = transform.FindChild ("caraHolder").GetComponent<SpriteRenderer> ();
		mainMenu.SetActive(false);
		settingsMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void btEnter(GameObject bt)
	{
		switch (bt.name) {
		case "BtJogar":

			break;
		case "BtPrograma":

			break;
		case "BtDefinicoes":

			break;	
			
		}
	}
	
	void btExit(GameObject bt)
	{
		switch (bt.name) {
		case "BtJogar":
			//bt.GetComponent<SpriteRenderer> ().sprite = btJogarNormal;
			break;
		case "BtPrograma":

			break;	
		case "BtDefinicoes":

			break;	
		}
	}
	
	void btClick(GameObject bt)
	{
		switch (bt.name) {
		case "BtSeguinte":
			ChangeFace("Next");
			break;
		case "BtAnterior":
			ChangeFace("Previous");
			break;
		case "BtContinuar":
			transform.animation.Play();
			mainMenu.SetActive(true);
			mainMenu.animation.Play("MainMenuIn");
			break;				
		case "BtBack":

			break;
		}
	}

	void ChangeFace(string direction)
	{
		switch (direction) {
			case "Next":
				currentFaceIndex ++;
			break;
			case "Previous":
				currentFaceIndex --;
			break;
		}

		if (currentFaceIndex >= faces.Length) {
			currentFaceIndex = 0;
		}
		else if(currentFaceIndex< 0){
			currentFaceIndex = faces.Length -1;
		}
		faceHolder.sprite = faces [currentFaceIndex];

	}

	void Deactivate()
	{
		transform.gameObject.SetActive (false);
	}
}
