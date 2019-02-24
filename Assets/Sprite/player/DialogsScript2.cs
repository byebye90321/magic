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

public class DialogsScript2 : MonoBehaviour
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
	public string playerName;
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
	public GameObject didaObj;
	public Sprite dida_monochrome_normal;
	public Sprite dida_monochrome_sad;
	public Sprite dida_monochrome_smile;
	public Sprite dida_rainbow_normal;
	public Sprite dida_rainbow_sad;
	public Sprite dida_beauty;
	public GameObject cocoObj;
	public Sprite coco_monochrome_normal;
	public Sprite coco_monochrome_sad;
	public Sprite coco_monochrome_cry;
	public Sprite coco_rainbow_normal;
	public Sprite coco_rainbow_sad;
	public Sprite coco_beauty;
	public GameObject dragonObj;
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

	public bool questionBool = false;
	//------------------------過場黑幕---------------------------
	public GameObject FadeOut;
	public GameObject FadeIn;
	Animator fadeOut;

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

	public GameObject MaskGroup;
	//2
	public GameObject clock;
	public bool Mirror;
	public GameObject card;
	[HideInInspector]
	public Animator cardAni;
	public Animator MirrorAni;
	public GameObject beatuy; //玩家角色選美頁面
	public GameObject AudienceTalk1; //觀眾對話1
	private Animator AudienceTalkAni1;
	public GameObject AudienceTalk2; //觀眾對話2
	private Animator AudienceTalkAni2;


	public GameObject beatuySmokeObj; //變形的煙
	public GameObject beatutMember; //選美型態的參賽者

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
	//-----------------vs
	public GameObject vsWiko;
	public GameObject vsYYJ;

	void Start() {

		//usually.TransitionTo(10f);
		fadeOut = FadeOut.GetComponent<Animator>();
		characterImage = characterImageObj.GetComponent<Image>();
		otherImage = otherImageObj.GetComponent<Image>();
		/*StaticObject.sister = 1; //妹妹解鎖
		PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
		StaticObject.book = 1; //魔法日報解鎖
		PlayerPrefs.SetInt("StaticObject.book", StaticObject.book);*/
		cardAni = card.GetComponent<Animator>();
		AudienceTalkAni1 = AudienceTalk1.GetComponent<Animator>();
		AudienceTalkAni2 = AudienceTalk2.GetComponent<Animator>();
		currentLine = 1;
		endAtLine = 4;
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
		if (!isActive)
			return;

		if (currentLine == 1 ||currentLine == 6)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
		}

		if (currentLine == 2 || currentLine == 7 || currentLine == 64 || currentLine == 70 || currentLine == 81)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_normal;
		}

		if (currentLine == 4 || currentLine == 11 || currentLine == 24 || currentLine == 79 || currentLine == 83 ||currentLine==154 || currentLine == 174)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
			characterImageObj.SetActive(true);
		}

		if (currentLine == 8 || currentLine==22)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_sad;
		}

		if (currentLine == 31)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_sad;
			npcTask.DidaCollider.enabled = true;
		}
		

		if (currentLine == 9 || currentLine == 12 || currentLine == 14)
		{
			whotalk.text = "魔法書籍";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = book;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 13 || currentLine == 16 || currentLine == 27 || currentLine == 73 || currentLine == 77)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_smile;
			Joystick.isMove = true;
		}

		if (currentLine == 18)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
			otherImageObj.SetActive(false);
		}

		if (currentLine == 19 || currentLine == 21 || currentLine == 25 || currentLine == 28|| currentLine == 95)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_monochrome_normal;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 26)
		{
			npcTask.taskPanel.SetActive(true);
			npcTask.TaskBtn.SetActive(true);
			DisableTextBox();
		}

		if (currentLine == 20)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_monochrome_sad;
		}

		if (currentLine == 23 || currentLine == 89)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_monochrome_smile;
		}

		if (currentLine == 30)
		{
			cameraFollow.isFollowTarget = false;
			cameraFollow.moveCount = 1;
			DisableTextBox();
		}
		

		if (currentLine == 33)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_normal;
			otherImageObj.SetActive(false);
		}

		if (currentLine == 35 || currentLine == 42 || currentLine == 47)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = coco_monochrome_sad;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 36)
		{
			//otherImageObj.SetActive(false);
			npcTask.cocoTaskStart();
			currentLine = 0;
		}

		if (currentLine == 37 || currentLine == 62 || currentLine == 65 || currentLine == 71)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = coco_monochrome_normal;
		}

		if (currentLine == 57 || currentLine == 63)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = coco_monochrome_cry;
		}

		if (currentLine == 57 || currentLine == 63)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = coco_monochrome_cry;
		}

		if (currentLine == 74)
		{
			DisableTextBox();
			npcTask.TaskFinish();
		}

		if (currentLine == 75)
		{
			whotalk.text = "龍~";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dragon_monochrome_angry;
		}

		if (currentLine == 76 || currentLine == 80 || currentLine == 82 || currentLine == 85)
		{
			whotalk.text = "龍~";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dragon_monochrome_smile;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 78 || currentLine == 84)
		{
			whotalk.text = "龍~";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dragon_monochrome_normal;
		}

		if (currentLine == 86)
		{
			DisableTextBox();
			clock.SetActive(true);
		}

		if (currentLine == 87)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_happy;
			otherImageObj.SetActive(false);
		}

		if (currentLine == 91)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_smile;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 92 ||currentLine==96)
		{
			DisableTextBox();
		}

		if (currentLine == 93)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_monochrome_sad;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 97 || currentLine == 101 || currentLine == 103)
		{
			whotalk.text = "魔鏡";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = mirror;
			otherImageObj.SetActive(false);
		}

		if (currentLine == 98)
		{
			whotalk.text = playerName;
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = sister_normal;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 100)
		{
			whotalk.text = playerName;
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = sister_smile;
		}

		if (currentLine == 102)
		{
			if (Mirror)
			{
				whotalk.text = playerName;
				characterImage.color = untalkNow;
				otherImage.color = talkNow;
				otherImage.sprite = sister_angry;
				otherImageObj.SetActive(true);
				endAtLine = 105;
			}
			else
			{
				DisableTextBox();
			}
		}

		if (currentLine == 105)
		{
			DisableTextBox();
			MirrorAni.SetTrigger("mirrorOpen");
		}

		if (currentLine == 107)
		{
			whotalk.text = "葛雷斯";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = Grace;
			GraceAni.SetTrigger("talk");
		}

		if (currentLine == 110)
		{
			whotalk.text = playerName;
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = sister_oops;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 112 || currentLine == 117 || currentLine == 120 || currentLine == 124 || currentLine == 132 || currentLine == 138 ||currentLine==155)
		{
			whotalk.text = "葛雷斯";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = Grace;
			otherImageObj.SetActive(false);
			GraceAni.SetTrigger("talk");
		}
		if (currentLine == 116)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_beauty;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 119)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = coco_beauty;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 122)
		{
			whotalk.text = "龍~";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dragon_beauty;
			otherImageObj.SetActive(true);
		}

		if (currentLine == 123)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_normal;
			otherImageObj.SetActive(false);
		}

		if (currentLine == 127 || currentLine == 156)
		{
			whotalk.text = "奧莉薇";
			characterImage.color = talkNow;
			otherImage.color = talkNow;
			characterImage.sprite = Olivia;
			OliviaAni.SetTrigger("talk");
		}

		if (currentLine == 129 )
		{
			whotalk.text = "錢多多";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = money_normal;
			moneyAni.SetTrigger("talk");
		}

		if (currentLine == 158)
		{
			whotalk.text = "錢多多";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = money_angry;
			moneyAni.SetTrigger("talk");
		}

		if (currentLine == 131)
		{
			whotalk.text = "神秘人";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = secretK;
			secretAni.SetTrigger("talk");
		}

		if (currentLine == 136)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_normal;
		}

		if (currentLine == 137)
		{
			DisableTextBox();
			beatuy.SetActive(true);
		}

		if (currentLine == 140)
		{
			DisableTextBox();
			AudienceTalk1.SetActive(true);
		}

		if (currentLine == 148 || currentLine == 167)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
			
		}

		if (currentLine == 151)
		{
			whotalk.text = "";
			characterImageObj.SetActive(false);
			otherImageObj.SetActive(false);
			AudienceTalkAni1.SetTrigger("Close");
		}

		if (currentLine == 153)
		{
			DisableTextBox();
			StartCoroutine("beatuySmoke");
		}

		if (currentLine == 160)
		{
			DisableTextBox();
			AudienceTalk2.SetActive(true);
		}

		if (currentLine == 170)
		{
			DisableTextBox();
			AudienceTalkAni2.SetTrigger("change");
		}

		if (currentLine == 175)
		{
			DisableTextBox();
			AudienceTalk2.SetActive(false);
			//K現身
		}

		/*if (currentLine == 9)
		{
			whotalk.text = "魔法書籍";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			if (isTyping == false)
			{
				theText.text = "(每個新場景前端會設立<color=#FF8888>補血站</color>，就在前方，站上去試試。)";
			}
		}*/

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
						//npcDisappeaar();
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

	/*IEnumerator BloodFlyAfter()
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
	}*/

	IEnumerator BeforeBossBattle()
	{
		yield return new WaitForSeconds(2);
		currentLine = 57;
		endAtLine = 60;
		NPCAppear();
		yield return new WaitUntil(() => currentLine >= 60);
		gameManager.vsPanel.SetActive(true);
		vsYYJ.SetActive(true);
		yield return new WaitForSeconds(3);
		gameManager.teachHintAni.SetTrigger("HintOpen");
		gameManager.teachHintText.text = "進入戰鬥";
		gameManager.attackRedImage.SetActive(true);
		EnemyController.isAttack = true;
		gameManager.vsPanel.SetActive(false);
		vsYYJ.SetActive(false);
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
	}

	public IEnumerator AfterMonsterBattle()
	{
		currentLine = 83;
		endAtLine = 89;
		NPCAppear();
		yield return new WaitUntil(() => currentLine == 89);
	}

	IEnumerator beatuyZoom()
	{
		cameraFollow.isFollowTarget = false; 
		cameraFollow.moveCount = 2;
		yield return new WaitForSeconds(2);
	}

	IEnumerator beatuySmoke()
	{
		beatuySmokeObj.SetActive(true);
		yield return new WaitForSeconds(2);
		AudienceTalkAni1.SetTrigger("particleClose");
		didaObj.transform.position = new Vector2(21.5f, 8.2f);
		cocoObj.transform.position = new Vector2(22.18f, 8.2f);
		dragonObj.transform.position = new Vector2(23.3f, 8.25f);
		beatutMember.SetActive(false);
		yield return new WaitForSeconds(3);
		beatuySmokeObj.SetActive(false);
		currentLine = 154;
		endAtLine = 160;
		NPCAppear();

	}

	public void NPCAppear()
	{
		EnableTextBox();
	}

	public void teleportation()
	{
		playerController.pickUpInt = 0;
		currentLine = 104;
		currentLine = 104;
		NPCAppear();
	}



	void OnTriggerEnter2D(Collider2D col)
	{
		/*if (col.gameObject.name == "vin1Collider") //藤蔓1對話
		{
			currentLine = 101;
			endAtLine = 103;
			NPCAppear();
			Destroy(col.gameObject);
		}*/

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

		if (col.gameObject.name == "beatuyZoomCollider") //進入小BOSS攻擊
		{
			cameraFollow.moveCount = 2;
			cameraFollow.isFollowTarget = false;
			//attackColliderCol.enabled = false;
			//gameManager.drawGame.TransitionTo(10f);
			StartCoroutine("beatuyZoom");
		}

		if (col.gameObject.name == "NPC_Grace")
		{
			currentLine = 107;
			endAtLine = 137;
			NPCAppear();
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
		yield return new WaitUntil(() => currentLine == 82);
		gameManager.vsPanel.SetActive(true);
		vsWiko.SetActive(true);
		yield return new WaitForSeconds(3);
		gameManager.teachHintAni.SetTrigger("HintOpen");
		gameManager.teachHintText.text = "進入戰鬥";
		gameManager.attackRedImage.SetActive(true);
		MonsterController.isAttack = true;
		MonsterController.enemy2Transform.localRotation = Quaternion.Euler(0, 180, 0);
		MonsterController.enemy2Transform.position = new Vector2(51f, 3.1f);
		gameManager.vsPanel.SetActive(false);
		vsWiko.SetActive(false);
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