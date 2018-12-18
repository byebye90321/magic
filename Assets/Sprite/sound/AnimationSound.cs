using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour {

	public AudioSource audio;
	public AudioClip Sound;
	public GameObject player;


	void SoundPlay()
	{
		if (Mathf.Abs(this.transform.position.x - player.transform.position.x) < 15)
		{
			audio.PlayOneShot(Sound);
		}
	}
}
