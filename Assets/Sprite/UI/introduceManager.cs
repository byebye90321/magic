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
		Text.text = "樂天主義者\n與哥哥卡特感情融洽\n是烏托邦的魔法生物迷\n頭上的獸耳與似尾巴的背包\n都是為了喬裝成小動物以便親近牠們\n夢想是希望能征服所有魔法生物的心";  //內文字
		NameText.text = "緹緹"; //名字
		characterImage.sprite = Sister;
	}

	public void borther()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "冷漠沉穩的人\n相當疼愛妹妹緹緹\n是烏托邦的植栽愛好者\n頭上的尖刺能種植花草\n肩上的蝴蝶能傳播花粉\n夢想是打造一座最芬芳的花園";  //內文字
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
		Text.text = "由追光者所撰寫的書籍\n被視為希望與改變的開始\n記錄了各種追求自我\n不受性別限制的研究與發現\n書籍選中之人將成為勇者\n得到繽紛的魔力為世界注入色彩";  //內文字
		NameText.text = "魔法書籍"; //名字
		characterImage.sprite = Book;
	}

	public void bobby()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "生活在森林的魔法生物\n與高大強壯的外表相反\n生性膽小怕生\n雄性頭上生草 雌性則長花\n雄性波比頭上的小花使牠被視為異類\n但牠依然格外珍視這特別的禮物";  //內文字
		NameText.text = "波比"; //名字
		characterImage.sprite = Bobby;
	}

	public void wiki()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "與弟弟維克一同生活在森林某處\n性格單純又火爆\n非常看不慣波比頭上異樣的小花\n總是想盡辦法想拔下它替框行道";  //內文字
		NameText.text = "維吉"; //名字
		characterImage.sprite = Wiki;
	}

	public void wiko()
	{
		back.SetActive(true);
		panel1.SetActive(true);
		Text.text = "與哥哥維吉一同生活在森林某處\n性格憨傻沒主見\n哥哥維吉說什麼都是對的\n總是和哥哥一起以欺負波比為樂";  //內文字
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
        NameText.text = "歪歪J"; //名字
        characterImage.sprite = J;
    }

    public void chichi()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "遍布於框框烏托邦的小型魔物\n身型成圓狀\n宛如那些不平等的竊竊私語般\n到處喧嘩且相當擾人\n令人感到嘈雜又厭惡不已";
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
        Text.text = "看似高雅而神秘的存在\n使用魔法操控著魔杖\n更像是一名臥底\n暗自觀望監控居民的色彩\n沒想到他的真實身分是...";
        NameText.text = "神秘人"; //名字
        characterImage.sprite = SecretK;
    }

    public void grace()
    {
        back.SetActive(true);
        panel1.SetActive(true);
        Text.text = "選美大賽每一屆的主持人\n非常會看風向討好評審\n戴著有色眼鏡面對參賽者\n夢想是成為框框烏托邦中\n最負盛名的主持人";
        NameText.text = "葛雷斯"; //名字
        characterImage.sprite = Grace;
    }

}
