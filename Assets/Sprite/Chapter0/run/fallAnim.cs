using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallAnim : MonoBehaviour {
	
	//Cystal
	Animator Cystal;
	public GameObject obstacleCystal_UP;
	bool CystalUp = false;

	//Bug
	Animator Bug;
	public GameObject obstacleBug_Up;
	bool BugUp = false;

	void Start () {
		Cystal = obstacleCystal_UP.GetComponent<Animator>();
		Bug = obstacleBug_Up.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "obstacleCystal_Collider")
		{
			CystalUp = true;
			if (CystalUp)
			{
				Cystal.SetBool("up", true);
			}
		}

		if (col.gameObject.name == "obstacleBug_Collider")
		{
			BugUp = true;
			if (BugUp)
			{
				Bug.SetBool("up", true);
			}
		}

	}
}
