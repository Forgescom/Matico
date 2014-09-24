using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	void Start() {
		Texture2D texture = new Texture2D(128, 128);
		renderer.material.mainTexture = texture;
		int y = 0;
		while (y < texture.height) {
			int x = 0;
			while (x < texture.width) {
				Color color = ((x < y) ? Color.white : Color.gray);
				color.a = 0.5f;
				texture.SetPixel(x, y, color);
				++x;
			}
			++y;
		}
		//texture.Apply();
		texture.SetPixel(80, 80, new Color(1f,1f,1f,0.5f));
		
		                 texture.Apply();
	}



}
