using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introduceManager : MonoBehaviour {

	public GameObject back;
	public GameObject panel3;
	public ScrollRect scrollRect;
	public Text Text;
	public Text NameText;
	public Image characterImage;

	public Sprite Sister;
	public Sprite Book;
	public Sprite Bobby;
	public Sprite Wiki;
	public Sprite Wiko;

	//-----------------關閉暗底
	public void backColse()
	{
		scrollRect.verticalNormalizedPosition = 1;
		back.SetActive(false);
		panel3.SetActive(false);
	}



	//-------------------角色介紹
	public void sister()
	{
		
		back.SetActive(true);
		panel3.SetActive(true);
		Text.text = "天生的樂天主義  很珍惜哥哥送的頭巾\n和哥哥 - 卡特的感情融洽\n受到驚嚇時耳朵和尾巴總會不經意冒的出來\n十分喜愛框框烏托邦的各種魔法生物\n不理解為什麼女孩子不可以爬樹或是和野獸玩\n夢想是希望有一天可以靠自己收服所有魔法生物的心";  //內文字
		NameText.text = "緹緹"; //名字
		characterImage.sprite = Sister;
	}

	public void borther()
	{
		back.SetActive(true);
		panel3.SetActive(true);
		Text.text = "總是一臉冷漠的待人  用尖刺保護自己\n但其實內心溫暖似火  如同肩膀上的蝴蝶\n把所有溫柔給了妹妹與花兒\n偷偷的研究各種妖精 花草們\n期望有一天可以創造出一座最美麗芬芳\n不管男孩女孩都可以一同欣賞的花園";  //內文字
		NameText.text = "卡特"; //名字
		//characterImage.sprite = Bother;
	}

	public void book()
	{
		back.SetActive(true);
		panel3.SetActive(true);
		Text.text = "貌似由一名自稱追光者的獨立思想家所撰寫\n裡面記錄著各種打破框架的研究與發現\n聽說翻閱此書的人將獲得新的魔力色彩";  //內文字
		NameText.text = "魔法書籍"; //名字
		characterImage.sprite = Book;
	}

	public void bobby()
	{
		back.SetActive(true);
		panel3.SetActive(true);
		Text.text = "生活在森林某處\n男孩頭上只會長草 女孩頭上只會長花\n與高大強壯的外表相反  其實生性膽小怕生\n受到驚嚇時會不經意發出「波比╴波比」的叫聲\n雖然很崇拜爸爸頭上強韌的小草\n但仍然喜愛頭上這朵跟媽媽一樣\n特別而溫柔的花朵";  //內文字
		NameText.text = "波比"; //名字
		characterImage.sprite = Bobby;
	}

	public void wiki()
	{
		back.SetActive(true);
		panel3.SetActive(true);
		Text.text = "與維克是一對性格單純火爆的兄弟\n認為所有雄性魔法生物就該展現威武的一面\n看著柔弱的波比還有頭上異樣的花朵很不順眼\n總是想盡辦法要拔下波比頭上的花";  //內文字
		NameText.text = "維吉"; //名字
		characterImage.sprite = Wiki;
	}

	public void wiko()
	{
		back.SetActive(true);
		panel3.SetActive(true);
		Text.text = "與維吉是一對性格單純火爆的兄弟\n崇拜維吉的男子氣概\n維吉說什麼都是對的";  //內文字
		NameText.text = "維克"; //名字
		characterImage.sprite = Wiko;
	}

}
