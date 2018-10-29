using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
	public GameObject winFade;
	Animator fade;
	// Use this for initialization
	void Start()
	{
		fade = winFade.GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == "Player")
		{
			DG_GameManager.Instance.win();
		}
	}

}
