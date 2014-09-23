using UnityEngine;
using System.Collections;

public class AcelerometerBrain : MonoBehaviour {

	//TEXTO NAS BOLHAS 
	public TextMesh[] bolhas = new TextMesh[10];
	public TextMesh calculoText;	
	public TextMesh msgfinalText;

	int num1;
	int num2;
	int num3;
	int resultado;

	public static GameObject boia;

	int vidas = 4;

	// Use this for initialization
	void Start () {
		set_values();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void set_values()
	{
		int result = resultado;
		int i = 0;
		int j = 1;
		
		
		int randomPositionForCorrect = Random.Range (0, 10);
		bolhas[randomPositionForCorrect].text = result.ToString ();
		bolhas[randomPositionForCorrect].transform.parent.tag = "Certo";
		
		for(i = 0; i<bolhas.Length; i++)
		{
			if(bolhas[i].text != result.ToString()){
				bolhas[i].transform.parent.tag = "Errado";
				if (i % 2 != 0) {
					//erro[i].transform.parent.position  = Random.insideUnitSphere * 10;					
					bolhas[i].text = (result + j).ToString();
				}
				else {
					//erro[i].transform.parent.position  = Random.insideUnitSphere * 10;
					
					bolhas[i].text = (result - j).ToString();
				}
				j++;
				
			}
		}
	}

	public void CollisionOccur (GameObject collisionWith)
	{
		Handheld.Vibrate ();
		Destroy(collisionWith);

		if (collisionWith.tag == "Certo")
		{
			GameEnd("Vitoria");
		}
		else if (collisionWith.tag == "Errado") 
		{
			vidas --;
			if(vidas == 0)
			{
				GameEnd("Derrota");
			}
		}

	}


	public static void GameEnd(string outcome)
	{

		if (outcome == "Vitoria") {
		
		}
		else if(outcome == "Derrota")
		{
			boia.GetComponent<PlayerController>().canMove = false;
		}
	
	}
}



/*

void setContaText () {
	num1 = Random.Range(1, 20);
	num2 = Random.Range(1, 20);
	num3 = Random.Range(1, 20);
	
	calculoText.text = "Calculo: " + num1.ToString() +" + "+ num2.ToString() +" + "+ num3.ToString() + " =? ";
	
	resultado = num1 + num2 + num3;
	
	displayNumbers (resultado);
}

void displayNumbers (int resultado) {
	int result = resultado;
	int i = 0;
	int j = 1;
	
	
	int randomPositionForCorrect = Random.Range (0, 10);
	bolhas [randomPositionForCorrect].text = result.ToString ();
	bolhas [randomPositionForCorrect].transform.parent.tag = "Certo";
	
	for(i = 0; i<bolhas.Length; i++)
	{
		if(bolhas[i].text != result.ToString()){
			bolhas [i].transform.parent.tag = "Errado";
			if (i % 2 != 0) {
				//erro[i].transform.parent.position  = Random.insideUnitSphere * 10;					
				bolhas[i].text = (result + j).ToString();
			}
			else {
				//erro[i].transform.parent.position  = Random.insideUnitSphere * 10;
				
				bolhas[i].text = (result - j).ToString();
			}
			j++;
			
		}
	}
	
}*/
