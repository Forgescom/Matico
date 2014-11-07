using UnityEngine;
using System.Collections;

public class countMenorScr : MonoBehaviour {

	// Use this for initialization
	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	
	void OnGUI() {
		//draw the background:

		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.DrawTexture(new Rect(0,0, size.x, size.y), emptyTex, ScaleMode.StretchToFill);
		
		//draw the filled-in part:

		GUI.BeginGroup(new Rect(0,0, size.x, size.y * (barDisplay*0.1f)));
		GUI.DrawTexture(new Rect(0,0, size.x, size.y), fullTex, ScaleMode.StretchToFill);
		GUI.EndGroup();
		GUI.EndGroup();
	}
	
	void Update() {
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		//barDisplay = Time.time*0.05f;
		//        barDisplay = MyControlScript.staticHealth;
	}
}
