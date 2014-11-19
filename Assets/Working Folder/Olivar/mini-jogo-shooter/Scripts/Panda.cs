using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
public class Panda : MonoBehaviour {

	GameObject GManager;
	GameObject questionArea;
	
	// Use this for initialization
	void Start()
	{
		GManager = GameObject.Find ("GameManager");
		//trailrenderer is not visible until we throw the panda
		GetComponent<TrailRenderer>().enabled = false;
		GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
		//no gravity at first
		GetComponent<Rigidbody2D>().isKinematic = true;
		//make the collider bigger to allow for easy touching
		GetComponent<CircleCollider2D>().radius = Constants.PandaColliderRadiusBig;
		State = PandaState.BeforeThrown;
	}
	
	void FixedUpdate()
	{
		//if we've thrown the panda
		//and its speed is very small
		if (State == PandaState.Thrown &&
		    GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
		{
			//destroy the panda after 2 seconds
			StartCoroutine(DestroyAfter(2));
		}
	}
	
	public void OnThrow()
	{
		//play the sound
//		GetComponent<AudioSource>().Play();
		//show the trail renderer
		GetComponent<TrailRenderer>().enabled = true;
		//allow for gravity forces
		GetComponent<Rigidbody2D>().isKinematic = false;
		//make the collider normal size
		GetComponent<CircleCollider2D>().radius = Constants.PandaColliderRadiusNormal;
		State = PandaState.Thrown;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		HideQuestion (false);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.name.Contains("Target"))
		{
//			col.SendMessage("AnimAndDestroy");
//			transform.position = Vector3.zero;
			if(col.tag == "Errado") {
				GManager.SendMessage("AnswerHit", false);
				print("Enviado: Errado");
			}
			else if (col.tag == "Certo") {
				GManager.SendMessage("AnswerHit", true);
				print("Enviado: Certo");
			}
		}
		else if (col.tag == "Question")
		{
			HideQuestion(true);
		}
	}

	void HideQuestion(bool hide)
	{
		questionArea = GameObject.FindGameObjectWithTag ("Question");
		if(hide)		
			questionArea.renderer.material.color = new Color (1, 1, 1, 0.5f);
		else
			questionArea.renderer.material.color = new Color (1, 1, 1, 1);
	}

	IEnumerator DestroyAfter(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy(gameObject);
	}
	
	public PandaState State
	{
		get;
		private set;
	}
}
