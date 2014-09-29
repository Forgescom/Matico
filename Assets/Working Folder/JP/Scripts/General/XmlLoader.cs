using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class XmlLoader : MonoBehaviour {

	public TextAsset xmlFile;
	List<Dictionary<string,string>> housesXml;
	Dictionary<string,string> houseDetails;
	void Awake()
	{
		//housesXml = transform.GetComponent<BoardMain> ().houses;
		housesXml = GameController.houses;
		ReadXML ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReadXML()
	{
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlFile.text);
		XmlNodeList housesXmlNodes = xmlDoc.GetElementsByTagName ("level");

		foreach (XmlNode levelInfo in housesXmlNodes) {
			XmlNodeList levelContent = levelInfo.ChildNodes;
			houseDetails = new Dictionary<string,string >();

			houseDetails.Add("HouseName",levelInfo.Attributes["id"].Value);
			houseDetails.Add("Blocked",levelInfo.Attributes["blocked"].Value);

			foreach(XmlNode levelsItems in levelContent)
			{
				switch(levelsItems.Name){

				case "typeofgame": houseDetails.Add("Typeofgame",levelsItems.InnerText); break;
				case "energiesspent": houseDetails.Add("EnergiesSpent",levelsItems.InnerText); break;
				case "levelofdificulty": houseDetails.Add("Dificulty",levelsItems.InnerText); break;
				}

			}
			housesXml.Add(houseDetails);

		}

		//transform.SendMessage ("LoadHousesSettings");

	}
}
