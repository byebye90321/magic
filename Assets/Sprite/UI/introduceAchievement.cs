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
    public void aa5()
    {

        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "跌到紅色水域";  //內文字
        NameText.text = "哎呀腳一滑"; //名字
        characterImage.sprite = a05;
    }

    public void aa8()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "幫助天平回復平衡";  //內文字
        NameText.text = "平衡超平衡"; //名字
        characterImage.sprite = a08;
    }

    public void aa10()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "抵達選美會場";  //內文字
        NameText.text = "跟著居民進來看熱鬧"; //名字
        characterImage.sprite = a10;
    }

    public void aa11()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "拜訪可可的住所";  //內文字
        NameText.text = "高樓上是誰？"; //名字
        characterImage.sprite = a11;
    }

    public void aa14()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "探訪龍~居住的橋";  //內文字
        NameText.text = "這才不是尼斯湖"; //名字
        characterImage.sprite = a14;
    }

    public void aa18()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "得到傳說中的最終技能";  //內文字
        NameText.text = "我這有本秘技"; //名字
        characterImage.sprite = a18;
    }
}
