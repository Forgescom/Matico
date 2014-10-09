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


	// Use this for initialization
	void Start () {
		housesXml = GameController.houses;
		housesXml.Clear ();
		ReadXML ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A))
		{
			WriteToXml();
		}
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

	public void WriteToXml()
	{
		
		string filepath = Application.dataPath + "/test.xml";

		print (filepath);
		XmlDocument xmlDoc = new XmlDocument();


		if(File.Exists (filepath))
		{
			for(int i = 0; i<GameController.houses.Count; i++)
			{
				xmlDoc.Load(filepath);
				
				XmlElement elmRoot = xmlDoc.DocumentElement;
				
				//elmRoot.RemoveAll(); // remove all inside the transforms node.
				
				XmlElement elmNew = xmlDoc.CreateElement("level"); // create the rotation node.
				elmNew.SetAttribute("id","Casa"+i.ToString());
				elmNew.SetAttribute("blocked","fuckyou");
				
				
				XmlElement rotation_X = xmlDoc.CreateElement("typeofgame"); // create the x node.
				rotation_X.InnerText = "scratch"; // apply to the node text the values of the variable.
				
				XmlElement rotation_Y = xmlDoc.CreateElement("energiesspent"); // create the y node.
				rotation_Y.InnerText = "1"; // apply to the node text the values of the variable.
				
				XmlElement rotation_Z = xmlDoc.CreateElement("levelofdificulty"); // create the z node.
				rotation_Z.InnerText = "1"; // apply to the node text the values of the variable.
				
				elmNew.AppendChild(rotation_X); // make the rotation node the parent.
				elmNew.AppendChild(rotation_Y); // make the rotation node the parent.
				elmNew.AppendChild(rotation_Z); // make the rotation node the parent.
				elmRoot.AppendChild(elmNew); // make the transform node the parent.
				
				xmlDoc.Save(filepath); // save file.
			}

		}
	}
}
