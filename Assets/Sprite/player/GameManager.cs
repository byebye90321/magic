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
	float r1 = 0.7372549f, g1 = 0.2078431f, b1 = 0.5568628f;  //原平衡條桃色
	float r2 = 0.7372549f, g2 = 0.2078431f, b2 = 0.3071967f;  //殘血後平衡條色
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

	public GameObject ParticleObj1;
	public GameObject ParticleObj2;
	public GameObject ParticleObj3;
	public GameObject ParticleObj4;
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
	public GameObject sHE1; //離開森林
	public GameObject sBE1; //迷失森林
	public GameObject sBE2; //回家

	//--------------------回饋介面----------------
	public GameObject winPanel;  //通關
	public GameObject losePanel;  //失敗
	public GameObject unlockPanel;  //解鎖
	public GameObject gameOverPenal;  //gameOver

	public Text gameOverText;
	//------------------FADE淡出-------------------
	public GameObject FadeOut; 
	private Animator FadeOutAni;
	public bool isRun = false;

	public GameObject FadeWhite;
	//--------------------音效---------------------
	public AudioSource audio;
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
		}

		if (StaticObject.G1 == 1)
			G1.SetActive(true);
		if (StaticObject.G2 == 1)
			G2.SetActive(true);
		/*if (StaticObject.G3 == 1)
			G3.SetActive(true);
		if (StaticObject.G4 == 1)
			G4.SetActive(true);*/
		if (StaticObject.B1 == 1)
			B1.SetActive(true);
		if (StaticObject.B2 == 1)
			B2.SetActive(true);
		/*if (StaticObject.B3 == 1)
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
			dialogsScript1.vine2text.SetActive(true);
		}
	}

	public IEnumerator floorOpen2()
	{
		if (cameraFollow.transform.position.x < 30.1f)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(0.5f);
			AirFloor2.SetActive(true);
			yield return new WaitForSeconds(2);
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
        KObj.SetActive(false);
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
		yield return new WaitForSeconds(.5f);
		gameOverPenal.SetActive(true);
		yield return new WaitForSeconds(2.5f);
		FadeOut.SetActive(true);
		FadeOutAni.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("LoadingToMain");  //先回主畫面
	}

	IEnumerator Win()  //過關
	{
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", playerController.HealthSlider.value);
		if (ChapterName == "1")
		{
			yield return new WaitForSeconds(2f);
		}
		winPanel.SetActive(true);
		/*if (StaticObject.sHE1 == 1)
		{
			winPanel.SetActive(true);
		}*/
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
		yield return new WaitForSeconds(2f);
		losePanel.SetActive(true);
		/*if (StaticObject.sBE1 == 1)
		{
			losePanel.SetActive(true);
		}*/
		yield return new WaitForSeconds(2.5f);
		FadeOut.SetActive(true);
		FadeOutAni.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		Debug.Log("666666");
		SceneManager.LoadScene("ChooseChapter");  //接下一關  //先回主畫面
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0.05f);
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
