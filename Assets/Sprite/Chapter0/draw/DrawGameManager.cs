using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public class DrawGameManager : MonoBehaviour {
	public enum DrawState
	{
		/*Start, Pause, playerRound, playerAtt, monsterRound, monsterAtt, Dead,  Win*/
		Start, Pause, Game, Dead, Win
	}
	//暫停物件
	public Button Puase;
	public GameObject pauseMenu;
	public GameObject black_bgImage;
	
	//血量
	public HeartSystem heartSystem;
	public HeartSystemMonster heartSystemMonster;

	public static DrawGameManager Instance;
	//public static DrawState drawState;
	public DrawState drawState = DrawState.Start;

	//控制玩家操作及操作窗口是否出现
	public bool isWaitForPlayer = true;
	//控制怪物操作
	public bool isEnemyAction = false;
	//获取玩家及敌人脚本的引用 
	private DrawPlayerController drawPlayerController;
	private DrawEnemyController drawEnemyController;

	public Text roundText;
	float StartTime = 1;
	//public GameObject winFade;
	//Animator fade;

	//Mask物件
	/*public GameObject maskPanel;
	public GameObject blackPanel;
	public GameObject maskPoint;
	public GameObject TeachPanel;
	public Image TeachImage;
	public int maskindex = 0;
	public Text hintText;
	public Animator mask;*/
	//----------------------二版------------
	public GameObject MaskCanvas;
	public GameObject MASKPANEL;
	public Image maskImage;
	public GameObject MASKALL;
	public Sprite mask1;
	public Sprite mask2;
	public Sprite mask3;
	public Sprite mask4;
	public Text hintText;
	public int maskindex = 0;

	//對話
	public GameObject textPanel;
	public Text text;
	public GameObject Player;

	//FADE淡出
	public GameObject winFade;
	Animator fade;
	public bool isRun = false;

	//--------------音效
	public AudioSource audio;
	public AudioClip runSound;

	void Start () {
		Time.timeScale = 1f;
		drawState = DrawState.Start;
		Puase.interactable = false;
		Instance = this;
		//InvokeRepeating("GameRound", 1, 1);
		fade = winFade.GetComponent<Animator>();
		StartCoroutine("teach");
	}

	public void NextTeach()
	{
		maskindex += 1;
	}

	void Update () {
		if (maskindex == 0)
		{
			hintText.enabled = true;
			hintText.text = "畫符關卡介紹";
			/*maskPanel.SetActive(true);
			blackPanel.SetActive(true);
			maskPoint.SetActive(false);
			TeachPanel.SetActive(false);*/
			MASKALL.SetActive(true);
			

		}
		else if (maskindex == 1)
		{
			/*maskPoint.SetActive(true);
			TeachPanel.SetActive(true);*/
			hintText.text = "畫符關卡採回合制\n先讓對方血量歸零者即獲勝";
			MASKALL.SetActive(false);
			MASKPANEL.SetActive(true);
		}
		else if (maskindex == 2)
		{
			hintText.text = "關卡資訊在畫面上方";
			//mask.SetInteger("maskindex", 1);
			maskImage.sprite = mask2;
		}
		else if (maskindex == 3)
		{
			hintText.text = "現有卡牌在下方\n在遊戲過程中可以透過觸發事件獲得不同卡牌";
			//mask.SetInteger("maskindex", 2);
			maskImage.sprite = mask3;
		}
		else if (maskindex == 4)
		{
			hintText.text = "累積一定回合能量卡牌變亮才可以使用";
		}
		else if (maskindex == 5)
		{
			hintText.text = "畫出卡牌上的圖案即可發揮卡牌力量進行攻擊";
		}
		else if (maskindex == 6)
		{
			hintText.text = "長按卡牌可以看到卡牌的所有資訊";
		}
		else if (maskindex == 7)
		{
			hintText.text = "接著來實際畫符看看吧！\n請在畫面中央畫出「M」圖形";
			//maskPoint.SetActive(false);
			

		}
		else if (maskindex == 8)
		{
			/*blackPanel.SetActive(false);
			maskPanel.SetActive(false);*/
			MASKPANEL.SetActive(false);
			MaskCanvas.SetActive(false);

		}
		else if (maskindex == 10)
		{
			/*blackPanel.SetActive(true);
			maskPanel.SetActive(true);
			maskPoint.SetActive(true);
			TeachPanel.SetActive(false);*/
			MaskCanvas.SetActive(true);
			MASKALL.SetActive(true);
			hintText.text = "接下來必須在限時之內點擊掉怪物攻擊\n否則將受到傷害";		
		}

		else if (maskindex == 11)
		{
			/*blackPanel.SetActive(false);
			maskPanel.SetActive(false);
			maskPoint.SetActive(false);
			TeachPanel.SetActive(false);*/
			MaskCanvas.SetActive(false);
		}

		if (DrawEnemyController.end == true) 
		{
			textPanel.SetActive(true);
			Puase.interactable = false;
			text.text = "糟了！他們好像太厲害了，我們還是先逃吧！那邊有門！";
			StartCoroutine("run");	
		}
		if (isRun == true)
		{
			Player.transform.position = new Vector3(Player.transform.position.x + 0.07f, Player.transform.position.y, 1);
			if (Player.transform.position.x > 7f) {
				Instance.win();
			}
		}

		if (drawState == DrawState.Game)
		{
			//如果任意一方生命值为0，则游戏结束  
			if (heartSystem.curHealth==0)
			{
				Debug.Log("战斗失败");
				drawState =DrawState.Dead;
			}
			if (heartSystemMonster.curHealth==0)
			{
				Debug.Log("战斗胜利");
				drawState = DrawState.Win;
			}

			if (isWaitForPlayer == true)
			{
				roundText.text = "玩家回合";
			}
			else {
				roundText.text = "敵方回合";
			}
		}

	}

	IEnumerator teach()
	{
		while (maskindex < 7)
		{
			yield return null;
		}
		if (maskindex == 7)
		{
			//InvokeRepeating("timer", 1, 1);
			InvokeRepeating("GameRound", 1, 1);
		}
	}

	public void GameRound() {
		StartTime -= 1;
		if (StartTime == 0)
		{
			Puase.interactable = true;
			CancelInvoke();
		}
		drawState = DrawState.Game;
		Debug.Log("Game");
	}

	IEnumerator run() {
		audio.clip = runSound;
		audio.Play();
		yield return new WaitForSeconds(2);
		textPanel.SetActive(false);
		isRun = true;
		DrawEnemyController.end = false;
		
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

	public void Reset()
	{
		StartCoroutine("WaitForAudio");

		drawState = DrawState.Game;
		Time.timeScale = 1;
	}

	public void Dead()
	{
		drawState = DrawState.Dead;
		StartCoroutine("GameOver");
	}

	public void win()
	{
		drawState = DrawState.Win;
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
		yield return new WaitForSeconds(0.1f);
		winFade.SetActive(true);
		fade.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("RunGame_chapter0");  //接下一關
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0);
		SceneManager.LoadScene(Application.loadedLevel);
	}

}
