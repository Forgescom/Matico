using UnityEngine;
using System.Collections;

public class LoginMenu : MonoBehaviour {

	string loginURL = "http://forgescom.pt/atico/login.php";

	string password = "";

	TouchScreenKeyboard keyboard;
	public TextMesh tilteText;
	public TextMesh codeText;

	void Update(){
		if (keyboard != null) {
			if (keyboard.active == true) {
					codeText.text = keyboard.text;
			}
			if(keyboard.done)
				keyboard.active = false;
		}
						
	}



	void btClick(GameObject clickedBt)
	{
		switch (clickedBt.name) {
			case "text":
				keyboard = TouchScreenKeyboard.Open("Insira o codigo");
				break;
			case "continuar":
				StartCoroutine(HandleLogin(codeText.text));
				break;

		}

	}



	IEnumerator HandleLogin(string password) {
				
				string loginURL = this.loginURL + "?password=" + password;
				WWW loginReader = new WWW (loginURL);
				yield return loginReader;

				if (loginReader.error != null) {
					tilteText.text = "Verifique o codigo";
				} else {
						if (loginReader.text == "success") {
						tilteText.text = "esta logado :D";
						Invoke("PlayAnimationOut",2);
						} else {
						tilteText.text = "codigo errado...";
						}
				}

		}
	void PlayAnimationOut()
	{
		transform.animation.Play ("ActivationOut");
	}
}
