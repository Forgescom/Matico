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
		if (keyboard.done)
						PlayerPrefs.SetString (Main.PREFS_PLAYER_NAME, textInput.text);
		/*if(TouchScreenKeyboard.visible ==true)
			if(keyboard.text.Length < 10)*/


	}

	void OnMouseDown(){
		//TouchScreenKeyboard.hideInput = true;

		keyboard = TouchScreenKeyboard.Open ("Nome");

		//textInput.text = keyboard.text;


	}
}
