using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Spine.Unity;
using Spine;
using UnityEngine.Audio;

public class DialogsScript : MonoBehaviour
{
	//------------------------引用程式----------------------------
	Draw_sister_forest_Enemy draw_Sister_Forest_Enemy;
	Draw_sister_GM draw_sister_GM;
	public move move;
	public GameObject TouchPanel;
	//-------------------------魔法日報---------------------------
	public GameObject paper1Ins;  //動態產生
	private bool paperBool1 = true;
	public GameObject paper2Ins;
	private bool paperBool2 = true;
	public GameObject paper3Ins;
	private bool paperBool3 = true;

	//--------------------------互動-----------------------------
	public GameObject interactCanvas;
	private int count = 0;
	public Image bobbyInteract;
	public Sprite bobbyInteract2;
	public Image flower;
	public Sprite flower2;
	public Sprite flower3;
	public GameObject shineParticle;
	public GameObject mouseEffect;
	public GameObject interactPanel;
	//--------------------------障礙物----------------------------
	public BoxCollider2D markCollider;
	public BoxCollider2D statueCollider;
	public BoxCollider2D monsterCollider;
	public BoxCollider2D flowerCollider;

	public BoxCollider2D test;
	public GameObject hintTest;

	/*public GameObject mark;
	public GameObject statue;*/
	//---------------------------頭貼----------------------------
	public Image characterImage; 
	public Sprite sister;
	public Sprite book;
	public Sprite bobby_oneColor;
	public Sprite bobby_rainbows;
	public Sprite wiki;
	public Sprite wiko;

	//---------------------------人物----------------------------
	public GameObject monster1;
	public GameObject monster2;
	public Transform flowermonster;
	public bool follow = false;
	public SkeletonAnimation bobbyAnimation;
	//----------------------------選擇---------------------------
	public GameObject Q1;

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
	AsyncOperation isDraw;
	//------------------------結局相關---------------------------
	//關卡樹狀圖，非圖鑑樹狀圖，不存檔
	public static bool sHE1 = false;
	public static bool sBE1 = false;

	public GameObject he;
	public GameObject be;

	//---------------人物動態-------------------
	public SkeletonAnimation skeletonAnimation_wiko;

	//----------------animation----------------------
	public Animator markAni;
	public Animator statueAni;
	public Animator flowerAni;

	//----------------audio----------------------
	public AudioSource audio;
	public AudioSource bookaudio;
	public AudioClip openBook;
	public AudioClip wikiPush;
	public AudioMixerSnapshot usually;
	//-----------------其他---------------------
	public GameObject pause;
	

	void Start() {

		usually.TransitionTo(10f);
		fadeOut = FadeOut.GetComponent<Animator>();
		skeletonAnimation_wiko.state.SetAnimation(0, "idle", true);
		StaticObject.sister = 1;
		PlayerPrefs.SetInt("StaticObject.sister", StaticObject.sister);
		StaticObject.book = 1;
		PlayerPrefs.SetInt("StaticObject.book", StaticObject.book);

		whotalk.text = "緹緹";
		currentLine = 1;
		endAtLine = 6;
		characterImage.sprite = sister;
		StartCoroutine("fadeIn");

		if (GameEnd == false)  //初始
		{
			whotalk.text = "緹緹";
			currentLine = 1;
			endAtLine = 6;
			characterImage.sprite = sister;
			StartCoroutine("fadeIn");
			//isDraw = SceneManager.LoadSceneAsync("DrawGame_sister_forest");
			//isDraw.allowSceneActivation = false;
		}
		else if(GameEnd == true){  //畫符完
			whotalk.text = "緹緹";
			currentLine = 26;
			endAtLine = 26;
			characterImage.sprite = sister;
			markCollider.GetComponent<BoxCollider2D>().enabled = false;
			statueCollider.GetComponent<BoxCollider2D>().enabled = false;
			monsterCollider.GetComponent<BoxCollider2D>().enabled = false;
			markAni.SetInteger("markindex", 2);
			statueAni.SetInteger("statueindex", 2);

			monster1.SetActive(false);
			monster2.SetActive(false);
			flowermonster.localRotation = Quaternion.Euler(0, 180, 0);
			move.rigid2D.position = new Vector3(28f, -3f, 10f);
			sBE1 = true;
			StartCoroutine("fadeIn");
		}

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
		if (count == 1)
		{
			flower.sprite = flower2;
		}
		if (count == 2)
		{
			flower.sprite = flower3;
		}
		if (count == 3)
		{
			bobbyInteract.sprite = bobbyInteract2;
			StartCoroutine("interactOver");
			count = 0;
			interactPanel.SetActive(false);
		}
			
	}


