using UnityEngine;
using System.Collections;

public class ChangeAlpha : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			transform.renderer.material.color = new Color (1, 1, 1, 0.5f);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			transform.renderer.material.color = new Color (1, 1, 1, 1f);
		}
	}
}

