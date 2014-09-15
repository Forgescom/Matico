using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	string maticoIntro = " Eu sou o Matico e vou acompanhar-te nesta aventura pelo Mundo Magico. \n Vou estar presente sempre que precisares de ajuda.";
	public GUIStyle textStyle;
	public GameObject maticoSprite;
	public TextMesh name;


	public BoardMain mainBoardBrain;

	// Use this for initialization
	void Start () {
		name.text = PlayerPrefs.GetString (Main.PREFS_PLAYER_NAME);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Q)) {
			MaticoOut();
		}
	}

	void OnGUI(){
		if(animation.isPlaying == false)
			GUI.Label (new Rect ((0.2f *Screen.width), (0.3f*Screen.height), 350, 200), maticoIntro,textStyle);
	}

	void MaticoOut(){
		animation.Play ("IntroOut");
		mainBoardBrain.canStartCamera = true;
	}


}
