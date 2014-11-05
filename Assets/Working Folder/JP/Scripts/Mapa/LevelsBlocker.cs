using UnityEngine;
using System.Collections;

public class LevelsBlocker : MonoBehaviour {

	public GameObject matico;
	public GameObject [] pricesHolder;

	public GameObject steps;


	void Start()
	{


		foreach (GameObject price in pricesHolder) {
			if(price.GetComponent<PriceHolder>().unlocked == false)
				price.SetActive(false);
		}
		matico.SetActive (false);

		DetermineTexturesToPrices ();
	}

	void ShowMatico()
	{
		Vector3 newPos = steps.transform.GetChild((GameController.CURRENT_LEVEL/3)-1).transform.position;
		newPos.z = matico.transform.position.z;
		matico.transform.position = newPos;
		matico.SetActive(true);
		matico.animation.Play("MaticoPopIn");
		DetermineTexturesToPrices ();

	}

	void DetermineTexturesToPrices()
	{
		int energiesSpent = 0;
		int stepIndex;

		for (int j= 0; j<GameController.houses.Count/3; j++) {
			int firstIndex = j *3;
			stepIndex = j;
			energiesSpent = 0;
			for(int i = firstIndex; i <firstIndex+3; i++)
			{

				if(GameController.houses[i]["Played"] == "false")
				{
					print ("SAI NO " + i);
					goto Foo;
				}
				else{
					int currentHouseEnergies;
					int.TryParse(GameController.houses[i]["EnergiesSpent"],out currentHouseEnergies);
					energiesSpent +=currentHouseEnergies;
				}

			}
			print("NUMERO DE ENERGIAS" + energiesSpent );

			AssignTextures(stepIndex,energiesSpent );

		}

		Foo:
			return;


	}


	void AssignTextures(int step,int energies){
		int indexNewTexture = 0;

		if(energies < 2){
			indexNewTexture = 2;
		}
		else if(energies >= 2 && energies <= 4){
			indexNewTexture = 1;
		}
		else if(energies > 4){
			indexNewTexture = 0;
		}
	
		print ("INDICE DA IMAGEM:" + indexNewTexture + "CURRENT INDEX: " + step);

		pricesHolder [step].GetComponent<PriceHolder> ().SetSprite (indexNewTexture);
		pricesHolder [step].GetComponent<PriceHolder> ().unlocked = true;
		if (matico.activeSelf == false) {
			pricesHolder [step].SetActive (true);
		}
	}




	void AnimObject()
	{
		int Index = (GameController.CURRENT_LEVEL / 3) - 1;
		pricesHolder [Index].SetActive (true);
		pricesHolder [Index].animation.Play ("PricePop");
		pricesHolder [Index].GetComponent<PriceHolder> ().unlocked = true;
	}

	void OnEnable()
	{

		MaticoUnlocker.showNewObjectEvent += AnimObject;
	}
	
	void OnDisable()
	{

		MaticoUnlocker.showNewObjectEvent -= AnimObject;
	}

	
}
