using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public enum ChapterState
{
	Teach, Pause, Game, Dead, Win, Lose
}
public class GameManager : MonoBehaviour {

	public string ChapterName; //關卡名稱
	public DG_playerController playerController; //玩家腳本
	public DialogsScript1 dialogsScript1;
	public DialogsScript2 dialogsScript2;
	public DialogsScript3 dialogsScript3;
	public DialogsScript4 dialogsScript4;
    public ExampleGestureHandler geature; //畫符腳本

	public string nextSceneName;
	//-----------------暫停物件-------------------
	public Button Pause;
	public GameObject pauseMenu;
	public GameObject black_bgImage;

	//------------------平衡條---------------------
	public Slider balanceSlider;
	//public Image sliderimage;
	public float balanceValue;
	public float spendTime;
	//float r1 = 0.7372549f, g1 = 0.2078431f, b1 = 0.5568628f;  //原平衡條桃色
	//float r2 = 0.7372549f, g2 = 0.2078431f, b2 = 0.3071967f;  //殘血後平衡條色
	public Text balanceText;

	public static DG_GameManager Instance;
	public CameraFollow cameraFollow;
	public static ChapterState chapterState;
	//------------------skill---------------------
	public GameObject G1;
	public GameObject B1;
	public GameObject G2;
	public GameObject B2;
	public GameObject G3;
	public GameObject B3;
	public GameObject G4;
	public GameObject B4;
	public GameObject G5;
	public GameObject G6;

    public GameObject ParticleObj1;
	public GameObject ParticleObj2;
	public GameObject ParticleObj3;
	public GameObject ParticleObj4;
	public GameObject ParticleObj5;
	public GameObject ParticleObj6;
    //------------------事件----------------------
    public GameObject eventObj;
	private Animator eventAni;
	public Text eventText;

	//----------------平台物件、互動物件------------------
	public GameObject AirFloor;
	public GameObject vine2;
	public GameObject AirFloor2;

	public GameObject Teleportation;
	public GameObject stoneDoorObj;
	[HideInInspector]
	public Animator stoneDoorAni;

	//-------------------對話----------------------
	public GameObject textPanel;
	public Text text;

	//-------------------畫符物件-----------------
	public GameObject attackRedImage;
	public GameObject teachHint;
	[HideInInspector]
	public Animator teachHintAni;
	public Text teachHintText;
	public GameObject downHint;
	[HideInInspector]
	public Animator downHintAni;
	public Text downHintText;

	public GameObject vsPanel;
	//-------------------角色---------------------
	public GameObject smallBoss;
	public GameObject monster;
	public GameObject K;
    public GameObject KObj;
    //--------------------結局--------------------
    public GameObject EndingCanvas;

    private GameObject sHE1; //離開森林
	private GameObject sBE1; //迷失森林
	private GameObject sBE2; //被遺忘的事
    private GameObject sHE2; //真實的世界
    private GameObject sBE3; //萬籟俱寂的等待

    public Text endTitle; //結局名
    public Text endText; //短語
	//--------------------回饋介面----------------
	public GameObject winPanel;  //通關
	public GameObject losePanel;  //失敗
	public GameObject unlockPanel;  //解鎖
	public GameObject gameOverPenal;  //gameOver

	public Text gameOverText; 
	//------------------FADE淡出-------------------
	public GameObject FadeOut; 
    [HideInInspector]
	public Animator FadeOutAni;
	public bool isRun = false;
	public GameObject FadeWhite;
    public GameObject FIFO;
    public GameObject FIFO_long;
	//--------------------音效---------------------
	public new AudioSource audio;
	public AudioMixerSnapshot usually;
	public AudioMixerSnapshot drawGame;
	public AudioMixerSnapshot drawGame2;
	public AudioMixerSnapshot beatuyChoose;


