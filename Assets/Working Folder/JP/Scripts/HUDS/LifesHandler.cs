using UnityEngine;
using System.Collections;

public class LifesHandler : MonoBehaviour {

	public GUITexture avatar;
	public GUITexture lives;

	public GUITexture [] livesTextures;
	public Texture [] faces;

	int currentLives;

	// Use this for initialization
	void Start () {
		currentLives = GameController.CURRENT_LIVES;
		avatar.texture = faces [GameController.PLAYER_FACE];
		int livesToHide = livesTextures.Length - currentLives;

		for (int i = 0 ;i <=currentLives-1; i ++) {
		
			livesTextures[i].gameObject.SetActive(true);
		}

		//

	}

	public void AnimateLifeLoose()
	{
		Invoke ("AnimLifeLoose", 2);
	}

	void AnimLifeLoose()
	{
		livesTextures [currentLives-1].animation.Play ("LifeFadeOut");
		//lives.texture = livesTextures [currentLives-1];
	}

	// Update is called once per frame
	void Update () {

	
	}

	public void UpdateLives(string outCome)
	{
		if(outCome == "Errado")
		{
			if (currentLives > 1) {
				//currentLives --;
				//lives.texture = livesTextures[currentLives -1];
			}
		}
		else 
		{
			if (currentLives <= 4) {
				//currentLives ++;
				//lives.texture = livesTextures[currentLives -1];
			}
		}
	}


	void OnEnable()
	{
		ScratchController.GameEnded += UpdateLives;
		AcelerometerBrain.endGame += UpdateLives;
	}
	void OnDisable()
	{
		ScratchController.GameEnded -= UpdateLives;
		AcelerometerBrain.endGame -= UpdateLives;
	}
}
