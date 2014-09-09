using UnityEngine;
using System.Collections;

public class BtHandler : MonoBehaviour {
	public Sprite btJogarNormal;
	public Sprite btJogarOver;
	public Sprite btProgramaNormal;
	public Sprite btProgramaOver;
	public Sprite btDefNormal;

	float startSize;

	float sizeProportion = 1.2f;

	SpriteRenderer spriteComponent;

	bool over =false;

	// Use this for initialization
	void Start () {
		spriteComponent = transform.GetComponent<SpriteRenderer> ();
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
		switch (transform.name) {
			case "BtJogar":
				spriteComponent.sprite = btJogarOver;				
			break;
			case "BtPrograma":
				spriteComponent.sprite = btProgramaOver;
			break;

		}

		over =true;
	}
	
	void OnMouseExit(){
		switch (transform.name) {
			case "BtJogar":
				spriteComponent.sprite = btJogarNormal;
				break;
			case "BtPrograma":
				spriteComponent.sprite = btProgramaNormal;
				break;				
		}
		over =false;
	}
	
	void OnMouseUp (){
		switch (transform.name) {
			case "BtJogar":
				Application.LoadLevel (1);	
				break;
			case "BtPrograma":
				spriteComponent.sprite = btProgramaNormal;
				break;				
		}

	}
}
