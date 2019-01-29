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
	public GameObject healthObj;

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
		//healthAni.SetTrigger("hurtText");
		//healthText.text = "-" + damageInt;
		GameObject NEWatkpreft = (GameObject)Instantiate(healthObj) as GameObject;
		NEWatkpreft.transform.position = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.5f, 4.0f), 0);

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
