using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BookInteract : MonoBehaviour
{
	AsyncOperation ToNEXT;

	public Animator book;
	public Animator Image;
	public GameObject fadeIn;

	public Button touchBtn1;
	public Button touchBtn2;
	public GameObject touchPanel1;
	public GameObject touchPanel2;
	public GameObject onBookImage;
	public GameObject Leftinteract;
	public GameObject Rightinteract;

	public GameObject ClickParticleL;
	public GameObject ClickParticleR;

    private Animator fingerAnim;
    public GameObject fingerObj;

	public GameObject Bookintercat;
	public GameObject card;

	public AudioSource audio;
	public AudioClip kiss;
	public AudioClip sword;

	public void Start()
	{	
        fingerAnim = fingerObj.GetComponent<Animator>();
        ToNEXT = SceneManager.LoadSceneAsync("Chapter0_3movie");
        ToNEXT.allowSceneActivation = false;
		StartCoroutine("wait");
		touchBtn1.interactable = false;
		touchBtn2.interactable = false;
		StartCoroutine("FADE");
	}

	public void touch1()
	{
		audio.PlayOneShot(kiss);
		onBookImage.SetActive(false);
		Leftinteract.SetActive(true);
		touchBtn1.interactable = false;
		ClickParticleL.SetActive(false);
		StartCoroutine("wait1");
	}

	public void touch2()
	{
		audio.PlayOneShot(sword);
		onBookImage.SetActive(false);
		Rightinteract.SetActive(true);
		ClickParticleR.SetActive(false);
		StartCoroutine("wait2");
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds(3);
		touchBtn1.interactable = true;
		ClickParticleL.SetActive(true);
	}

	IEnumerator wait1()
	{
		touchPanel1.SetActive(false);
		yield return new WaitForSeconds(2);
		Leftinteract.SetActive(false);
		touchBtn1.interactable = true;
		touchPanel2.SetActive(true);
		onBookImage.SetActive(true);
		Image.SetInteger("index", 1);
		book.SetInteger("index", 1);
		yield return new WaitForSeconds(3);
		ClickParticleR.SetActive(true);
		touchBtn2.interactable = true;
	}

	IEnumerator wait2()
	{
		touchPanel2.SetActive(false);
		yield return new WaitForSeconds(2);
		book.SetInteger("index", 2);
		Bookintercat.SetActive(false);
		card.SetActive(true);
	}

	IEnumerator FADE() {
		yield return new WaitForSeconds(1f);
		fadeIn.SetActive(false);
	}

	public void next() {
		ToNEXT.allowSceneActivation = true;
	}
}
