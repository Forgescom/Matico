using UnityEngine;
using System.Collections;

public class LifesHandler : MonoBehaviour {

	public GUITexture avatar;
	public GUITexture lives;

	public Texture [] livesTextures;

	int currentLives = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnMouseDown()
	{
		RemoveLives ();
	}

	void OnMouseEnter()
	{
		AddLives ();
	}

	void AddLives(){
		if (currentLives <= 4) {
			currentLives ++;
			lives.texture = livesTextures[currentLives -1];
		}
	}

	void RemoveLives(){
		if (currentLives > 1) {
			currentLives --;
			lives.texture = livesTextures[currentLives -1];
		}
	}
}
