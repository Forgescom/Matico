using UnityEngine;
using System.Collections;

public class GuiHandler : MonoBehaviour {
	string avatarName = "Nome";
	public GUIStyle estiloTextField;


	public float positionX;
	public float positionY;

	TextMesh textInWorld;

	bool windowShouldBeDrawnCondition = true;

	void Start()
	{
		textInWorld = transform.GetComponent<TextMesh> ();
	}
	void Update()
	{
		if (transform.gameObject.activeSelf == false) {
			windowShouldBeDrawnCondition = false;
		}

		// open dialog upon Enter
		if (Input.GetKeyUp(KeyCode.Return))
		{		
			windowShouldBeDrawnCondition = !windowShouldBeDrawnCondition;
		}
	}
	
	void OnGUI()
	{
		if (windowShouldBeDrawnCondition)
		{
			// draw dialog
			GUI.skin.settings.cursorColor = Color.black;
			GUILayout.Window(transform.GetInstanceID(), 
			                 new Rect(positionX * Screen.width, positionY * Screen.height, 300, 0), 
			                 DrawMyWindow, "");

		}
			
	}
	
	void DrawMyWindow(int windowID)
	{
		// check for keydown event
		if (Event.current.type == EventType.KeyDown)
		{
			// no matter where, but if Escape was pushed, close the dialog
			if (Event.current.keyCode == KeyCode.Escape)
			{
				windowShouldBeDrawnCondition = false;
				return; // no point in continuing if closed
			}
			
			// we look if the event occured while the focus was in our input element
			if (GUI.GetNameOfFocusedControl() == "myTextField" && Event.current.keyCode == KeyCode.Return)
			{
				SubmitInputValue();
			}
		}
		
		GUI.SetNextControlName("myTextField");
		avatarName = GUILayout.TextField(avatarName);
		textInWorld.text = avatarName;
		// in case nothing else if focused, focus our input
		if (GUI.GetNameOfFocusedControl() == string.Empty)
		{
			GUI.FocusControl("myTextField");
		}

	}
	
	private void SubmitInputValue()
	{      
		windowShouldBeDrawnCondition = !windowShouldBeDrawnCondition;
		// do not proceed if empty
		if (avatarName.Length > 0)
		{
			// do something
			Debug.Log(avatarName);
			
			//avatarName = ""; // usually, upon sending, the field should be reset
		}
	}
}







/*

	void OnGUI()
	{
		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return ){
			print ("SAVE NAME");
			GUIEnabled = false;
		}

		if (GUI.GetNameOfFocusedControl() == "myTextField" && Event.current.keyCode == KeyCode.Return)
		{
			Input.eatKeyPressOnTextFieldFocus = false;
		}


		if (GUIEnabled = true) {
			bool needsRefocus = false;

			GUI.SetNextControlName ("myTextField");
			avatarName = GUI.TextField (new Rect (positionX * Screen.width, positionY * Screen.height, 180, 40), avatarName, 13, estiloTextField);

			TextEditor te = (TextEditor)GUIUtility.GetStateObject (typeof(TextEditor), GUIUtility.keyboardControl);

			if (needsRefocus) {
					needsRefocus = false;

					GUI.FocusControl ("myTextField");

					//needs to be redefined after setting the FocusControl        
					te = (TextEditor)GUIUtility.GetStateObject (typeof(TextEditor), GUIUtility.keyboardControl);  

			}
			textInWorld.text = avatarName;
		}
	}
}*/
