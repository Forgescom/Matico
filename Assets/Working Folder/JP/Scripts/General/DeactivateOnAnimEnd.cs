using UnityEngine;
using System.Collections;

public class DeactivateOnAnimEnd : MonoBehaviour {

	public delegate void EndAnimation();
	public static event EndAnimation animationFinish;

	void Desativar()
	{


		if (animationFinish != null) {
			animationFinish();

		}
		transform.gameObject.SetActive (false);
	}
}
