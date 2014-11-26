using UnityEngine;
using System.Collections;

public class NumberSrc : MonoBehaviour {

	public string foodNumber;
	private TextMesh vNum;
	// Use this for initialization
	void Start () {

		vNum = GameObject.Find("infoNum").GetComponent<TextMesh>();
		vNum.text = foodNumber.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		vNum.text = foodNumber;
	}
}
