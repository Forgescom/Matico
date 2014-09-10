using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {

	SpriteRenderer spriteRender;
	Color startColor;
	Color pressedColor = new Color(0.78F, 0.78F, 0.78F, 1F);
	bool buttonOvered = false;
	
	
	// Use this for initialization
	void Start () {
		spriteRender = transform.GetComponent<SpriteRenderer> ();
		startColor = spriteRender.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		spriteRender.color = pressedColor;
	}
	
	
	void OnMouseEnter(){
		buttonOvered = true;
	}
	
	void OnMouseExit(){
		buttonOvered = false;
		spriteRender.color = startColor;
	}
	
	void OnMouseUp (){
		spriteRender.color = startColor;
		
		if(buttonOvered == true)
			transform.parent.SendMessage ("ButtonPressed");
		
	}
	
	
	
}
