using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour {

	//public GameObject s1;
	public GameObject sHE1;
	public GameObject sBE1;

	void Start () {
		if (DialogsScript.sHE1 == true)
		{			
			sHE1.SetActive(true);
			DialogsScript.sHE1 = false;
		}
		else if (DialogsScript.sBE1 == true)
		{
			sBE1.SetActive(true);
			DialogsScript.sBE1 = false;
		}
	}

}