	void Update() {

		if (currentLine == 1)
		{
			if (isTyping == false)
			{
				//theText.text = testText.text;
				theText.text = "這是<color=#00ffffff>哪裡?</color>";
			}
		}


		if (!isActive)
			return;

		if (currentLine == 2) 
		{
			whotalk.text = "魔法書籍";
			characterImage.sprite = book;
		}
		if (currentLine == 3) 
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
		}
		if (currentLine == 5) 
		{
			whotalk.text = "魔法書籍";
			characterImage.sprite = book;
		}
		if (currentLine == 6)
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
		}
		if (currentLine == 8) //mark日報1
		{
			TouchPanel.SetActive(false);
			StartCoroutine("Paper1");
			markCollider.GetComponent<BoxCollider2D>().enabled = false;
			StaticObject.Paper1 = 1;
			PlayerPrefs.SetInt("StaticObject.Paper1", StaticObject.Paper1);
		}
		if (currentLine == 10) 
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
		}

		if (currentLine == 12) //statue日報2
		{
			TouchPanel.SetActive(false);
			StartCoroutine("Paper2");
			statueCollider.GetComponent<BoxCollider2D>().enabled = false;
			StaticObject.Paper2 = 1;
			PlayerPrefs.SetInt("StaticObject.Paper2", StaticObject.Paper2);
		}
		if (currentLine == 14)
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
		}
		if (currentLine == 16)
		{
			whotalk.text = "維吉";
			characterImage.sprite = wiki;
			StaticObject.wiki = 1;
			PlayerPrefs.SetInt("StaticObject.wiki", StaticObject.wiki);
		}
		if (currentLine == 17)
		{
			whotalk.text = "維克";
			characterImage.sprite = wiko;
			StaticObject.wiko = 1;
			PlayerPrefs.SetInt("StaticObject.wiko", StaticObject.wiko);
		}
		if (currentLine == 18)
		{
			whotalk.text = "波比";
			characterImage.sprite = bobby_oneColor;
			StaticObject.bobby = 1;
			PlayerPrefs.SetInt("StaticObject.bobby", StaticObject.bobby);
		}
		if (currentLine == 19)
		{
			whotalk.text = "維克";
			characterImage.sprite = wiko;
		}
		if (currentLine == 21)
		{
			whotalk.text = "波比";
			characterImage.sprite = bobby_oneColor;
		}
		if (currentLine == 22)
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
			monsterCollider.GetComponent<BoxCollider2D>().enabled = false;
			TouchPanel.SetActive(false);
			
		}
		if (currentLine == 23) {
			DisableTextBox();
			StartCoroutine("waitChoose");
		}
		if (currentLine == 26) {
			characterImage.sprite = sister;
			StartCoroutine("interact");
		}
		if (currentLine == 27)
		{
			whotalk.text = "波比";
			characterImage.sprite = bobby_rainbows;
			bobbyAnimation.state.SetAnimation(0, "idle__Multicolor", true);
		}

		if (currentLine == 28)
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
		}
		if (currentLine == 29)
		{
			flowermonster.transform.position = new Vector3(move.rigid2D.position.x, move.rigid2D.position.y-0.2f, 10);
			flowermonster.localRotation = Quaternion.Euler(0, 0, 0);
			follow = true;
		}
		if (currentLine == 31)
		{
			whotalk.text = "緹緹";
			characterImage.sprite = sister;
			flowerCollider.GetComponent<BoxCollider2D>().enabled = false;
		}
		if (currentLine == 33)
		{
			whotalk.text = "波比";
			characterImage.sprite = bobby_rainbows;
			flowerCollider.GetComponent<BoxCollider2D>().enabled = false;
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
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "mark")
		{
			TouchPanel.SetActive(false);
			move.target.x = other.transform.position.x-1f;
			currentLine = 8;
			endAtLine = 8;			
			NPCAppear();
		}
		if (other.gameObject.name == "statue")
		{
			TouchPanel.SetActive(false);
			move.target.x = other.transform.position.x - 1f;
			currentLine = 12;
			endAtLine = 12;
			NPCAppear();
		}

		if (other.gameObject.name == "monster")
		{
			TouchPanel.SetActive(false);
			move.target.x = other.transform.position.x - 1f;
			currentLine = 16;
			endAtLine = 23;
			NPCAppear();
			StartCoroutine("WikiPush");
		}

		if (other.gameObject.name == "flower")
		{
			TouchPanel.SetActive(false);
			move.target.x = other.transform.position.x;
			StartCoroutine("Paper3");
			StaticObject.Paper3 = 1;
			PlayerPrefs.SetInt("StaticObject.Paper3", StaticObject.Paper3);
		}

		if (other.gameObject.name == "EndPoint")
		{
			TouchPanel.SetActive(false);
			pause.SetActive(false);
			if (GameEnd == true)  //畫符完 HE
			{				
				sHE1 = true;
				StaticObject.s1 = 1;
				PlayerPrefs.SetInt("StaticObject.s1", StaticObject.s1);
				StaticObject.sHE1 = 1;
				PlayerPrefs.SetInt("StaticObject.sHE1", StaticObject.sHE1);
				/*StaticObject.card02 = false;
				StaticObject.card05 = false;
				StaticObject.card06 = false;
				StaticObject.card10 = false;
				StaticObject.card13 = false;
				StaticObject.card14 = false;
				Draw_sister_GM.end = false;*/
				GameObject HE = (GameObject)Instantiate(he) as GameObject;
				
				StartCoroutine("fadeOutAni");
				Debug.Log("HE");
			}
			else if (GameEnd == false)  //初始 BE
			{		
				sBE1 = true;
				StaticObject.s1 = 1;
				PlayerPrefs.SetInt("StaticObject.s1", StaticObject.s1);
				StaticObject.sBE1 = 1;
				PlayerPrefs.SetInt("StaticObject.sBE1", StaticObject.sBE1);
				/*.card02 = false;
				StaticObject.card05 = false;
				StaticObject.card06 = false;
				StaticObject.card10 = false;
				StaticObject.card13 = false;
				StaticObject.card14 = false;*/
				GameObject BE = (GameObject)Instantiate(be) as GameObject;
				StartCoroutine("fadeOutAni");
				Debug.Log("BE");
			}
		}

		if (other.gameObject.name == "test")
		{
			hintTest.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "test")
		{
			hintTest.SetActive(false);
		}
	}

	private void NPCAppear()
	{
		//NPC.SetActive(true);
		TouchPanel.SetActive(false);
		EnableTextBox();
	}

	public void paper1After()
	{
		//TouchPanel.SetActive(true);
		TouchPanel.SetActive(false);
		currentLine = 10;
		endAtLine = 10;
		StartCoroutine("wait");
	}

	public void paper2After()
	{
		TouchPanel.SetActive(false);
		currentLine = 14;
		endAtLine = 14;
		StartCoroutine("wait");
	}

	public void paper3After()
	{
		TouchPanel.SetActive(false);
		currentLine = 31;
		endAtLine = 33;
		StartCoroutine("wait");
	}

	IEnumerator wait() {
		yield return new WaitForSeconds(0.5f);
		NPCAppear();
	}

	IEnumerator WikiPush() {
		yield return new WaitUntil(() => currentLine ==17);
		audio.PlayOneShot(wikiPush);
		skeletonAnimation_wiko.state.SetAnimation(0, "hit", false);
		skeletonAnimation_wiko.state.AddAnimation(0, "idle", true, 0);

	}
	
	//----------------------------魔法日報
	IEnumerator Paper1() {	
		yield return new WaitUntil(() => currentLine > endAtLine);
		TouchPanel.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		if(paperBool1 == true){
			GameObject Paper1 = (GameObject)Instantiate(paper1Ins) as GameObject;
			Paper1.transform.SetParent(GameObject.Find("Canvas").transform, false);
			bookaudio.PlayOneShot(openBook);
			paperBool1 = false;
		}		
	}
	IEnumerator Paper2()
	{
		yield return new WaitUntil(() => currentLine > endAtLine);
		TouchPanel.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		if (paperBool2 == true)
		{
			GameObject Paper2 = (GameObject)Instantiate(paper2Ins) as GameObject;
			Paper2.transform.SetParent(GameObject.Find("Canvas").transform, false);
			bookaudio.PlayOneShot(openBook);
			paperBool2 = false;
		}

	}
	IEnumerator Paper3()
	{
		yield return new WaitForSeconds(0.1f);
		if (paperBool3 == true)
		{
			GameObject Paper3 = (GameObject)Instantiate(paper3Ins) as GameObject;
			Paper3.transform.SetParent(GameObject.Find("Canvas").transform, false);
			bookaudio.PlayOneShot(openBook);
			paperBool3 = false;
		}
	}

	//------------------互動
	IEnumerator interact() {					
		yield return new WaitUntil(() => currentLine > endAtLine);
		TouchPanel.SetActive(false);
		interactCanvas.SetActive(true);
		pause.SetActive(false);
	}

	public void OnTouch(BaseEventData bData)
	{
		count++;
	}

