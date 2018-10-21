using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryBGM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(GameObject.FindGameObjectWithTag("sound"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
