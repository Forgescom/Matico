using UnityEngine;
using System.Collections;

public class MaticoHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.gameObject.SetActive (false);

		if (GameController.CURRENT_LIVES == 0) {
			Invoke("HandleNoLives",2f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void HandleNoLives ()
	{
		transform.gameObject.SetActive (true);
		transform.animation.Play ("MaticoHuD");

	}


}
