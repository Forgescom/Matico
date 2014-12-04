using UnityEngine;
using System.Collections;

public class GuiTextScroller : MonoBehaviour {

	public string [] arrayOfTexts;
	int textIndex = 0;
	int timeForText = 2;
	GUIText thisText;

	// Use this for initialization
	void Start () {
		textIndex = 0;
		thisText = transform.GetComponent<GUIText> ();
		//InvokeRepeating ("ChangeText", 0, timeForText);

	}

	void ChangeText()
	{
		if (textIndex < arrayOfTexts.Length) {
			thisText.text = arrayOfTexts [textIndex];
			textIndex ++;
		}
		else if (textIndex == arrayOfTexts.Length ) {
			CancelInvoke();
			transform.parent.SendMessage("MessageFinishHandler");
		}
	}

	void StartText()
	{
		textIndex = 0;
		InvokeRepeating ("ChangeText", 2, timeForText);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
