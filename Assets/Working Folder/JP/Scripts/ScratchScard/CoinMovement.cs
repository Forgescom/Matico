using UnityEngine;
using System.Collections;

public class CoinMovement : MonoBehaviour {

	public ParticleSystem particulasScratch;

	int startAlpha = 1;
	float endAlpha;
	Color32 colorFinal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckFingerTouch() == true) {
			DragCoin();
		}

	}

	bool CheckFingerTouch()
	{
		if (Input.touchCount > 0) {
			
			Vector3 wp  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos  = new Vector2(wp.x, wp.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);
			
			if(hit){
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	void DragCoin()
	{
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 5));
	}

	void OnTriggerStay2D(Collider2D col)
	{

		col.renderer.material.color = new Color(1,1,1,( Mathf.Lerp(col.renderer.material.color.a,0,Time.deltaTime /10f)));
	}

	void OnTriggerEnter2D(Collider2D col){
		//print ("COLIDI COM " + col.name);
		particulasScratch.Play ();

	
	}
	void OnTriggerExit2D(Collider2D col){
		print ("COLIDI COM " + col.name);
		particulasScratch.Stop ();
		
	}


}
