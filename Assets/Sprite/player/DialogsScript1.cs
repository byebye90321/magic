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
	public NPCTask npcTask;
	//-------------------------魔法日報---------------------------

	//--------------------------互動-----------------------------
	//--------------------------障礙物----------------------------

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

	//-----------------------教學物件變數-------------------------
	public bool teachBlood=false;

	//----------------audio----------------------
	public AudioSource audio;
	public AudioMixerSnapshot usually;
	//-----------------其他---------------------
	public GameObject pause;


	void Start() {

		usually.TransitionTo(10f);
		fadeOut = FadeOut.GetComponent<Animator>();
		characterImage = characterImageObj.GetComponent<Image>();
		otherImage = otherImageObj.GetComponent<Image>();
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

		if (currentLine == 1 || currentLine == 2|| currentLine == 8 || currentLine == 15)
		{
			whotalk.text = "緹緹";
			characterImageObj.transform.SetAsLastSibling();
			otherImageObj.transform.SetAsFirstSibling();
			characterImage.sprite = sister_opps;
		}
		if (currentLine == 3|| currentLine == 12)
		{
			whotalk.text = "緹緹";
			characterImageObj.transform.SetAsLastSibling();
			otherImageObj.transform.SetAsFirstSibling();
			characterImage.sprite = sister_normal;
		}
		if (currentLine == 4 || currentLine == 13 || currentLine == 16 || currentLine == 17)
		{
			whotalk.text = "魔法書籍";
			characterImageObj.transform.SetAsFirstSibling();
			otherImageObj.transform.SetAsLastSibling();
			otherImageObj.SetActive(true);
			otherImage.sprite = book;
		}
		if (currentLine == 5|| currentLine == 6|| currentLine == 7|| currentLine == 20)
		{
			whotalk.text = "緹緹";
			characterImageObj.transform.SetAsLastSibling();
			otherImageObj.transform.SetAsFirstSibling();
			characterImage.sprite = sister_angry;
		}

		if (currentLine == 9)
		{
			whotalk.text = "魔法書籍";
			characterImageObj.transform.SetAsFirstSibling();
			otherImageObj.transform.SetAsLastSibling();
			if (isTyping == false)
			{
				theText.text = "(每個新場景前端會設立<color=#FF8888>補血站</color>，就在前方，站上去試試。)";
			}
		}
		if (currentLine == 11)
		{
			whotalk.text = "緹緹";
			characterImageObj.transform.SetAsLastSibling();
			otherImageObj.transform.SetAsFirstSibling();
			characterImage.sprite = sister_happy;
		}

		if (currentLine == 19)  //書本飛翔
		{
			npcTask.bookFly();
			DisableTextBox();
			StartCoroutine("BloodFlyAfter");
		}

		if (currentLine == 22)
		{
			whotalk.text = "緹緹";
			characterImageObj.transform.SetAsLastSibling();
			otherImageObj.transform.SetAsFirstSibling();
			characterImage.sprite = sister_sad;
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
		yield return new WaitForSeconds(3);
		currentLine = 20;
		endAtLine = 20;
		NPCAppear();	
	}

	public void NPCAppear()
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