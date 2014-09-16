using UnityEngine;
using System.Collections;

public class AvatarHandler : MonoBehaviour {

	public Main mainBrain;


	void btClick(GameObject bt)
	{
		switch (bt.name) {		
		case "BtContinuar":				
			
			mainBrain.ChangeMenu(transform.gameObject, mainBrain.mainMenu);			
			break;			
		}
	}



	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void btEnter(GameObject bt)
	{
		switch (bt.name) {		
		case "BtContinuar":
					
			break;			
		}
	}
	
	void btExit(GameObject bt)
	{
		switch (bt.name) {		
		case "BtContinuar":
				
			break;			
		}
	}
	

}
