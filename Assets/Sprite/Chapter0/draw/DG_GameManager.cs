﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public enum DrawState
{
	/*Start, Pause, playerRound, playerAtt, monsterRound, monsterAtt, Dead,  Win*/
	/*Start, Pause, Game, Dead, Win*/
	Teach, Pause, Game, Dead, Win
}

public class DG_GameManager : MonoBehaviour {

	public DG_playerController dg_playerController;
	public DG_EnemyController dg_enemyController;
	public cut cut1;
	public cut cut2;
	public ExampleGestureHandler geature;
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

	public static DG_GameManager Instance;
	public static DrawState drawState;
	//public DrawState drawState = DrawState.Game;

	float StartTime = 2;

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

	//------------------教學暫停物件------------------
	public Text teachText;
	public GameObject finger;
	public Image joystick; //image射線關閉
	public Image jumpBtn; //image設限關閉
	//public GameObject healthBar;
	public Canvas drawCanvas;
	public bool TeachMove = false;
	public bool TeachJump = false;
	public RawImage mask;
	public GameObject maskObj;
	private Animator HitOpen;
	public GameObject HitObj;
	private Animator fingerAnim;
	public GameObject fingerObj;
	private Animator enemyAnim;
	public GameObject enemy;
	public GameObject BossEnemy;
	//public BoxCollider2D BossCollider;
	private bool end;
	public GameObject wall;
	public GameObject blade;

	void Start () {
		Time.timeScale = 1f;
		drawState = DrawState.Teach;
		Puase.interactable = true;
		Instance = this;
		//InvokeRepeating("StarGame", 1, 1);
		fade = winFade.GetComponent<Animator>();

		//---------------教學--------------
		finger.SetActive(false);
		joystick.raycastTarget = false;
		jumpBtn.raycastTarget = false;
		drawCanvas.GetComponent<Canvas>().enabled = false;
		HitOpen = HitObj.GetComponent<Animator>();
		fingerAnim = fingerObj.GetComponent<Animator>();
		enemyAnim = enemy.GetComponent<Animator>();


	}

	void Awake()
	{
		StartCoroutine("count1");
	}

