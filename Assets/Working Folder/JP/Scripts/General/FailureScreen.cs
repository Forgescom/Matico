using UnityEngine;
using System.Collections;

public class FailureScreen : MonoBehaviour {


	public delegate void FailureScreenEvent();
	public static event FailureScreenEvent RestartGame;

	public delegate void NoLivesEvent();
	public static event NoLivesEvent NoLives;

	// Use this for initialization
	void Start () {
		transform.parent.GetComponent<AudioSource> ().enabled = GameController.BG_SOUND;
	}
	
	void Update () {
		if (CheckFingerTouch() == true || Input.GetMouseButtonUp(0) == true) {

			if(GameController.CURRENT_LIVES > 0)
			{
				if(RestartGame !=  null)
				{
					RestartGame();
					
				}
			}
			else
			{
				if(NoLives != null)
				{
					NoLives();
				}
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
