using UnityEngine;
using System.Collections;

public class CasaController : MonoBehaviour {

	public delegate void HouseClicked(string houseName);
	public static event HouseClicked throwGame;

	public GameObject bloqueadoSprite;
	public GameObject desbloquadoSprite;
	public GameObject highlightSprite;
	
	float maxHighlightSize = 1.2f;
	float minHighlightSize = 1f;
	float timeFactor = 5f;
	
	public bool isHighLighted = false;
	
	// Use this for initialization
	void Start () {
	/*	desbloquadoSprite.SetActive(false);
		highlightSprite.SetActive(false);*/
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isHighLighted == true) {
			highlightSprite.transform.localScale = new Vector3(Mathf.PingPong((Time.time/timeFactor), maxHighlightSize-minHighlightSize)+minHighlightSize,
			                                                   Mathf.PingPong((Time.time/timeFactor), maxHighlightSize-minHighlightSize)+minHighlightSize, 
			                                                   1f);		
		}
	}
	
	void ButtonPressed(){
		if (throwGame != null) {
			throwGame (transform.name);
		}
	}
	
	public void UnlockButton(){
		desbloquadoSprite.SetActive(true);
		highlightSprite.SetActive(true);
		bloqueadoSprite.animation.Play ();
	}


	
	void RemoveLocker()
	{
		bloqueadoSprite.SetActive (false);
	}
}
