using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public enum GameState
{
	Start, Pause, Running, Dead, MonsterCatch,  Win
}

public class RunGameManager : MonoBehaviour {

	public string NextSceneName;
	public static RunGameManager Instance;
	public static GameState gameState;
	public string chapterName;

	//-----------------------平衡條 & 血條-----------------------
	public float balanceValue;

	private GameObject balanceSliderObj;
	private Slider balanceSlider;

	private GameObject balanceTextObj;
	private Text balanceText;

	public float playerHealth;
	public Slider HealthSlider;
	//--------------------------距離條----------------------
	public GameObject distance;

	//教學物件
	public GameObject Arrow;

	//暫停物件
	public GameObject pauseMenu;
	public GameObject black_bgImage;
	public Button Puase;
	float StartTime = 1;

	//FADE淡出
	public GameObject FadeOut;
	Animator fade;
	public SkeletonAnimation fadeAni;
	public GameObject lose_Fade;

	//倒數
	public Text countdown;
	int time_int = 4;

	public GameObject warning;
	public Canvas canvas;

	//------------------------------------結算-----------------------
	public GameObject winObj;  //過關
	public GameObject gameoverObj; //血量歸零
	public GameObject loseObj; //被怪物追到
	private float addInt;
	private float subtractInt;
	private GameObject addSubTextObj;
	private Text addSubText;

	//--------------音效
	public AudioSource audio;
	public AudioClip countSound;

	//---------------教學Next物件---------------
	public GameObject TouchNextImage;
	private int count = 0;
	public GameObject maskGroup;
	public GameObject mask;
	public GameObject HintObj;
	[HideInInspector]
	public Animator HintAni;
	public Text HintText;
	public GameObject NextFlashText;
	//----------------------對話框物件-------------
	public GameObject textPanel;
	public Text text;

	void Start () {
		Puase.interactable = false;
		Instance = this;
		fade = FadeOut.GetComponent<Animator>();
		HintAni = HintObj.GetComponent<Animator>();
		lose_Fade.SetActive(false);
		Application.targetFrameRate = 100;  //幀數
		//↓以下要打開
		balanceValue = PlayerPrefs.GetFloat("StaticObject.balanceSlider");
		playerHealth = PlayerPrefs.GetFloat("StaticObject.playerHealth");
		HealthSlider.value = playerHealth;
		//balanceSlider.value = balanceValue;
		//balanceText.text = Mathf.Floor(balanceValue).ToString("0");
		Debug.Log(balanceValue);
		Debug.Log(playerHealth);
		gameState = GameState.Start;
		if (chapterName == "0")
		{
			StartCoroutine("Target");
		}
		else if (chapterName == "1")
		{
			StartCoroutine("Conversation");
            StaticObject.nowClass = 1.5f;
            PlayerPrefs.SetFloat("StaticObject.nowClass", StaticObject.nowClass);
        }


	}

	public void Update()
	{
		/*if (gameState == GameState.Win || gameState == GameState.Dead || gameState == GameState.MonsterCatch)
		{
			balanceSlider.value = balanceValue;
			balanceText.text = balanceSlider.value.ToString("0");
			/*if (balanceSlider.value >= 100 || balanceValue >= 100)
			{
				balanceValue = 100;
				balanceSlider.value = balanceValue;
				balanceText.text = Mathf.Floor(balanceValue).ToString("0");
			}
			else if (balanceSlider.value < 100)
			{
				balanceValue += 100f;
				balanceSlider.value = balanceValue;
				balanceText.text = Mathf.Floor(balanceValue).ToString("0");
			}

		//目前問題，win lose gameover介面加減數值問題 開啟後用find?
		}*/
	}

	IEnumerator Target() {
		gameState = GameState.Start;
		maskGroup.SetActive(true);
		mask.GetComponent<RectTransform>().anchoredPosition = new Vector2(900, -280);
		NextFlashText.SetActive(true);
		HintAni.SetTrigger("HintOpen");
		HintText.text = "目標！逃離地下道抵達終點";
		yield return new WaitUntil(() => count == 1);
		HintText.text = "上方為你與怪物的距離條";
		distance.transform.SetSiblingIndex(6);
		yield return new WaitUntil(() => count == 2);
		HintText.text = "若觸碰到後方怪物即挑戰失敗，請注意";
		yield return new WaitUntil(() => count == 3);
		HintText.text = "挑戰即將開始";
		yield return new WaitUntil(() => count == 4);
		NextFlashText.SetActive(false);
		distance.transform.SetAsFirstSibling();
		HintAni.SetTrigger("close");
		maskGroup.SetActive(false);
		TouchNextImage.SetActive(false);
		InvokeRepeating("timer", 1, 1);
	}

	IEnumerator Conversation()
	{
		textPanel.SetActive(true);
		text.text = "終於離開那片森林了，看來前面就是城鎮了吧";
		yield return new WaitUntil(() => count == 1);
		text.text = "！";
		yield return new WaitUntil(() => count == 2);
		text.text = "歪歪為什麼又追了上來！？";
		yield return new WaitUntil(() => count == 3);
		text.text = "不管了，趕緊逃跑吧！";
		yield return new WaitUntil(() => count == 4);
		textPanel.SetActive(false);
		TouchNextImage.SetActive(false);
		InvokeRepeating("timer", 1, 1);
	}

