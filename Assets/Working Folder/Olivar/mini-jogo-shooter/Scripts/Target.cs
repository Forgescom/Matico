using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
	
	//    public float Health = 150f;
	//    public Sprite SpriteShownWhenHurt;
	//    private float ChangeSpriteHealth;
	// Use this for initialization
	void Start()
	{
//        ChangeSpriteHealth = Health - 30f;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
	
		
		//if we are hit by a panda
		if (col.gameObject.tag == "Panda")
		{
			transform.animation.Play("TargetHit");
		}
	}

	IEnumerator DestroyAfter(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}
}
