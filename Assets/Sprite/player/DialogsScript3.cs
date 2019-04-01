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

public class DialogsScript3 : MonoBehaviour
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
	/*public GameObject didaObj;
    public SkeletonAnimation didaAni;
	public Sprite dida_monochrome_normal;
	public Sprite dida_monochrome_sad;
	public Sprite dida_monochrome_smile;
	public Sprite dida_rainbow_normal;
	public Sprite dida_rainbow_sad;
	public Sprite dida_beauty;
	public GameObject cocoObj;
    public SkeletonAnimation cocoAni;
    public Sprite coco_monochrome_normal;
	public Sprite coco_monochrome_sad;
	public Sprite coco_monochrome_cry;
	public Sprite coco_rainbow_normal;
	public Sprite coco_rainbow_sad;
	public Sprite coco_beauty;
	public GameObject dragonObj;
    public SkeletonAnimation dragonAni;
    public Sprite dragon_monochrome_normal;
	public Sprite dragon_monochrome_smile;
	public Sprite dragon_monochrome_angry;
	public Sprite dragon_rainbow_smile;
	public Sprite dragon_rainbow_closedEyes;
	public Sprite dragon_beauty;
	public Sprite mirror;
	public Animator GraceAni;
	public Sprite Grace; //主持人
	public Animator OliviaAni;
	public Sprite Olivia;
	public Animator moneyAni;
	public Sprite money_normal;
	public Sprite money_angry;
	public Animator secretAni;
	public Sprite secretK; //神秘人
	public Sprite YYK; //K*/

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

    public GameObject Key;

	//------------------Attack----------------------
	//小BOSS
	public GameObject attackColliderBorder;

	//----------------audio----------------------
	public new AudioSource audio;
    /*public AudioClip cheer;
    public AudioClip quarrel;*/
    //-----------------其他---------------------
    public GameObject pause;
	private Color talkNow = new Color(1, 1, 1, 1);
	private Color untalkNow = new Color(.6f, .6f, .6f, 1);
	public GameObject BEfogParticle;
	//-----------------vs
	public GameObject vsYYK;

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
            TextAsset textFile1 = Resources.Load("Text/bother2") as TextAsset;
            textFile = textFile1;
            playerName = "卡特";
            sister_angry = Resources.Load("characterImage/bother/bother_angry", typeof(Sprite)) as Sprite;
            sister_happy = Resources.Load("characterImage/bother/bother_happy", typeof(Sprite)) as Sprite;
            sister_normal = Resources.Load("characterImage/bother/bother_normal", typeof(Sprite)) as Sprite;
            sister_oops = Resources.Load("characterImage/bother/bother_oops", typeof(Sprite)) as Sprite;
            sister_sad = Resources.Load("characterImage/bother/bother_sad", typeof(Sprite)) as Sprite;
            sister_smile = Resources.Load("characterImage/bother/bother_normal", typeof(Sprite)) as Sprite;
            sister_monochrome_normal = Resources.Load("characterImage/bother/bother_monochrome_normal", typeof(Sprite)) as Sprite;

        }
        else if (StaticObject.whoCharacter == 2)
        {
            TextAsset textFile1 = Resources.Load("Text/sister2") as TextAsset;
            textFile = textFile1;
            playerName = "緹緹";
            sister_angry = Resources.Load("characterImage/sister/sister_angry", typeof(Sprite)) as Sprite;
            sister_happy = Resources.Load("characterImage/sister/sister_happy", typeof(Sprite)) as Sprite;
            sister_normal = Resources.Load("characterImage/sister/sister_normal", typeof(Sprite)) as Sprite;
            sister_oops = Resources.Load("characterImage/sister/sister_oops", typeof(Sprite)) as Sprite;
            sister_sad = Resources.Load("characterImage/sister/sister_sad", typeof(Sprite)) as Sprite;
            sister_smile = Resources.Load("characterImage/sister/sister_smile", typeof(Sprite)) as Sprite;
            sister_monochrome_normal = Resources.Load("characterImage/sister/sister_monochrome_normal", typeof(Sprite)) as Sprite;
        }

        currentLine = 1;
        endAtLine = 4;

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

        StaticObject.nowClass = 2;
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
		SceneManager.LoadScene("Settle");
	}

    public IEnumerator pillarCameraMove()
    {
        yield return new WaitForSeconds(1f);
        cameraFollow.isFollowTarget = false;
        cameraFollow.moveCount = 1;
    }

	public void FixedUpdate()
	{
		if (!isActive)
			return;

		if (currentLine == 1 ||currentLine == 6 || currentLine == 190)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
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
	public void BloodStation()  //補血站
	{
		teachBlood = true;
		currentLine = 6;
		endAtLine = 16;
		NPCAppear();
	}


	/*IEnumerator BeforeKBattle()
	{
        EnemyController = GameObject.Find("BossEnemy").GetComponent<DG_EnemyController>();
        playerAddParticle.SetActive(true);
		addText.SetActive(true);
        UpText.SetActive(true);
        yield return new WaitForSeconds(3);
		gameManager.vsPanel.SetActive(true);
		vsYYK.SetActive(true);
		yield return new WaitForSeconds(3);
		gameManager.teachHintAni.SetTrigger("HintOpen");
		gameManager.teachHintText.text = "進入戰鬥";
		gameManager.drawGame.TransitionTo(10f);
		BossKCollider.enabled = true;
		gesture.AddAttack = 5;
		gameManager.attackRedImage.SetActive(true);
		EnemyController.isAttack = true;
		gameManager.vsPanel.SetActive(false);
		vsYYK.SetActive(false);
		EnemyController.HealthCanvas.SetActive(true);
	}

	public IEnumerator AfterKBattle()
	{
		currentLine = 189;
		endAtLine = 197;
		NPCAppear();
        playerAddParticle.SetActive(false);
        addText.SetActive(false);
        UpText.SetActive(false);
        yield return new WaitUntil(() => currentLine == 197);
	}*/

	public void NPCAppear()
	{
		EnableTextBox();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "battleCollider") //進入K攻擊
		{
			gameManager.drawGame.TransitionTo(10f);
			StartCoroutine("BeforeKBattle");
		}

		if (col.gameObject.name == "beatuyZoomCollider") //進入選美舞台
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
		}
	}
	//----------------------------選擇----------------------------
	/*public void Choose1_gohome() //回家
	{
		currentLine = 208;
		endAtLine = 210;
		NPCAppear();
		choose1.SetActive(false);
		StaticObject.sHE2 = 0;
		StaticObject.sBE2 = 1;
		PlayerPrefs.SetInt("StaticObject.sHE2", StaticObject.sHE2);
		PlayerPrefs.SetInt("StaticObject.sBE2", StaticObject.sBE2);
	}

	public void Choose1_continue() //繼續
	{
		currentLine = 211;
		endAtLine = 213;
		NPCAppear();
		choose1.SetActive(false);
		StaticObject.sHE2 = 1;
		StaticObject.sBE2 = 0;
		PlayerPrefs.SetInt("StaticObject.sHE2", StaticObject.sHE2);
		PlayerPrefs.SetInt("StaticObject.sBE2", StaticObject.sBE2);
	}*/

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