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
	public ExampleGestureHandler gesture;

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
	public Sprite sister_monochrome_normal;
	public GameObject otherImageObj; //右邊角色對話框
	private Image otherImage;
	public Sprite book;
	public GameObject didaObj;
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
	public Sprite YYK; //K

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
	//2
	public GameObject clock;
	public bool Mirror;
	public GameObject card;
	[HideInInspector]
	public Animator cardAni;
	public Animator MirrorAni;
    public GameObject beatuyCloud;
	public GameObject beatuy; //玩家角色選美頁面
	public BoxCollider2D graceCol; //主持人
	public GameObject AudienceTalk1; //觀眾對話1
	private Animator AudienceTalkAni1;
	public GameObject AudienceTalk2; //觀眾對話2
	private Animator AudienceTalkAni2;
	public GameObject AudienceTalk3; //觀眾對話3
	public GameObject AudienceTalk4; //觀眾對話4

	public GameObject beatuySmokeObj; //變形的煙
	public GameObject beatutMember; //選美型態的參賽者

	public GameObject secertKObj; //神秘人Obj
	public GameObject KParticle; //神秘人變身particle
	public GameObject K;

	public GameObject playerAddParticle; //player攻擊加成特效
	public GameObject addText; //攻擊加成文字
	public BoxCollider2D BossKCollider;
	//------------------Attack----------------------
	//小BOSS
	//public GameObject attackCollider;
	//private BoxCollider2D attackColliderCol;
	public GameObject attackColliderBorder;

	//----------------audio----------------------
	public AudioSource audio;
    public AudioClip cheer;
    public AudioClip quarrel;
    //-----------------其他---------------------
    public GameObject pause;
	private Color talkNow = new Color(1, 1, 1, 1);
	private Color untalkNow = new Color(.6f, .6f, .6f, 1);
	public GameObject BEfogParticle;
	//-----------------vs
	public GameObject vsYYK;

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

		if (currentLine == 2 || currentLine == 7 || currentLine == 64 || currentLine == 70 || currentLine == 81)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_normal;
		}

		if (currentLine == 4 || currentLine == 11 || currentLine == 24 || currentLine == 79 || currentLine == 83 ||currentLine==154 || currentLine == 174 || currentLine == 202)
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
		

		if (currentLine == 9 || currentLine == 12 || currentLine == 14 || currentLine == 203)
		{
			whotalk.text = "魔法書籍";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = book;
			otherImageObj.SetActive(true);
			AudienceTalk4.SetActive(false);
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
		

		if (currentLine == 33 || currentLine == 204)
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
            beatuyCloud.SetActive(true);

        }

		if (currentLine == 107)
		{
			whotalk.text = "葛雷斯";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = Grace;
			GraceAni.SetTrigger("talk");
			graceCol.enabled = false;
            if(!audio.isPlaying)
                audio.PlayOneShot(cheer);
        }

		if (currentLine == 110)
		{
			whotalk.text = playerName;
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = sister_oops;
			otherImageObj.SetActive(true);
            otherImage.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

		if (currentLine == 112 || currentLine == 117 || currentLine == 120 || currentLine == 124 || currentLine == 132 || currentLine == 138 ||currentLine==155)
		{
			whotalk.text = "葛雷斯";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = Grace;
			otherImageObj.SetActive(false);
			GraceAni.SetTrigger("talk");
            Joystick.isMove = true;
        }
		if (currentLine == 116)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_beauty;
			otherImageObj.SetActive(true);
            otherImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
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

		if (currentLine == 127 || currentLine == 156 || currentLine == 201)
		{
			whotalk.text = "奧莉薇";
			characterImage.color = talkNow;
			otherImage.color = talkNow;
			characterImage.sprite = Olivia;
			OliviaAni.SetTrigger("talk");
		}

		if (currentLine == 129 || currentLine == 200)
		{
			whotalk.text = "錢多多";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = money_normal;
			moneyAni.SetTrigger("talk");
            otherImageObj.SetActive(false);
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
            gameManager.beatuyChoose.TransitionTo(10f);
        }

		if (currentLine == 140)
		{
			DisableTextBox();
			AudienceTalk1.SetActive(true);
		}

        if (currentLine == 148)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_angry;
            if (!audio.isPlaying)
                audio.PlayOneShot(quarrel);
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

        if (currentLine == 167 || currentLine == 192)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_angry;
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
			StartCoroutine("KChange");
		}

		if (currentLine == 176 || currentLine == 189)
		{
			whotalk.text = "歪歪K";
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = YYK;
			characterImageObj.SetActive(true);
			otherImageObj.SetActive(false);
		}

        if (currentLine == 177)
        {
            whotalk.text = playerName;
            characterImage.color = untalkNow;
            otherImage.color = talkNow;
            otherImage.sprite = sister_oops;
            otherImageObj.SetActive(true);
            otherImage.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (currentLine == 178)
		{
			AudienceTalk3.SetActive(true);
			DisableTextBox();
		}

        if (currentLine == 182)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_angry;
            characterImageObj.SetActive(true);
            otherImageObj.SetActive(false);
            //otherImage.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (currentLine == 183)
		{
			whotalk.text = "滴答";
			characterImage.color = talkNow;
			otherImage.color = talkNow;
            otherImage.sprite = dida_rainbow_normal;
            otherImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
            otherImageObj.SetActive(true);
            AudienceTalk3.SetActive(false);
        }

		if (currentLine == 184)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
            otherImage.sprite = coco_rainbow_normal;
            otherImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

		if (currentLine == 185)
		{
			whotalk.text = "龍~";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dragon_rainbow_smile;
			//otherImageObj.SetActive(true);
			//characterImageObj.SetActive(false);
            otherImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

		if (currentLine == 186)
		{
			whotalk.text = "滴答&可可";
			characterImage.color = talkNow;
			otherImage.color = talkNow;
			characterImage.sprite =dida_rainbow_normal;
			otherImage.sprite = coco_rainbow_normal;
			characterImageObj.SetActive(true);
            characterImage.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (currentLine == 187)
        {
            whotalk.text = "龍~";
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = dragon_rainbow_smile;
            characterImageObj.SetActive(true);
            otherImageObj.SetActive(false);
            //characterImage.transform.localRotation = Quaternion.Euler(0, 180, 0);

        }

        if (currentLine == 188)
		{
			DisableTextBox();
			Joystick.isMove = true;
            gameManager.drawGame2.TransitionTo(5f);
            StartCoroutine("BeforeKBattle");
		}

        if (currentLine == 189)
        {
            whotalk.text = "歪歪K";
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = YYK;
            characterImageObj.SetActive(true);
            otherImageObj.SetActive(false);
            didaAni.state.SetAnimation(0, "idle_C", true);
            cocoAni.state.SetAnimation(0, "idle_C", true);
            dragonAni.state.SetAnimation(0, "idle_C", true);
            characterImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (currentLine == 194)
		{
			whotalk.text = "可可";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = coco_rainbow_sad;
            otherImageObj.SetActive(true);
        }

		if (currentLine == 195)
		{
			whotalk.text = "滴答";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dida_rainbow_sad;
		}

		if (currentLine == 196)
		{
			whotalk.text = "龍~";
			characterImage.color = untalkNow;
			otherImage.color = talkNow;
			otherImage.sprite = dragon_rainbow_closedEyes;
		}

		if (currentLine == 197)
		{
			DisableTextBox();
			AudienceTalk4.SetActive(true);
		}

		if (currentLine == 207)
		{
			DisableTextBox();
			choose1.SetActive(true);
			attackColliderBorder.SetActive(false);
			
		}

		if (currentLine == 208)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_monochrome_normal;
		}

		if (currentLine == 211)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 210 || currentLine == 213)
		{
			DisableTextBox();
			cameraFollow.moveCount = 0;
			cameraFollow.isFollowTarget = true;
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

	IEnumerator KChange()
	{
		KParticle.SetActive(true);
		yield return new WaitForSeconds(1);
		secertKObj.SetActive(false);
		yield return new WaitForSeconds(2);
		K.SetActive(true);
	}

	IEnumerator BeforeKBattle()
	{		
		playerAddParticle.SetActive(true);
		addText.SetActive(true);
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
        yield return new WaitUntil(() => currentLine == 197);
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
		didaObj.transform.position = new Vector2(21.1f, 8.2f);
		cocoObj.transform.position = new Vector2(21.7f, 8.2f);
		dragonObj.transform.position = new Vector2(22.7f, 8.25f);
        /*didaAni.state.SetAnimation(0, "idle_C", true);
        cocoAni.state.SetAnimation(0, "idle_C", true);
        dragonAni.state.SetAnimation(0, "idle_C", true);*/
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
		}

		if (col.gameObject.name == "NPC_Grace")
		{
			currentLine = 107;
			endAtLine = 137;
			NPCAppear();
		}
	}
	//----------------------------選擇----------------------------
	public void Choose1_gohome() //回家
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
		PlayerPrefs.SetInt("StaticObject.sHE1", StaticObject.sHE2);
		PlayerPrefs.SetInt("StaticObject.sBE1", StaticObject.sBE2);
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