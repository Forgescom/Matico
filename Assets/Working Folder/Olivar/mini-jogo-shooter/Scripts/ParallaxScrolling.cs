using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour {

	public float ParallaxFactor;
	Vector3 previousCameraTransform;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		previousCameraTransform = camera.transform.position;
	}
	
	Camera camera;
	
	/// <summary>
	/// similar tactics just like the "CameraMove" script
	/// </summary>
	void Update () {
		Vector3 delta = camera.transform.position - previousCameraTransform;
		delta.y = 0; delta.z = 0;
		transform.position += delta / ParallaxFactor;
		previousCameraTransform = camera.transform.position;
	}
	
}
