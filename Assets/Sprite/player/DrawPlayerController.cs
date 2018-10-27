using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DrawPlayerController : MonoBehaviour {

	public static DrawPlayerController drawPlayer;
	public ExampleGestureHandler exampleGestureHandler;

	//--------------------------畫符，滑鼠-------------------------
	public GameObject Gesture;
	public GameObject mouseEffect;

	//--------------------------Animation-------------------------
	public SkeletonAnimation skeletonAnimation_S;
	public SkeletonAnimation skeletonAnimation_B;

	//-----------------------------血量---------------------------
	public HeartSystemMonster heartSystemMonster;
	public HeartSystem heartSystem;
	public int attack = 10;

	public GameObject hurtTextObj;
	public Text hurtText;
	//--------------------------回合控制脚本----------------------
	private DrawGameManager drawGameManager;

	//获取敌人脚本的引用 
	private DrawEnemyController drawEnemyController;

	public Text hint;
	
	//-----------------------------卡牌--------------------------
	public CardChapter0 card1;
	/*public CardCD card2;
	public CardCD card3;
	public CardCD card4;*/
	//private CardInfo cardInfo;
	public CardData cardData;
	float r = 0.38f, g = 0.38f, b = 0.38f, a = 1f;
	public Text curCD;

	public int index = 1;
	public int cardindex1=2;
	public int cardindex2=1;
	public int cardindex3=1;
	public int cardindex4=1;
	//--------------音效
	public AudioSource audio;
	public AudioClip atkSound;


	void Start()
	{
		drawGameManager = GameObject.Find("DrawGameController").GetComponent<DrawGameManager>();
		drawEnemyController = GameObject.FindGameObjectWithTag("monster").GetComponent<DrawEnemyController>();
		Debug.Log("現在第" + index);
		skeletonAnimation_S.state.SetAnimation(0, "idle", true);  //(起始偵,動畫名,loop)
		skeletonAnimation_B.state.SetAnimation(0, "idle", true);
		Gesture.SetActive(true);
		mouseEffect.SetActive(true);
	}

	void Update()
	{
		if (drawGameManager.drawState == DrawGameManager.DrawState.Game) {
			if (drawGameManager.isWaitForPlayer)
			{				
				StartCoroutine("playerWait");
			}
		}
		if (drawGameManager.isRun == true)
		{
			skeletonAnimation_S.AnimationName = "run";
			skeletonAnimation_B.AnimationName = "run";
		}
	}
	IEnumerator playerWait() {
		hint.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(0.5f);
		/*if (cardData.card00.isAtk == false && cardData.card02.isAtk == false && cardData.card05.isAtk == false && cardData.card06.isAtk == false && cardData.card10.isAtk == false && cardData.card13.isAtk == false && cardData.card14.isAtk == false)
		{
			hint.text = "現在沒有可使用卡牌！";
			yield return new WaitForSeconds(2f);
			drawGameManager.isWaitForPlayer = false;
			yield return new WaitUntil(() => drawGameManager.maskindex == 11);
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
		
		}*/
		cardData.card00.isAtk = true;
		playRound();
		hint.text = "畫出指定圖形打擊敵人！";
	}

	public void playRound() {
		
		if (ExampleGestureHandler.playerAtk == true)
		{
			mouseEffect.SetActive(false);
			Gesture.SetActive(false);
			drawGameManager.isWaitForPlayer = false;	
			Debug.Log("主角使用了普通攻击");
			audio.PlayOneShot(atkSound);
			hint.GetComponent<Text>().enabled = false;
			skeletonAnimation_S.state.SetAnimation(0, "hit", false);
			skeletonAnimation_S.state.AddAnimation(0, "idle", true, 0);
			skeletonAnimation_B.state.SetAnimation(0, "hit", false);
			skeletonAnimation_B.state.AddAnimation(0, "idle", true, 0);
			card1.cardImage.color = new Color(r, g, b, a);
			ExampleGestureHandler.cardisAtk1 = false;
			curCD.text = "0/2";
			StartCoroutine("waitAtk");
			ExampleGestureHandler.playerAtk = false;
			StartCoroutine("EnemyWait");
		}
		else{
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
			
			yield return new WaitForSeconds(1f);
			//heartSystemMonster.TakeDamage(exampleGestureHandler.cardAtk);
			heartSystemMonster.TakeDamage(3);
			hurtTextObj.SetActive(true);
			hurtText.text = "-3";
			drawEnemyController.skeletonAnimation_E1.state.SetAnimation(0, "death", false);
			drawEnemyController.skeletonAnimation_E1.state.AddAnimation(0, "idle", true, 0);
			drawEnemyController.skeletonAnimation_E2.state.SetAnimation(0, "death", false);
			drawEnemyController.skeletonAnimation_E2.state.AddAnimation(0, "idle", true, 0);
			//exampleGestureHandler.effectB.SetActive(false);

		}
	}

	//在切换到敌人操作前添加延迟
	IEnumerator EnemyWait()
	{
		
		hint.text = "";
		yield return new WaitForSeconds(2f);
		hurtTextObj.SetActive(false);
		drawGameManager.maskindex = 10;
		Gesture.SetActive(false);
		mouseEffect.SetActive(false);
		hint.text = "";
		hint.GetComponent<Text>().enabled = false;
		//Debug.Log(drawGameManager.maskindex);
		yield return new WaitUntil(() => drawGameManager.maskindex == 11);
		cardData.card00.isAtk = false;
		drawGameManager.isWaitForPlayer = false;
		drawGameManager.isEnemyAction = true;
	}

	//承受伤害  
	public void ReceiveDamage(int damage)
	{
		heartSystem.TakeDamage(1);

		if (heartSystem.curHealth <= 0)
		{
			Debug.Log("playerDead");
			/*skeletonAnimation_S.state.SetAnimation(0, "death", false);
			skeletonAnimation_B.state.SetAnimation(0, "death", false);*/
		}
	}


}
