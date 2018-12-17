using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ChapterState
{
	Teach, Pause, Game, Dead, Win
}
public class GameManager : MonoBehaviour {

	public string ChapterName; //關卡名稱
	public DG_playerController playerController; //玩家腳本
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

	//-------------------血量----------------------
	//private float playerHealth;
	//public Slider HealthSlider;
	//----------------平台物件開關------------------
	public GameObject AirFloor;
	public GameObject vine2;
	//-------------------對話----------------------
	public GameObject textPanel;
	public Text text;

	//------------------FADE淡出-------------------
	public GameObject winFade;
	Animator fade;
	public bool isRun = false;

	//--------------------音效---------------------
	public AudioSource audio;


	void Start () {
		chapterState = ChapterState.Game;
		balanceValue = PlayerPrefs.GetFloat("StaticObject.balanceSlider");
		playerController.curHealth = PlayerPrefs.GetFloat("StaticObject.playerHealth");
		playerController.HealthSlider.value = playerController.curHealth;
		AirFloor.SetActive(false);
		//HealthSlider.value = playerHealth;
		balanceSlider.value = balanceValue;
		balanceText.text = Mathf.Floor(balanceValue).ToString("0");
		Debug.Log(balanceValue);
		Debug.Log(playerController.curHealth);
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

	//-----------------------------camera相關
	public IEnumerator floorOpen()
	{
		if (cameraFollow.transform.position.y <= 3.1)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(0.5f);
			AirFloor.SetActive(true);
			yield return new WaitForSeconds(3);
			cameraFollow.isFollowTarget = true;
		}
	}

	public IEnumerator vineOpen()
	{
		if (cameraFollow.transform.position.y >= 5.9)
		{
			cameraFollow.moveCount = 0;
			yield return new WaitForSeconds(0.5f);
			vine2.SetActive(true);
			yield return new WaitForSeconds(3);
			cameraFollow.isFollowTarget = true;
		}
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
		yield return new WaitForSeconds(0.1f);
		winFade.SetActive(true);
		fade.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		//SceneManager.LoadScene("RunGame_chapter0");  //接下一關
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0.05f);
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
