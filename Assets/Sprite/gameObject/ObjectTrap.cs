using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTrap : MonoBehaviour {

	public DG_playerController playerController;
	public Animator characterAni;
	public Animator healthAni;
	public Text healthText;
	public Animator flash;

	public int damageInt;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			InvokeRepeating("beaten", 0f, 1f);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			CancelInvoke("beaten");
		}
	}

	void beaten()
	{
		if (!this.gameObject.activeInHierarchy)
			CancelInvoke("beaten");

		playerController.TakeDamage(damageInt);
		characterAni.SetTrigger("beaten");	
		StartCoroutine("beatens");
	}

	IEnumerator beatens()
	{
		healthAni.SetTrigger("hurtText");
		healthText.text = "-" + damageInt;
		flash.SetTrigger("flash");
		playerController.SpineSister.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
		if(playerController.ChapterName=="0")
			playerController.SpineBother.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
		yield return new WaitForSeconds(.5f);
		playerController.SpineSister.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);
		if (playerController.ChapterName == "0")
			playerController.SpineBother.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);
	}
}
