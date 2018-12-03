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
	public DG_playerController playerController;
	//-------------------------魔法日報---------------------------

	//--------------------------互動-----------------------------
	public GameObject mouseEffect;
	public GameObject TouchPanel;
	//--------------------------障礙物----------------------------

	//---------------------------頭貼----------------------------
	public Image characterImage;
	public Sprite sister;
	public Sprite book;

	//---------------------------人物----------------------------

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

	//---------------人物動態-------------------



	//----------------audio----------------------
	public AudioSource audio;
	public AudioMixerSnapshot usually;
	//-----------------其他---------------------
	public GameObject pause;


	void Start() {

		usually.TransitionTo(10f);
		fadeOut = FadeOut.GetComponent<Animator>();
		StaticObject.sister = 1; //魔法日報解鎖
		PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
		StaticObject.book = 1;
		PlayerPrefs.SetInt("StaticObject.book", StaticObject.book);

		whotalk.text = "緹緹";
		currentLine = 1;
		endAtLine = 3;
		characterImage.sprite = sister;
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

	void FixedUpdate()
	{
		//Debug.Log(currentLine);
	}


	void Update() {

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

		if (currentLine == 4)
		{
			whotalk.text = "魔法書籍";
			characterImage.sprite = book;
		}
		if (currentLine == 5)
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
		}
		if (currentLine == 9)
		{
			whotalk.text = "魔法書籍";
			characterImage.sprite = book;
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

	//-------------------------碰撞-----------------------------


	private void NPCAppear()
	{
		//TouchPanel.SetActive(false);
		EnableTextBox();
	}

	/*public void OnTouch(BaseEventData bData)
	{
		count++;
	}*/

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
		StartCoroutine(TextScroll(textLines[currentLine]));
	}
	public void DisableTextBox()
	{
		isActive = false;
		playerController.drawCanvas.enabled = true;
		textBox.SetActive(false);
	}

}