using UnityEngine;
using System.Collections;

public class cntWays2Die : MonoBehaviour {

		public GameObject coala;	

		//guardar Sprite;
		SpriteRenderer thisSprite;
		
		public Tilt_brain Brain;

		//Baloon
		public Animator baloon32;
		public Animator baloon21;

		//public GameObject control;	
		public Sprite burnmfkr;
		public Sprite [] arrayLives;

		//FACE
		public Animator face;

	void Start () {
		//aceder a sprite
		thisSprite = coala.GetComponent<SpriteRenderer> ();
	}

	void Update(){
		if (Brain.lives == 0) {
			face.GetComponent<SpriteRenderer>().active = false;
			StartCoroutine(WaitFotIt());
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		if (Brain.lives > 0) {
			switch (col.name) {
			case "raio": 
					delSprite32 ();
					delSprite21 ();
					face.GetComponent<SpriteRenderer> ().active = false;
					thisSprite.sprite = burnmfkr;
					Brain.lives--;
					StartCoroutine (WaitFotIt ()); 
					break;
			case "ave_head":
					Brain.lives--;
					changeSprite ();
					if (Brain.lives == 2) {
							baloon32.GetComponent<SpriteRenderer> ().active = true;
							baloon32.SetBool ("pop", true);
							Invoke ("delSprite32", 1);
					}
					if (Brain.lives == 1) {
							baloon21.GetComponent<SpriteRenderer> ().active = true;
							baloon21.SetBool ("pop2", true);
							Invoke ("delSprite21", 1);
					}
					break;
			}
		}
	}
	void changeSprite(){
		thisSprite.sprite = arrayLives[Brain.lives];
	}
	void delSprite32(){
		baloon32.GetComponent<SpriteRenderer> ().active = false;
	}
	void delSprite21(){
		baloon21.GetComponent<SpriteRenderer> ().active = false;
	}
	IEnumerator WaitFotIt() {
		if (Brain.lives == 0) {
			yield return new WaitForSeconds (1);
			changeSprite ();
			coala.GetComponent<Rigidbody2D> ().gravityScale = 1;	
		} else {
				yield return new WaitForSeconds (1);
				changeSprite ();
				face.GetComponent<SpriteRenderer> ().active = true;
		}
	}
}
