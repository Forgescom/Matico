using UnityEngine;
using System.Collections;

public class LanternController : MonoBehaviour {
	public Camera camera;
	public float speed;
	
	private Vector3 touchPosition;
	private Vector3 direction;
	private float distanceFromObject;

	public LineRenderer LanternLineRenderer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RotateLantern ();
	}


	void RotateLantern ()
	{

		if (Input.touchCount > 0)
		{
			//Grab the current mouse position on the screen
			touchPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - camera.transform.position.z));
			
			//Rotates toward the mouse
			rigidbody2D.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((touchPosition.y - transform.position.y), (touchPosition.x - transform.position.x))*Mathf.Rad2Deg - 90);
			
			//Judge the distance from the object and the mouse
			distanceFromObject = (Input.mousePosition - camera.WorldToScreenPoint(transform.position)).magnitude;
			
			//Move towards the mouse
			rigidbody2D.AddForce(direction * speed * distanceFromObject * Time.deltaTime);
		}
	}

}
