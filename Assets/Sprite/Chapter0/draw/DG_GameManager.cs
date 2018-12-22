using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public enum DrawState
{
	Teach, Pause, Game, Dead, Win
}

public class DG_GameManager : MonoBehaviour {

	public string ChapterName;
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
	float r2 = 0.7372549f, g2 = 0.2078431f, b2 = 0.3071967f;  //殘血後平衡條色
	public Text balanceText;

	public static DG_GameManager Instance;
	public static DrawState drawState;
	//public DrawState drawState = DrawState.Game;

	float StartTime = 2;

	//-------------------對話----------------------
	public GameObject textPanel;
	public Text text;

	//------------------FADE淡出-------------------
	public GameObject winFade;
	Animator fade;
	public bool isRun = false;

	//--------------------音效---------------------
	public AudioSource audio;
	public AudioClip runSound;
	public AudioClip warningSound;

	//------------------教學暫停物件------------------
	public Text teachText;
	public GameObject finger;
	public Image joystick; //image射線關閉，準心點
	public Image joystickOutline; //外框區
	public Image jumpBtn; //image射線關閉，跳躍鍵
	public Canvas drawCanvas;
	[HideInInspector]
	public bool TeachMove = false;
	[HideInInspector]
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
	public BoxCollider2D BossCollider;
	private bool end;
	public GameObject blade;
	public GameObject warningRedImage;
	public GameObject warningExclamation;
	public GameObject arrow;
	public GameObject notDrawImage;
	//---------------------------------3版-------------------
	public GameObject TouchNextImage;
	private int count = 0;
	public GameObject joystickCanvas;
	public GameObject NextFlashText;


	void Start () {

		if (ChapterName == "0")  //序章初始
		{
			Time.timeScale = 1f;
			drawState = DrawState.Game;
			Instance = this;
			//InvokeRepeating("StarGame", 1, 1);
			fade = winFade.GetComponent<Animator>();

			//---------------教學--------------
			finger.SetActive(false);

			//-----------虛擬搖桿-----------
			joystick.raycastTarget = false;
			jumpBtn.raycastTarget = false;
			joystick.color = new Color(255, 255, 255, 0);
			joystickOutline.color = new Color(255, 255, 255, 0);
			jumpBtn.color = new Color(255, 255, 255, 0);

			drawCanvas.GetComponent<Canvas>().enabled = false;
			HitOpen = HitObj.GetComponent<Animator>();
			fingerAnim = fingerObj.GetComponent<Animator>();
			enemyAnim = enemy.GetComponent<Animator>();
			mask.uvRect = new Rect(1.15f, 0.26f, 1.5f, 1.5f);
			//joystickCanvas.SetActive(false);
			StartCoroutine("count1");
		}
	}

	void FixedUpdate () {


		if (drawState == DrawState.Game)
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

	public void Next() {
		count += 1;
	}


	IEnumerator count1() //開頭
	{
		teachText.text = "目標！使用魔法擊退敵人！";
		teachText.fontSize = 34;
		maskObj.SetActive(true);
		yield return new WaitUntil(() => count == 1);
		StartCoroutine("count2");
	}

	IEnumerator count2()  //平衡條介紹
	{
		HitOpen.SetTrigger("HitOpen");
		teachText.fontSize = 28;
		balanceSlider.transform.SetSiblingIndex(4);
		teachText.text = "上方的平衡條代表世界的平衡值";
		yield return new WaitUntil(() => count == 2);
		teachText.text = "數值會隨時間流逝";
		yield return new WaitUntil(() => count == 3);
		teachText.text = "若數值歸零，則必須重新遊玩！";
		yield return new WaitUntil(() => count == 4);	
		balanceSlider.transform.SetAsFirstSibling();		//balanceSlider.transform.SetAsLastSibling();
		StartCoroutine("count3");
	}

	IEnumerator count3() //小怪物
	{
		//----------小怪物傷害----------
		HitObj.SetActive(true);
		teachText.text = "接下來將介紹基本攻擊方式";
		yield return new WaitUntil(() => count == 5);
		maskObj.SetActive(false);
		HitObj.SetActive(true);
		teachText.text = "小怪物來襲！";
		cut1.GetComponent<cut>().enabled = false;
		cut2.GetComponent<cut>().enabled = false;
		enemy.SetActive(true);
		dg_playerController.graphics.localRotation = Quaternion.Euler(0, 0, 0);
		dg_playerController.healthCanvas.localRotation = Quaternion.Euler(0, 0, 0);
		yield return new WaitUntil(() => count == 6);
		NextFlashText.SetActive(false);
		TouchNextImage.SetActive(false);
		teachText.text = "被小怪物觸碰到會損失血量，請注意";
		yield return new WaitForSeconds(1f);
		enemyAnim.SetTrigger("Atk");
		yield return new WaitForSeconds(2f);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "現在準備反擊！";
		//----------反擊----------
		yield return new WaitForSeconds(1f);
		teachText.text = "在場景上畫出線條，消滅小怪物";
		cut1.GetComponent<cut>().enabled = true;
		cut2.GetComponent<cut>().enabled = true;
		fingerObj.SetActive(true);
		//fingerAnim.SetInteger("finger", 1);
		drawCanvas.GetComponent<Canvas>().enabled = true;
		yield return new WaitUntil(() => cut.isDeath >= 1);
		fingerObj.SetActive(false);
		yield return new WaitUntil(() => cut.isDeath >= 2);
		teachText.text = "消滅成功！Perfect！";
		drawCanvas.GetComponent<Canvas>().enabled = false;
		blade.transform.position = new Vector2(0, 0);
		yield return new WaitForSeconds(1f);
		StartCoroutine("count4");
	}

	IEnumerator count4() //大怪物
	{
		//----------大怪物傷害----------
		HitObj.SetActive(false);
		warningRedImage.SetActive(true);
		for (int i = 0; i < 3; i++)
		{
			audio.PlayOneShot(warningSound);
			warningExclamation.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			warningExclamation.SetActive(false);
			yield return new WaitForSeconds(0.3f);
		}
		HitObj.SetActive(true);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "警告！小BOSS即將來襲！！！";
		yield return new WaitForSeconds(1f);
		BossEnemy.SetActive(true);
		TouchNextImage.SetActive(true);
		NextFlashText.SetActive(true);
		yield return new WaitUntil(() => count == 7);
		HitOpen.SetTrigger("HitOpen");
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "小BOSS會使用技能造成巨大傷害";
		yield return new WaitUntil(() => count == 8);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "攻擊即將來襲，請注意";
		yield return new WaitUntil(() => count == 9);
		TouchNextImage.SetActive(false);
		NextFlashText.SetActive(false);
		dg_enemyController.W1_Particle();
		yield return new WaitForSeconds(2f);
		//----------反擊----------
		teachText.text = "接下來輪到我們使出攻擊！";
		TouchNextImage.SetActive(true);
		NextFlashText.SetActive(true);
		yield return new WaitUntil(() => count == 10);
		TouchNextImage.SetActive(false);
		NextFlashText.SetActive(false);
		drawCanvas.GetComponent<Canvas>().enabled = true;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "連續攻擊小BOSS";
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 1);
		yield return new WaitUntil(() => dg_enemyController.curHealth <= 99);
		fingerObj.SetActive(false);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "繼續攻擊";
		yield return new WaitUntil(() => dg_enemyController.curHealth <= 90);  //BUG
		drawCanvas.GetComponent<Canvas>().enabled = false;
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "看來我們得使用其他方法才能加快攻擊";
		maskObj.SetActive(true);
		mask.uvRect = new Rect(1.15f, 0.26f, 1.5f, 1.5f);
		TouchNextImage.SetActive(true);
		NextFlashText.SetActive(true);
		yield return new WaitUntil(() => count == 11);
		TouchNextImage.SetActive(false);
		NextFlashText.SetActive(false);
		StartCoroutine("count5");
	}

