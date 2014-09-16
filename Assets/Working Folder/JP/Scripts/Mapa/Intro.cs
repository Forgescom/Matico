using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	string maticoIntro = " Eu sou o Matico e vou acompanhar-te nesta aventura pelo Mundo Magico. \n Vou estar presente sempre que precisares de ajuda.";
	public GUIStyle textStyle;

	public float xPos;
	public float yPos;

	void Start()
	{

	}

	void OnGUI(){
		if(animation.isPlaying == false)
			GUI.Label (new Rect ((xPos *Screen.width), (yPos*Screen.height), 270, 200), maticoIntro,textStyle);
	}


	void ExitAnim(){
	//	animation.Play ("IntroOut");

	}





















	/*
	 *
	string maticoIntro = " Eu sou o Matico e vou acompanhar-te nesta aventura pelo Mundo Magico. \n Vou estar presente sempre que precisares de ajuda.";
	public GUIStyle textStyle;
	public GameObject maticoSprite;
	public TextMesh name;




	public BoardMain mainBoardBrain;

	// Use this for initialization
	void Start () {
		name.text = PlayerPrefs.GetString (Main.PREFS_PLAYER_NAME);

		StartCoroutine ("ShowIntro");
	}
	


	IEnumerator ShowIntro()
	{
		yield return new WaitForSeconds (4f);
		animation.Play ("IntroOut");
		mainBoardBrain.canStartCamera = true;
	}



	void MaticoOut(){
		animation.Play ("IntroOut");
		mainBoardBrain.canStartCamera = true;
	}
*/

}
