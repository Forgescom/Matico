using UnityEngine;
using System.Collections;

public class LifesHandler : MonoBehaviour {

	public GUITexture avatar;
	public GUITexture lives;

	public Texture [] livesTextures;
	public Texture [] faces;

	int currentLives;

	// Use this for initialization
	void Start () {
		currentLives = GameController.CURRENT_LIVES;
		print (currentLives);
		lives.texture = livesTextures [currentLives];


	
		avatar.texture = faces [GameController.PLAYER_FACE];
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void UpdateLives(string outCome)
	{
		if(outCome == "Errado")
		{
			if (currentLives > 1) {
				currentLives --;
				lives.texture = livesTextures[currentLives -1];
			}
		}
		else 
		{
			if (currentLives <= 4) {
				currentLives ++;
				lives.texture = livesTextures[currentLives -1];
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
