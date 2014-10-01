using UnityEngine;
using System.Collections;

public class AcelerometerBrain : MonoBehaviour {

	//TEXTO NAS BOLHAS 
	public TextMesh[] bolhas = new TextMesh[10];
	public TextMesh calculoText;	
	public TextMesh finalMessage;
	public TextMesh vidasText;

	int num1;
	int num2;
	int num3;
	int result;

	public GameObject boia;
	public GameObject sharkfin;

	int vidas = 3;

	// Use this for initialization
	void Start () {
		set_values ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel(1);
		}
		vidasText.text = "Vidas: " + vidas.ToString();
	}

	void set_values()
	{
		num1 = Random.Range(1, 20);
		num2 = Random.Range(1, 20);
		num3 = Random.Range(1, 20);
		
		calculoText.text = "Calculo: " + num1.ToString() +" + "+ num2.ToString() +" + "+ num3.ToString() + " =? ";
		
		result = num1 + num2 + num3;

		int i = 0;
		int j = 1;

		int randomPositionForCorrect = Random.Range (0, 10);
		bolhas[randomPositionForCorrect].text = result.ToString ();
		bolhas[randomPositionForCorrect].transform.parent.tag = "Certo";
		
		for(i = 0; i < bolhas.Length; i++)
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

		Animator bubbleAnimator = collisionWith.transform.GetComponent<Animator> ();
		bubbleAnimator.SetBool ("pop",true);

		Animator boiaAnimator = boia.transform.GetComponent<Animator> ();

		//Destroy(collisionWith);

		if (collisionWith.tag == "Certo")
		{
			GameEnd("Vitoria", vidas);
		}
		else if (collisionWith.tag == "Errado")
		{
			DisableCollider(collisionWith);
			vidas--;

			if(vidas == 2)
			{
				boiaAnimator = boia.transform.GetComponent<Animator> ();
				boiaAnimator.SetBool ("Damage1", true);
				vidasText.text = vidas.ToString();
			}
			if(vidas == 1)
			{
				boiaAnimator = boia.transform.GetComponent<Animator> ();
				boiaAnimator.SetBool ("Damage2", true);
				vidasText.text = vidas.ToString();
			}
			if(vidas == 0)
			{
				boiaAnimator = boia.transform.GetComponent<Animator> ();
				boiaAnimator.SetBool ("FinalDamage", true);
				vidasText.text = vidas.ToString();
				GameEnd("Derrota", vidas);
			}
		}
		else if (collisionWith.tag == "Shark")
		{
			vidas--;
			
			if(vidas == 2)
			{
				boiaAnimator = boia.transform.GetComponent<Animator> ();
				boiaAnimator.SetBool ("Damage1", true);
				vidasText.text = vidas.ToString();
			}
			if(vidas == 1)
			{
				boiaAnimator = boia.transform.GetComponent<Animator> ();
				boiaAnimator.SetBool ("Damage2", true);
				vidasText.text = vidas.ToString();
			}
			if(vidas == 0)
			{
				boiaAnimator = boia.transform.GetComponent<Animator> ();
				boiaAnimator.SetBool ("FinalDamage", true);
				vidasText.text = vidas.ToString();
				GameEnd("Derrota", vidas);
			}
		}
	}

	void DisableCollider(GameObject col)
	{
		col.GetComponent<BoxCollider2D> ().enabled = false;
	}

	public void GameEnd(string outcome, int vidas)
	{
		if ((outcome == "Vitoria") && (vidas == 3)) {
			finalMessage.text = "Resposta certa, excelente!";
		}
		else if ((outcome == "Vitoria") && (vidas == 2)) {
			finalMessage.text = "Resposta certa, bom jogo!";
		}
		else if ((outcome == "Vitoria") && (vidas == 1)) {
			finalMessage.text = "Resposta certa, foi por pouco!";
		}
		else if(outcome == "Derrota")
		{
			finalMessage.text = "Essa nao e' a resposta certa, tenta outra vez!";
		}
		boia.GetComponent<PlayerController> ().canMove = false;
		sharkfin.GetComponent<SharkFin> ().enabled = false;
	}

}

