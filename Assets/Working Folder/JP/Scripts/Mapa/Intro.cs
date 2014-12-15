using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	string maticoIntro = " Eu sou o Mático e vou acompanhar-te nesta aventura pelo Mundo Mágico. \n Vou estar presente sempre que precisares de ajuda.";
	public GUIStyle textStyle;

	public float xPos;
	public float yPos;

	public TextMesh playerName;

	void Start()
	{
		playerName.text= PlayerPrefs.GetString (GameController.PREFS_PLAYER_NAME);
		//StartCoroutine ("ExitAnim");
	}

	void OnGUI(){
		if(animation.isPlaying == false)
			GUI.Label (new Rect ((xPos *Screen.width), (yPos*Screen.height), 270, 200), maticoIntro,textStyle);
	}


	/*IEnumerator ExitAnim(){
		yield return new WaitForSeconds(7f);
		animation.Play ("IntroOut");

	}*/

	void ExitAnim()
	{
		animation.Play ("IntroOut");
	}

	void OnEnable()
	{	
		ClickToUnlock.unlockScreen += ExitAnim;		
	}
	
	
	
	void OnDisable()
	{
		ClickToUnlock.unlockScreen -= ExitAnim;
	}


}
