using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Spine.Unity;
using Spine;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;

public class DialogsScript4 : MonoBehaviour
{
    //------------------------引用程式----------------------------
    private CameraFollow cameraFollow;
    public DG_playerController playerController;
    private GameManager gameManager;
    private NPCTask npcTask;
    private DG_EnemyController EnemyController;
    public ExampleGestureHandler gesture;

    //---------------------------頭貼----------------------------
    public string playerName;
    public GameObject characterImageObj; //左邊主角對話框
    private Image characterImage;
    private Sprite sister_angry;
    private Sprite sister_happy;
    private Sprite sister_normal;
    private Sprite sister_oops;
    private Sprite sister_sad;
    private Sprite sister_smile;
    private Sprite sister_monochrome_normal;

    public GameObject otherImageObj; //右邊角色對話框
    private Image otherImage;
    public Sprite book;
    public Sprite king;
    public Sprite hikari;

    private string anotherName;
    private Sprite anotherSprite;
    //----------------------------選擇---------------------------
    //public GameObject choose1;
    //----------------------------對話---------------------------
    public GameObject textBox;

    public Text theText;
    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    private bool isActive = true;
    private bool isTyping = false;
    private bool cancelTyping = false;
    public static bool GameEnd;
    public Text whotalk;
    public float typeSpeed;

    public bool questionBool = false;
    //------------------------過場黑幕---------------------------
    public GameObject FadeOut;
    public GameObject FadeIn;
    Animator fadeOut;

    //-----------------------教學、互動物件變數-------------------------
    public bool teachBlood = false;

    public GameObject Platform;
    public GameObject Platform2; //框框後
    public GameObject kingChiCha;
    public GameObject crystalCharacter;

    public GameObject crystalHikari;
    public GameObject crystalBother;
    public GameObject crystalSister;

    [HideInInspector]
    public bool kingBool = false;
    //------------------Attack----------------------
    //小BOSS
    public BoxCollider2D kingCollider;
    //----------------audio----------------------
    public new AudioSource audio;
    //-----------------其他---------------------
    //public GameObject pause;
	private Color talkNow = new Color(1, 1, 1, 1);
	private Color untalkNow = new Color(.6f, .6f, .6f, 1);
	//-----------------vs
	public GameObject vsKing;
    public GameObject vsBother;
    public GameObject vsSister;

