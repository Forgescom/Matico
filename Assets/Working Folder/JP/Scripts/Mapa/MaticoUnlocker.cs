using UnityEngine;
using System.Collections;

public class MaticoUnlocker : MonoBehaviour {


	public delegate void ShowNewObject();
	public static event ShowNewObject showNewObjectEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowBoardObject(){
		if (showNewObjectEvent != null) {
			showNewObjectEvent();
		}
	}
}
