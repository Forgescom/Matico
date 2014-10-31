using UnityEngine;
using System.Collections;

public class raioOnOff : MonoBehaviour {

	public float OnOff = 2f;

	// Use this for initialization
	void Start () {
		//StartCoroutine(cycle());
		Invoke ("TurnOffOn", OnOff);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void TurnOffOn()
	{
		if (transform.gameObject.activeSelf == true) {
			transform.gameObject.SetActive(false);
		}
		else{
			transform.gameObject.SetActive(true);
		}
		Invoke ("TurnOffOn", OnOff);
	}
}