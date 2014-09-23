using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<TextMesh> ().text = BoardMain.currentLevelType.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
