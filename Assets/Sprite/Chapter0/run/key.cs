using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour {
	public GameObject winFade;
	Animator fade;
	// Use this for initialization
	void Start () {
		fade = winFade.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.tag == "Player")
		{
			RunGameManager.Instance.win();
		}
	}

}
