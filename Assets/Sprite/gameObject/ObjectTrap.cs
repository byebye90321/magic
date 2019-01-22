using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTrap : MonoBehaviour {

	public DG_playerController playerController;
	public Animator characterAni;
	public GameObject healthTextObj;
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
			healthTextObj.SetActive(false);
			CancelInvoke("beaten");
		}
	}

	void beaten()
	{
		if (!this.gameObject.activeInHierarchy)
			CancelInvoke("beaten");

		playerController.TakeDamage(damageInt);
		characterAni.SetTrigger("beaten");
		healthTextObj.SetActive(true);
		healthText.text = "-" + damageInt;
		StartCoroutine("beatens");
	}

	IEnumerator beatens()
	{
		flash.SetTrigger("flash");
		yield return new WaitForSeconds(.5f);
		healthTextObj.SetActive(false);
	}
}
