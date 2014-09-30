using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour {

	public ParticleSystem particulasScratch;

	Vector3 startPosition;
	bool grabed = false;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckFingerTouch() == true || grabed == true) {
			DragCoin();
		}

	}

	bool CheckFingerTouch()
	{

		if (Input.touchCount > 0) {
			
			Vector3 wp  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos  = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			
			if(hit && hit.name == "moeda"){
				print (hit.name);
				grabed = true;
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			transform.position = startPosition;
			grabed = false;
			return false;
		}
	}

	void DragCoin()
	{
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 5));
	}

	void OnTriggerStay2D(Collider2D col)
	{
		col.SendMessage ("Scratch", true);


	}

	void OnTriggerEnter2D(Collider2D col){
		particulasScratch.Play ();

	
	}
	void OnTriggerExit2D(Collider2D col){
		col.SendMessage ("Scratch", false);

		
	}


}
