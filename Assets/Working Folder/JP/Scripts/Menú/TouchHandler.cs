using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour {
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
	

		CheckFingerTouch ();
		
	}
	
	
	void CheckFingerTouch()
	{
		
		if (Input.touchCount > 0) {
			
			Vector3 wp  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos  = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			
			if(hit && hit.name == transform.name){
				if(Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
				{

					transform.localScale = new Vector3 (startSize*sizeProportion, startSize*sizeProportion, 1);
				}
				else if(Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					menuController = GameObject.Find(menuController.name);
					menuController.SendMessage ("btClick", transform.gameObject);
					transform.localScale = new Vector3(startSize,startSize,1);
				}			
			}
		}
	}
	

}
