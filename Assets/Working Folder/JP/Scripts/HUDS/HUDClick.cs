using UnityEngine;
using System.Collections;

public class HUDClick : MonoBehaviour {
	
	
	//CLICK TYPE BUTTON
	public GameObject menuController;
	public Texture overState;
	public Texture normalState;
	GUITexture spriteActive;
	
	//IN CASE OF A TOGGLE BUTTON
	public bool toggleButton;
	public bool buttonOn;
	public Texture toggleOff;
	public Texture toggleOn;
	
	//SCRIPT ANIMATION
	float startSize;
	float sizeProportion = 1.2f;
	bool over =false;
	
	
	
	// Use this for initialization
	void Start () {
		startSize = transform.localScale.x;
		spriteActive = GetComponent<GUITexture> ();
	}
	
	// Update is called once per frame
	void Update () {
		//ANIMATIONS

		
		
		
	}
	
	void OnMouseEnter(){
		if (overState != null) {
			spriteActive.texture = overState;
			
		}
		over =true;
		
	}
	
	void OnMouseExit(){
		if (normalState != null) {
			spriteActive.texture = normalState;
		}
		over =false;
		
	}
	
	void OnMouseUp (){
		
		if (over == true) {
			menuController.SendMessage ("btClick", transform.gameObject);
			ToggleButton ();
		}
		
		
	}
	
	void ToggleButton(){
		
		if (toggleButton) {
			if(buttonOn)
			{
				spriteActive.texture = toggleOff;
				buttonOn =false;
			}
			else
			{
				spriteActive.texture = toggleOn;
				buttonOn =true;
			}
		}
		
	}
	public void updateButton()
	{
		spriteActive = GetComponent<GUITexture> ();
		if(buttonOn)
		{
			spriteActive.texture = toggleOn;
		}
		else
		{
			spriteActive.texture = toggleOff;
		}
	}
}
