using UnityEngine;
using System.Collections;

public class MaticoHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//transform.gameObject.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HandleNoLives ()
	{

		transform.gameObject.SetActive (true);
		transform.animation ["MaticoHuD"].normalizedTime = 0f;
		transform.animation ["MaticoHuD"].speed = 1.0f;
		transform.animation.Play ("MaticoHuD");



	}

	void StartDisplayText()
	{
		transform.FindChild ("GUI Text").SendMessage ("StartText");
	}

	void MessageFinishHandler()
	{
		transform.animation ["MaticoHuD"].normalizedTime = 1.0f;
		transform.animation ["MaticoHuD"].speed = -1.0f;
		transform.animation.Play ("MaticoHuD");
	}

	void OnEnable()
	{
		GameController.lockHouse += HandleNoLives;
	}
	void OnDisable()
	{
		GameController.lockHouse -= HandleNoLives;
	}

}
