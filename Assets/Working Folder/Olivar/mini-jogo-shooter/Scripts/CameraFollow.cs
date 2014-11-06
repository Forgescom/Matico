using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	[HideInInspector]
	public Vector3 StartingPosition;

	public GameObject question;

	float MINSCALE = 5.4F; 
	float MAXSCALE = 7.4F; 
	float MAX_X_ZOOMIN = 18.5f;
	float MAX_X_ZOOMOUT = 15;

	float MAX_Y_ZOOMIN = 4.12F;
	float MAX_Y_ZOOMOUT = 6.16F;

	[HideInInspector]
	public bool IsFollowing;
	[HideInInspector]
	public Transform PandaToFollow;

	// Use this for initialization
	void Start()
	{
		StartingPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (IsFollowing)
		{
			if (PandaToFollow != null) //panda will be destroyed if it goes out of the scene
			{
				float currentSize = transform.camera.orthographicSize;
				float factor = (MAX_X_ZOOMIN - MAX_X_ZOOMOUT)/(MAXSCALE-MINSCALE);
				float factorY = (MAX_Y_ZOOMOUT - MAX_Y_ZOOMIN)/(MAXSCALE-MINSCALE);
				
				float xMin = 0 +(currentSize - MINSCALE)*factor;
				float xMax = MAX_X_ZOOMIN -(currentSize-MINSCALE)*factor;

				var pandaPosition = PandaToFollow.transform.position;
				float x = Mathf.Clamp(pandaPosition.x, xMin, xMax);

				float yMin = 4.15f +(currentSize-MINSCALE)*factorY;
				float yMax = MAX_Y_ZOOMOUT -(currentSize-MINSCALE)*factorY;

				//camera follows panda's x position
				transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
				
				question.transform.position = new Vector3 (transform.position.x,
				                                           Mathf.Clamp (transform.position.y, yMin, yMax),
				                                           10);
			}
			else
				IsFollowing = false;
		}
	}

}