	void Start () {
		chapterState = ChapterState.Game;
		usually.TransitionTo(10f);
		//balanceValue = PlayerPrefs.GetFloat("StaticObject.balanceSlider");
		//playerController.curHealth = PlayerPrefs.GetFloat("StaticObject.playerHealth");
		playerController.HealthSlider.value = playerController.curHealth;	
		balanceSlider.value = balanceValue;
		balanceText.text = Mathf.Floor(balanceValue).ToString("0");
		Debug.Log(balanceValue);
		Debug.Log(playerController.curHealth);
		PlayerPrefs.GetInt("StaticObject.G2", StaticObject.G2);	
		FadeOutAni = FadeOut.GetComponent<Animator>();
		teachHintAni = teachHint.GetComponent<Animator>();
		downHintAni = downHint.GetComponent<Animator>();
		Application.targetFrameRate = 100;  //幀數

        if (ChapterName == "1")
        {
            AirFloor.SetActive(false);
            stoneDoorAni = stoneDoorObj.GetComponent<Animator>();

            //↓校內展需要程式，非正式版本
            StaticObject.G1 = 0;
            PlayerPrefs.SetInt("StaticObject.G1", StaticObject.G1);
            StaticObject.G2 = 0;
            PlayerPrefs.SetInt("StaticObject.G2", StaticObject.G2);
            StaticObject.G3 = 0;
            PlayerPrefs.SetInt("StaticObject.G3", StaticObject.G3);
            StaticObject.G4 = 0;
            PlayerPrefs.SetInt("StaticObject.G4", StaticObject.G4);
            StaticObject.B1 = 0;
            PlayerPrefs.SetInt("StaticObject.B1", StaticObject.B1);
            StaticObject.B2 = 0;
            PlayerPrefs.SetInt("StaticObject.B2", StaticObject.B2);
            StaticObject.B3 = 0;
            PlayerPrefs.SetInt("StaticObject.B3", StaticObject.B3);
            StaticObject.B4 = 0;
            PlayerPrefs.SetInt("StaticObject.B4", StaticObject.B4);

        }
        else if (ChapterName == "2")
        {
            //↓校內展需要程式，非正式版本
            StaticObject.G1 = 0;
            PlayerPrefs.SetInt("StaticObject.G1", StaticObject.G1);
            StaticObject.G2 = 0;
            PlayerPrefs.SetInt("StaticObject.G2", StaticObject.G2);
            StaticObject.G3 = 0;
            PlayerPrefs.SetInt("StaticObject.G3", StaticObject.G3);
            StaticObject.G4 = 0;
            PlayerPrefs.SetInt("StaticObject.G4", StaticObject.G4);
            StaticObject.B1 = 0;
            PlayerPrefs.SetInt("StaticObject.B1", StaticObject.B1);
            StaticObject.B2 = 0;
            PlayerPrefs.SetInt("StaticObject.B2", StaticObject.B2);
            StaticObject.B3 = 0;
            PlayerPrefs.SetInt("StaticObject.B3", StaticObject.B3);
            StaticObject.B4 = 0;
            PlayerPrefs.SetInt("StaticObject.B4", StaticObject.B4);
            G1.SetActive(true);
            G2.SetActive(true);
        }

        //正式版本應該是這樣的
		/*if (StaticObject.G1 == 1)
			G1.SetActive(true);
		if (StaticObject.G2 == 1)
			G2.SetActive(true);
		if (StaticObject.G3 == 1)
			G3.SetActive(true);
		if (StaticObject.G4 == 1)
			G4.SetActive(true);
		if (StaticObject.B1 == 1)
			B1.SetActive(true);
		if (StaticObject.B2 == 1)
			B2.SetActive(true);
        if (StaticObject.B3 == 1)
			B3.SetActive(true);
		if (StaticObject.B4 == 1)
			B4.SetActive(true);*/

	}
	
