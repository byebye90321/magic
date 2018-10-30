using System.Collections;
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
	public int count = 1;
	public GameObject finger;
	public Image joystick; //image射線關閉
	public Image jumpBtn; //image設限關閉
	public GameObject healthBar;
	public Canvas drawCanvas;
	public bool TeachMove = false;
	public bool TeachJump = false;
	public RawImage mask;
	public GameObject maskObj;
	public Animator HitOpen;
	public GameObject HitObj;
	public Animator fingerAnim;
	public GameObject fingerObj;
	public Animator enemyAnim;
	public GameObject enemy;


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
	}

	void Awake()
	{
		StartCoroutine("count1");
	}

	void FixedUpdate () {

		/*if (DrawEnemyController.end == true) 
		{
			textPanel.SetActive(true);
			Puase.interactable = false;
			text.text = "糟了！他們好像太厲害了，我們還是先逃吧！那邊有門！";
			StartCoroutine("run");	
		}*/
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
		teachText.text = "目標！使用魔法擊退敵人！";
		teachText.fontSize = 34;
		black_bgImage.SetActive(true);
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
		HitOpen.SetTrigger("HitOpen");
		fingerObj.SetActive(true);
		maskObj.SetActive(true);
		mask.uvRect = new Rect(0.33f, 0.26f, 1.5f, 1.5f);
		teachText.text = "使用移動鍵移動角色";
		teachText.fontSize = 28;
		joystick.raycastTarget = true;
		black_bgImage.SetActive(false);
		finger.SetActive(true);
		yield return new WaitUntil(() => TeachMove);
		maskObj.SetActive(false);
		joystick.raycastTarget = false;
		jumpBtn.raycastTarget = true;
		fingerObj.SetActive(false);
		yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
		StartCoroutine("count4");
	}

	IEnumerator count4()
	{
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "Good！";
		yield return new WaitForSeconds(2);
		StartCoroutine("count5");
	}

	IEnumerator count5()
	{
		fingerObj.SetActive(true);
		maskObj.SetActive(true);
		mask.uvRect = new Rect(-0.75f, 0.34f, 1.5f, 1.5f);
		fingerAnim.SetInteger("finger", 1);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "點擊跳鍵讓角色跳躍";
		yield return new WaitUntil(() => TeachJump);
		maskObj.SetActive(false);
		fingerObj.SetActive(false);
		StartCoroutine("count6");
	}

	IEnumerator count6()
	{
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "GREAT！";
		dg_playerController.graphics.localRotation = Quaternion.Euler(0, 0, 0);
		jumpBtn.raycastTarget = false;
		yield return new WaitForSeconds(2);
		HitObj.SetActive(false);
		StartCoroutine("count7");
	}

	IEnumerator count7()
	{
		enemy.SetActive(true);
		yield return new WaitForSeconds(2);
		HitObj.SetActive(true);
		HitOpen.SetTrigger("HitOpen");
		teachText.text = "小怪物來襲！";
		yield return new WaitForSeconds(1.5f);
		HitObj.SetActive(false);
		enemyAnim.SetTrigger("Atk");
	}


	public void StarGame() {
		StartTime -= 1*Time.deltaTime;
		if (StartTime < 0)
		{
			count = 4;
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
