using UnityEngine;
using System.Collections;

public class ScratchBox : MonoBehaviour {

	TextMesh textAnswer;
	float minAlpha = 0.2f;
	ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		textAnswer = transform.FindChild ("text").GetComponent<TextMesh>();
		particleSystem = transform.FindChild ("particles").GetComponent<ParticleSystem> ();
		particleSystem.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Scratch(bool state)
	{
		particleSystem.enableEmission = true;
		//particleSystem.Play ();

		if(state == true)
		{
			if(transform.renderer.material.color.a >= minAlpha)
			{
				//transform.renderer.material.color = new Color(1,1,1,( Mathf.Lerp(transform.renderer.material.color.a,0,Time.deltaTime /5f)));
				textAnswer.renderer.material.color = new Color(1,1,1,( Mathf.Lerp(textAnswer.renderer.material.color.a,0,Time.deltaTime /5f)));
				print("IF1");
			}
			else
			{

				transform.gameObject.SetActive(false);
			}
		}
		else{
		//	particleSystem.Stop();
			particleSystem.enableEmission = false;
			print("IF2");
		}
		//print (particleSystem.isPlaying);
	}
}
