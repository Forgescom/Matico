using UnityEngine;
using System.Collections;

public class BtHandler : MonoBehaviour {


	//CLICK TYPE BUTTON
	public GameObject menuController;
	public Sprite overState;
	public Sprite normalState;
	SpriteRenderer spriteActive;

	//IN CASE OF A TOGGLE BUTTON
	public bool toggleButton;
	public bool buttonOn;
	public Sprite toggleOff;
	public Sprite toggleOn;

	//SCRIPT ANIMATION
	float startSize;
	float sizeProportion = 1.2f;
	bool over =false;



	// Use this for initialization
	void Start () {
		startSize = transform.localScale.x;
		spriteActive = GetComponent<SpriteRenderer> ();
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
		if (overState == null) {		
			over =true;
		}
		else{
			spriteActive.sprite = overState;
		}

	}
	
	void OnMouseExit(){
		if (normalState == null) {
			over =false;
		}
		else{
			spriteActive.sprite = normalState;
		}

	}
	
	void OnMouseUp (){
		ToggleButton ();

		menuController.SendMessage ("btClick", transform.gameObject);
	}

	void ToggleButton(){

		if (toggleButton) {
			if(buttonOn)
			{
				spriteActive.sprite = toggleOff;
				buttonOn =false;
			}
			else
			{
				spriteActive.sprite = toggleOn;
				buttonOn =true;
			}
		}

	}
	public void updateButton()
	{
		spriteActive = GetComponent<SpriteRenderer> ();
		if(buttonOn)
		{
			spriteActive.sprite = toggleOn;
		}
		else
		{
			spriteActive.sprite = toggleOff;
		}
	}
}
