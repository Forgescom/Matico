using UnityEngine;
using System.Collections;

public class Tilt_brain : MonoBehaviour {

	public int lives;
	// Use this for initialization
	//contadores
	public countMenorScr countMenor;
	public int Menor;
	public countIgualSrc countIgual;
	public int Igual;
	public countMaiorSrc countMaior;
	public int Maior;

	void Update(){
		countMaior.barDisplay = Maior;
		countIgual.barDisplay = Igual;
		countMenor.barDisplay = Menor;
	}
}
