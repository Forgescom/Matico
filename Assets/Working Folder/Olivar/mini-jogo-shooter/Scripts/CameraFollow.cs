﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	
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
				var pandaPosition = PandaToFollow.transform.position;
				float x = Mathf.Clamp(pandaPosition.x, minCameraX, maxCameraX);
				//camera follows bird's x position
				transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
			}
			else
				IsFollowing = false;
		}
	}
	
	[HideInInspector]
	public Vector3 StartingPosition;

	private const float minCameraX = -20;
	private const float maxCameraX = 16;
	[HideInInspector]
	public bool IsFollowing;
	[HideInInspector]
	public Transform PandaToFollow;
}
