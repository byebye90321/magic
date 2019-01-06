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

public class DialogsScript1 : MonoBehaviour
{
	//------------------------引用程式----------------------------
	public CameraFollow cameraFollow;
	public DG_playerController playerController;
	public GameManager gameManager;
	public NPCTask npcTask;
	public DG_EnemyController EnemyController;
	public DG_EnemyController MonsterController;
	//--------------------------互動對話-----------------------------
	public GameObject vine2text;
	private int bobbyCount = 1;

	//---------------------------頭貼----------------------------
	public GameObject characterImageObj; //左邊主角對話框
	private Image characterImage;
	public Sprite sister_angry;
	public Sprite sister_happy;
	public Sprite sister_normal;
	public Sprite sister_oops;
	public Sprite sister_sad;
	public Sprite sister_smile;
	public GameObject otherImageObj; //右邊角色對話框
	private Image otherImage;
	public Sprite book;
	public Sprite bobby_rainbow_normal;
	public Sprite bobby_rainbow_happy;
	public Sprite bobby_monochrome_cry;
	public Sprite statue_rainbow;
	public Sprite statue_monochrom;
	public Sprite wiki_normal;
	public Sprite wiki_attack;
	public Sprite wiki_cry;
	public Sprite wiki_good;
	public Sprite wiko_oops;
	public Sprite wiko_attack;
	public Sprite wiko_cry;
	public Sprite wiko_good;


	//----------------------------選擇---------------------------
	public GameObject choose1;
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

	public GameObject statueObj;
	private BoxCollider2D statueCollider;

	public GameObject vine3;
	private BoxCollider2D vine3Collider;

	public GameObject mark2Obj;
	private BoxCollider2D mark2Collider;
	private Animator mark2Ani;
	//------------------Attack----------------------
	//小BOSS
	public GameObject attackCollider;
	private BoxCollider2D attackColliderCol;
	public GameObject attackColliderBorder;

	//維吉維克
	public GameObject monsterCollider;
	private BoxCollider2D monsterColliderCol;
	public GameObject monsterColliderBorder;
	//----------------audio----------------------
	public AudioSource audio;
	//-----------------其他---------------------
	public GameObject pause;
	private Color talkNow = new Color(1, 1, 1, 1);
	private Color untalkNow = new Color(.6f, .6f, .6f, 1);
	public GameObject BEfogParticle;


	void Start() {

		//usually.TransitionTo(10f);
		fadeOut = FadeOut.GetComponent<Animator>();
		characterImage = characterImageObj.GetComponent<Image>();
		otherImage = otherImageObj.GetComponent<Image>();
		markAni = markObj.GetComponent<Animator>();
		markCollider = markObj.GetComponent<BoxCollider2D>();
		attackColliderCol = attackCollider.GetComponent<BoxCollider2D>();
		monsterColliderCol = monsterCollider.GetComponent<BoxCollider2D>();
		statueCollider = statueObj.GetComponent<BoxCollider2D>();
		vine3Collider = vine3.GetComponent<BoxCollider2D>();
		mark2Collider = mark2Obj.GetComponent<BoxCollider2D>();
		mark2Ani = mark2Obj.GetComponent<Animator>();
		/*StaticObject.sister = 1; //魔法日報解鎖
		PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
		StaticObject.book = 1; //魔法日報解鎖
		PlayerPrefs.SetInt("StaticObject.book", StaticObject.book);*/

		currentLine = 1;
		endAtLine = 10;
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

		if (currentLine == 1 || currentLine == 2 || currentLine == 8 || currentLine == 15 || currentLine == 38 || currentLine == 40)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
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

		if (currentLine == 10)
		{
			otherImageObj.SetActive(false);
			DisableTextBox();
		}

		if (currentLine == 11)
		{
			otherImageObj.SetActive(true);
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
			Joystick.isMove = true;
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
			otherImageObj.SetActive(false);
		}

		if (currentLine == 24 || currentLine == 26 || currentLine == 45 || currentLine == 52)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
		}

		if (currentLine == 25 || currentLine == 27 || currentLine == 31)
		{
			whotalk.text = "波比";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImageObj.SetActive(true);
			otherImage.sprite = bobby_monochrome_cry; 
		}

