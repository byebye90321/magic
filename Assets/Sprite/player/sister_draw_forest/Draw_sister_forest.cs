using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class Draw_sister_forest : MonoBehaviour
{

	/*public static Draw_sister_forest drawPlayerController;
	public ExampleGestureHandler exampleGestureHandler;
	//--------------------------畫符，滑鼠-------------------------
	public GameObject Gesture;
	public GameObject mouseEffect;
	//--------------------------Animation-------------------------
	public SkeletonAnimation skeletonAnimation_S;

	//-----------------------------血量---------------------------
	public HeartSystemMonster heartSystemMonster;
	public HeartSystem heartSystem;
	public int attack = 10;

	public GameObject hurtTextObj;
	public Text hurtText;

	//--------------------------回合控制脚本----------------------
	private Draw_sister_GM drawGameManager;

	//获取敌人脚本的引用 
	private Draw_sister_forest_Enemy drawEnemyController;

	public Text hint;

	//-----------------------------卡牌--------------------------
	public CardCD_sister_forest card1;
	public CardCD_sister_forest card2;
	public CardCD_sister_forest card3;
	public CardCD_sister_forest card4;
	private CardInfo cardInfo;
	public CardData cardData;


	public int index = 1;
	public int cardindex1 = 1;
	public int cardindex2 = 1;
	public int cardindex3 = 1;
	public int cardindex4 = 1;

	//--------------音效
	public AudioSource audio;
	public AudioClip atkSound;

	public GameObject effectTest;
	void Start()
	{
		drawGameManager = GameObject.Find("DrawGameController").GetComponent<Draw_sister_GM>();
		drawEnemyController = GameObject.FindGameObjectWithTag("monster").GetComponent<Draw_sister_forest_Enemy>();
		Debug.Log("現在第" + index);
		//audio = GetComponent<AudioSource>();
		//skeletonAnimation = GetComponent<SkeletonAnimation>();
		skeletonAnimation_S.state.SetAnimation(0, "idle", true);  //(起始偵,動畫名,loop)

	}

	void Update()
	{

		if (drawGameManager.drawState == Draw_sister_GM.DrawState.Game)
		{
			if (drawGameManager.isWaitForPlayer)
			{
				StartCoroutine("playerWait");
			}
		}

	}

	IEnumerator playerWait()
	{
		hint.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(1f);
		if (cardData.card00.isAtk == false && cardData.card02.isAtk == false && cardData.card05.isAtk == false && cardData.card06.isAtk == false && cardData.card10.isAtk == false && cardData.card13.isAtk == false && cardData.card14.isAtk == false)
		{
			hint.text = "現在沒有可使用卡牌！";
			yield return new WaitForSeconds(2f);
			drawGameManager.isWaitForPlayer = false;
			drawGameManager.isEnemyAction = true;
			hint.text = "";
			hint.GetComponent<Text>().enabled = false;
		}
		else
		{
			Gesture.SetActive(true);
			mouseEffect.SetActive(true);
			playRound();
			hint.text = "畫出指定圖形打擊敵人！";
		}
	}

	public void playRound()
	{
		if (ExampleGestureHandler.playerAtk == true)
		{
			drawGameManager.isWaitForPlayer = false;

			Debug.Log("主角使用了普通攻击");
			audio.PlayOneShot(atkSound);
			hint.GetComponent<Text>().enabled = false;
			skeletonAnimation_S.state.SetAnimation(0, "hit", false);
			skeletonAnimation_S.state.AddAnimation(0, "idle", true, 0);
			StartCoroutine("waitAtk");
			ExampleGestureHandler.playerAtk = false;
			StartCoroutine("EnemyWait");
		}
		else
		{
			ExampleGestureHandler.playerAtk = false;
		}
	}
	IEnumerator waitAtk()
	{
		while (!ExampleGestureHandler.playerAtk)
		{
			yield return null;
		}

		if (ExampleGestureHandler.playerAtk == true)
		{
			//yield return new WaitForSeconds(1f);
			hurtTextObj.SetActive(true);
			hurtText.text = "-" + exampleGestureHandler.cardAtk;
			effectTest.SetActive(true);
			heartSystemMonster.TakeDamage(exampleGestureHandler.cardAtk);
			drawEnemyController.skeletonAnimation_E1.state.SetAnimation(0, "death", false);
			drawEnemyController.skeletonAnimation_E1.state.AddAnimation(0, "idle", true, 0);
			drawEnemyController.skeletonAnimation_E2.state.SetAnimation(0, "death", false);
			drawEnemyController.skeletonAnimation_E2.state.AddAnimation(0, "idle", true, 0);
			
			//drawEnemyController.skeletonAnimation_E2.skeleton.SetColor(new Color(255,255,255,0.1f));
			//drawEnemyController.E1.transform.position = new Vector2(drawEnemyController.E1.transform.position.x + 0.5f, drawEnemyController.E1.transform.position.y);
			yield return new WaitForSeconds(1f);
			effectTest.SetActive(false);
			Gesture.SetActive(false);
			hurtTextObj.SetActive(false);
			if (GameObject.Find("ps1").activeSelf == true)
			{
				GameObject.Find("ps1").SetActive(false);
			}
			mouseEffect.SetActive(false);
			/*exampleGestureHandler.effectA.SetActive(false);
			exampleGestureHandler.effectB.SetActive(false);
			exampleGestureHandler.effectC.SetActive(false);
		}
	}

	//在切换到敌人操作前添加延迟
	IEnumerator EnemyWait()
	{
		hint.text = "";
		yield return new WaitForSeconds(2f);
		drawGameManager.isWaitForPlayer = false;
		Gesture.SetActive(false);
		if (GameObject.Find("ps1").activeSelf == true)
		{
			GameObject.Find("ps1").SetActive(false);
		}
		mouseEffect.SetActive(false);
		hint.text = "";
		hint.GetComponent<Text>().enabled = false;
		drawGameManager.isEnemyAction = true;
	}

	//承受伤害  
	public void ReceiveDamage(int damage)
	{
		heartSystem.TakeDamage(drawEnemyController.Atk);


		if (heartSystem.curHealth <= 0)
		{
			Debug.Log("playerDead");
			/*skeletonAnimation_S.state.SetAnimation(0, "death", false);
			skeletonAnimation_B.state.SetAnimation(0, "death", false);
		}
	}

*/


}
