using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectAnimaton : MonoBehaviour {

	public Animator Ani;
	public string AniName;
	public bool isText;
	public Text AniText;
	public string TextString;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			Ani.SetTrigger(AniName);
			if (isText)
				AniText.text = TextString;
		}
	}
}
