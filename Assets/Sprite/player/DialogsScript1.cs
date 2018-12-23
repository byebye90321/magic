using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Spine.Unity;
using Spine;
using UnityEngine.Audio;

public class DialogsScript1 : MonoBehaviour
{
	//------------------------引用程式----------------------------
	public CameraFollow cameraFollow;
	public DG_playerController playerController;
	public GameManager gameManager;
	public NPCTask npcTask;
	//--------------------------互動對話-----------------------------
	public GameObject vine2text;
	private int bobbyCount = 1;
	
	//---------------------------頭貼----------------------------
	public GameObject characterImageObj; //左邊主角對話框
	private Image characterImage;
	public Sprite sister_angry;
	public Sprite sister_happy;
	public Sprite sister_normal;
	public Sprite sister_opps;
	public Sprite sister_sad;
	public Sprite sister_smile;
	public GameObject otherImageObj; //右邊角色對話框
	private Image otherImage;
	public Sprite book;
	public Sprite bobby_normal;
	public Sprite bobby_cry;


	//----------------------------選擇---------------------------

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

	//------------------------過場黑幕---------------------------
	public GameObject FadeOut;
	public GameObject FadeIn;
	Animator fadeOut;

	//--------------------------場景-----------------------------

	//------------------------結局相關---------------------------
	//關卡樹狀圖，非圖鑑樹狀圖，不存檔
	/*public static bool sHE1 = false;
	public static bool sBE1 = false;

	public GameObject he;
	public GameObject be;*/

	//-----------------------教學、互動物件變數-------------------------
	public bool teachBlood = false;
	public GameObject markObj; //路標對話框
	private Animator markAni;
	private BoxCollider2D markCollider;
	
	private BoxCollider2D statueCollider;
	//------------------Attack----------------------
	public GameObject attackCollider;
	private BoxCollider2D attackColliderCol;
	public GameObject attackColliderBorder;
	//----------------audio----------------------
	public AudioSource audio;
	public AudioMixerSnapshot usually;
	//-----------------其他---------------------
	public GameObject pause;
	private Color talkNow = new Color(1, 1, 1, 1);
	private Color untalkNow = new Color(.6f, .6f, .6f, 1);


