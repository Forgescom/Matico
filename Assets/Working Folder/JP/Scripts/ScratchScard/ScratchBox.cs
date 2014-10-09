using UnityEngine;
using System.Collections;

public class ScratchBox : MonoBehaviour {

	float minAlpha = 0.2f;
	public ParticleSystem particlesSystem;
	public TextMesh textAnswer;
	public GameObject answerSprite;

	public Sprite correctSprite;
	public Sprite wrongSprite;

	private bool enable = true;


	// Use this for initialization
	void Start () {
		particlesSystem.enableEmission = false;


	}

	public void SetSprites()
	{
		if (transform.tag == "Certo") {
			answerSprite.GetComponent<SpriteRenderer>().sprite = correctSprite;
		}
		else
		{
			answerSprite.GetComponent<SpriteRenderer>().sprite = wrongSprite;
		}
		answerSprite.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}

	public delegate void ScratchFinish(string state);
	public static event ScratchFinish finishEvent;

	void Scratch(bool state)
	{
		if(state == true && enable == true)
		{
			if(transform.renderer.material.color.a >= minAlpha)
			{
				transform.renderer.material.color = new Color(1,1,1,( Mathf.Lerp(transform.renderer.material.color.a,0,Time.deltaTime /2f)));
				textAnswer.renderer.material.color = new Color(1,1,1,( Mathf.Lerp(textAnswer.renderer.material.color.a,0,Time.deltaTime /2f)));				
				particlesSystem.enableEmission = true;
			}
			else
			{
				answerSprite.SetActive (true);
				answerSprite.animation.Play("AnswerPop");
				transform.renderer.material.color = new Color(1,1,1,0);
				textAnswer.renderer.material.color = new Color(1,1,1,0);	
				enable = false;
				StartCoroutine("FinishGame");

			}
		}
		else{
			particlesSystem.enableEmission = false;
		}

	}

	IEnumerator FinishGame()
	{
		yield return new WaitForSeconds(2f);
		if(finishEvent !=null)
		{
			finishEvent(transform.tag);
		}
	}

	void RestartShapes()
	{
		transform.renderer.material.color = new Color(1,1,1,1);
		textAnswer.renderer.material.color = new Color(1,1,1,1);
		answerSprite.SetActive (false);

		enable = true;
	}

	void OnEnable()
	{
		ScratchController.GameTryAgain += RestartShapes;

	}
	void OnDisable()
	{
		ScratchController.GameTryAgain -= RestartShapes;
		
	}

}