	public void Next()
	{
		count += 1;
	}

	void timer()
	{
		if (time_int > 0)
		{
			audio.PlayOneShot(countSound);
			time_int -= 1;
			countdown.text = time_int + "";
		}
		
		if (time_int == 0)
		{
			countdown.text = "Start";
			CancelInvoke("timer");
			InvokeRepeating("StartGame", 0, 1);
			countdown.enabled = false;
		}
	}

	void StartGame()
	{
		StartTime -= 1;
		if (StartTime == 0)
		{
			gameState = GameState.Running;
			Puase.interactable = true;
			CancelInvoke();
		}
	}

	public void pause()
	{
		black_bgImage.SetActive(true);
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
		gameState = GameState.Pause;
	}

	public void gamecontinue()
	{
		Time.timeScale = 1f;
		gameState = GameState.Running;
		pauseMenu.SetActive(false);
		black_bgImage.SetActive(false);
	}

	public void Reset()
	{
		StartCoroutine("WaitForAudio");	
		gameState = GameState.Running;
		Time.timeScale = 1;
	}

	public void Dead()
	{
		gameState = GameState.Dead;
		StartCoroutine("dead");
	}

	public void MonsterCatch() //被怪物抓到
	{
		gameState = GameState.MonsterCatch;
		StartCoroutine("monsterCatch");
	}

	public void Win()
	{
		gameState = GameState.Win;	
		StartCoroutine("win");
	}

	IEnumerator dead()  //血量歸零
	{
		yield return new WaitForSeconds(.5f);
		lose_Fade.SetActive(true);			
		canvas.GetComponent<Canvas>().enabled = false;
		fadeAni.state.SetAnimation(0, "animation", false);
		yield return new WaitForSeconds(1f);
		gameoverObj.SetActive(true);
		addSub();
		yield return new WaitForSeconds(2f);
		FadeOut.SetActive(true);
		fade.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(Application.loadedLevel);
	}

	IEnumerator monsterCatch()  //被怪物追到
	{
		yield return new WaitForSeconds(.5f);
		lose_Fade.SetActive(true);
		canvas.GetComponent<Canvas>().enabled = false;
		fadeAni.state.SetAnimation(0, "animation", false);
		yield return new WaitForSeconds(1f);
		loseObj.SetActive(true);
		addSub();
		yield return new WaitForSeconds(2f);
		FadeOut.SetActive(true);
		fade.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(Application.loadedLevel);
	}

	IEnumerator win()
	{
		winObj.SetActive(true);
		//addInt = 100 - Mathf.Floor(balanceValue);

		addInt = 20;
		addSubTextObj = GameObject.Find("addSubText");
		balanceTextObj = GameObject.Find("balanceText");
		balanceSliderObj = GameObject.Find("BalanaceSlider");
		addSubText = addSubTextObj.GetComponent<Text>();
		balanceText = balanceTextObj.GetComponent<Text>();
		balanceSlider = balanceSliderObj.GetComponent<Slider>();
		balanceSlider.value = balanceValue;
		addSubText.text = "+" + addInt;
		if (balanceValue + addInt >= 100)
		{
			balanceValue = 100;
		}
		else
		{
			balanceValue = balanceValue + addInt;
		}
		balanceSlider.value = balanceValue;
		Debug.Log(balanceSlider.value);
		Debug.Log(balanceValue);
		balanceText.text = balanceValue.ToString("0");

		yield return new WaitForSeconds(4f);
		FadeOut.SetActive(true);
		fade.SetBool("FadeOut", true);
		StaticObject.balanceSlider = balanceValue;
		StaticObject.playerHealth = HealthSlider.value;
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", HealthSlider.value);
		Debug.Log(StaticObject.balanceSlider);
		Debug.Log(StaticObject.playerHealth);
		yield return new WaitForSeconds(1.5f);

        if (chapterName == "0")
        {
            SceneManager.LoadScene("Chapter0_5movie");
        }
        else
        {
            SceneManager.LoadScene("Loading");  //接下一關
        }
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0);
		SceneManager.LoadScene(Application.loadedLevel);
	}

	public void addSub()
	{

		subtractInt = 10;
		addSubTextObj = GameObject.Find("addSubText");
		balanceTextObj = GameObject.Find("balanceText");
		balanceSliderObj = GameObject.Find("BalanaceSlider");
		addSubText = addSubTextObj.GetComponent<Text>();
		balanceText = balanceTextObj.GetComponent<Text>();
		balanceSlider = balanceSliderObj.GetComponent<Slider>();
		balanceSlider.value = balanceValue;
		addSubText.text = "-" + subtractInt;
		balanceValue = balanceValue - subtractInt;
		balanceSlider.value = balanceValue;
		Debug.Log(balanceSlider.value);
		Debug.Log(balanceValue);
		balanceText.text = balanceValue.ToString("0");
	}
}
