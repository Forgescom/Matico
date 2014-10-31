using UnityEngine;
using System.Collections;

public class LevelsBlocker : MonoBehaviour {

	public GameObject matico;
	public GameObject [] pricesHolder;

	public GameObject steps;


	void Start()
	{
		print ("start");
		//pricesHolder.SetActive (false);

		DetermineTexturesToPrices ();

		foreach (GameObject price in pricesHolder) {
			if(price.GetComponent<PriceHolder>().unlocked == false)
				price.SetActive(false);
		}


		matico.SetActive (false);

		//DetermineTexturesToPrices ();
	//	ShowUnlockedPrices ();

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
					goto Foo;
				}
				else{
					int currentHouseEnergies;
					int.TryParse(GameController.houses[i]["EnergiesSpent"],out currentHouseEnergies);
					energiesSpent +=currentHouseEnergies;
				}

			}
			AssignTextures(stepIndex,energiesSpent );

		}

		Foo:
			return;


	}

	void ShowUnlockedPrices(){
		//print ("UNLOCK" + GameController.CURRENT_LEVEL_STEP);
		/*if ((GameController.CURRENT_LEVEL_STEP) !=0) {

			for(int i=0; i < GameController.CURRENT_LEVEL_STEP; i++)
			{
				//print ("INSIDE FOR + " + i);
				//pricesHolder[i].GetComponent<PriceHolder> ().unlocked = true;
				pricesHolder [i].SetActive (true);
			
				//pricesHolder [i].animation.Play ("PricePop");
			}

		}*/

		print (GameController.housesUnlocked.Count);
		foreach (GameObject price in GameController.housesUnlocked) {
			print (price);
			price.SetActive(true);
		}


		
	}
	
	void AssignTextures(int step,int energies){
		int indexNewTexture = 0;

		if(energies <= 6){
			indexNewTexture = 2;
		}
		else if(energies > 6 && energies <= 12){
			indexNewTexture = 1;
		}
		else if(energies > 12){
			indexNewTexture = 0;
		}
	
		pricesHolder [step].GetComponent<PriceHolder> ().SetSprite (indexNewTexture);

	}

	public void CheckIfBlocker(bool initialRequest = false)
	{
		matico.transform.position = steps.transform.GetChild(GameController.CURRENT_LEVEL_STEP-1).transform.position;
		matico.SetActive(true);
		matico.animation.Play("MaticoPopIn");
	}



	void AnimObject()
	{
		pricesHolder [GameController.CURRENT_LEVEL_STEP-1].SetActive (true);
		GameController.housesUnlocked.Add(pricesHolder [GameController.CURRENT_LEVEL_STEP - 1].gameObject);
		pricesHolder [GameController.CURRENT_LEVEL_STEP-1].animation.Play ("PricePop");
		pricesHolder [GameController.CURRENT_LEVEL_STEP - 1].GetComponent<PriceHolder> ().unlocked = true;
	}

	void OnEnable()
	{
		GameController.showUnlocked += ShowUnlockedPrices;
		GameController.checkStepPrices += CheckIfBlocker;
		MaticoUnlocker.showNewObjectEvent += AnimObject;
	}
	
	void OnDisable()
	{
		GameController.showUnlocked -= ShowUnlockedPrices;
		GameController.checkStepPrices -= CheckIfBlocker;
		MaticoUnlocker.showNewObjectEvent -= AnimObject;
	}
	
	
}
