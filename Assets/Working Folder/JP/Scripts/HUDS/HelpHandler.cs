using UnityEngine;
using System.Collections;

public class HelpHandler : MonoBehaviour {
	bool open = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void btClick(GameObject clickedBt){
		
		
		switch (clickedBt.name) {
		case "openclose":
			if (open == false) {
				animation.Play("HelpIn");
				open = true;
			}
			else
			{
				animation.Play("HelpOut");
				open = false;
			}
			break;
		
			
		}
		
		
		
	}
}
