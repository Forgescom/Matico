using UnityEngine;
using System.Collections;

public class SuccessScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<AudioSource> ().enabled = GameController.BG_SOUND;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
