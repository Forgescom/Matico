using UnityEngine;
using System.Collections;

public class BtHandler : MonoBehaviour {

	float startSize;
	float sizeProportion = 1.2f;
	bool over =false;

	public GameObject menuController;

	// Use this for initialization
	void Start () {
		startSize = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		//ANIMATIONS
		if (over == true) {
			transform.localScale = new Vector3 (startSize*sizeProportion, startSize*sizeProportion, 1);
		}
		else{
			transform.localScale = new Vector3(startSize,startSize,1);
		}

	}

	void OnMouseEnter(){
		menuController.SendMessage ("btEnter", transform.gameObject);
		over =true;
	}
	
	void OnMouseExit(){
		menuController.SendMessage ("btExit", transform.gameObject);

		over =false;
	}
	
	void OnMouseUp (){
		menuController.SendMessage ("btClick", transform.gameObject);

	}
}
