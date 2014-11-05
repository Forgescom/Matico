using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CameraMove : MonoBehaviour
{
	private float dragSpeed = 0.01f;
	private float timeDragStarted;
	private Vector3 previousPosition = Vector3.zero;
	
	public SlingShot Slingshot;
	public GameObject question;

	public float orthoZoomSpeed = 0.3f;

	Vector3 hit_position = Vector3.zero;
	Vector3 currentPosition = Vector3.zero;
	Vector3 startPosition;
	
	float MINSCALE = 5.4F; 
	float MAXSCALE = 7.4F; 
	float MAX_X_ZOOMIN = 18.5f;
	float MAX_X_ZOOMOUT = 15;

	float MAX_Y_ZOOMIN = 4.12F;
	float MAX_Y_ZOOMOUT = 6.16F;
	// Update is called once per frame
	void Update()
	{
		float currentSize = transform.camera.orthographicSize;




		if(Input.touchCount == 1 ) {
			if(Input.GetTouch(0).phase == TouchPhase.Began){
				SaveFirstPosition();				
			}
			
			if(Input.GetTouch(0).phase == TouchPhase.Moved){
				MoveCamera();
			}
			
		}
		else if (Input.touchCount == 2)
		{
			ZoomInOutCamera();
		}
	}	

	void SaveFirstPosition(){
		hit_position = Input.GetTouch(0).position;	
	}

	void MoveCamera ()
	{
		if ((Slingshot.slingshotState == SlingshotState.Idle) && (GameManager.CurrentGameState == GameState.Playing)) {
			currentPosition = Input.GetTouch(0).position;
			// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
			// anyways.  
			Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hit_position);		
			// Invert direction to that terrain appears to move with the mouse.
			direction = direction * -1;

			startPosition = transform.position;		
			Vector3 newPosition = startPosition + direction;
			newPosition.z = startPosition.z;		
			transform.position = Vector3.Lerp(startPosition,newPosition,0.5f);		

		}

		ClampCameraMovement ();

	}

	void ZoomInOutCamera(Touch[] inputValues = null ){
		//VERSION 1		

		// Store both touches.
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);
		
		// Find the position in the previous frame of each touch.
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
		
		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
		
		// Find the difference in the distances between each frame.
		float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
		
		// If the camera is orthographic...
		if (camera.isOrthoGraphic)
		{
			// ... change the orthographic size based on the change in distance between the touches.
			camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
			
			// Make sure the orthographic size never drops below zero.
			camera.orthographicSize = Mathf.Max(camera.orthographicSize, 5.4f);
			camera.orthographicSize = Mathf.Min(camera.orthographicSize, 7.4f);
		}	
		ClampCameraMovement ();
	}

	void ClampCameraMovement()
	{
		float currentSize = transform.camera.orthographicSize;
		float factor = (MAX_X_ZOOMIN - MAX_X_ZOOMOUT)/(MAXSCALE-MINSCALE);
		float factorY = (MAX_Y_ZOOMOUT - MAX_Y_ZOOMIN)/(MAXSCALE-MINSCALE);

		float xMin = 0 +(currentSize - MINSCALE)*factor;
		float xMax = MAX_X_ZOOMIN -(currentSize-MINSCALE)*factor;

		float yMin = 4.15f +(currentSize-MINSCALE)*factorY;
		float yMax = MAX_Y_ZOOMOUT -(currentSize-MINSCALE)*factorY;

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, xMin, xMax),
		                                  Mathf.Clamp (transform.position.y, 0, 0),
		                                  transform.position.z);
	
		question.transform.position = new Vector3 (transform.position.x,
		                                           Mathf.Clamp (transform.position.y, yMin, yMax),
		                                           10);
	}

}