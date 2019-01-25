﻿using System.Collections;
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

	public static RunGameManager Instance;
	public static GameState gameState;
	public string chapterName;

	//-----------------------平衡條 & 血條-----------------------
	public float balanceValue;
	public Slider balanceSlider;
	public Text balanceText;

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
	public GameObject winFade;
	Animator fade;
	public SkeletonAnimation fadeAni;
	public GameObject lose_Fade;

	//倒數
	public Text countdown;
	int time_int = 4;

	public GameObject warning;
	public Canvas canvas;

	//結算
	public GameObject winObj;
	private float addInt;
	public Text addText;

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

	void Start () {
		Puase.interactable = false;
		Instance = this;
		fade = winFade.GetComponent<Animator>();
		HintAni = HintObj.GetComponent<Animator>();
		lose_Fade.SetActive(false);
		Application.targetFrameRate = 100;  //幀數
		balanceValue = PlayerPrefs.GetFloat("StaticObject.balanceSlider");
		playerHealth = PlayerPrefs.GetFloat("StaticObject.playerHealth");
		HealthSlider.value = playerHealth;
		balanceSlider.value = balanceValue;
		balanceText.text = Mathf.Floor(balanceValue).ToString("0");
		Debug.Log(balanceValue);
		Debug.Log(playerHealth);
		if (chapterName == "0")
		{
			StartCoroutine("Target");
		}
		else if (chapterName == "1")
		{
			InvokeRepeating("timer", 1, 1);
		}
	}

	public void Update()
	{
		if (gameState == GameState.Win)
		{
			if (balanceSlider.value >= 100 || balanceValue >=100)
			{
				balanceValue = 100;
				balanceSlider.value = balanceValue;
				balanceText.text = Mathf.Floor(balanceValue).ToString("0");
			}
			else if(balanceSlider.value < 100)
			{
				balanceValue += 100f;
				balanceSlider.value = balanceValue;
				balanceText.text = Mathf.Floor(balanceValue).ToString("0");
			}
		}

	}

	IEnumerator Target() {
		gameState = GameState.Start;
		maskGroup.SetActive(true);
		//mask.uvRect = new Rect(0.79f, 0.26f, 1.5f, 1.5f);
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
		//mask.uvRect = new Rect(0.38f, 0.3f, 1.5f, 1.5f);
		HintAni.SetTrigger("close");
		maskGroup.SetActive(false);
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
		winObj.SetActive(true);
		addInt = 100 - Mathf.Floor(balanceValue);
		addText.text = "+" + addInt;
		yield return new WaitForSeconds(4f);
		winFade.SetActive(true);
		fade.SetBool("FadeOut", true);
		StaticObject.balanceSlider = balanceValue;
		StaticObject.playerHealth = HealthSlider.value;
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", HealthSlider.value);
		Debug.Log(StaticObject.balanceSlider);
		Debug.Log(StaticObject.playerHealth);
		yield return new WaitForSeconds(1.5f);
		
		SceneManager.LoadScene("Chapter0_5movie");  //接下一關
	}

	IEnumerator WaitForAudio()
	{
		yield return new WaitForSeconds(0);
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
