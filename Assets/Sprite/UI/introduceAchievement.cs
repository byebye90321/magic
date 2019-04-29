using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introduceAchievement : MonoBehaviour
{

    public GameObject back;
    public GameObject panel1;
    public ScrollRect scrollRect;
    public Text Text;
    public Text NameText;
    public Image characterImage;

    public Sprite a01;
    public Sprite a02;
    public Sprite a03;
    public Sprite a04;
    public Sprite a05;
    public Sprite a06;
    public Sprite a07;
    public Sprite a08;
    public Sprite a09;
    public Sprite a10;
    public Sprite a11;
    public Sprite a12;
    public Sprite a13;
    public Sprite a14;
    public Sprite a15;
    public Sprite a16;
    public Sprite a17;
    public Sprite a18;
    public Sprite a19;
    public Sprite a20;
    public Sprite a21;
    public Sprite a22;
    public Sprite a23;
    public Sprite a24;
    public Sprite a25;
    public Sprite a26;
    public Sprite a27;
    public Sprite a28;
    public Sprite a29;
    public Sprite a30;
    public Sprite a31;
    public Sprite a32;
    public Sprite a33;

    //-----------------關閉暗底
    public void backColse()
    {
        scrollRect.verticalNormalizedPosition = 1;
        back.SetActive(false);
        panel1.SetActive(false);
    }

    //-------------------角色介紹
    public void a5()
    {

        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "跌到紅色水域";  //內文字
        NameText.text = "哎呀腳一滑"; //名字
        characterImage.sprite = a05;
    }

    public void a8()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "幫助天平回復平衡";  //內文字
        NameText.text = "平衡超平衡"; //名字
        characterImage.sprite = a08;
    }



}
