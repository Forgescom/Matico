﻿using UnityEngine;
using System.Collections;

public class AcelerometerBrain : MonoBehaviour {

	//TEXTO NAS BOLHAS 
	public TextMesh[] bolhas = new TextMesh[10];
	public TextMesh calculoText;	
	public TextMesh finalMessage;
	public TextMesh vidasText;
	public GameObject successScreen;
	public GameObject failureScreen;

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

		successScreen.SetActive(false);
		failureScreen.SetActive (false);

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

	void btClick(GameObject btClicked)
	{
		print (btClicked.name);
	}

	public void CollisionOccur (GameObject collisionWith)
	{
		Handheld.Vibrate ();

		Animator bubbleAnimator = collisionWith.transform.GetComponent<Animator> ();
//		bubbleAnimator.SetBool ("pop", true);

		Animator boiaAnimator = boia.transform.GetComponent<Animator> ();

		if (collisionWith.tag == "Certo")
		{
			bubbleAnimator.SetBool ("pop", true);
			GameEnd("Vitoria", vidas);
		}

		if (collisionWith.tag == "Errado")
		{
			bubbleAnimator.SetBool ("pop", true);
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

		if (collisionWith.tag == "Shark")
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
				GameEnd("Derrotatubarao", vidas);
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
			successScreen.SetActive(true);
		}
		else if ((outcome == "Vitoria") && (vidas == 2)) {
			finalMessage.text = "Resposta certa, bom jogo!";
			successScreen.SetActive(true);
		}
		else if ((outcome == "Vitoria") && (vidas == 1)) {
			finalMessage.text = "Resposta certa, foi por pouco!";
			successScreen.SetActive(true);
		}
		else if(outcome == "Derrota")
		{
			finalMessage.text = "Perdeste todas as vidas, tenta outra vez!";
			failureScreen.SetActive (true);
		}
		else if(outcome == "Derrotatubarao")
		{
			finalMessage.text = "Foste apanhado pelo tubarao! Tenta outra vez!";
			failureScreen.SetActive (true);
		}
		boia.GetComponent<PlayerController> ().canMove = false;
		sharkfin.GetComponent<SharkFin> ().enabled = false;
	}
}

