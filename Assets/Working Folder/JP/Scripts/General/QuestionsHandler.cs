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
	


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void SetQuestionValues()
	{
		int randomQuestionNumber = Random.Range (0, XmlQuestionsLoader.questions.Count);

		print (randomQuestionNumber);

		string questionNumber;
		XmlQuestionsLoader.questions [randomQuestionNumber].TryGetValue ("question", out questionNumber);
		questionHolder.text = questionNumber;

		string answerA;
		XmlQuestionsLoader.questions [randomQuestionNumber].TryGetValue ("false1", out answerA);
		answer1.text = answerA;

		string answerB;
		XmlQuestionsLoader.questions [randomQuestionNumber].TryGetValue ("false2", out answerB);
		answer2.text = answerB;

		string answerC;
		XmlQuestionsLoader.questions [randomQuestionNumber].TryGetValue ("false3", out answerC);
		answer3.text = answerC;

		string answerD;
		XmlQuestionsLoader.questions [randomQuestionNumber].TryGetValue ("correct", out answerD);
		answer4.transform.parent.tag = "Certo";
		answer4.text = answerD;

		
		print(XmlQuestionsLoader.questions.Count);
	}

	void OnEnable()
	{
		XmlQuestionsLoader.xmlLoaded += SetQuestionValues;
	}

	void OnDisable()
	{
		XmlQuestionsLoader.xmlLoaded -= SetQuestionValues;
	}
}
