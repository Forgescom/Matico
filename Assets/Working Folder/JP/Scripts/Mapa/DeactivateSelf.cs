using UnityEngine;
using System.Collections;

public class DeactivateSelf : MonoBehaviour {

	void Desactivar(){
		transform.parent.SendMessage ("RemoveLocker");
	}
	void Deactivate()
	{
		transform.gameObject.SetActive (false);
	}
}

