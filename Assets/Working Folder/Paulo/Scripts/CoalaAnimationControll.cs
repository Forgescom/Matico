using UnityEngine;
using System.Collections;

public class CoalaAnimationControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TurnOffAnimation(){
		transform.GetComponent<Animator> ().SetBool ("Eat", false);
	}
}
