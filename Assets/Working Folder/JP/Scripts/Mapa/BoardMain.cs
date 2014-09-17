using UnityEngine;
using System.Collections;
using System.Linq;
public class BoardMain : MonoBehaviour {

	public GameObject intro;
	public GameObject bg;
	public GameObject housesHolder;

	public GameObject [] houses;

	public CameraMovesHandler cameraScript;

	void Start(){
		houses =   GameObject.FindGameObjectsWithTag("House").OrderBy( go => go.name ).ToArray();

		LoadLevelStats ();
		ShowObjects ();
		UnlockHouses ();

	}

	void ShowObjects(){
		if (Main.CURRENT_LEVEL == 0) {
			intro.SetActive(true);
			bg.GetComponent<BackgroundTouch>().enabled = false;

		}
		else
		{
			intro.SetActive(false);
			bg.SetActive(true);

		}
	}

	void UnlockHouses()
	{
		for (int i = 0; i<Main.CURRENT_LEVEL; i ++) {
			houses[i].GetComponent<CasaController>().UnlockButton();
			if(i == (Main.CURRENT_LEVEL -1))
			{
				houses[i].GetComponent<CasaController>().isHighLighted = true;
			}
		}
	}

	void LoadLevelStats()
	{

	}

	void EnableMap()
	{
		bg.GetComponent<BackgroundTouch>().enabled = true;
		cameraScript.startAnimBoard (houses [Main.CURRENT_LEVEL].transform.position);
	}

	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += EnableMap;
	}

	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= EnableMap;
	}
}
