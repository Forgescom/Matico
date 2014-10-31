using UnityEngine;
using System.Collections;

public class PriceHolder : MonoBehaviour {

	public Sprite [] SpriteSet;
	SpriteRenderer transformSprite;
	public bool unlocked = false;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSprite(int index){
		transformSprite = transform.GetComponent<SpriteRenderer> ();


		transformSprite.sprite = SpriteSet [2];
	}
}
