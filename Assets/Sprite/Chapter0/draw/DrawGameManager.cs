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

	public DG_playerController playerController;
	public DG_EnemyController enemyController;
	//-----------------暫停物件-------------------
	public Button Puase;
	public GameObject pauseMenu;
	public GameObject black_bgImage;

	//------------------平衡條---------------------
	public Slider balanceSlider;
	public Image sliderimage;
	public float balanceValue;
	public float spendTime;
	float r1 = 0.7372549f, g1 = 0.2078431f, b1 = 0.5568628f;  //原平衡條桃色
	float r2 = 0.7372549f, g2 = 0.2078431f, b2 = 0.3071967f;  //新

	public static DrawGameManager Instance;
	//public static DrawState drawState;
	public DrawState drawState = DrawState.Start;

	float StartTime = 1;

	//對話
	public GameObject textPanel;
	public Text text;
	//public GameObject Player;

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
		InvokeRepeating("GameRound", 1, 1);
		fade = winFade.GetComponent<Animator>();
	}

	void FixedUpdate () {

		/*if (DrawEnemyController.end == true) 
		{
			textPanel.SetActive(true);
			Puase.interactable = false;
			text.text = "糟了！他們好像太厲害了，我們還是先逃吧！那邊有門！";
			StartCoroutine("run");	
		}*/

		if (drawState == DrawState.Game)
		{
			//----------------------平衡條----------------------------
			balanceValue -= Time.deltaTime * spendTime;
			balanceSlider.value = balanceValue;

			if (balanceSlider.value == 0)
			{
				balanceValue = 0;
				Debug.Log("DEATH");
			}
			else if (balanceSlider.value > 100)
			{
				balanceValue = 100;
			}
			else if (balanceSlider.value < 10)
			{
				sliderimage.color = Color.Lerp(new Color(r2, g2, b2), new Color(r1, g1, b1), balanceSlider.value / 10);  //從G變R
			}


			if (balanceSlider.value == 0 || playerController.curHealth ==0)
			{
				Debug.Log("战斗失败");
				drawState =DrawState.Dead;
			}
			if (enemyController.curHealth==0)
			{
				Debug.Log("战斗胜利");
				drawState = DrawState.Win;
			}

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
		//DrawEnemyController.end = false;
		
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
