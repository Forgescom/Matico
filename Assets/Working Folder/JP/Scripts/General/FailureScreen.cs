using UnityEngine;
using System.Collections;

public class FailureScreen : MonoBehaviour {


	public delegate void FailureScreenEvent();
	public static event FailureScreenEvent RestartGame;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		if (CheckFingerTouch() == true || Input.GetMouseButtonUp(0) == true) {
			if(RestartGame !=  null)
			{
				RestartGame();

			}
			Destroy(transform.parent.gameObject);
		}
		
	}
	
	bool CheckFingerTouch()
	{
		
		if (Input.touchCount > 0) {
			
			Vector3 wp  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos  = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			
			if(hit && hit.name == "bg"){
			
				return true;
			}
			else
			{
				return false;
			}
		}
		else{
			return false;
		}

	}


}
