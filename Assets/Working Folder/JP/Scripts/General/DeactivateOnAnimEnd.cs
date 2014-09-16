using UnityEngine;
using System.Collections;

public class DeactivateOnAnimEnd : MonoBehaviour {

	public delegate void EndAnimation();
	public static event EndAnimation animationFinish;

	void Desativar()
	{
		transform.gameObject.SetActive (false);

		if (animationFinish != null) {
			animationFinish();

		}
	}
}
