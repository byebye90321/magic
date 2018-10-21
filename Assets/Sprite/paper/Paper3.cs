using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper3 : MonoBehaviour {

	private DialogsScript dialogsScript;

	public Toggle option1;
	public Toggle option2;
	public Toggle option3;
	public Toggle option4;

	public GameObject Bingo;
	public GameObject Wrong;

	public Button OKButton;
	public GameObject CloseButton;

	public Animator paperClose3;
	public GameObject Ans;
	//public GameObject TouchPanel;
	public GameObject paper3;
	public GameObject blackPanel;
	
	private Animator flower;
	
	public AudioSource audio;
	public AudioClip bingoSound;
	public AudioClip wrongSound;

	public GameObject ClickParticle;

	public void Start() {
		option1 = option1.GetComponent<Toggle>();
		option2 = option2.GetComponent<Toggle>();
		option3 = option3.GetComponent<Toggle>();
		option4 = option4.GetComponent<Toggle>();
		dialogsScript = GameObject.Find("Player").GetComponent<DialogsScript>();
		flower = GameObject.Find("obstacle/flower").GetComponent<Animator>();
	}

	public void CheackOption1() {
		StaticObject.card10 = true; //答錯
		StaticObject.card02 = false;
	}

	public void CheackOption2()
	{
		StaticObject.card10 = true; //答錯
		StaticObject.card02 = false;
	}

	public void CheackOption3()
	{
		StaticObject.card10 = true; //答錯
		StaticObject.card02 = false;
	}

	public void CheackOption4()
	{	
		StaticObject.card02 = true; //答對
		StaticObject.card10 = false;
	}

	public void OKBtn() {
		if (StaticObject.card10 == true)
		{
			ClickParticle.SetActive(true);
			audio.PlayOneShot(wrongSound);
			Wrong.SetActive(true);
			blackPanel.SetActive(true);
		}
		if (StaticObject.card02 == true)
		{
			ClickParticle.SetActive(true);
			audio.PlayOneShot(bingoSound);
			Bingo.SetActive(true);
			blackPanel.SetActive(true);
		}
	}

	public void CloseCard()
	{
		Wrong.SetActive(false);
		Bingo.SetActive(false);
		ClickParticle.SetActive(false);
		OKButton.interactable = false;
		CloseButton.SetActive(true);
		option1.interactable = false;
		option2.interactable = false;
		option3.interactable = false;
		option4.interactable = false;
		Ans.SetActive(true);
		blackPanel.SetActive(false);
	}

	public void CloseBtn() {
		paperClose3.SetBool("PaperClose", true);
		//TouchPanel.SetActive(true);
		dialogsScript.paper3After();
		StartCoroutine("waitClose");
	}

	IEnumerator waitClose()
	{
		yield return new WaitForSeconds(0.2f);
		//paper3.SetActive(false);
		flower.SetInteger("flowerindex", 1);
		Destroy(paper3);
	}
}
