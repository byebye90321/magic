using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BookInteract : MonoBehaviour
{
	AsyncOperation ToNEXT;

	public Animator book;  //書本底圖動畫
	public Animator bookImage;  //圖案漸入動畫
	public GameObject fadeIn;  //白色淡入

	public GameObject touchPanel1;
	public GameObject touchPanel2;

	public GameObject onBookImage;  //點擊後動畫
	public GameObject SonwInteract;
	public GameObject RedInteract;

	//點擊/提示特效
	public GameObject ClickParticleL;
	public GameObject ClickParticleR;
    private Animator fingerAnim;
    public GameObject fingerObj;

	public GameObject Bookintercat;
	public GameObject skill;

	public AudioSource audio;
	public AudioClip kiss;
	public AudioClip sword;

	public GameObject blackImage;


	public void Start()
	{
		fingerAnim = fingerObj.GetComponent<Animator>();
        ToNEXT = SceneManager.LoadSceneAsync("Chapter0_3movie"); //預載場景
        ToNEXT.allowSceneActivation = false;
		StartCoroutine("Startwait");
		touchPanel1.SetActive(false);
		StartCoroutine("FADE");
	}

	public void touch1()
	{
		audio.PlayOneShot(kiss);
		onBookImage.SetActive(false);
		SonwInteract.SetActive(true);
		ClickParticleL.SetActive(false);
		fingerObj.SetActive(false);
		StartCoroutine("wait1");
	}

	public void touch2()
	{
		audio.PlayOneShot(sword);
		onBookImage.SetActive(false);
		RedInteract.SetActive(true);
		ClickParticleR.SetActive(false);
		fingerObj.SetActive(false);
		StartCoroutine("wait2");
	}

	IEnumerator Startwait()
	{
		yield return new WaitForSeconds(3);
		touchPanel1.SetActive(true);
		fingerObj.SetActive(true);
	}

	IEnumerator wait1()
	{
		touchPanel1.SetActive(false);
		yield return new WaitForSeconds(2);
		SonwInteract.SetActive(false);
		onBookImage.SetActive(true);
		bookImage.SetInteger("index", 1);
		book.SetInteger("index", 1);
		yield return new WaitForSeconds(3);
		touchPanel2.SetActive(true);
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 1);
	}

	IEnumerator wait2()
	{
		touchPanel2.SetActive(false);
		yield return new WaitForSeconds(2);
		book.SetInteger("index", 2);
		Bookintercat.SetActive(false);
		blackImage.SetActive(true);
		skill.SetActive(true);
	}

	IEnumerator FADE() {
		yield return new WaitForSeconds(1f);
		fadeIn.SetActive(false);
	}

	public void next() {
		ToNEXT.allowSceneActivation = true;
	}
}
