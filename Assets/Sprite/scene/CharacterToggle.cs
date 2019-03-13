using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterToggle : MonoBehaviour
{

	public Toggle bother;
	public Toggle sister;
	public GameObject botherText;
	public GameObject sisterText;
	public GameObject OKBtn;
	public GameObject light1;
	public GameObject light2;
	public GameObject fadeIn;

	// Use this for initialization
	void Start()
	{
		bother = bother.GetComponent<Toggle>();
		sister = sister.GetComponent<Toggle>();
		StartCoroutine("FadeIn");

        StaticObject.sister = 1; //妹妹解鎖
        PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
        StaticObject.bother = 1; //哥哥解鎖
        PlayerPrefs.SetInt("StaticObject.bother", StaticObject.bother);
        StaticObject.hikari = 1; //追光者解鎖
        PlayerPrefs.SetInt("StaticObject.hikari", StaticObject.hikari);
    }

	public void bortherClick()
	{
		botherText.SetActive(true);
		sisterText.SetActive(false);
		OKBtn.SetActive(false); //確定鍵
		light1.SetActive(true);
		light2.SetActive(false);
	}
	public void sisterClick()
	{
		sisterText.SetActive(true);
		botherText.SetActive(false);
		OKBtn.SetActive(true);
		light2.SetActive(true);
		light1.SetActive(false);
	}

	IEnumerator FadeIn() {
		yield return new WaitForSeconds(1);
		fadeIn.SetActive(false);

	}


}