    void Awake()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
        npcTask = GameObject.Find("NPC").GetComponent<NPCTask>();
        characterImage = characterImageObj.GetComponent<Image>();
        otherImage = otherImageObj.GetComponent<Image>();
    }

	void Start() {
        //StaticObject.whoCharacter = 2;
        if (StaticObject.whoCharacter == 1)
        {
            TextAsset textFile1 = Resources.Load("Text/bother4") as TextAsset;
            textFile = textFile1;
            playerName = "卡特";
            sister_angry = Resources.Load("characterImage/bother/bother_angry", typeof(Sprite)) as Sprite;
            sister_happy = Resources.Load("characterImage/bother/bother_happy", typeof(Sprite)) as Sprite;
            sister_normal = Resources.Load("characterImage/bother/bother_normal", typeof(Sprite)) as Sprite;
            sister_oops = Resources.Load("characterImage/bother/bother_oops", typeof(Sprite)) as Sprite;
            sister_sad = Resources.Load("characterImage/bother/bother_sad", typeof(Sprite)) as Sprite;
            sister_smile = Resources.Load("characterImage/bother/bother_normal", typeof(Sprite)) as Sprite;
            sister_monochrome_normal = Resources.Load("characterImage/bother/bother_monochrome_normal", typeof(Sprite)) as Sprite;
            anotherName = "緹緹";
            anotherSprite = Resources.Load("characterImage/sister/sister_happy", typeof(Sprite)) as Sprite;
            vsBother.SetActive(true);
            GameObject sisterObj = GameObject.Find("interactObject/crystal/character/sisterEnd");
            sisterObj.SetActive(true);
            crystalSister.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("redCrystal_people");

        }
        else if (StaticObject.whoCharacter == 2)
        {
            TextAsset textFile1 = Resources.Load("Text/sister4") as TextAsset;
            textFile = textFile1;
            playerName = "緹緹";
            sister_angry = Resources.Load("characterImage/sister/sister_angry", typeof(Sprite)) as Sprite;
            sister_happy = Resources.Load("characterImage/sister/sister_happy", typeof(Sprite)) as Sprite;
            sister_normal = Resources.Load("characterImage/sister/sister_normal", typeof(Sprite)) as Sprite;
            sister_oops = Resources.Load("characterImage/sister/sister_oops", typeof(Sprite)) as Sprite;
            sister_sad = Resources.Load("characterImage/sister/sister_sad", typeof(Sprite)) as Sprite;
            sister_smile = Resources.Load("characterImage/sister/sister_smile", typeof(Sprite)) as Sprite;
            sister_monochrome_normal = Resources.Load("characterImage/sister/sister_monochrome_normal", typeof(Sprite)) as Sprite;
            anotherName = "卡特";
            anotherSprite = Resources.Load("characterImage/bother/bother_happy", typeof(Sprite)) as Sprite;
            vsSister.SetActive(true);
            GameObject botherObj = GameObject.Find("interactObject/crystal/character/botherEnd");
            botherObj.SetActive(true);
            crystalBother.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("blueCrystal_people");
        }

        currentLine = 1;
        endAtLine = 12;

        fadeOut = FadeOut.GetComponent<Animator>();
        
		StartCoroutine("fadeIn");

		if (currentLine > endAtLine)
		{
			isActive = false;
			textBox.SetActive(false);
			GameEnd = false;
		}
		if (textFile != null)
			textLines = (textFile.text.Split('\n'));
		if (endAtLine == 0)
			endAtLine = textLines.Length - 1;
		if (isActive)
			EnableTextBox();
		else
		{
			DisableTextBox();
		}

        StaticObject.nowClass = 4;
        PlayerPrefs.SetFloat("StaticObject.nowClass", StaticObject.nowClass);
    }

	IEnumerator fadeIn()
	{
		FadeIn.SetActive(true);
		fadeOut.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		FadeIn.SetActive(false);
	}

	IEnumerator fadeOutAni()
	{
		yield return new WaitForSeconds(2f);
		FadeOut.SetActive(true);
		fadeOut.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		GameEnd = false;
	}

    public IEnumerator PlatformOpen()
    {
        yield return new WaitForSeconds(1f);
        Platform.SetActive(true);
    }

	public void FixedUpdate()
	{
		if (!isActive)
			return;

		if (currentLine == 1)
		{
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_oops;
        }

        if (currentLine == 2)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_sad;
        }

        if (currentLine == 4 || currentLine == 8)
        {
            whotalk.text = "魔法書籍";
            characterImage.color = untalkNow;
            otherImage.color = talkNow;
            otherImage.sprite = book;
            otherImageObj.SetActive(true);
        }

        if (currentLine == 7 || currentLine == 10)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_angry;
        }

        if (currentLine == 14 || currentLine == 17)
        {
            whotalk.text = "框框";
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = king;
            Joystick.isMove = true;
            otherImageObj.SetActive(false);
            kingBool = true;
        }
        if (currentLine == 16)
        {
            DisableTextBox();
            StartCoroutine("BeforeKing");
        }

        if (currentLine == 20 || currentLine==28)
        {
            whotalk.text = "追光者";
            characterImage.color = untalkNow;
            otherImage.color = talkNow;
            otherImage.sprite = hikari;
            otherImageObj.SetActive(true);
            characterImage.sprite = sister_smile;
            otherImageObj.SetActive(true);
        }

        if (currentLine == 22)
        {
            whotalk.text = anotherName;
            characterImage.color = untalkNow;
            otherImage.color = talkNow;
            otherImage.sprite = anotherSprite;
        }

        if (currentLine == 23)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_happy;
            otherImageObj.SetActive(false);
        }

        if (currentLine == 25)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            otherImage.sprite = sister_smile;
        }

        if (currentLine == 19 || currentLine == 31)
        {
            DisableTextBox();
        }

        if (Input.GetMouseButtonDown(0))
		{
			if (!questionBool)
			{
				if (!isTyping)
				{
					currentLine += 1;
					if (currentLine > endAtLine)
					{
						DisableTextBox();
					}
					else
						StartCoroutine(TextScroll(textLines[currentLine]));
				}
				else if (isTyping && !cancelTyping)
					cancelTyping = true;
			}
		}
	}

	//-------------------------碰撞對話-----------------------------
	/*public void BloodStation()  //補血站
	{
		teachBlood = true;
		currentLine = 6;
		endAtLine = 16;
		NPCAppear();
	}*/


	IEnumerator BeforeKing()
	{
        EnemyController = GameObject.Find("BossEnemy").GetComponent<DG_EnemyController>();
        
        yield return new WaitForSeconds(1);
		gameManager.vsPanel.SetActive(true);
		vsKing.SetActive(true);
		yield return new WaitForSeconds(3);
		gameManager.teachHintAni.SetTrigger("HintOpen");
		gameManager.teachHintText.text = "進入戰鬥";
		gameManager.drawGame.TransitionTo(10f);
		//BossKCollider.enabled = true;
        EnemyController.GetComponent<BoxCollider2D>().enabled = true;
        gameManager.attackRedImage.SetActive(true);
		EnemyController.isAttack = true;
		gameManager.vsPanel.SetActive(false);
		vsKing.SetActive(false);
		EnemyController.HealthCanvas.SetActive(true);
	}

	public IEnumerator AfterKingBattle()
	{
		currentLine = 17;
		endAtLine = 19;
		NPCAppear();
        yield return new WaitUntil(() => currentLine >= 19);
        yield return new WaitForSeconds(1f);
        kingChiCha.SetActive(true);
        Platform2.SetActive(true);

    }

    IEnumerator GetSkill6()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager.eventObj.SetActive(true);
        gameManager.eventText.text = "獲得技能六";
        yield return new WaitForSeconds(0.5f);
        gameManager.ParticleObj6.SetActive(true); //skill Particle
        yield return new WaitForSeconds(0.5f);
        StaticObject.G6 = 1;
        gameManager.G6.SetActive(true);
        PlayerPrefs.SetInt("StaticObject.G6", StaticObject.G6);       
        yield return new WaitForSeconds(1f);
        gameManager.ParticleObj6.SetActive(false);
        gameManager.eventObj.SetActive(false);
    }

    IEnumerator hikariText()
    {
        npcTask.bookObj.GetComponent<Animator>().SetBool("isOpen", false);
        yield return new WaitForSeconds(1.5f);
        crystalCharacter.SetActive(true);
        crystalHikari.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Crystal_not");
        crystalSister.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("redCrystal_not");
        crystalBother.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("blueCrystal_not");
        yield return new WaitForSeconds(1.5f);
        currentLine = 20;
        endAtLine = 31;
        NPCAppear();
        yield return new WaitUntil(() => currentLine >= 31);
        yield return new WaitForSeconds(1f);
        gameManager.win();
    }


    public void NPCAppear()
	{
		EnableTextBox();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "kingCol") //進入框框範圍
		{
            currentLine = 14;
            endAtLine = 16;
            NPCAppear();
            Destroy(col);
        }

        if (col.gameObject.name == "skill6") //吃到技能7
        {
            Destroy(col.gameObject);
            StartCoroutine("GetSkill6");
            
        }

        if (col.gameObject.name == "crystal") //追光者對話
        {
            Destroy(col);
            StartCoroutine("hikariText");
        }

        /*if (col.gameObject.name == "beatuyZoomCollider") //進入選美舞台
		{
			cameraFollow.moveCount = 2;
			cameraFollow.isFollowTarget = false;
			StartCoroutine("beatuyZoom");
            StaticObject.Olivia = 1; //Olivia解鎖
            PlayerPrefs.SetInt("StaticObject.Olivia", StaticObject.Olivia);
            StaticObject.money = 1; //money解鎖
            PlayerPrefs.SetInt("StaticObject.money", StaticObject.money);
            StaticObject.secretK = 1; //secretK解鎖
            PlayerPrefs.SetInt("StaticObject.secretK", StaticObject.secretK);
            StaticObject.Grace = 1; //Grace解鎖
            PlayerPrefs.SetInt("StaticObject.Grace", StaticObject.Grace);
        }

		if (col.gameObject.name == "NPC_Grace")
		{
			currentLine = 107;
			endAtLine = 137;
			NPCAppear();
		}*/
    }

	//----------------------------對話----------------------------
	private IEnumerator TextScroll(string lineOfText)
	{
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
		{
			theText.text += lineOfText[letter];
			//bool 判斷<color> </color>
			/*if (theText.text == "<")
			{
				Debug.Log("12");
				yield return new WaitUntil(() => theText.text==">");		
			}*/

			letter += 1;
			yield return new WaitForSeconds(typeSpeed);

		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}
	public void EnableTextBox()
	{
		playerController.drawCanvas.enabled = false;		
		isActive = true;
		//playerController.cutting = false;
		textBox.SetActive(true);
		gameManager.Pause.interactable = false;
		npcTask.bookBtn.interactable = false;
		StartCoroutine(TextScroll(textLines[currentLine]));
	}
	public void DisableTextBox()
	{
		isActive = false;
		//playerController.cutting = true;
		playerController.drawCanvas.enabled = true;
		textBox.SetActive(false);
		gameManager.Pause.interactable = true;
		npcTask.bookBtn.interactable = true;
	}

}