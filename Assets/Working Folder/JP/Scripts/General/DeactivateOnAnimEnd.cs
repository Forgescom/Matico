using UnityEngine;
using System.Collections;

public class DeactivateOnAnimEnd : MonoBehaviour {

	void Desativar()
	{
		transform.gameObject.SetActive (false);
	}
}
