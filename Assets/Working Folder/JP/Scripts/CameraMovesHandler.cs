using UnityEngine;
using System.Collections;

public class CameraMovesHandler : MonoBehaviour {

	//MOVEMENT
	public float speedFactor = 0.05f;
	Vector3 startPosition;
	
	//CLAMP
	public float [] boundaries = new float[2];
	
	//ZOOM
	
	float speed = 0.05f; 
	float MINSCALE = 2.0F; 
	float MAXSCALE = 5.1F; 
	float minPinchSpeed = 5.0F; 
	float varianceInDistances = 5.0F; 
	float touchDelta = 0.0F; 
	Vector2 prevDist = new Vector2(0,0); 
	Vector2 curDist = new Vector2(0,0); 
	float speedTouch0 = 0.0F; 
	float speedTouch1 = 0.0F;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void MoveCamera(Vector3 direction){
		startPosition = transform.position;
		
		
		Vector3 newPosition = startPosition + direction;
		newPosition.z = startPosition.z;
		
		transform.position = Vector3.Lerp(startPosition,newPosition,speedFactor);
		/*transform.position = new Vector3 (Mathf.Clamp (transform.position.x, boundaries[0], boundaries[1]),
		                                  Mathf.Clamp (transform.position.y, boundaries[2], boundaries[3]),
		                                  transform.position.z);*/
		
		
		ClampCameraMovement ();
		
		
	}
	
	void ZoomInOutCamera(Touch[] inputValues ){
		curDist = inputValues[0].position - inputValues[1].position; //current distance between finger touches
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
		
		ClampCameraMovement ();
	}
	
	//CLAMP DEPENDING ON CAMERA SIZE
	void ClampCameraMovement()
	{
		float currentSize = transform.camera.orthographicSize;
		float factor = boundaries [1] / (-1*boundaries [0]);
		float margin = MAXSCALE - currentSize;
		//print (factor);
		//print (margin);
		
		
		
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -margin *factor, margin*factor),
		                                  Mathf.Clamp (transform.position.y, -margin, margin),
		                                  transform.position.z);
	}
}
