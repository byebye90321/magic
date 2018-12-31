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
	public ExampleGestureHandler geature; //畫符腳本

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

	public GameObject ParticleObj1;
	public GameObject ParticleObj2;
	//------------------成就----------------------
	public GameObject achievementObj;
	private Animator achievementAni;
	public Text achievementText;

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
	//-------------------角色---------------------
	public GameObject smallBoss;
	public GameObject monster;
	//--------------------結局--------------------
	public GameObject sHE1;
	public GameObject sBE1;
	//--------------------回饋介面----------------
	public GameObject winPanel;
	public GameObject losePanel;
	public GameObject unlockPanel;
	public GameObject gameOverPenal;
	//------------------FADE淡出-------------------
	public GameObject FadeOut; 
	private Animator FadeOutAni;
	public bool isRun = false;

	public GameObject FadeWhite;
	//--------------------音效---------------------
	public AudioSource audio;
	public AudioMixerSnapshot usually;
	public AudioMixerSnapshot drawGame;


	void Start () {
		chapterState = ChapterState.Game;
		usually.TransitionTo(10f);
		balanceValue = PlayerPrefs.GetFloat("StaticObject.balanceSlider");
		playerController.curHealth = PlayerPrefs.GetFloat("StaticObject.playerHealth");
		playerController.HealthSlider.value = playerController.curHealth;
		AirFloor.SetActive(false);
		balanceSlider.value = balanceValue;
		balanceText.text = Mathf.Floor(balanceValue).ToString("0");
		Debug.Log(balanceValue);
		Debug.Log(playerController.curHealth);
		PlayerPrefs.GetInt("StaticObject.G2", StaticObject.G2);
		//Debug.Log("StaticObject.G2：" + StaticObject.G2);
		stoneDoorAni = stoneDoorObj.GetComponent<Animator>();
		FadeOutAni = FadeOut.GetComponent<Animator>();
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
			}
			/*else if (balanceSlider.value < 10)  //殘血變色
			{
				sliderimage.color = Color.Lerp(new Color(r2, g2, b2), new Color(r1, g1, b1), balanceSlider.value / 10);  //從G變R
			}*/

			if (balanceSlider.value == 0 || playerController.curHealth == 0)
			{
				Debug.Log("战斗失败");
				chapterState = ChapterState.Dead;
			}

			/*if (dg_enemyController.curHealth == 0)
			{
				Debug.Log("战斗胜利");
				chapterState = ChapterState.Win;
			}*/
		}
	}

	//-----------------------------Camera移轉鏡頭相關--------------------
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
		teachHint.SetActive(false);
		attackRedImage.SetActive(false);
		cameraFollow.moveCount = 7;
		yield return new WaitForSeconds(0.3f);
		achievementObj.SetActive(true);
		achievementText.text = "擊敗歪歪";
		smallBoss.SetActive(false);
		cameraFollow.moveCount = 0;
		cameraFollow.isFollowTarget = true;
		yield return new WaitForSeconds(2f);
		achievementObj.SetActive(false);
		dialogsScript1.attackColliderBorder.SetActive(false);
		dialogsScript1.attackCollider.SetActive(false);
		StartCoroutine(dialogsScript1.AfterBossBattle());
	}

	public IEnumerator MonsterAttackWin()
	{
		usually.TransitionTo(10f);
		teachHint.SetActive(false);
		attackRedImage.SetActive(false);
		cameraFollow.moveCount = 9;
		yield return new WaitForSeconds(0.3f);
		achievementObj.SetActive(true);
		achievementText.text = "擊敗維吉維克";
		monster.SetActive(false);
		cameraFollow.moveCount = 0;
		cameraFollow.isFollowTarget = true;
		yield return new WaitForSeconds(2f);
		achievementObj.SetActive(false);
		dialogsScript1.monsterColliderBorder.SetActive(false);
		dialogsScript1.monsterCollider.SetActive(false);
		StartCoroutine(dialogsScript1.AfterMonsterBattle());
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

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(0.1f);
		//BackGrounaAudio.Stop();
		//Time.timeScale = 0;
		/*winFade.SetActive(true);
		fade.SetBool("win", true);*/
		Debug.Log("Dead");
		yield return new WaitForSeconds(1.5f);

	}

	IEnumerator Win()
	{
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", playerController.HealthSlider.value);
		yield return new WaitForSeconds(2f);
		if (StaticObject.sHE1 == 1)
		{
			winPanel.SetActive(true);
		}
		yield return new WaitForSeconds(2.5f);
		FadeOut.SetActive(true);
		FadeOutAni.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		Debug.Log("77878789");
		SceneManager.LoadScene("LoadingToMain");  //接下一關 //先回主畫面
	}

	IEnumerator Lose()
	{
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", playerController.HealthSlider.value);
		yield return new WaitForSeconds(3f);
		if (StaticObject.sBE1 == 1)
		{
			losePanel.SetActive(true);
		}
		yield return new WaitForSeconds(2.5f);
		FadeOut.SetActive(true);
		FadeOutAni.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		Debug.Log("666666");
		SceneManager.LoadScene("LoadingToMain");  //接下一關  //先回主畫面
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0.05f);
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