	void FixedUpdate () {

		Debug.Log(drawState);
		if (end) 
		{
			textPanel.SetActive(true);
			Puase.interactable = false;
			text.text = "趁他們暈的時候，我們趕快從後面的門逃吧！";
			StartCoroutine("run");	
		}
		/*if (drawState == DrawState.Teach) //平衡條不扣
		{

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

			if (balanceSlider.value == 0 || dg_playerController.curHealth ==0)
			{
				Debug.Log("战斗失败");
				drawState =DrawState.Dead;
			}
			if (dg_enemyController.curHealth==0)
			{
				Debug.Log("战斗胜利");
				drawState = DrawState.Win;
			}

		}
	}

	IEnumerator count1()
	{
		drawState = DrawState.Teach;
		teachText.text = "目標！使用魔法擊退敵人！";
		teachText.fontSize = 34;
		maskObj.SetActive(true);
		mask.uvRect = new Rect(1.15f, 0.26f, 1.5f, 1.5f);
		//black_bgImage.SetActive(true);
		yield return new WaitForSeconds(2);
		StartCoroutine("count2");
	}

	IEnumerator count2()
	{
		HitOpen.SetTrigger("HitOpen");
		teachText.fontSize = 28;
		teachText.text = "上方的平衡條會隨挑戰時間流逝";
		balanceSlider.transform.SetAsLastSibling();
		yield return new WaitForSeconds(2);
		teachText.text = "當數值歸零，則挑戰失敗！";
		yield return new WaitForSeconds(2);
		teachText.text = "接下來嘗試看看移動吧！";
		balanceSlider.transform.SetAsFirstSibling();
		yield return new WaitForSeconds(2);
		StartCoroutine("count3");
	}


	IEnumerator count3()
	{
		//drawState = DrawState.Game;
		HitOpen.SetTrigger("HitOpen");
		fingerObj.SetActive(true);
		//maskObj.SetActive(true);
		mask.uvRect = new Rect(0.33f, 0.26f, 1.5f, 1.5f);
		teachText.text = "使用移動鍵移動角色";
		teachText.fontSize = 28;
		joystick.raycastTarget = true;
		//black_bgImage.SetActive(false);
		finger.SetActive(true);
		yield return new WaitUntil(() => TeachMove);
		maskObj.SetActive(false);
		joystick.raycastTarget = false;
		fingerObj.SetActive(false);
		yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
		StartCoroutine("count4");
	}

	IEnumerator count4()
	{
		drawState = DrawState.Teach;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "Good！";
		yield return new WaitForSeconds(2);
		StartCoroutine("count5");
	}

	IEnumerator count5()
	{
		//drawState = DrawState.Game;
		fingerObj.SetActive(true);
		maskObj.SetActive(true);
		mask.uvRect = new Rect(-0.75f, 0.34f, 1.5f, 1.5f);
		fingerAnim.SetInteger("finger", 1);
		HitOpen.SetTrigger("HitOpen");
		jumpBtn.raycastTarget = true;
		teachText.text = "點擊跳鍵讓角色跳躍";
		yield return new WaitUntil(() => TeachJump);
		maskObj.SetActive(false);
		fingerObj.SetActive(false);
		StartCoroutine("count6");
	}

	IEnumerator count6()
	{
		//drawState = DrawState.Teach;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "GREAT！";
		jumpBtn.raycastTarget = false;
		yield return new WaitForSeconds(2);
		HitObj.SetActive(false);
		StartCoroutine("count7");
	}

	IEnumerator count7()
	{
		HitObj.SetActive(true);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "小怪物來襲！";
		cut1.GetComponent<cut>().enabled = false;
		cut2.GetComponent<cut>().enabled = false;
		enemy.SetActive(true);
		dg_playerController.graphics.localRotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitForSeconds(2f);
		teachText.text = "被怪物觸碰到會損失血量，請小心";
		yield return new WaitForSeconds(2f);
		enemyAnim.SetTrigger("Atk");
		StartCoroutine("count8");
	}

	IEnumerator count8()
	{
		yield return new WaitForSeconds(2f);
		//drawState = DrawState.Game;	
		HitObj.SetActive(true);
		teachText.text = "現在準備反擊！";
		HitOpen.SetTrigger("HitOpen");
		yield return new WaitForSeconds(2f);
		teachText.text = "在場景上畫出線條，消滅小怪物";
		cut1.GetComponent<cut>().enabled = true;
		cut2.GetComponent<cut>().enabled = true;
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 2);
		drawCanvas.GetComponent<Canvas>().enabled = true;
		yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
		fingerObj.SetActive(false);
		yield return new WaitUntil(() => cut.isDeath >=2);
		teachText.text = "消滅成功！Perfect！";
		drawCanvas.GetComponent<Canvas>().enabled = false;
		blade.transform.position = new Vector2(0,0);
		//drawState = DrawState.Teach;
		yield return new WaitForSeconds(2f);
		StartCoroutine("count9");
	}

	IEnumerator count9()
	{
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "警告！小BOSS即將來襲！！！";
		yield return new WaitForSeconds(1f);
		BossEnemy.SetActive(true);
		yield return new WaitForSeconds(2f);
		drawCanvas.GetComponent<Canvas>().enabled = true;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "連續攻擊小BOSS";
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 3);
		yield return new WaitUntil(() => Input.GetMouseButton(0));
		fingerObj.SetActive(false);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "繼續攻擊";
		yield return new WaitUntil(() => dg_enemyController.curHealth<=80);  //BUG
		drawCanvas.GetComponent<Canvas>().enabled = false;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "看來我們該使用其他方法才能加快攻擊";
		maskObj.SetActive(true);
		mask.uvRect = new Rect(1.15f, 0.26f, 1.5f, 1.5f);
		yield return new WaitForSeconds(3f);
		StartCoroutine("count10");
	}

	IEnumerator count10()
	{
		//maskObj.SetActive(true);
		mask.uvRect = new Rect(-0.21f, 0.34f, 2.5f, 2.5f);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "下方為已蒐集到技能";
		yield return new WaitForSeconds(2f);
		drawState = DrawState.Game;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "等待技能冷卻完畢即可使用";
		yield return new WaitForSeconds(5f);    //M技能時間 
		maskObj.SetActive(false);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "使用M技能\n在螢幕中央一筆畫出M字形";
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 4);
		drawCanvas.GetComponent<Canvas>().enabled = true;
		yield return new WaitUntil(() => geature.isAtk ==true);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "Excellent！";
		fingerObj.SetActive(false);
		//BossCollider.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(2f);
		wall.SetActive(false);
		HitObj.SetActive(false);
		end = true;
	}

	public void StarGame() {
		StartTime -= 1*Time.deltaTime;
		if (StartTime < 0)
		{		
			CancelInvoke();
		}
		Debug.Log(StartTime);
	}


	IEnumerator run() {
		
		audio.clip = runSound;
		audio.Play();
		yield return new WaitForSeconds(2);
		textPanel.SetActive(false);
		isRun = true;
		end = false;
		//dg_playerController.animator_S.SetBool("run", true);
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
