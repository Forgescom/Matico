using UnityEngine;
using System.Collections;

public class AvatarPiker : MonoBehaviour {
	public Main mainBrain;

	public SpriteRenderer faceHolder;
	public Sprite [] faces;
	public int currentFaceIndex = 0;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCurrentFaceIndex(int index)
	{
		faceHolder.sprite = faces [index];
		currentFaceIndex = index;
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

		}
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
		PlayerPrefs.SetInt (Main.PREFS_PLAYER_AVATAR, currentFaceIndex);	

		
	}
}
