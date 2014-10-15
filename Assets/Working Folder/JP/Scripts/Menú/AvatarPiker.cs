using UnityEngine;
using System.Collections;

public class AvatarPiker : MonoBehaviour {


	//Face
	public SpriteRenderer faceHolder;
	public Sprite [] faces;
	public int currentFaceIndex = 0;

	//NAME
	TouchScreenKeyboard keyboard;
	public TextMesh avatarName;



	// Use this for initialization
	void Start () {

		avatarName.text = GameController.PLAYER_NAME;
		currentFaceIndex = GameController.PLAYER_FACE;


		SetValues (currentFaceIndex,avatarName.text);

	}
	
	// Update is called once per frame
	void Update () {

		if(TouchScreenKeyboard.visible ==true)
			avatarName.text = keyboard.text;

		if(avatarName.text.Length >= 10)
			keyboard.active = false;

		if(keyboard!=null)
			if(keyboard.done)
				GameController.SavePlayerPref(avatarName.text,currentFaceIndex);

	}

	public void SetValues(int avatarIndex, string avatarNameIn)
	{
		faceHolder.sprite = faces [avatarIndex];
		currentFaceIndex = avatarIndex;
		avatarName.text = avatarNameIn;
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
			case "NameHolder":
				InsertName();
			break;		

		}
	}

	void InsertName(){
		avatarName.text = "";
		keyboard = TouchScreenKeyboard.Open (GameController.PLAYER_NAME);		

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
		GameController.SavePlayerPref(avatarName.text,currentFaceIndex);
	}
}
