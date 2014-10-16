using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	
    void OnTriggerEnter2D(Collider2D col)
    {
		print ("Colidiu");
        //destroyers are located in the borders of the screen
        //if something collides with them, the'll destroy it
        string tag = col.gameObject.tag;
        if(tag == "Panda" || tag == "Target" || tag == "Bamboo")
        {
            Destroy(col.gameObject);
        }
    }
}
