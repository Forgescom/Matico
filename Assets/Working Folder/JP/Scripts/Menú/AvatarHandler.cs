using UnityEngine;
using System.Collections;

public class AvatarHandler : MonoBehaviour {

	public Main mainBrain;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void btClick(GameObject bt)
	{
		switch (bt.name) {		
		case "BtContinuar":				
			
			mainBrain.ChangeMenu(transform.gameObject, mainBrain.mainMenu);			
			break;			
		}
	}

}
