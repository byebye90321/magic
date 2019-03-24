using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AnimationSound : MonoBehaviour {

	public new AudioSource audio;
	public AudioClip Sound;
	public GameObject player;


	void SoundPlay()
	{
		if (Mathf.Abs(this.transform.position.x - player.transform.position.x) < 15)
		{
			audio.PlayOneShot(Sound);
		}
	}

	void AnimatorSoundPlay() //np player
	{
		audio.PlayOneShot(Sound);
		
	}
}
