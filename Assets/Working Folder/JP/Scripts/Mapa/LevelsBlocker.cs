using UnityEngine;
using System.Collections;

public class LevelsBlocker : MonoBehaviour {

	public GameObject matico;
	public Sprite [] firstStepPrices;
	//public GameObject firstStepHolder;
	public GameObject [] pricesHolder;

	public GameObject steps;
	SpriteRenderer currentStepSprite;

	void Start()
	{
		//pricesHolder.SetActive (false);
		foreach (GameObject price in pricesHolder) {
			price.SetActive(false);
		}
		currentStepSprite = pricesHolder [GameController.CURRENT_LEVEL_STEP].GetComponent<SpriteRenderer> ();

		matico.SetActive (false);


	}

	public void CheckIfBlocker()
	{
		if(GameController.CURRENT_LEVEL % 3==0)
		{
			int energiesSpent =0;
			int firstIndexForLoop = GameController.CURRENT_LEVEL_STEP * 3;


			for(int i = firstIndexForLoop; i <firstIndexForLoop +3; i++)
			{

				int currentHouseEnergie;
				int.TryParse(GameController.houses[i]["EnergiesSpent"],out currentHouseEnergie);
				energiesSpent += currentHouseEnergie;
			}

			AssignTextures(energiesSpent);
			matico.transform.position = steps.transform.GetChild(GameController.CURRENT_LEVEL_STEP).transform.position;
			matico.SetActive(true);
			matico.animation.Play("MaticoPopIn");




		}
		else
		{
			print ("NOR A MULTIPLE");
		}
	}

	void AssignTextures(int energies){
		if(energies <= 6){
			currentStepSprite.sprite = firstStepPrices[2];
		}
		else if(energies > 6 && energies <= 12){
			currentStepSprite.sprite = firstStepPrices[1];
		}
		else if(energies > 12){
			currentStepSprite.sprite = firstStepPrices[0];
		}
	}

	void AnimObject()
	{
		pricesHolder[GameController.CURRENT_LEVEL_STEP].SetActive (true);
		pricesHolder[GameController.CURRENT_LEVEL_STEP].animation.Play("PricePop");

		GameController.CURRENT_LEVEL_STEP ++;
	}

	void OnEnable()
	{
		GameController.checkStepPrices += CheckIfBlocker;
		MaticoUnlocker.showNewObjectEvent += AnimObject;
	}
	
	void OnDisable()
	{
		GameController.checkStepPrices -= CheckIfBlocker;
		MaticoUnlocker.showNewObjectEvent -= AnimObject;
	}
	
	
}
