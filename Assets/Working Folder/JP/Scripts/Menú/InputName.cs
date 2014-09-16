using UnityEngine;
using System.Collections;

public class InputName : MonoBehaviour {

	// Use this for initialization
	TouchScreenKeyboard keyboard;
	TextMesh textInput;
	void Start () {
		textInput = transform.GetComponent<TextMesh> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(TouchScreenKeyboard.visible ==true)
			textInput.text = keyboard.text;


		//WHEN DONE SAVE ON PREFAB THE NEW NAME
		if(keyboard!= null)
			if (keyboard.done)
				Main.SavePlayerPref(textInput.text);
	}	

	void OnMouseDown(){

		keyboard = TouchScreenKeyboard.Open (Main.PLAYER_NAME);

	}
}
