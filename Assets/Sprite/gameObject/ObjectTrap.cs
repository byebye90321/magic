using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTrap : MonoBehaviour {

	public DG_playerController playerController;
    public Animator characterAni;
	//public Animator healthAni;
	private Text healthText;
	public Animator flash;
	public GameObject healthObj;
	public GameObject canvas;

	public int damageInt;

    public void Start()
    {
        if (playerController.ChapterName == "0")
        {
            
        }

        //StaticObject.whoCharacter = 1;
        if (StaticObject.whoCharacter == 1)
        {
            GameObject playB = GameObject.FindWithTag("Player");
            characterAni = playB.GetComponent<Animator>();
        }
        else if (StaticObject.whoCharacter == 2)
        {
            GameObject playS = GameObject.FindWithTag("Player");
            characterAni = playS.GetComponent<Animator>();
        }
    }

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
		
		GameObject NEWatkpreft = Instantiate(healthObj) as GameObject;
		NEWatkpreft.transform.SetParent(canvas.transform, false);		
		NEWatkpreft.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
		healthText = NEWatkpreft.GetComponentInChildren<Text>();
		healthText.text = "-" + damageInt;

		flash.SetTrigger("flash");
		playerController.SpineSister.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
		if(playerController.ChapterName=="0")
			playerController.SpineBother.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
		yield return new WaitForSeconds(.5f);
		playerController.SpineSister.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);
		if (playerController.ChapterName == "0")
			playerController.SpineBother.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);

		Destroy(NEWatkpreft,.5f);
	}
	
}
