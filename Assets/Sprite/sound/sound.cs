using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Button))]
public class sound : MonoBehaviour {
	public Button musicBtn;
	public Button sfxBtn;

	public Sprite On;
	public Sprite Off;

	private int counterMusic = 1;
	private int counterSfx = 1;

	public Slider musicSlider;
	public Slider sfxSlider;

	private float BgmVolume;
	private float SfxVolume;

	public AudioMixer masterMixer;

	void Start () {		
		BgmVolume = PlayerPrefs.GetFloat("StaticObject.bgmVolume", StaticObject.bgmVolume);
		SfxVolume = PlayerPrefs.GetFloat("StaticObject.sfxVolume", StaticObject.sfxVolume);

		musicSlider.value = BgmVolume;
		sfxSlider.value = SfxVolume;
	}

	//Music開關
	public void musicOnOff()
	{
		counterMusic++;
		if (counterMusic % 2 == 0)
		{
			musicBtn.image.overrideSprite = Off;
			musicSlider.value = -80;

			//全域
			StaticObject.bgmVolume = musicSlider.value;
			PlayerPrefs.SetFloat("StaticObject.bgmVolume", musicSlider.value);
		}
		else
		{
			musicBtn.image.overrideSprite = On;
			musicSlider.value = 0;

			//全域
			StaticObject.bgmVolume = musicSlider.value;
			PlayerPrefs.SetFloat("StaticObject.bgmVolume", musicSlider.value);
		}
	}

	//Sfx開關
	public void sfxOnOff()
	{
		counterSfx++;
		if (counterSfx % 2 == 0)
		{
			sfxBtn.image.overrideSprite = Off;
			sfxSlider.value = -80;

			//全域
			StaticObject.sfxVolume = sfxSlider.value;
			PlayerPrefs.SetFloat("StaticObject.sfxVolume", sfxSlider.value);
		}
		else
		{
			sfxBtn.image.overrideSprite = On;
			sfxSlider.value = 0;

			//全域
			StaticObject.sfxVolume = sfxSlider.value;
			PlayerPrefs.SetFloat("StaticObject.sfxVolume", sfxSlider.value);
		}
	}

	//music音量
	public void Music_volume(float music)
	{
		//全域
			StaticObject.bgmVolume = musicSlider.value;
		PlayerPrefs.SetFloat("StaticObject.bgmVolume", musicSlider.value);
		masterMixer.SetFloat(("music"), music);
		if (musicSlider.value == -80)
		{
			musicBtn.image.overrideSprite = Off;
		}
		else {
			musicBtn.image.overrideSprite = On;
		}

	}

	//sfx音量
	public void Sfx_volume(float sfx)
	{
		//全域
		StaticObject.sfxVolume = sfxSlider.value;
		PlayerPrefs.SetFloat("StaticObject.sfxVolume", sfxSlider.value);
		masterMixer.SetFloat(("sfx"), sfx);
		if (sfxSlider.value == -80)
		{
			sfxBtn.image.overrideSprite = Off;
		}
		else
		{
			sfxBtn.image.overrideSprite = On;
		}

	}
}