	void FixedUpdate () {

		if (chapterState == ChapterState.Game)
		{
			//----------------------平衡條----------------------------
			balanceValue -= Time.deltaTime * spendTime;
			balanceSlider.value = balanceValue;
			balanceText.text = Mathf.Floor(balanceValue).ToString("0");
			if (balanceSlider.value == 0)
			{
				balanceValue = 0;
				Debug.Log("DEATH");
			}
			else if (balanceSlider.value > 100)
			{
				balanceValue = 100;
				playerController.curHealth = 100;
			}
			/*else if (balanceSlider.value < 10)  //殘血變色
			{
				sliderimage.color = Color.Lerp(new Color(r2, g2, b2), new Color(r1, g1, b1), balanceSlider.value / 10);  //從G變R
			}*/

			if (balanceSlider.value <= 0 || playerController.curHealth <= 0)
			{
				Debug.Log("战斗失败");
				Dead();
			}

			/*if (dg_enemyController.curHealth == 0)
			{
				Debug.Log("战斗胜利");
				chapterState = ChapterState.Win;
			}*/
		}
	}

	//-----------------------------Camera移轉鏡頭相關--------------------
	//第一章
	public IEnumerator floorOpen()
	{
		if (cameraFollow.transform.position.y <= 3.6)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(0.5f);
			AirFloor.SetActive(true);
			yield return new WaitForSeconds(2);
			cameraFollow.isFollowTarget = true;
		}
	}

	public IEnumerator vineOpen()
	{
		if (cameraFollow.transform.position.y >= 4.6f)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(0.5f);
			vine2.SetActive(true);
			yield return new WaitForSeconds(2);
			cameraFollow.isFollowTarget = true;
		}
	}

	public IEnumerator floorOpen2()
	{
		if (cameraFollow.transform.position.x < 30.1f)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(0.5f);
			AirFloor2.SetActive(true);
			yield return new WaitForSeconds(1);
			cameraFollow.isFollowTarget = true;
		}
	}

	public IEnumerator BossAttackWin()
	{
		usually.TransitionTo(10f);
		teachHintAni.SetTrigger("close");
		attackRedImage.SetActive(false);
		cameraFollow.moveCount = 7;
		yield return new WaitForSeconds(0.3f);
		eventObj.SetActive(true);
		eventText.text = "擊敗歪歪JQ";
		smallBoss.SetActive(false);
		cameraFollow.moveCount = 0;
		cameraFollow.isFollowTarget = true;
		yield return new WaitForSeconds(2f);
		eventObj.SetActive(false);
		dialogsScript1.attackColliderBorder.SetActive(false);
		dialogsScript1.attackCollider.SetActive(false);
        dialogsScript1.bossCol.enabled = false;
        StartCoroutine(dialogsScript1.AfterBossBattle());
	}

	public IEnumerator MonsterAttackWin()
	{
		usually.TransitionTo(10f);
		teachHintAni.SetTrigger("close");
		attackRedImage.SetActive(false);
		cameraFollow.moveCount = 9;
		yield return new WaitForSeconds(0.3f);
		eventObj.SetActive(true);
		eventText.text = "擊敗維吉維克";
		monster.SetActive(false);
		cameraFollow.moveCount = 0;
		cameraFollow.isFollowTarget = true;
		yield return new WaitForSeconds(2f);
		eventObj.SetActive(false);
		dialogsScript1.monsterColliderBorder.SetActive(false);
		dialogsScript1.monsterCollider.SetActive(false);
        dialogsScript1.monsterCol.enabled = false;
        StartCoroutine(dialogsScript1.AfterMonsterBattle());
	}

	//第二章
	public IEnumerator highest()
	{
		if (cameraFollow.transform.position.y >= 8.9f)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(1);
			cameraFollow.isFollowTarget = true;
		}
	}

	public IEnumerator KAttackWin()
	{
        StaticObject.K = 1; //K解鎖
        PlayerPrefs.SetInt("StaticObject.K", StaticObject.K);
        usually.TransitionTo(3f);
		teachHintAni.SetTrigger("close");
		attackRedImage.SetActive(false);
        yield return new WaitForSeconds(0.3f);
		eventObj.SetActive(true);
		eventText.text = "擊敗歪歪K";
        K.SetActive(false);
        yield return new WaitForSeconds(2f);     
        eventObj.SetActive(false);
        StartCoroutine(dialogsScript2.AfterKBattle());
        KObj.SetActive(false);  //K
    }

    //第三章
    public IEnumerator pillarKey()
    {
        yield return new WaitForSeconds(1.5f);
        dialogsScript3.Key.SetActive(true);        
        yield return new WaitForSeconds(2f);      
        cameraFollow.moveCount = 0;
        cameraFollow.isFollowTarget = true;
    }

    //第三章水晶室
    public IEnumerator KingAttackWin()
    {
        StaticObject.EnemyKing = 1; //K解鎖
        PlayerPrefs.SetInt("StaticObject.EnemyKing", StaticObject.EnemyKing);
        usually.TransitionTo(3f);
        teachHintAni.SetTrigger("close");
        attackRedImage.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        eventObj.SetActive(true);
        eventText.text = "擊敗框框";
        K.SetActive(false);
        yield return new WaitForSeconds(2f);
        eventObj.SetActive(false);
        StartCoroutine(dialogsScript4.AfterKingBattle());
        dialogsScript4.kingCollider.enabled = false;
    }

    public void pause()
	{
		black_bgImage.SetActive(true);
		pauseMenu.SetActive(true);
		//drawState = DrawState.Pause;
		Time.timeScale = 0f;
		Debug.Log("PAUSE");
	}

	public void gamecontinue()
	{
		Time.timeScale = 1f;
		//drawState = DrawState.Game;
		pauseMenu.SetActive(false);
		black_bgImage.SetActive(false);
		Debug.Log("gamecontinue");
	}

	public void Reset()
	{
		StartCoroutine("WaitForAudio");

		chapterState = ChapterState.Game;
		Time.timeScale = 1;
	}

	public void Dead()
	{
		chapterState = ChapterState.Dead;
		if (balanceSlider.value <= 0)
			gameOverText.text = "世界已失衡\n遊戲失敗";
		if (playerController.curHealth <= 0)
			gameOverText.text = "血量耗盡\n遊戲失敗";
		StartCoroutine("GameOver");
	}

	public void win()
	{
		chapterState = ChapterState.Win;
		StartCoroutine("Win");
	}

	public void lose()
	{
		chapterState = ChapterState.Lose;
		StartCoroutine("Lose");
	}

	IEnumerator GameOver()  //GameOver
	{
        if (ChapterName == "4" && playerController.dialogsScript4.kingBool ==true)
        {
            losePanel.SetActive(true);
            endTitle.text = "萬籟俱寂的等待";
            endText.text = "平等的璀璨之色仍未完全消失，\n卻需要更長久的時間等待轉機到來。";
            yield return new WaitForSeconds(3f);
            FIFO_long.SetActive(true);
            yield return new WaitForSeconds(1f);
            losePanel.SetActive(false);
            yield return new WaitForSeconds(3f);
            sBE2 = Resources.Load<GameObject>("EndingCanvas/BE3");
            GameObject BE2 = Instantiate(sBE2) as GameObject;
            BE2.transform.SetParent(EndingCanvas.transform, false);
            yield return new WaitForSeconds(2.5f);
            FadeOut.SetActive(true);
            FadeOutAni.SetBool("FadeOut", true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("ChooseChapter");  //先回主畫面
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            gameOverPenal.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            FadeOut.SetActive(true);
            FadeOutAni.SetBool("FadeOut", true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("LoadingToMain");  //先回主畫面
        }
	}

	IEnumerator Win()  //過關
	{
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", playerController.HealthSlider.value);       
		winPanel.SetActive(true);

        if (ChapterName == "1")
        {
            yield return new WaitForSeconds(2f);
            FIFO.SetActive(true);
            FIFO.GetComponent<Animator>().SetBool("FIFO", true);
            yield return new WaitForSeconds(1f);
            winPanel.SetActive(false);
            if (StaticObject.whoCharacter == 1)
            {
                sHE1 = Resources.Load<GameObject>("EndingCanvas/HE1b");
                GameObject HE1 = Instantiate(sHE1) as GameObject;
                HE1.transform.SetParent(EndingCanvas.transform, false);
            }
            else
            {
                sHE1 = Resources.Load<GameObject>("EndingCanvas/HE1s");
                GameObject HE1 = Instantiate(sHE1) as GameObject;
                HE1.transform.SetParent(EndingCanvas.transform, false);
            }
        }
        else if (ChapterName == "4")
        {
            endTitle.text = "真實的世界";
            endText.text = "恢復色彩的世界就是這麼繽紛，\n不僅是烏托邦的世界，也是我們的世界。";
            yield return new WaitForSeconds(3f);
            FIFO_long.SetActive(true);
            yield return new WaitForSeconds(1f);
            winPanel.SetActive(false);
            yield return new WaitForSeconds(3f);
            sHE2 = Resources.Load<GameObject>("EndingCanvas/HE2");
            GameObject HE2 = Instantiate(sHE2) as GameObject;
            HE2.transform.SetParent(EndingCanvas.transform, false);

        }
        yield return new WaitForSeconds(2.5f);
		FadeOut.SetActive(true);
		FadeOutAni.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Loading");  //接下一關 
	}

	IEnumerator Lose()  //失敗
	{
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", playerController.HealthSlider.value);
		losePanel.SetActive(true);

        if (ChapterName == "1")
        {
            endTitle.text = "迷失森林";
            endText.text = "在黑霧籠罩的森林中看見一位步履蹣跚之人，\n據說那是失去色彩、迷失自我者唯一的歸處。";
            yield return new WaitForSeconds(3f);
            FIFO_long.SetActive(true);
            yield return new WaitForSeconds(1f);
            losePanel.SetActive(false);
            yield return new WaitForSeconds(3f);
            if (StaticObject.whoCharacter == 1)
            {
                sBE1 = Resources.Load<GameObject>("EndingCanvas/BE1b");
                GameObject BE1 = Instantiate(sBE1) as GameObject;
                BE1.transform.SetParent(EndingCanvas.transform, false);
            }
            else
            {
                sBE1 = Resources.Load<GameObject>("EndingCanvas/BE1s");
                GameObject BE1 = Instantiate(sBE1) as GameObject;
                BE1.transform.SetParent(EndingCanvas.transform, false);
            }
        }
        else if (ChapterName == "2")
        {
            endTitle.text = "被遺忘的事";
            endText.text = "那些冒險的記憶正逐漸模糊，\n紅藍二色再次侵蝕了自我...";
            yield return new WaitForSeconds(3f);
            FIFO_long.SetActive(true);
            yield return new WaitForSeconds(1f);
            losePanel.SetActive(false);
            yield return new WaitForSeconds(3f);
            sBE2 = Resources.Load<GameObject>("EndingCanvas/BE2");
            GameObject BE2 = Instantiate(sBE2) as GameObject;
            BE2.transform.SetParent(EndingCanvas.transform, false);
        }
        /*else if (ChapterName == "4")
        {
            endTitle.text = "萬籟俱寂的等待";
            endText.text = "平等的璀璨之色仍未完全消失，\n卻需要更長久的時間等待轉機到來。";
            yield return new WaitForSeconds(3f);
            FIFO_long.SetActive(true);
            yield return new WaitForSeconds(1f);
            losePanel.SetActive(false);
            yield return new WaitForSeconds(3f);
            sBE2 = Resources.Load<GameObject>("EndingCanvas/BE2");
            GameObject BE2 = Instantiate(sBE2) as GameObject;
            BE2.transform.SetParent(EndingCanvas.transform, false);
        }*/
        yield return new WaitForSeconds(2.5f);
        FadeOut.SetActive(true);
		FadeOutAni.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("ChooseChapter");  //先回主畫面
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0.05f);
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
