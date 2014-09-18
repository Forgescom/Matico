using UnityEngine;
using System.Collections;

public class SettingsHUD : MonoBehaviour {

	bool open = false;
	Vector3 startPosition;
	public Vector3 endPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){

		if (open == false) {
			animation.Play("SettingsIn");
			open = true;
		}
		else
		{
			animation.Play("SettingsOut");
			open = false;
		}


	}
}
