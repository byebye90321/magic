using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public enum GameState
{
	Start, Pause, Running, Dead,  Win
}

public class RunGameManager : MonoBehaviour {
	
	//暫停物件
	public GameObject pauseMenu;
	public GameObject black_bgImage;
	public Button Puase;

	public static RunGameManager Instance;
	public static GameState gameState;
	
	float StartTime = 1;

	//FADE淡出
	public GameObject winFade;
	Animator fade;
	public SkeletonAnimation fadeAni;
	public GameObject fadeObj;

	//倒數
	public Text countdown;
	int time_int = 3;

	public GameObject warning;
	public Canvas canvas;

	//--------------音效
	public AudioSource audio;
	public AudioClip countSound;

	void Start () {
		Time.timeScale = 1f;
		gameState = GameState.Start;	
		Puase.interactable = false;
		Instance = this;
		fade = winFade.GetComponent<Animator>();
		fadeObj.SetActive(false);
		/*maskPanel.SetActive(false);
		blackPanel.SetActive(false);*/
		//StartCoroutine("teach");
		Application.targetFrameRate = 100;
		InvokeRepeating("timer", 1, 1);
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
		//counter.text = "" + StartTime;
		if (StartTime == 0)
		{
			gameState = GameState.Running;
			Puase.interactable = true;
			CancelInvoke();
			//RunningBGM();
		}
	}

	public void pause()
	{
		black_bgImage.SetActive(true);
		//warning.SetActive(false);
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
		gameState = GameState.Pause;
	}

	public void gamecontinue()
	{
		Time.timeScale = 1f;
		gameState = GameState.Running;
		pauseMenu.SetActive(false);
		//warning.SetActive(true);
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
		StartCoroutine("GameOver");
	}

	public void win()
	{
		gameState = GameState.Win;
		StartCoroutine("Win");
	
	}

	IEnumerator GameOver()
	{
		//yield return new WaitForSeconds(0.1f);
		fadeObj.SetActive(true);
		canvas.GetComponent<Canvas>().enabled = false;
		fadeAni.state.SetAnimation(0, "animation", false);
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(Application.loadedLevel);
	}

	IEnumerator Win()
	{
		winFade.SetActive(true);
		fade.SetBool("FadeOut", true);	
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Chapter0_5movie");  //接下一關
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0);
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
