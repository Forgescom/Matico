using UnityEngine;
using System.Collections;

public class QuestionsHandler : MonoBehaviour {

	public TextMesh questionHolder;
	public TextMesh [] answers;

	public bool randomAnswers = false;

	// Use this for initialization
	void Start () {
		SetQuestionValues ();
	}
	
	bool TypeGameEquals(int index, string currentType, int currentDificulty)
	{
		//DIFICULTY
		string dificulty;
		GameController.questions [index].TryGetValue ("QuestionLevel", out dificulty);
		int dificultyInt;
		int.TryParse(dificulty,out dificultyInt);

		//TYPE OF GAME
		string type;
		GameController.questions [index].TryGetValue ("QuestionType", out type);


		//AVAILABLE TYPE OF GAMES
		string[] strArr = null;
		int count = 0;
		char[] splitchar = { ' ' };		
		strArr = type.Split(splitchar);

		bool foundEqual = false;

		for (count = 0; count <= strArr.Length - 1; count++)
		{
			strArr[count].Trim();
			if(currentType == strArr[count])
			{
				foundEqual =true;
			}

		}

		if (currentDificulty != dificultyInt) {
			foundEqual = false;
		}
	
		return foundEqual;
	}



	void SetQuestionValues()
	{
		//PICK RANDOM QUESTION 
		int randomQuestionNumber = 0;
	

		//ENSURE THAT RANDOM QUESTION IS SAME LEVEL DIFICULTY AS CURRENT LEVEL
		/*do {

			randomQuestionNumber = Random.Range (0, GameController.questions.Count);		

		} while (TypeGameEquals(randomQuestionNumber,GameController.CURRENT_LEVEL_TYPE,GameController.CURRENT_LEVEL_DIFICULTY) != true);


*/
		//PICK QUESTION TEXT FROM PREVIOUS NUMBER
		string questionNumber;
		GameController.questions [randomQuestionNumber].TryGetValue ("question", out questionNumber);
		questionHolder.text = questionNumber;



		//RANDOM NUMBER TO PLACE THE CORRECT ANSWER
		int randomCorrectAnswerNumber = Random.Range (0, answers.Length);

		//CORRECT ANSWER
		string correctAnswerText;
		GameController.questions [randomQuestionNumber].TryGetValue ("correct", out correctAnswerText);
		float correctAnswerFloat;
		float.TryParse (correctAnswerText,out correctAnswerFloat);

		//INDEX TO GET THE WRONG ANSWER ON DICTIONARY
		int wrongAnswerNr = 1;


		int j = 1;

		//ASSIGN VALUES
		for (int i= 0; i <answers.Length; i++) {

			if(i == randomCorrectAnswerNumber)
			{
				answers[i].transform.parent.tag = "Certo";
				if(answers[i].transform.parent.GetComponent<ScratchBox>() != null)
					answers[i].transform.parent.GetComponent<ScratchBox>().SetSprites();
				answers[i].text = correctAnswerText;
			}
			else{
				if(randomAnswers == false)
				{
					string wrongAnswerText;
					string questionDictionayKey = "false" + wrongAnswerNr.ToString();
					GameController.questions [randomQuestionNumber].TryGetValue (questionDictionayKey, out wrongAnswerText);
					answers[i].text = wrongAnswerText;

					if(answers[i].transform.parent.GetComponent<ScratchBox>() != null)
						answers[i].transform.parent.GetComponent<ScratchBox>().SetSprites();

					wrongAnswerNr ++;
				}
				else{
					if (i % 2 != 0) {
						answers[i].text =  (correctAnswerFloat + j).ToString();
					}
					else {
						answers[i].text =  (correctAnswerFloat - j).ToString();
					}
					answers[i].transform.parent.tag = "Errado";

					j++;
				}
			}
		}
		GameController.questions.RemoveAt (randomQuestionNumber);

	}

}
