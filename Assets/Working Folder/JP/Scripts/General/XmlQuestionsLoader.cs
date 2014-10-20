using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class XmlQuestionsLoader : MonoBehaviour {

//	public delegate void XmlReady();
//	public static event XmlReady xmlLoaded;

	public TextAsset xmlFile;
	public static List<Dictionary<string,string>> questions = new List<Dictionary<string, string>>();
	Dictionary<string,string> questionDetails;

	// Use this for initialization
	void Start () {
		questions = GameController.questions;
		questions.Clear ();
		ReadXML ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void ReadXML()
	{
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlFile.text);
		XmlNodeList housesXmlNodes = xmlDoc.GetElementsByTagName ("questionDetails");
		
		foreach (XmlNode levelInfo in housesXmlNodes) {
			XmlNodeList levelContent = levelInfo.ChildNodes;
			questionDetails = new Dictionary<string,string >();
			
			questionDetails.Add("QuestionNumber",levelInfo.Attributes["id"].Value);
			questionDetails.Add("QuestionLevel",levelInfo.Attributes["level"].Value);
			questionDetails.Add("QuestionType",levelInfo.Attributes["typeofgame"].Value);
			
			foreach(XmlNode levelsItems in levelContent)
			{
				switch(levelsItems.Name){
					
				case "question": questionDetails.Add("question",levelsItems.InnerText); break;
				case "false1": questionDetails.Add("false1",levelsItems.InnerText); break;
				case "false2": questionDetails.Add("false2",levelsItems.InnerText); break;
				case "false3": questionDetails.Add("false3",levelsItems.InnerText); break;
				case "correct": questionDetails.Add("correct",levelsItems.InnerText); break;
				}
				
			}
			questions.Add(questionDetails);
			
		}

		/*if (xmlLoaded != null) {
			xmlLoaded();
			
		}*/

		//	questionHolder.transform.SendMessage ("LoadHousesSettings");
		
	}
}
