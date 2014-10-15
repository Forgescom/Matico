using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;


public class XmlLoader : MonoBehaviour {

	List<Dictionary<string,string>> housesXml;
	Dictionary<string,string> houseDetails;

	public TextAsset xmlFile;
	string xmlToString;
	FileInfo fileXmlHouses;

	// Use this for initialization
	void Start () {
		housesXml = GameController.houses;
		housesXml.Clear ();

		fileXmlHouses = new FileInfo (Application.persistentDataPath + "\\" + "levelsInfo.xml");
	

		xmlToString = xmlFile.text;

		if(!fileXmlHouses.Exists)
		{
			Save(xmlToString);
			Load();
		}
		else
		{
			Load();
		}
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A))
		{
			WriteToXml();
	

		}
	}

	void Save(string fileToSave)
	{
		StreamWriter w;

		if(!fileXmlHouses.Exists)
		{
			w = fileXmlHouses.CreateText();
			w.WriteLine(fileToSave);
		}
		else{
			fileXmlHouses.Delete();
			w = fileXmlHouses.CreateText();
			print (fileToSave);
			w.WriteLine(fileToSave);
		}
		w.Close ();
	}

	void Load()
	{
		StreamReader r = File.OpenText (Application.persistentDataPath + "\\" + "levelsInfo.xml");
		string info = r.ReadToEnd ();

		r.Close ();
		xmlToString = info;
		ReadXML ();
	}

	void ReadXML()
	{
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (xmlToString);
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

		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml(xmlToString);
		
		XmlElement elmRoot = xmlDoc.DocumentElement;
		
		elmRoot.RemoveAll(); // remove all inside the transforms node.
		
		for(int i = 0; i<housesXml.Count; i++)
		{				
			XmlElement elmNew = xmlDoc.CreateElement("level"); // create the rotation node.
			elmNew.SetAttribute("id",housesXml [i]["HouseName"]);
			elmNew.SetAttribute("blocked",housesXml [i]["Blocked"]);				

			XmlElement typeofgame = xmlDoc.CreateElement("typeofgame"); // create the x node.
			typeofgame.InnerText = housesXml[i]["Typeofgame"]; // apply to the node text the values of the variable.
			
			XmlElement energiesspent = xmlDoc.CreateElement("energiesspent"); // create the y node.
			energiesspent.InnerText = housesXml[i]["EnergiesSpent"]; // apply to the node text the values of the variable.
			
			XmlElement levelofdificulty = xmlDoc.CreateElement("levelofdificulty"); // create the z node.
			levelofdificulty.InnerText = housesXml[i]["Dificulty"]; // apply to the node text the values of the variable.
			
			elmNew.AppendChild(typeofgame); // make the rotation node the parent.
			elmNew.AppendChild(energiesspent); // make the rotation node the parent.
			elmNew.AppendChild(levelofdificulty); // make the rotation node the parent.
			elmRoot.AppendChild(elmNew); // make the transform node the parent.

		}
		
		xmlToString = xmlDoc.InnerXml;
		
		Save (xmlToString);
					
	}
}
