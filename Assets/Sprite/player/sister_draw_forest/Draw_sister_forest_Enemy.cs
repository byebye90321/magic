using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class Draw_sister_forest_Enemy : MonoBehaviour
{

	public static Draw_sister_forest_Enemy drawEnemy;

	//--------------------------Animation-------------------------
	public SkeletonAnimation skeletonAnimation_E1;
	public SkeletonAnimation skeletonAnimation_E2;


	//----------------血量--------------
	public HeartSystem heartSystem;
	public HeartSystemMonster heartSystemMonster;

	public GameObject hurtTextObj;
	public Text hurtText;

	public GameObject AllClear;

	//回合控制脚本
	private Draw_sister_GM drawGameManager;

	//获取敌人脚本的引用 
	private Draw_sister_forest drawPlayerController;
	private Defensive defensive;
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
	//卡牌
	public CardData cardData;
	public ExampleGestureHandler exampleGestureHandler;

	public Transform graphics1;
	public Transform graphics2;

	//--------------音效
	public AudioSource audio;
	public AudioSource countAudio;
	public AudioClip countSound;
	public AudioClip atkSound;

	void Start()
	{
		drawGameManager = GameObject.Find("DrawGameController").GetComponent<Draw_sister_GM>();
		drawPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Draw_sister_forest>();
		//audio = GetComponent<AudioSource>();
		//skeletonAnimation_E1 = GetComponent<SkeletonAnimation>();
		skeletonAnimation_E1.state.SetAnimation(0, "idle", true);  //(起始偵,動畫名,loop)
		skeletonAnimation_E2.state.SetAnimation(0, "idle", true);

	}

	void Update()
	{
		if (drawGameManager.drawState == Draw_sister_GM.DrawState.Game)
		{
			if (Attack == true)
			{

				if (atkpreft.activeInHierarchy == true)
				{
					Debug.Log(Attack);
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
		if (heartSystemMonster.curHealth <= 0)
		{
			StartCoroutine("monsterRun");

		}
	}

	IEnumerator timer(float time)
	{
		timerLock = true;
		yield return new WaitForSeconds(time);
		time_int -= 1;
		countdown.text = time_int + "";

		if (time_int == 0)
		{
			/*countdown.text = "0";
			yield return new WaitForSeconds(1);*/
			countdown.enabled = false;
			if (defensiveCount == AtkCount)//全點掉完  
			{
				AllClear.SetActive(true);
				skeletonAnimation_E1.state.SetAnimation(0, "death", false);
				skeletonAnimation_E1.state.AddAnimation(0, "idle", true, 0);
				skeletonAnimation_E2.state.SetAnimation(0, "death", false);
				skeletonAnimation_E2.state.AddAnimation(0, "idle", true, 0);
				drawPlayerController.hurtTextObj.SetActive(true);
				drawPlayerController.hurtText.text = "-2";
				heartSystemMonster.TakeDamage(2);  //ALL CLear
				yield return new WaitForSeconds(1);
				AllClear.SetActive(false);
				drawPlayerController.hurtTextObj.SetActive(false);
				StartCoroutine("PlayerUIWait");
				drawGameManager.isEnemyAction = false;
			}
			else
			{
				StartCoroutine("Atking");
			}

		}
		timerLock = false;
	}

	IEnumerator startAtk()
	{
		while (!drawGameManager.isEnemyAction)
		{
			yield return null;
		}
		if (drawGameManager.isEnemyAction )
		{		
			InvokeRepeating("AtkPoistion", 0, 1);
			yield return new WaitForSeconds(1);

		}
	}
	public void AtkPoistion()
	{
		if (drawGameManager.isEnemyAction && Draw_sister_GM.end == false)
		{
			Atk_int -= 1;
			if (Atk_int == 0)
			{
				for (int i = 0; i < AtkCount; i++)
				{
					GameObject NEWatkpreft = (GameObject)Instantiate(atkpreft) as GameObject;
					NEWatkpreft.transform.position = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.5f, 4.0f), 0);
					/*Debug.Log(NEWatkpreft.activeInHierarchy);
					atkpreft = NEWatkpreft;
					Debug.Log(atkpreft.activeInHierarchy);*/
				}
				CancelInvoke("AtkPoistion");

			}

		}
	}

	IEnumerator Atking()
	{

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

	IEnumerator AtkHeart()
	{
		while (atkpreft.transform.position.x - player.transform.position.x > 0.1)
		{
			yield return null;
		}
		if (Mathf.Abs(atkpreft.transform.position.x - player.transform.position.x) < 0.1f)
		{
			yield return new WaitForSeconds(1f);
			hurtTextObj.SetActive(true);
			hurtText.text = "-" + (AtkCount - defensiveCount);
			shake.shakeCamera();
			drawPlayerController.skeletonAnimation_S.state.SetAnimation(0, "death", false);
			drawPlayerController.skeletonAnimation_S.state.AddAnimation(0, "idle", true, 0);
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			falsh.SetActive(false);
		}
	}

	IEnumerator monsterRun()
	{
		yield return new WaitForSeconds(1f);
		skeletonAnimation_E1.state.SetAnimation(0, "walk", true);
		graphics1.localRotation = Quaternion.Euler(0, 0, 0);
		skeletonAnimation_E2.state.SetAnimation(0, "walk", true);
		graphics2.localRotation = Quaternion.Euler(0, 0, 0);
		graphics1.transform.position = new Vector2(graphics1.transform.position.x + 0.1f, graphics2.transform.position.y);
		graphics2.transform.position = new Vector2(graphics2.transform.position.x + 0.1f, graphics2.transform.position.y);
	}

	//在切换到玩家UI前延迟
	IEnumerator PlayerUIWait()
	{
		hurtTextObj.SetActive(false);
		drawGameManager.isEnemyAction = false;
		yield return new WaitForSeconds(0);

		time_int = 3;
		countdown.text = time_int + "";
		/*Atk1.SetActive(false);
		Atk2.SetActive(false);*/
		Attack = false;
		Atk_int = 1;
		drawPlayerController.index += 1;
		drawPlayerController.cardindex1 += 1;
		drawPlayerController.cardindex2 += 1;
		drawPlayerController.cardindex3 += 1;
		drawPlayerController.cardindex4 += 1;
		Debug.Log("現在第幾回" + drawPlayerController.index);
		defensiveCount = 0;
		drawGameManager.isWaitForPlayer = true;
	}

	//承受伤害  
	public void ReceiveDamage(int damage)
	{
		heartSystemMonster.TakeDamage(exampleGestureHandler.cardAtk);

		if (heartSystemMonster.curHealth <= 0)
		{
			Debug.Log("mosnterDead");
		}
	}
}


