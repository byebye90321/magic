using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public enum GameState
{
	Teach, Start, Pause, Running, Dead,  Win
}

public class RunGameManager : MonoBehaviour {

	public static RunGameManager Instance;
	public static GameState gameState;
	public string chapterName;

	//教學物件
	public GameObject targetText;
	public GameObject Arrow;


	//暫停物件
	public GameObject pauseMenu;
	public GameObject black_bgImage;
	public Button Puase;	
	float StartTime = 1;

	//FADE淡出
	public GameObject winFade;
	Animator fade;
	public SkeletonAnimation fadeAni;
	public GameObject lose_Fade;

	//倒數
	public Text countdown;
	int time_int = 4;

	public GameObject warning;
	public Canvas canvas;

	//--------------音效
	public AudioSource audio;
	public AudioClip countSound;

	void Start () {

		StartCoroutine("Target");
		gameState = GameState.Start;	
		Puase.interactable = false;
		Instance = this;
		fade = winFade.GetComponent<Animator>();
		lose_Fade.SetActive(false);
		Application.targetFrameRate = 100;  //幀數
		
	}

	IEnumerator Target() {
		black_bgImage.SetActive(true);
		targetText.SetActive(true);
		yield return new WaitForSeconds(2f);
		black_bgImage.SetActive(false);
		targetText.SetActive(false);
		InvokeRepeating("timer", 1, 1);
		gameState = GameState.Start;
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
		StartCoroutine("GameOver");
	}

	public void win()
	{
		gameState = GameState.Win;
		StartCoroutine("Win");
	}

	IEnumerator GameOver()
	{
		lose_Fade.SetActive(true);
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
