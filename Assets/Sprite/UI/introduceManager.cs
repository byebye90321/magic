using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introduceManager : MonoBehaviour {

	public GameObject back;
	public GameObject panel1;
	public ScrollRect scrollRect;
	public Text Text;
	public Text NameText;
	public Image characterImage;

	public Sprite Sister;
	public Sprite Bother;
	public Sprite Hikari;
    public Sprite Book;
	public Sprite Bobby;
	public Sprite Wiki;
	public Sprite Wiko;
    public Sprite EnemyKing;
    public Sprite K;
    public Sprite Q;
    public Sprite J;
  
    public Sprite Chichi;
    public Sprite Chacha;

    public Sprite Dida;
    public Sprite Coco;
    public Sprite Dragon;
    public Sprite Olivia;
    public Sprite OliviaDog;
    public Sprite Money;
    public Sprite SecretK;
    public Sprite Grace;
    //-----------------關閉暗底
    public void backColse()
	{
		scrollRect.verticalNormalizedPosition = 1;
		back.SetActive(false);
		panel1.SetActive(false);
	}

	//-------------------角色介紹
	public void sister()
	{
		
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "天生的樂天主義  很珍惜哥哥送的頭巾\n和哥哥 - 卡特的感情融洽\n受到驚嚇時耳朵和尾巴總會不經意冒的出來\n十分喜愛框框烏托邦的各種魔法生物\n不理解為什麼女孩子不可以爬樹或是和野獸玩\n夢想是希望有一天可以靠自己收服所有魔法生物的心";  //內文字
		NameText.text = "緹緹"; //名字
		characterImage.sprite = Sister;
	}

	public void borther()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "總是一臉冷漠的待人  用尖刺保護自己\n但其實內心溫暖似火  如同肩膀上的蝴蝶\n把所有溫柔給了妹妹與花兒\n偷偷的研究各種妖精 花草們\n期望有一天可以創造出一座最美麗芬芳\n不管男孩女孩都可以一同欣賞的花園";  //內文字
		NameText.text = "卡特"; //名字
        characterImage.sprite = Bother;
    }

    public void hikari()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "獨立思想家\n奮力對抗黑魔法並堅持自我\n認為個體發展不該因性別受拘束\n雖憑一己之力仍不敵反派而被囚禁\n卻堅信自己埋下的希望種子\n將隨著勇者的到來萌芽\n";  //內文字
        NameText.text = "追光者"; //名字
        characterImage.sprite = Hikari;
    }

    public void book()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "貌似由一名自稱追光者的獨立思想家所撰寫\n裡面記錄著各種打破框架的研究與發現\n聽說翻閱此書的人將獲得新的魔力色彩";  //內文字
		NameText.text = "魔法書籍"; //名字
		characterImage.sprite = Book;
	}

	public void bobby()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "生活在森林某處\n男孩頭上只會長草 女孩頭上只會長花\n與高大強壯的外表相反  其實生性膽小怕生\n受到驚嚇時會不經意發出「波比╴波比」的叫聲\n雖然很崇拜爸爸頭上強韌的小草\n但仍然喜愛頭上這朵跟媽媽一樣\n特別而溫柔的花朵";  //內文字
		NameText.text = "波比"; //名字
		characterImage.sprite = Bobby;
	}

	public void wiki()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "與維克是一對性格單純火爆的兄弟\n認為所有雄性魔法生物就該展現威武的一面\n看著柔弱的波比還有頭上異樣的花朵很不順眼\n總是想盡辦法要拔下波比頭上的花";  //內文字
		NameText.text = "維吉"; //名字
		characterImage.sprite = Wiki;
	}

	public void wiko()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "與維吉是一對性格單純火爆的兄弟\n崇拜維吉的男子氣概\n維吉說什麼都是對的";  //內文字
		NameText.text = "維克"; //名字
		characterImage.sprite = Wiko;
	}

    public void enemyKing()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "控制框框烏托邦的大魔王\n不達目的絕不罷休\n自傲的性格決心要剷除異己\n他的到來使世界僅剩紅色與藍色\n卻無人知曉他是從何而來\n為何出現";
        NameText.text = "框框"; //名字
        characterImage.sprite = EnemyKing;
    }

    public void k()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "歪歪K是框框最得力的手下\n非常聽從大魔王的命令\n尊崇與迷戀著框框\n發誓要為了大魔王效命至死";
        NameText.text = "歪歪K"; //名字
        characterImage.sprite = K;
    }

    public void q()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "歪歪Q是歪歪J的好夥伴\n特徵是頭上那三個刺刺的東西\n負責監視與管理居民\n並用框框給予的黑魔法教訓抗命之人";
        NameText.text = "歪歪Q"; //名字
        characterImage.sprite = Q;
    }

    public void j()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "歪歪J與歪歪Q經常結伴而行\n特徵是頭上那兩根觸鬚\n協助歪歪Q巡視居民狀況\n一旦有異樣便立刻追捕處理";
        NameText.text = "框J"; //名字
        characterImage.sprite = J;
    }

    public void chichi()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "遍布於框框烏托邦的小型魔物\n身型成扁圓狀\n宛如那些不平等的竊竊私語般\n到處喧嘩且相當擾人\n令人感到嘈雜又厭惡不已";
        NameText.text = "嘰嘰"; //名字
        characterImage.sprite = Chichi;
    }

    public void chacha()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "遍布於框框烏托邦的小型魔物\n身型成扁圓狀\n宛如那些不平等的竊竊私語般\n到處喧嘩且相當擾人\n令人感到嘈雜又厭惡不已";
        NameText.text = "喳喳"; //名字
        characterImage.sprite = Chacha;
    }

    public void dida()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "曾經相當美麗的女子\n因為歲月而老去的她\n不甘於女性只因外表而被嫌棄\n更討厭女性老了就沒價值這種說法\n努力善用時間魔法尋找改變契機";
        NameText.text = "滴答"; //名字
        characterImage.sprite = Dida;
    }

    public void coco()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "悲觀又樂觀的夢想追尋者\n熱衷於女裝、化妝等嗜好\n投入興趣中感到滿足之時\n卻也因為性別氣質不同常人而難過\n善用化妝魔法努力探尋自我";
        NameText.text = "可可"; //名字
        characterImage.sprite = Coco;
    }

    public void dragon()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "要拉長音的說「龍~」才是牠的名字\n身為龍~一族因此身材有點寬\n雖然在族中視為可愛\n但仍嚮往人類中瘦即美的看法\n變形魔法讓牠能融入人群";
        NameText.text = "龍~"; //名字
        characterImage.sprite = Dragon;
    }

    public void olivia()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "身為前屆選美冠軍的奧莉薇\n氣場逼人、魅力無邊\n深得評審們的青睞\n被認為是框框烏托邦中最女人的女人\n不知自己正被物化而依然自豪著";
        NameText.text = "奧莉薇"; //名字
        characterImage.sprite = Olivia;
    }

    public void oliviaDog()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "汪！";
        NameText.text = "奧莉薇的狗"; //名字
        characterImage.sprite = OliviaDog;
    }

    public void money()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "有錢就是任性!\n是錢多多的經典口頭禪\n喜歡欣賞、評鑑美女\n做為選美大賽最大的贊助商\n使他更能口無遮攔的品頭論足";
        NameText.text = "錢多多"; //名字
        characterImage.sprite = Money;
    }

    public void secretK()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "";
        NameText.text = "神秘人"; //名字
        characterImage.sprite = SecretK;
    }

    public void grace()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "曾經相當美麗的女子\n因為歲月而老去的她\n不甘於女性只因外表而被嫌棄\n更討厭女性老了就沒價值這種說法\n努力善用時間魔法尋找改變契機";
        NameText.text = "葛雷斯"; //名字
        characterImage.sprite = Grace;
    }

}
