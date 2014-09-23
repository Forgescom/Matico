using UnityEngine;
using System.Collections;

public class CameraMovesHandler : MonoBehaviour {

	//MOVEMENT
	public float speedFactor = 0.05f;
	Vector3 startPosition;
	
	//CLAMP
	public float [] boundaries = new float[2];
	
	//ZOOM
	float MAXSCALE = 5F; 
	/*float speed = 0.05f; 
	float MINSCALE = 2.0F; 

	float minPinchSpeed = 5.0F; 
	float varianceInDistances = 5.0F; 
	float touchDelta = 0.0F; 
	Vector2 prevDist = new Vector2(0,0); 
	Vector2 curDist = new Vector2(0,0); 
	float speedTouch0 = 0.0F; 
	float speedTouch1 = 0.0F;*/

	//BOARD START ANIMATION
	Vector3 startPos;
	Vector3 endPos;
	float transitionDuration = 1.5f;

	public float orthoZoomSpeed = 0.005f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 2)
		{
			ZoomInOutCamera();
		}
	}




	public void  startAnimBoard(Vector3 finPos)
	{
		startPos = new Vector3(0,0,0);
		endPos = finPos;
		endPos.z = -10;
		StartCoroutine("moveCameraOnBoardStart");
	}

	IEnumerator moveCameraOnBoardStart()
	{
		
		float t = 0.0f;
		
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			transform.camera.orthographicSize = Mathf.Lerp(5,2,t);
			transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return 0;
		}	
	}


	void MoveCamera(Vector3 direction){
		startPosition = transform.position;		
		Vector3 newPosition = startPosition + direction;
		newPosition.z = startPosition.z;		
		transform.position = Vector3.Lerp(startPosition,newPosition,speedFactor);		
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
			camera.orthographicSize = Mathf.Max(camera.orthographicSize, 2f);
			camera.orthographicSize = Mathf.Min(camera.orthographicSize, 5f);
		}

		ClampCameraMovement ();

		//VERSION2
	/*	curDist = inputValues[0].position - inputValues[1].position; //current distance between finger touches
		prevDist = ((inputValues[0].position - inputValues[0].deltaPosition) - (inputValues[1].position - inputValues[1].deltaPosition)); //difference in previous locations using delta positions
		touchDelta = curDist.magnitude - prevDist.magnitude;
		speedTouch0 = inputValues[0].deltaPosition.magnitude / inputValues[0].deltaTime;
		speedTouch1 = inputValues[1].deltaPosition.magnitude / inputValues[1].deltaTime;
		
		if ((touchDelta + varianceInDistances <= 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
		{
			transform.camera.orthographicSize = Mathf.Clamp(transform.camera.orthographicSize + (1 * speed),MINSCALE,MAXSCALE);
		}
		
		if ((touchDelta +varianceInDistances > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
		{			
			transform.camera.orthographicSize = Mathf.Clamp(transform.camera.orthographicSize - (1 * speed),MINSCALE,MAXSCALE);
		}
		
		ClampCameraMovement ();*/
	}
	
	//CLAMP DEPENDING ON CAMERA SIZE
	void ClampCameraMovement()
	{
		float currentSize = transform.camera.orthographicSize;
		float factor = boundaries [1] / (-1*boundaries [0]);
		float margin = MAXSCALE - currentSize;

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -margin *factor, margin*factor),
		                                  Mathf.Clamp (transform.position.y, -margin, margin),
		                                  transform.position.z);
	}
}
