﻿using UnityEngine;
using System.Collections;

public class BackgroundTouch : MonoBehaviour {

	//DRAG 
	Vector3 hit_position = Vector3.zero;
	Vector3 currentPosition = Vector3.zero;
	
	//ZOOM 

	
	//MAIN CAMERA
	public Camera mainCamera;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.touchCount == 1 ) {
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				SaveFirstPosition();				
			}
			
			if(Input.GetTouch(0).phase == TouchPhase.Moved){
				TouchDrag();
			}
			
		}

	}
	
	
	void SaveFirstPosition()
	{
		hit_position = Input.GetTouch(0).position;	
	}
	
	void TouchDrag(){
		currentPosition = Input.GetTouch(0).position;
		// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
		// anyways.  
		Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hit_position);		
		// Invert direction to that terrain appears to move with the mouse.
		direction = direction * -1;
		mainCamera.SendMessage ("MoveCamera", direction);
	}
	
	
	
}
