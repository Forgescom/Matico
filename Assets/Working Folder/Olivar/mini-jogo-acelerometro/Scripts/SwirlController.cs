using UnityEngine;
using System.Collections;

public class SwirlController : MonoBehaviour {

	float speed = 1f;
	Vector3 initialScale = new Vector3(0.1f,0.1f,0.1f);
	Vector3 finalScale = new Vector3(1,1,1);
	public float zoomInTime = 2f;
	int xRange = 6;
	int yRange = 3;
	CircleCollider2D transformCollider;


	// Use this for initialization
	void Start () {
		transformCollider = transform.GetComponent<CircleCollider2D>();
		transformCollider.enabled = false;

		print ("PASSED ON START");
	}

	public void SpawnSwirl()
	{


		transform.localScale = initialScale;
		
		float newX = Random.Range (-xRange, xRange);
		float newY = Random.Range (-yRange, yRange);
		
		transform.position = new Vector3 (newX, newY, 6);

		StartCoroutine ("SwirlStop");
	}
	
	public void ZoomIn()
	{

		transform.scaleTo (zoomInTime, finalScale).setOnCompleteHandler (originalTween => transformCollider.enabled = true);
	}

	IEnumerator SwirlStop()
	{

		yield return new WaitForSeconds (5f);

		transformCollider.enabled = false;
		transform.scaleTo (zoomInTime, initialScale).setOnCompleteHandler (originalTween => {																			
		                                                                   transform.gameObject.SetActive(false);});
	}

	void StopAutoDestroy()
	{
		StopCoroutine("SwirlStop");

	}


	// Update is called once per frame
	void Update () {
		//transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		
	}
}
