using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {
	public static Hurt hurt;

	//--------------音效
	public AudioSource audio;
	public AudioClip hurtSound;
	void Start () {
		hurt = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "obstacle")
		{
			audio.PlayOneShot(hurtSound);
			RG_playerController.Player.Hurt();
		}
	}
}
