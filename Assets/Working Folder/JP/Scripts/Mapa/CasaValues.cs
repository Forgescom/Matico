using UnityEngine;
using System.Collections;

public class CasaValues : MonoBehaviour {

	public enum TypeOfGames{
		shooter,
		accelerometer,
		scratchcard,
		tilt
	};

	public int energiesSpent;
	public int dificulty;
	public TypeOfGames gameType;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
