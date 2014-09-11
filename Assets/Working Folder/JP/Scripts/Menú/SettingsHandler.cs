using UnityEngine;
using System.Collections;

public class SettingsHandler : MonoBehaviour {

	public GameObject backButton;
	GameObject background;
	GameObject title;
	// Use this for initialization
	void Start () {
		background = transform.FindChild ("bg").gameObject;
		title = transform.FindChild ("title").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Activate()
	{
		StartAnimations ();
	}

	void Deactivate()
	{
		BackAnimations ();
	}


	void StartAnimations()
	{

		background.GetComponent<Animation> ().Play ("BGSlideUp");
		title.GetComponent<Animation> ().Play ("MenuTabIn");
		backButton.GetComponent<Animation> ().Play ("BtBackIn");
	}

	void BackAnimations()
	{
		background.GetComponent<Animation> ().Play ("BGSlideDown");
		title.GetComponent<Animation> ().Play ("MenuTabOut");
		backButton.GetComponent<Animation> ().Play ("BtBackOut");
	}

}
