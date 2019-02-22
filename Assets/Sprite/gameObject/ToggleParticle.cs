using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleParticle : MonoBehaviour {

	public DialogsScript2 dialogsScript2;
	public GameObject beatuy;
	public Toggle toggle;
	public GameObject button;
	public GameObject particle;

	public void isOn()
	{
		if (toggle.isOn)
		{
			button.SetActive(true);
			particle.SetActive(true);
		}
		else
		{
			particle.SetActive(false);
		}
	}

	public void beatuyClose()
	{
		beatuy.SetActive(false);
		dialogsScript2.currentLine = 138;
		dialogsScript2.endAtLine = 140;
		dialogsScript2.NPCAppear();
	}
}
