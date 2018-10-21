using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DrawEnemyController : MonoBehaviour{

	public static DrawEnemyController drawEnemy;

	//--------------------------Animation-------------------------
	/*[SpineAnimation]
	public string jumpAnim;*/
	public SkeletonAnimation skeletonAnimation_E1;
	public SkeletonAnimation skeletonAnimation_E2;

	//----------------血量-----------------
	public HeartSystem heartSystem;
	public HeartSystemMonster heartSystemMonster;

	public GameObject hurtTextObj;
	public Text hurtText;

	//回合控制脚本
	private DrawGameManager drawGameManager;

	//获取敌人脚本的引用 
	private DrawPlayerController drawPlayerController;
	//private Defensive defensive;
	//倒數
	public Text countdown;
	private bool timerLock = false;//防止計時器計時到之前被呼叫
	private int count1 = 0;
	public int time_int = 3;

	//Atk
	int Atk_int = 1;
	public int Atk;
	public bool Attack = false;
	public GameObject player;
	private bool DownState = false;

	public GameObject atkpreft;
	public int AtkCount;
	public int defensiveCount;
	public GameObject falsh;
	public GameObject circle;
	//卡牌
	//public CardData cardData;
	//public ExampleGestureHandler exampleGestureHandler;

	//--------------音效
	public AudioSource audio;
	public AudioSource countAudio;
	public AudioClip countSound;
	public AudioClip atkSound;
	//結束
	public static bool end = false;

	void Start()
	{		
		drawGameManager = GameObject.Find("DrawGameController").GetComponent<DrawGameManager>();
		drawPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<DrawPlayerController>();
		//audio = GetComponent<AudioSource>();
		//skeletonAnimation_E1 = GetComponent<SkeletonAnimation>();
		skeletonAnimation_E1.state.SetAnimation(0, "idle", true);  //(起始偵,動畫名,loop)
		skeletonAnimation_E2.state.SetAnimation(0, "idle", true);
		
	}
	
	void Update()
	{
		if (drawGameManager.drawState == DrawGameManager.DrawState.Game)
		{
			if (Attack == true)
			{
				if (atkpreft.activeInHierarchy == true)
				{
					//defensive = GameObject.FindGameObjectWithTag("EnemyAttack").GetComponent<Defensive>();
					atkpreft.transform.position = GameObject.FindGameObjectWithTag("EnemyAttack").transform.position;
					atkpreft.transform.position = Vector3.Lerp(atkpreft.transform.position, player.transform.position, Time.time);
				}
			}
			if (drawGameManager.isEnemyAction & time_int > 0)
			{
				countdown.enabled = true;
				if (timerLock == false)
				{
					countAudio.PlayOneShot(countSound);
					StartCoroutine(timer(1f));//延遲1秒
				}
				StartCoroutine("startAtk");
			}
		}

	}

	IEnumerator timer(float time)
	{
		circle.SetActive(true);
		timerLock = true;
		yield return new WaitForSeconds(time);
		time_int -= 1;
		countdown.text = time_int + "";
		
		if (time_int == 0)
		{
			/*countdown.text = "0";
			yield return new WaitForSeconds(1);*/
			circle.SetActive(false);
			countdown.enabled = false;
			if (defensiveCount==AtkCount)//全攻擊完  
			{
				yield return new WaitForSeconds(1);
				StartCoroutine("PlayerUIWait");
				drawGameManager.isEnemyAction = false;
			}
			else {
				StartCoroutine("Atking");
			}
			

		}
		timerLock = false;
	}

	IEnumerator startAtk() {
		while (!drawGameManager.isEnemyAction) {
			yield return null;
		}
		if (drawGameManager.isEnemyAction && end == false) {
			InvokeRepeating("AtkPoistion", 0, 1);
			yield return new WaitForSeconds(1);
		}
	}
	public void AtkPoistion() {
		if (drawGameManager.isEnemyAction)
		{
			Atk_int -= 1;
			if (Atk_int == 0)
			{
				for (int i = 0;i < AtkCount; i++){
					GameObject NEWatkpreft = (GameObject)Instantiate(atkpreft) as GameObject;
					NEWatkpreft.transform.position = new Vector3(Random.Range(-4.0f, 3.0f), Random.Range(-2.0f, 2.0f),1);
					/*Debug.Log(NEWatkpreft.activeInHierarchy);
					atkpreft = NEWatkpreft;
					Debug.Log(atkpreft.activeInHierarchy);*/
				}
				CancelInvoke("AtkPoistion");
				
			}
			
		}
	}

	IEnumerator Atking() {
		
		Debug.Log("敌人使用了普通攻击");
		audio.PlayOneShot(atkSound);
		skeletonAnimation_E1.state.SetAnimation(0, "hit", false);
		skeletonAnimation_E1.state.AddAnimation(0, "idle", true, 0);
		skeletonAnimation_E2.state.SetAnimation(0, "hit", false);
		skeletonAnimation_E2.state.AddAnimation(0, "idle", true, 0);
		yield return new WaitForSeconds(0.5f);
		Attack = true;
		StartCoroutine("AtkHeart");
		
		//延迟1s出现玩家操作UI
		yield return new WaitForSeconds(2);
		
		StartCoroutine("PlayerUIWait");
		//drawGameManager.isEnemyAction = false;
	}

	IEnumerator AtkHeart() {
		while (atkpreft.transform.position.x- player.transform.position.x>0.1)
		{	
			yield return null;
		}
		if (Mathf.Abs(atkpreft.transform.position.x - player.transform.position.x) < 0.1f)
		{
			yield return new WaitForSeconds(1f);
			hurtTextObj.SetActive(true);
			hurtText.text = "-" + (AtkCount - defensiveCount);
			drawPlayerController.skeletonAnimation_S.state.SetAnimation(0, "death", false);
			drawPlayerController.skeletonAnimation_S.state.AddAnimation(0, "idle", true, 0);
			drawPlayerController.skeletonAnimation_B.state.SetAnimation(0, "death", false);
			drawPlayerController.skeletonAnimation_B.state.AddAnimation(0, "idle", true, 0);		
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			falsh.SetActive(false);
		}
		

	}

	//在切换到玩家UI前延迟
	IEnumerator PlayerUIWait()
	{
		hurtTextObj.SetActive(false);
		drawGameManager.isEnemyAction = false;
		drawGameManager.roundText.enabled = false;
		yield return new WaitForSeconds(0);
		end = true;

	}

	//承受伤害  
	public void ReceiveDamage(int damage)
	{
		//heartSystemMonster.TakeDamage(exampleGestureHandler.cardAtk);
		heartSystemMonster.TakeDamage(1);

		if (heartSystemMonster.curHealth <= 0)
		{
			Debug.Log("mosnterDead");
			/*skeletonAnimation_E1.state.SetAnimation(0, "death", false);
			skeletonAnimation_E2.state.SetAnimation(0, "death", false);*/
		}
	}
}


