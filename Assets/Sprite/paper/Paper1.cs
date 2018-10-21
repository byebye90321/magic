using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper1 : MonoBehaviour {

	private DialogsScript dialogsScript;

	public Toggle option1;
	public Toggle option2;
	public Toggle option3;
	public Toggle option4;

	public GameObject Bingo;
	public GameObject Wrong;

	public Button OKButton;
	public GameObject CloseButton;

	public Animator paperClose1;
	public GameObject Ans;
	//private GameObject TouchPanel;
	public GameObject paper1;
	public GameObject blackPanel;

	private Animator mark;

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
		mark = GameObject.Find("obstacle/mark").GetComponent<Animator>();
	}

	public void CheackOption1() {
		StaticObject.card13 = true; //答錯
		StaticObject.card05 = false;
	}

	public void CheackOption2()
	{
		StaticObject.card13 = true; //答錯
		StaticObject.card05 = false;
	}

	public void CheackOption3()
	{
		StaticObject.card13 = true; //答錯
		StaticObject.card05 = false;
	}

	public void CheackOption4()
	{
		StaticObject.card05 = true; //答對
		StaticObject.card13 = false;
	}

	public void OKBtn() {
		
		if (StaticObject.card13 == true)
		{
			ClickParticle.SetActive(true);
			audio.PlayOneShot(wrongSound);
			Wrong.SetActive(true);
			blackPanel.SetActive(true);
		}
		if (StaticObject.card05 == true)
		{
			ClickParticle.SetActive(true);
			audio.PlayOneShot(bingoSound);
			Bingo.SetActive(true);
			blackPanel.SetActive(true);
		}
	}

	public void CloseCard() {
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
		paperClose1.SetBool("PaperClose", true);
		dialogsScript.paper1After();
		StartCoroutine("waitClose");
	}

	IEnumerator waitClose() {
		yield return new WaitForSeconds(0.2f);
		mark.SetInteger("markindex", 1);
		//paper1.SetActive(false);
		Destroy(paper1);
	}
}
