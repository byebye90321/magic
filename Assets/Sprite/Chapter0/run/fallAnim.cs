using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallAnim : MonoBehaviour {
	

	Animator fall;
	Animation fallAnimation;
	Animator up;
	Animation upAnimation;
	public GameObject collider;
	public GameObject collider2;
	bool isFall = false;
	bool isUp = false;
	// Use this for initialization
	void Start () {
		up = collider2.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "fall")
		{
			isFall = true;
			if (isFall)
			{
				fall.SetBool("fallAnim", true);
			}
		}
		if (col.tag == "obstacleUp")
		{
			isUp = true;
			if (isUp)
			{
				up.SetBool("up", true);
			}
		}

	}
}
