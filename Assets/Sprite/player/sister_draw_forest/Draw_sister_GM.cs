using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Draw_sister_GM : MonoBehaviour
{
	public enum DrawState
	{
		Start, Pause, Game, Dead, Win
	}
	//暫停物件
	public Button Puase;
	public GameObject pauseMenu;
	public GameObject black_bgImage;

	//血量
	public HeartSystem heartSystem;
	public HeartSystemMonster heartSystemMonster;

	public static Draw_sister_GM Instance;
	public static DialogsScript dialogsScript;
	public DrawState drawState = DrawState.Start;

	//控制玩家操作及操作窗口是否出现
	public bool isWaitForPlayer = true;
	//控制怪物操作
	public bool isEnemyAction = false;
	//获取玩家及敌人脚本的引用 
	private Draw_sister_forest drawPlayerController;
	private Draw_sister_forest_Enemy drawEnemyController;

	public Text roundText;
	float StartTime = 1;
	public GameObject FadeOut;
	public GameObject FadeIn;
	Animator fadeOut;

	public GameObject textPanel;
	public Text loseText;

	public static bool end = false;

	public AudioMixerSnapshot drawGame;

	void Start()
	{
		drawGame.TransitionTo(10f);
		//Time.timeScale = 1f;
		drawState = DrawState.Start;
		StartCoroutine("fadeIn");

		InvokeRepeating("GameRound", 1, 1);
		Puase.interactable = false;
		Instance = this;
		//fadeIn = FadeIn.GetComponent<Animator>();
		fadeOut = FadeOut.GetComponent<Animator>();
	}
	IEnumerator fadeIn()
	{
		FadeIn.SetActive(true);
		//fade.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		FadeIn.SetActive(false);
	}



	void Update()
	{
		if (drawState == DrawState.Game)
		{
			//如果任意一方生命值为0，则游戏结束  
			if (heartSystem.curHealth == 0)
			{
				isEnemyAction = false;
				isWaitForPlayer = false;

				Debug.Log("战斗失败");
				drawState = DrawState.Dead;
				Instance.Dead();
			}
			if (heartSystemMonster.curHealth <= 0)
			{
				end = true;
				Debug.Log("战斗胜利");
				isEnemyAction = false;
				isWaitForPlayer = false;
				drawState = DrawState.Win;
				DialogsScript.GameEnd = true;
				Instance.win();
			}

			if (isWaitForPlayer == true)
			{
				roundText.text = "玩家回合";
			}
			else if (isEnemyAction == true)
			{
				roundText.text = "敵方回合";
			}
			else
			{
				roundText.text = "";
			}
		}
	}

	public void GameRound()
	{
		StartTime -= 1;
		if (StartTime == 0)
		{
			Puase.interactable = true;
			CancelInvoke();
		}
		drawState = DrawState.Game;
		Debug.Log("Game");
	}

	public void pause()
	{
		black_bgImage.SetActive(true);
		pauseMenu.SetActive(true);
		drawState = DrawState.Pause;
		Time.timeScale = 0f;
		Debug.Log("PAUSE");
	}

	public void gamecontinue()
	{
		Time.timeScale = 1f;
		drawState = DrawState.Game;
		pauseMenu.SetActive(false);
		black_bgImage.SetActive(false);
		Debug.Log("gamecontinue");
	}

	public void Dead()
	{
		drawState = DrawState.Dead;
		Puase.interactable = false;
		StartCoroutine("GameOver");
		
	}

	public void win()
	{
		drawState = DrawState.Win;
		StartCoroutine("Win");
	}

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(1f);
		//BackGrounaAudio.Stop();
		//Time.timeScale = 0;
		/*winFade.SetActive(true);
		fade.SetBool("win", true);*/
		textPanel.SetActive(true);
		loseText.text = "可...可惡，我一定會成功打倒你們，保護小花怪的！";
		Debug.Log("Dead");
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(Application.loadedLevel);
	}

	IEnumerator Win()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("win");
		FadeOut.SetActive(true);
		fadeOut.SetBool("FadeOut", true);
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("sister_forest");  //接下一關
	}

	/*IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0.05f);
		SceneManager.LoadScene(Application.loadedLevel);
	}*/
}
