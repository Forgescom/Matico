using UnityEngine;
using System.Collections;

public class CloudTest : MonoBehaviour {

	public GameObject cloud1;
	public GameObject cloud2;

	// Use this for initialization
	void Start () {
		print ("CLOUD 1" +cloud1.GetComponent<Clouds>().speed);
		print ("CLOUD 2" + cloud2.GetComponent<Clouds>().speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