	void Start() {

		usually.TransitionTo(10f);
		fadeOut = FadeOut.GetComponent<Animator>();
		characterImage = characterImageObj.GetComponent<Image>();
		otherImage = otherImageObj.GetComponent<Image>();
		markAni = markObj.GetComponent<Animator>();
		markCollider = markObj.GetComponent<BoxCollider2D>();
		attackColliderCol = attackCollider.GetComponent<BoxCollider2D>();

		StaticObject.sister = 1; //魔法日報解鎖
		PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
		StaticObject.book = 1; //魔法日報解鎖
		PlayerPrefs.SetInt("StaticObject.book", StaticObject.book);
		
		currentLine = 1;
		endAtLine = 9;
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

	public void FixedUpdate()
	{

		if (currentLine == 1)
		{
			if (isTyping == false)
			{
				//theText.text = testText.text;
				//theText.text = "這是<color=#00ffffff>哪裡?</color>看起來好痛啊！我要不是應該去幫助他咧?";
			}
		}

		if (!isActive)
			return;

		if (currentLine == 1 || currentLine == 2 || currentLine == 8 || currentLine == 15 ||currentLine ==38 || currentLine == 40)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_opps;
		}
		if (currentLine == 3 || currentLine == 12)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_normal;
		}
		if (currentLine == 4 || currentLine == 13 || currentLine == 16 || currentLine == 17)
		{
			whotalk.text = "魔法書籍";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImageObj.SetActive(true);
			otherImage.sprite = book;
		}
		if (currentLine == 5 || currentLine == 6 || currentLine == 7)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 9)
		{
			whotalk.text = "魔法書籍";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			if (isTyping == false)
			{
				theText.text = "(每個新場景前端會設立<color=#FF8888>補血站</color>，就在前方，站上去試試。)";
			}
		}
		if (currentLine == 11 || currentLine == 43)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
		}

		if (currentLine == 19)  //書本飛翔
		{
			npcTask.bookFly();
			DisableTextBox();
			StartCoroutine("BloodFlyAfter");
		}
		if (currentLine == 20)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImageObj.SetActive(false);
			characterImage.sprite = sister_angry;
		}
		if (currentLine == 22)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			characterImage.sprite = sister_sad;
		}

		if (currentLine == 24 || currentLine == 26 || currentLine == 52)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_opps;
		}

		if (currentLine == 25 || currentLine == 27 || currentLine == 31)
		{
			whotalk.text = "波比";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImageObj.SetActive(true);
			otherImage.sprite = bobby_cry; //-------------------------要替換成bobby_cry頭貼
		}

		if (currentLine == 29 || currentLine == 33 || currentLine ==51)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 32)
		{
			npcTask.taskPanel.SetActive(true);
			DisableTextBox();
		}

		if (currentLine == 34)
		{
			whotalk.text = "波比";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			if (isTyping == false)
			{
				theText.text = "嘰嘰喳喳佔領了一些地方，<color=#FF8888>形石</color>或許在牠們身上，路上小心...";
			}
		}

		if (currentLine == 35)
		{
			cameraFollow.isFollowTarget = false;  //準備開啟平台
			cameraFollow.moveCount = 1;
			DisableTextBox();
		}

		if (currentLine == 36)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_sad;
		}

		if (currentLine == 37)
		{
			npcTask.BobbyCollider.enabled = true;
			DisableTextBox();
		}

		if (currentLine == 38)
		{
			otherImageObj.SetActive(false);
		}

		if (currentLine == 39)
		{
			markAni.SetBool("isOpen", true);
		}

		if (currentLine == 41)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_smile;
		}

		if (currentLine == 42)
		{
			//DialogsPanelAni.SetBool("isOpen", false);
			markCollider.enabled = false;
			DisableTextBox();
		}

		if (currentLine == 46)
		{
			cameraFollow.moveCount = 4;
		}

		if (currentLine == 47||currentLine==60 || currentLine == 62 || currentLine == 67 || currentLine == 69)
		{
			DisableTextBox();
		}
		if (currentLine == 53)
		{
			npcTask.StatueCollider.enabled = true;
			DisableTextBox();
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (!isTyping)
			{
				currentLine += 1;
				if (currentLine > endAtLine)
				{
					DisableTextBox();
					//npcDisappeaar();
				}
				else
					StartCoroutine(TextScroll(textLines[currentLine]));
			}
			else if (isTyping && !cancelTyping)
				cancelTyping = true;
		}

	}

	//-------------------------碰撞對話-----------------------------
	public void BloodStation()  //補血站
	{
		teachBlood = true;
		currentLine = 11;
		endAtLine = 19;
		NPCAppear();
	}

	IEnumerator BloodFlyAfter()
	{
		yield return new WaitForSeconds(1.5f);
		currentLine = 20;
		endAtLine = 20;
		NPCAppear();	
	}

	IEnumerator cameraToBalance()  
	{
		cameraFollow.isFollowTarget = false; //看向歪斜天平
		cameraFollow.moveCount = 3;
		yield return new WaitForSeconds(2);
		currentLine = 45;
		endAtLine = 47;
		NPCAppear();
		yield return new WaitUntil(()=>currentLine == 47);
		cameraFollow.moveCount = 0;
		cameraFollow.isFollowTarget = true;
	}

	IEnumerator BeforeBossBattle()
	{
		yield return new WaitForSeconds(2);
		currentLine = 57;
		endAtLine = 60;
		NPCAppear();
		yield return new WaitUntil(() => currentLine == 60);
		gameManager.teachHint.SetActive(true);
		gameManager.attackRedImage.SetActive(true);
	}

	public IEnumerator AfterBossBattle()
	{
		currentLine = 61;
		endAtLine = 62;
		NPCAppear();
		yield return new WaitUntil(() => currentLine == 62);

	}

	public void NPCAppear()
	{
		EnableTextBox();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "NPC_Bobby" &&bobbyCount == 1) //遇到波比對話
		{
			bobbyCount = 0;
			currentLine = 22;
			endAtLine = 22;
			NPCAppear();
		}

		if (col.gameObject.name == "mark") //遇到路標對話
		{
			currentLine = 38;
			endAtLine = 42;
			NPCAppear();
		}

		if (col.gameObject.name == "vin2text") //藤蔓對話
		{
			currentLine = 43;
			endAtLine = 43;
			NPCAppear();
			Destroy(col.gameObject);
		}

		if (col.gameObject.name == "balanceText") //看見天平對話
		{
			StartCoroutine("cameraToBalance");
			Destroy(col.gameObject);
		}

		if (col.gameObject.name == "battleCollider") //進入小BOSS攻擊
		{
			cameraFollow.moveCount = 6;
			cameraFollow.isFollowTarget = false;
			attackColliderCol.enabled = false;
			attackColliderBorder.SetActive(true); //開啟邊界
			gameManager.drawGame.TransitionTo(10f);
			StartCoroutine("BeforeBossBattle");
		}
	}
	//----------------------------選擇----------------------------

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
			if (theText.text == "<")
			{
				Debug.Log("12");
				yield return new WaitUntil(() => theText.text==">");		
			}

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
		textBox.SetActive(true);
		gameManager.Pause.interactable = false;
		npcTask.bookBtn.interactable = false;
		StartCoroutine(TextScroll(textLines[currentLine]));
	}
	public void DisableTextBox()
	{
		isActive = false;
		playerController.drawCanvas.enabled = true;
		textBox.SetActive(false);
		gameManager.Pause.interactable = true;
		npcTask.bookBtn.interactable = true;
	}

}