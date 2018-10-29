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
	private DG_GameManager drawGameManager;

	//获取敌人脚本的引用 
	//private DrawEnemyController drawEnemyController;

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
		//drawGameManager = GameObject.Find("DrawGameController").GetComponent<DrawGameManager>();
		//drawEnemyController = GameObject.FindGameObjectWithTag("monster").GetComponent<DrawEnemyController>();

	}

	void Update()
	{
		
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