		if (currentLine == 29 || currentLine == 51)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 32)
		{
			npcTask.taskPanel.SetActive(true);
			otherImageObj.SetActive(false);
			DisableTextBox();
		}

		if (currentLine == 33)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
			otherImageObj.SetActive(true);
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
			otherImageObj.SetActive(false);
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
			markCollider.enabled = false;
			DisableTextBox();
		}

		if (currentLine == 43)
		{
			otherImageObj.SetActive(false);
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
		}

		if (currentLine == 45)
		{
			otherImageObj.SetActive(false);
			cameraFollow.moveCount = 3;
		}

		if (currentLine == 46)
		{
			cameraFollow.moveCount = 4;
		}

		if (currentLine == 47 || currentLine == 60 || currentLine == 62 || currentLine == 67 )
		{
			DisableTextBox();
		}

		if (currentLine == 48 || currentLine == 49 || currentLine == 55)
		{
			whotalk.text = "雕像";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = statue_monochrom;
		}

		if (currentLine == 50)
		{
			npcTask.StatueCollider.enabled = true;
			npcTask.taskPanel.SetActive(true);
			DisableTextBox();
		}

		if (currentLine == 53)
		{
			npcTask.StatueCollider.enabled = true;
			DisableTextBox();
		}
		if (currentLine == 54)
		{
			whotalk.text = "雕像";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = statue_rainbow;
		}
		if (currentLine == 55)
		{
			whotalk.text = "雕像";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = statue_monochrom;
		}

		if (currentLine == 57)  
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
			otherImageObj.SetActive(false);
		}

		if (currentLine == 59)  
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
			Joystick.isMove = true;
		}

		if(currentLine == 61)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
		}

		if (currentLine == 63 || currentLine == 64)
		{
			whotalk.text = "波比";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = bobby_rainbow_normal;
		}

		if (currentLine == 65 || currentLine == 66)
		{
			whotalk.text = "波比";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = bobby_rainbow_happy;
		}

		if (currentLine == 68 || currentLine == 69)
		{
			whotalk.text = "波比";
			characterImageObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = bobby_monochrome_cry;
		}

		if (currentLine == 70 || currentLine == 82)
		{
			characterImageObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
			DisableTextBox();
		}

		if (currentLine == 71 || currentLine ==73)
		{
			whotalk.text = "維吉";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = wiki_normal;
		}

		if (currentLine == 72 ||currentLine==74)
		{
			otherImageObj.SetActive(true);
			whotalk.text = "維克";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = wiko_oops;
		}

		if (currentLine == 75)
		{
			whotalk.text = "維克維克";
			characterImage.color = talkNow;
			otherImage.color = talkNow;
			characterImage.sprite = wiki_attack;
			otherImage.sprite = wiko_attack;
		}

		if (currentLine == 76)
		{
			otherImageObj.SetActive(false);
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 81)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 79)
		{
			Joystick.isMove = true;
			DisableTextBox();
		}

		if (currentLine == 83)
		{
			otherImageObj.SetActive(true);
			vine3Collider.enabled = true;
			whotalk.text = "維吉";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			characterImage.sprite = sister_angry;
			otherImage.sprite = wiki_cry;
		}
		if (currentLine == 84)
		{
			whotalk.text = "維克";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			characterImage.sprite = sister_angry;
			otherImage.sprite = wiko_cry;
		}
		if (currentLine == 85)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}
		if (currentLine == 86)
		{
			whotalk.text = "維吉";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = wiki_good;
		}
		if (currentLine == 87)
		{
			whotalk.text = "維克";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = wiko_good;
		}
		if (currentLine == 88)
		{
			whotalk.text = "緹緹";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
			otherImageObj.SetActive(false);
		}
		if (currentLine==89 || currentLine == 96)
		{
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
		yield return new WaitUntil(() => currentLine >= 47);
		cameraFollow.moveCount = 0;
		cameraFollow.isFollowTarget = true;
	}

	IEnumerator BeforeBossBattle()
	{
		yield return new WaitForSeconds(2);
		currentLine = 57;
		endAtLine = 60;
		NPCAppear();
		yield return new WaitUntil(() => currentLine >= 60);
		//gameManager.teachHint.SetActive(true);
		gameManager.teachHintAni.SetTrigger("HintOpen");
		gameManager.attackRedImage.SetActive(true);
		EnemyController.isAttack = true;
		yield return new WaitForSeconds(.1f);
	}

	public IEnumerator AfterBossBattle()
	{
		currentLine = 61;
		endAtLine = 62;
		NPCAppear();
		yield return new WaitUntil(() => currentLine == 62);
	}

	IEnumerator BeforeMonsterBattle()
	{
		yield return new WaitForSeconds(2);
		currentLine = 71;
		endAtLine = 79;
		NPCAppear();
		yield return new WaitUntil(() => currentLine >= 79);
		choose1.SetActive(true);  //開啟任務面板
		yield return new WaitForSeconds(.1f);
	}

	public IEnumerator AfterMonsterBattle()
	{
		currentLine = 83;
		endAtLine = 89;
		NPCAppear();
		yield return new WaitUntil(() => currentLine == 89);
	}

	public void NPCAppear()
	{
		EnableTextBox();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "NPC_Bobby" && bobbyCount == 1) //遇到波比對話
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

		if (col.gameObject.name == "monsterCollider") //進入維吉維克攻擊
		{
			cameraFollow.moveCount = 8;
			cameraFollow.isFollowTarget = false;
			monsterColliderCol.enabled = false;
			monsterColliderBorder.SetActive(true); //開啟邊界
			gameManager.drawGame.TransitionTo(10f);
			StartCoroutine("BeforeMonsterBattle");
		}

		if (col.gameObject.name == "mark2") //遇到路標2
		{
			if (StaticObject.sHE1 == 1) //拯救
			{
				mark2Ani.SetBool("save", true);
			}
			else
			{
				mark2Ani.SetBool("noSave", true);
			}
			mark2Collider.enabled = false;
		}
	}
	//----------------------------選擇----------------------------
	public void Choose1_save() //拯救
	{
		currentLine = 81;
		endAtLine = 82;
		NPCAppear();
		choose1.SetActive(false);
		StartCoroutine("waitMonsterAttack");
	}

	public void Choose1_NoSave() //不拯救
	{
		currentLine = 94;
		endAtLine = 96;
		NPCAppear();
		choose1.SetActive(false);
		StartCoroutine("noMonsterAttack");
	}

	IEnumerator waitMonsterAttack()  //拯救-戰鬥
	{
		StaticObject.sHE1 = 1;
		StaticObject.sBE1 = 0;
		PlayerPrefs.SetInt("StaticObject.sHE1", StaticObject.sHE1);
		PlayerPrefs.SetInt("StaticObject.sBE1", StaticObject.sBE1);
		yield return new WaitUntil(() => currentLine ==82);
		//gameManager.teachHint.SetActive(true);
		gameManager.teachHintAni.SetTrigger("HintOpen");
		gameManager.attackRedImage.SetActive(true);
		MonsterController.isAttack = true;
		MonsterController.enemy2Transform.localRotation = Quaternion.Euler(0, 180, 0);
		MonsterController.enemy2Transform.position = new Vector2(50.8f, 3.38f);
	}

	IEnumerator noMonsterAttack()  //不拯救-不戰鬥
	{
		cameraFollow.moveCount = 9;
		StaticObject.sHE1 = 0;
		StaticObject.sBE1 = 1;
		PlayerPrefs.SetInt("StaticObject.sHE1", StaticObject.sHE1);
		PlayerPrefs.SetInt("StaticObject.sBE1", StaticObject.sBE1);
		yield return new WaitUntil(() => currentLine == 96);
		vine3Collider.enabled = true;
		yield return new WaitForSeconds(0.3f);
		cameraFollow.isFollowTarget = true;
		MonsterController.isAttack = false; //維吉維克不戰鬥
		MonsterController.HealthCanvas.SetActive(false);
		monsterColliderBorder.SetActive(false);
		BEfogParticle.SetActive(true);
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
		playerController.cutting = false;
		textBox.SetActive(true);
		gameManager.Pause.interactable = false;
		npcTask.bookBtn.interactable = false;
		StartCoroutine(TextScroll(textLines[currentLine]));
	}
	public void DisableTextBox()
	{
		isActive = false;
		playerController.cutting = true;
		playerController.drawCanvas.enabled = true;
		textBox.SetActive(false);
		gameManager.Pause.interactable = true;
		npcTask.bookBtn.interactable = true;
	}

}