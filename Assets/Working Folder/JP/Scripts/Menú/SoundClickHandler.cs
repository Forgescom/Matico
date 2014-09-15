using UnityEngine;
using System.Collections;

public class SoundClickHandler : MonoBehaviour {

	public Sprite imageOff;
	public Sprite imageOn;

	bool on = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.touchCount >= 1 )
		{
			Ray cursorRay = Camera.main.ScreenPointToRay( iPhoneInput.GetTouch(0).position );
			RaycastHit hit;
			if( collider.Raycast( cursorRay, out hit, 1000.0f ) )
			{
				Debug.Log( "Hit detected on object " + hit.transform.name + " at point " + hit.point );
			}
		}

	}

	void OnMouseUp()
	{
		if (on == true) {
			on = false;
			transform.GetComponent<SpriteRenderer> ().sprite = imageOff;
		}
		else{
			on = true;
			transform.GetComponent<SpriteRenderer> ().sprite = imageOn;
		}

	}



}
