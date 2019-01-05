using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTrap : MonoBehaviour {

	public DG_playerController playerController;
	public Animator characterAni;
	public GameObject healthTextObj;
	public Text healthText;
	public GameObject flash;

	public int damageInt;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{

			InvokeRepeating("beaten", 0f, 2f);
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
		playerController.TakeDamage(damageInt);
		characterAni.SetTrigger("beaten");
		healthTextObj.SetActive(true);
		healthText.text = "-" + damageInt;
		StartCoroutine("beatens");
	}

	IEnumerator beatens()
	{
		for (int i = 0; i < 2; i++)
		{
			flash.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			flash.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(.5f);
		healthTextObj.SetActive(false);
	}
}
