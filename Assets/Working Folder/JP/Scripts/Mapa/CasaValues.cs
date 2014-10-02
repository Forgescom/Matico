using UnityEngine;
using System.Collections;

public enum TypeOfGames{
	shooter,
	accelerometer,
	scratchcard,
	tilt
};

public class CasaValues : MonoBehaviour {



	public int energiesSpent;
	public int dificulty;
	public TypeOfGames gameType;
	public bool locked = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
