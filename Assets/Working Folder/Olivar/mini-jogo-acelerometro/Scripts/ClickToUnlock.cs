﻿using UnityEngine;
using System.Collections;

public class ClickToUnlock : MonoBehaviour {

	public delegate void UnlockScreenDelegate();
	public static event UnlockScreenDelegate unlockScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (CheckFingerTouch () == true || Input.GetMouseButtonDown(0)== true) {
		
			if(unlockScreen !=null)
			{

				unlockScreen();
				gameObject.SetActive(false);

			}

		}
	}

   	bool CheckFingerTouch()
	{

		if (Input.touchCount > 0) {
			
			Vector3 wp  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos  = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			
			if(hit && hit.name == transform.name){		

				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{		
			return false;
		}
	}
}