IEnumerator interactOver() {
		shineParticle.SetActive(true);
		mouseEffect.SetActive(false);
		yield return new WaitForSeconds(3f);
		pause.SetActive(true);
		interactCanvas.SetActive(false);
		TouchPanel.SetActive(false);
		shineParticle.SetActive(false);
		currentLine = 27;
		endAtLine = 29;
		NPCAppear();
	}

	//----------------------------選擇----------------------------
	public void Q1_1() {
		Q1.SetActive(false);
		currentLine = 24;
		endAtLine = 24;
		NPCAppear();
		StartCoroutine("waitToDraw");	
	}

	public void Q1_2()
	{
		Q1.SetActive(false);
		flowerCollider.GetComponent<BoxCollider2D>().enabled = false;
		currentLine = 35;
		endAtLine = 35;
		NPCAppear();
		sBE1 = true;
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
		TouchPanel.SetActive(false);
		isActive = true;
		textBox.SetActive(true);
		StartCoroutine(TextScroll(textLines[currentLine]));
	}
	public void DisableTextBox()
	{
		isActive = false;
		TouchPanel.SetActive(true);
		textBox.SetActive(false);
	}

	//--------------------遊戲--------------------
	IEnumerator waitChoose() {
		TouchPanel.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		Q1.SetActive(true);
		TouchPanel.SetActive(false);
	}

	IEnumerator waitToDraw() {
		TouchPanel.SetActive(false);
		yield return new WaitUntil(() => currentLine > endAtLine);
		FadeOut.SetActive(true);
		fadeOut.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		//SceneManager.LoadScene("DrawGame_sister_forest"); 
		isDraw.allowSceneActivation = true;
		Debug.Log("進入畫符");
	}
}