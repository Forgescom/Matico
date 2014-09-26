using UnityEngine;
using System.Collections;

public class ScratchController : MonoBehaviour {

	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject scratchCard;

	int currentScreen = 0;

	// Use this for initialization
	void Start () {
		explanationScreen.SetActive (false);
		introScreen.SetActive (false);
		scratchCard.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel(1);
		}
	}

	void ChangeScreen()
	{
		if (currentScreen == 0) {
			explanationScreen.SetActive(true);
			explanationScreen.animation.Play("Explanation");
			currentScreen ++;
		}
		else if(currentScreen == 1)
		{
			scratchCard.SetActive(true);
			currentScreen ++;
		}
	}


	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
	}

	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
	}

}
