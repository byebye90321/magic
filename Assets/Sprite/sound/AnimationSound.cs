using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour {

	public AudioSource audio;
	public AudioClip boxSound;
	public AudioClip crystalSound;

	public GameObject player;
	public GameObject box;
	public GameObject crystal;

	void BoxSound() {
		if (Mathf.Abs(box.transform.position.x - player.transform.position.x) < 15)
		{
			audio.PlayOneShot(boxSound);
		}
	}

	void CrystalSound()
	{
		if (Mathf.Abs(crystal.transform.position.x - player.transform.position.x) < 15)
		{
			audio.PlayOneShot(crystalSound);
		}
	}

}
