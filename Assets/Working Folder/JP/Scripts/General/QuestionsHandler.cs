using UnityEngine;
using System.Collections;

public class QuestionsHandler : MonoBehaviour {

	public TextMesh questionHolder;
	public TextMesh [] answers;

	// Use this for initialization
	void Start () {
		SetQuestionValues ();
	}
	

	void SetQuestionValues()
	{
		//PICK RANDOM QUESTION NUMBER
		int randomQuestionNumber = Random.Range (0, GameController.questions.Count);

		//PICK QUESTION TEXT FROM PREVIOUS NUMBER
		string questionNumber;
		GameController.questions [randomQuestionNumber].TryGetValue ("question", out questionNumber);
		questionHolder.text = questionNumber;

		//RANDOM NUMBER TO PLACE THE CORRECT ANSWER
		int randomCorrectAnswerNumber = Random.Range (0, answers.Length);

		//INDEX TO GET THE WRONG ANSWER ON DICTIONARY
		int wrongAnswerNr = 1;


		//ASSIGN VALUES
		for (int i= 0; i <answers.Length; i++) {

			if(i == randomCorrectAnswerNumber)
			{
				string correctAnswerText;
				GameController.questions [randomQuestionNumber].TryGetValue ("correct", out correctAnswerText);
				answers[i].transform.parent.tag = "Certo";
				answers[i].transform.parent.GetComponent<ScratchBox>().SetSprites();
				answers[i].text = correctAnswerText;
			}
			else{
				string wrongAnswerText;
				string questionDictionayKey = "false" + wrongAnswerNr.ToString();
				GameController.questions [randomQuestionNumber].TryGetValue (questionDictionayKey, out wrongAnswerText);
				answers[i].text = wrongAnswerText;
				answers[i].transform.parent.GetComponent<ScratchBox>().SetSprites();
				wrongAnswerNr ++;
			}

		}

	}

}