	IEnumerator count5() //CD技能
	{
		mask.uvRect = new Rect(-0.21f, 0.34f, 2.5f, 2.5f);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "下方為已蒐集到技能";
		TouchNextImage.SetActive(true);
		NextFlashText.SetActive(true);
		yield return new WaitUntil(() => count == 12);
		notDrawImage.SetActive(true);
		TouchNextImage.SetActive(false);
		NextFlashText.SetActive(false);
		maskObj.SetActive(false);
		teachText.text = "使用M技能\n在螢幕中央一筆畫出M字形";
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 2);
		drawCanvas.GetComponent<Canvas>().enabled = true;
		yield return new WaitUntil(() => geature.skill0.currentCoolDown == 0);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "Excellent！";
		notDrawImage.SetActive(false);
		warningRedImage.SetActive(false);
		fingerObj.SetActive(false);
		drawCanvas.GetComponent<Canvas>().enabled = false;
		BossCollider.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(2f);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "技能須等待冷卻時間才可繼續使用";
		TouchNextImage.SetActive(true);
		NextFlashText.SetActive(true);
		yield return new WaitUntil(() => count == 13);
		HitObj.SetActive(false);

		textPanel.SetActive(true);
		Puase.interactable = false;
		text.text = "趁他們暈的時候，我們趕快從後面的門逃吧！";
		TouchNextImage.SetActive(true);
		NextFlashText.SetActive(true);
		yield return new WaitUntil(() => count == 14);
		HitObj.SetActive(true);
		TouchNextImage.SetActive(false);
		textPanel.SetActive(false);
		NextFlashText.SetActive(false);

		StartCoroutine("count6");
	}

	IEnumerator count6() //CD技能
	{
		
		//-----------虛擬搖桿-----------
		joystick.raycastTarget = true;
		jumpBtn.raycastTarget = true;
		joystick.color = new Color(255, 255, 255, 1);
		joystickOutline.color = new Color(255, 255, 255, 1);
		jumpBtn.color = new Color(255, 255, 255, 1);

		fingerObj.SetActive(true);
		maskObj.SetActive(true);
		mask.uvRect = new Rect(0.33f, 0.26f, 1.5f, 1.5f);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "使用移動鍵與跳鍵逃離這裡";
		fingerObj.SetActive(true);
		fingerAnim.SetInteger("finger", 3);
		yield return new WaitUntil(() => TeachMove);
		arrow.SetActive(true);
		maskObj.SetActive(false);
		fingerObj.SetActive(false);
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
		
		//audio.clip = runSound;
		//audio.Play();
		yield return new WaitForSeconds(3);
		textPanel.SetActive(false);
		//isRun = true;
		end = false;
		//dg_playerController.animator_S.SetBool("run", true);
		joystick.raycastTarget = true;
		jumpBtn.raycastTarget = true;
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
		PlayerPrefs.SetFloat("StaticObject.balanceSlider", balanceValue);
		PlayerPrefs.SetFloat("StaticObject.playerHealth", dg_playerController.HealthSlider.value);
		yield return new WaitForSeconds(0.1f);
		winFade.SetActive(true);
		fade.SetBool("FadeOut", true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("RunGame_chapter0");  //接下一關
	}

}
