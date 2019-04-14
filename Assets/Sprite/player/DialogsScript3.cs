﻿using UnityEngine;
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
    public GameObject blackSmokeParticle;

	//------------------Attack----------------------
	//小BOSS
	public GameObject attackColliderBorder;

	//----------------audio----------------------
	public new AudioSource audio;
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
            TextAsset textFile1 = Resources.Load("Text/bother3") as TextAsset;
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
            TextAsset textFile1 = Resources.Load("Text/sister3") as TextAsset;
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
        endAtLine = 9;

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

        StaticObject.nowClass = 3;
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

		if (currentLine == 1 || currentLine == 5 || currentLine == 7 || currentLine == 11)
		{
			whotalk.text = playerName;
			characterImage.color = talkNow;
			otherImage.color = untalkNow;
			characterImage.sprite = sister_oops;
		}

        if (currentLine == 2 || currentLine == 3 || currentLine == 13)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_sad;          
        }

        if (currentLine == 4 || currentLine == 6 || currentLine == 8 || currentLine == 14)
        {
            whotalk.text = "魔法書籍";
            characterImage.color = untalkNow;
            otherImage.color = talkNow;
            otherImage.sprite = book;
            otherImageObj.SetActive(true);
        }

        if (currentLine == 9)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_smile;
        }

        if (currentLine == 15)
        {
            whotalk.text = playerName;
            characterImage.color = talkNow;
            otherImage.color = untalkNow;
            characterImage.sprite = sister_angry;
        }

        if (currentLine == 18)
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
	public void BloodStation()  //補血站
	{
		teachBlood = true;
		currentLine = 6;
		endAtLine = 16;
		NPCAppear();
	}

	public void NPCAppear()
	{
		EnableTextBox();
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