using UnityEngine;
using System.Collections;

public class CasaController : MonoBehaviour {

	public delegate void HouseClicked(Transform house);
	public static event HouseClicked throwGame;

	public GameObject bloqueadoSprite;
	public GameObject desbloquadoSprite;
	public GameObject highlightSprite;
	
	float maxHighlightSize = 1.2f;
	float minHighlightSize = 1f;
	float timeFactor = 5f;
	
	public bool isHighLighted = false;
	public bool locked = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isHighLighted == true) {
			highlightSprite.transform.localScale = new Vector3(Mathf.PingPong((Time.time/timeFactor), maxHighlightSize-minHighlightSize)+minHighlightSize,
			                                                   Mathf.PingPong((Time.time/timeFactor), maxHighlightSize-minHighlightSize)+minHighlightSize, 
			                                                   1f);		
		}
		else{
			highlightSprite.transform.localScale = new Vector3(1f,1f,1f);		
		}
	}

	public void UpdateState(){
		if (locked == false)
			UnlockButton ();


	}

	void ButtonPressed(){
		if (throwGame != null) {
			throwGame (transform);
		}
	}
	
	public void UnlockButton(){
		desbloquadoSprite.SetActive(true);
		highlightSprite.SetActive(true);
		bloqueadoSprite.animation.Play ();
	}

	public void LockButton(){
		desbloquadoSprite.SetActive(false);
		highlightSprite.SetActive(false);
		bloqueadoSprite.SetActive (true);
		bloqueadoSprite.animation.Play ("CasaFadeIn");
	}


	
	void RemoveLocker()
	{
		bloqueadoSprite.SetActive (false);
	}
}
