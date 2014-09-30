using UnityEngine;
using System.Collections;

public class QuestionsHandler : MonoBehaviour {

	public TextMesh questionHolder;
	public TextMesh answer1;
	public TextMesh answer2;
	public TextMesh answer3;
	public TextMesh answer4;

	// Use this for initialization
	void Start () {
		SetQuestionValues ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void SetQuestionValues()
	{
		int randomQuestionNumber = Random.Range (0, GameController.questions.Count);

		//print (GameController.questions.Count);

		string questionNumber;
		GameController.questions [randomQuestionNumber].TryGetValue ("question", out questionNumber);
		questionHolder.text = questionNumber;

		string answerA;
		GameController.questions [randomQuestionNumber].TryGetValue ("false1", out answerA);
		answer1.text = answerA;
		//print (answer1.transform.parent.GetComponent<ScratchBox>().SetSprites());
		answer1.transform.parent.GetComponent<ScratchBox>().SetSprites();
		//answer1.transform.parent.SendMessage ("SetSprites");

		string answerB;
		GameController.questions [randomQuestionNumber].TryGetValue ("false2", out answerB);
		answer2.text = answerB;
		//answer2.transform.parent.SendMessage ("SetSprites");
		answer2.transform.parent.GetComponent<ScratchBox>().SetSprites();

		string answerC;
		GameController.questions [randomQuestionNumber].TryGetValue ("false3", out answerC);
		answer3.text = answerC;
		//answer3.transform.parent.SendMessage ("SetSprites");
		answer3.transform.parent.GetComponent<ScratchBox>().SetSprites();

		string answerD;
		GameController.questions [randomQuestionNumber].TryGetValue ("correct", out answerD);
		answer4.transform.parent.tag = "Certo";
	//	answer4.transform.parent.SendMessage ("SetSprites");
		answer4.transform.parent.GetComponent<ScratchBox>().SetSprites();
		answer4.text = answerD;

	}

	void OnEnable()
	{
		//XmlQuestionsLoader.xmlLoaded += SetQuestionValues;
	}

	void OnDisable()
	{
	//	XmlQuestionsLoader.xmlLoaded -= SetQuestionValues;
	}
}
