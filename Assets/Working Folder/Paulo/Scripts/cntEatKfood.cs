using UnityEngine;
using System.Collections;

public class cntEatKfood : MonoBehaviour {
	
	public Tilt_brain Brain;
	
	public Animator face;
	
	void OnTriggerEnter2D(Collider2D col){
		switch(col.name){	
			case "Menor":
				face.SetBool("Eat",true);
				Brain.Menor = Brain.Menor + 1;
			break;
			case "Igual":
				face.SetBool("Eat",true);
				Brain.Igual = Brain.Igual + 1;
			break;
			case "Maior":
				face.SetBool("Eat",true);
				Brain.Maior = Brain.Maior + 1;
			break;
		}
	}
}
