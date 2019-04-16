using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterToggle : MonoBehaviour
{
    public string ChapterName;
	public Toggle bother;
	public Toggle sister;
	public GameObject botherText;
	public GameObject sisterText;
	public GameObject OKBtn;
	public GameObject light1;
	public GameObject light2;
	public GameObject fadeIn;

    //關卡選擇
    public Button Chapter1;
    public Button Chapter2;


	void Start()
	{
        bother = bother.GetComponent<Toggle>();
        sister = sister.GetComponent<Toggle>();
        if (ChapterName == "characterChoose")
        {
            StartCoroutine("FadeIn");
            StaticObject.sister = 1; //妹妹解鎖
            PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
            StaticObject.bother = 1; //哥哥解鎖
            PlayerPrefs.SetInt("StaticObject.bother", StaticObject.bother);
            StaticObject.hikari = 1; //追光者解鎖
            PlayerPrefs.SetInt("StaticObject.hikari", StaticObject.hikari);
            StaticObject.ad0 = 1; //歷程圖解鎖
            PlayerPrefs.SetInt("StaticObject.ad0", StaticObject.ad0);
        }
        else  //關卡選擇
        {
            classSister();
        }

    }

	public void bortherClick()
	{
		botherText.SetActive(true);
		sisterText.SetActive(false);
		light1.SetActive(true);
		light2.SetActive(false);
        StaticObject.whoCharacter = 1;
        PlayerPrefs.SetInt("StaticObject.whoCharacter", StaticObject.whoCharacter);
	}
	public void sisterClick()
	{
		sisterText.SetActive(true);
		botherText.SetActive(false);
		OKBtn.SetActive(true);
		light2.SetActive(true);
		light1.SetActive(false);
        StaticObject.whoCharacter = 2;
        PlayerPrefs.SetInt("StaticObject.whoCharacter", StaticObject.whoCharacter);
    }

	IEnumerator FadeIn() {
		yield return new WaitForSeconds(1);
		fadeIn.SetActive(false);

	}

    //--------------------選關處用(選擇關卡)-------------------
    public void classBorther()
    {
        StaticObject.whoCharacter = 1;
        PlayerPrefs.SetInt("StaticObject.whoCharacter", StaticObject.whoCharacter);
    }

    public void classSister()
    {
        StaticObject.whoCharacter = 2;
        PlayerPrefs.SetInt("StaticObject.whoCharacter", StaticObject.whoCharacter);
    }


}
