using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class DG_playerController : MonoBehaviour
{
	public string ChapterName;
	public DG_GameManager dg_GameManager; //序章
	public GameManager gameManager; //正章
	public NPCTask npcTask;
	public DialogsScript1 dialogsScript1; //正章1對話
	public ExampleGestureHandler gesture;
	//------------playerControl----------------------
	public Rigidbody2D rigid2D;
	public Transform graphics;
	public float speed = 3.0f;
	public GameObject PickUpBtn;
	private Image PickUpImg;
	public GameObject ClimbBtn;
	private Image ClimbImg;

	public LayerMask whatIsGround;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public bool grounded = false;
	public float jumpForce = 12f;
	public bool jumping = false; //是否可跳
	public bool isActive = true;  //是否可移動、跳躍
	public bool isClimb = false;  //是否可攀爬
	public bool isClimbBtn = false;  //爬鍵是否開啟
	public bool isPickUp = false;  //是否可拾取
								   //藤蔓
	private bool vine1 = false;
	private bool vine2 = false;
	private bool vine3 = false;
	//拾取物件	
	public GameObject stoneObj1;
	private bool stone1 = false;
	public GameObject stoneObj2;
	private bool stone2 = false;
	public GameObject stoneObj3;
	private bool stone3 = false;
	public GameObject stoneObj4;
	private bool stone4 = false;
	public GameObject stoneObj5;
	private bool stone5 = false;

	public GameObject redFairy;
	[HideInInspector]
	public bool isRedFairy;
	private BoxCollider2D redFairyCollider;

	public GameObject blueFairy;
	[HideInInspector]
	public bool isBlueFairy;
	private BoxCollider2D blueFairyCollider;

	public GameObject redFlower;
	[HideInInspector]
	public bool isRedFlower;
	[HideInInspector]
	public BoxCollider2D redFlowerCollider;

	public GameObject blueFlower;
	[HideInInspector]
	public bool isBlueFlower;
	[HideInInspector]
	public BoxCollider2D blueFlowerCollider;
	//--------------SpineAnimation----------------
	public Animator animator_S;
	public Animator animator_B;

	//--------------------Health-------------------
	public float curHealth = 100f;
	public float maxHealth = 100f;
	public GameObject playerHealth;
	public Slider HealthSlider;
	public Transform healthCanvas;
	bool isDead;
	bool damaged;
	bool addBlood;
	public GameObject falsh;
	public GameObject healthTextObj;
	private Text healthText;
	public GameObject lineParticle;
	//------------------Enemy-----------------------
	public int enemyAtk;
	public int BossAtk;

	//------------------draw-------------------------
	public Canvas drawCanvas;
	public bool cutting; //小怪
	//-----------------Particle System---------------
	public Transform attackParticle;
	public GameObject NPCPoint; //NPC驚嘆號
	public GameObject redFairyParticle;
	public GameObject blueFairyParticle;
	//public GameObject G1_Skill;
	public GameObject W1_beaten;
	//------------------Audio--------------------
	public AudioSource audio;
	public AudioClip AtkSound;

	public Vector2 player;


	void Start()
	{
		rigid2D.velocity = new Vector2(0, 0f);		
		healthCanvas = playerHealth.GetComponent<Transform>();
		healthText = healthTextObj.GetComponent<Text>();

		if (ChapterName == "1")
		{
			ClimbImg = ClimbBtn.GetComponent<Image>();
			PickUpImg = PickUpBtn.GetComponent<Image>();
			redFairyCollider = redFairy.GetComponent<BoxCollider2D>();
			blueFairyCollider = blueFairy.GetComponent<BoxCollider2D>();
			redFlowerCollider = redFlower.GetComponent<BoxCollider2D>();
			blueFlowerCollider = blueFlower.GetComponent<BoxCollider2D>();

		}
	}

	public void Update() {
		//----------health------------
		if (damaged)
		{
			if (curHealth < HealthSlider.value)
			{
				HealthSlider.value -= 1;
			}
			else if (curHealth == HealthSlider.value)
			{
				HealthSlider.value = curHealth;
			}
		}

		if (ChapterName == "1")
		{
			if (curHealth < 100)
			{
				if (addBlood)
				{
					curHealth += 0.1f;
					HealthSlider.value = curHealth;
				}
			}
			else
			{
				lineParticle.SetActive(false);
			}

			if (curHealth >= 100 && !dialogsScript1.teachBlood) //補血站教學
			{
				dialogsScript1.BloodStation();
			}
		}
	}

	public void FixedUpdate()
	{
		//-------------JUMP-----------------------------
		if (isActive)
		{
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
			animator_S.SetBool("ground", grounded);
			if (ChapterName == "0")
			{
				animator_B.SetBool("ground", grounded);
			}

			if (grounded)
			{
				jumping = false;
				animator_S.SetBool("isJump", jumping);
				if (CrossPlatformInputManager.GetButtonDown("Jump")/*||Input.GetKeyDown(KeyCode.B)*/)
				{
					jumping = true;
					rigid2D.velocity = new Vector2(0, jumpForce);
					animator_S.SetBool("isJump", jumping);
				}
			}


			//-------------MOVE----------------------------

			Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertiacl")) * speed;
			rigid2D.velocity = new Vector2(moveVec.x, rigid2D.velocity.y);

			if (ChapterName == "0")
			{
				animator_S.SetFloat("run", Mathf.Abs(moveVec.x));
				animator_B.SetFloat("run", Mathf.Abs(moveVec.x));
			}
			else if (ChapterName == "1")
			{
				animator_S.SetFloat("Speed", Mathf.Abs(moveVec.x));
			}


			if (moveVec.x > 0)
			{
				graphics.localRotation = Quaternion.Euler(0, 0, 0);
				healthCanvas.localRotation = Quaternion.Euler(0, 0, 0);
				attackParticle.rotation = Quaternion.Euler(0, 0, 0);
				var G0 = gesture.G0_ParticleP.textureSheetAnimation;
				G0.flipU = 0;
				var G1 = gesture.G1_ParticleP.textureSheetAnimation;
				G1.flipU = 0;
				var G2 = gesture.G2_ParticleP.textureSheetAnimation;
				G2.flipU = 0;
				var B1 = gesture.B1_ParticleP.textureSheetAnimation;
				B1.flipU = 0;
				var B2 = gesture.B2_ParticleP.textureSheetAnimation;
				B2.flipU = 0;
				if (ChapterName == "0")
				{
					dg_GameManager.TeachMove = true;
				}
			}
			else if (moveVec.x < 0)
			{
				graphics.localRotation = Quaternion.Euler(0, 180, 0);
				healthCanvas.localRotation = Quaternion.Euler(0, 180, 0);
				attackParticle.rotation = Quaternion.Euler(0, 180, 0);
				var G0 = gesture.G0_ParticleP.textureSheetAnimation;
				G0.flipU = 1;
				var G1 = gesture.G1_ParticleP.textureSheetAnimation;
				G1.flipU = 1;
				var G2 = gesture.G2_ParticleP.textureSheetAnimation;
				G2.flipU = 1;
				var B1 = gesture.B1_ParticleP.textureSheetAnimation;
				B1.flipU = 1;
				var B2 = gesture.B2_ParticleP.textureSheetAnimation;
				B2.flipU = 1;
				if (ChapterName == "0")
				{
					dg_GameManager.TeachMove = true;
				}
			}
		}
		//-----------------------Climb--------------------------

		if (CrossPlatformInputManager.GetButtonDown("Climb"))
		{
			isClimbBtn = true;
			animator_S.SetBool("climb",true);
		}

		if (isClimb && isClimbBtn)
		{
			rigid2D.MovePosition(rigid2D.position + Vector2.up * 2 * Time.deltaTime);

            if (vine1 == true)  //藤蔓1
            {
                //rigid2D.position = new Vector2(4.1f, rigid2D.position.y);
                if (rigid2D.position.y >= 5)
                {
                    rigid2D.position = new Vector2(4.7f, 6);
                    animator_S.SetBool("climb", false);
                    isClimbBtn = false;
                    isClimb = false;
                    vine1 = false;
                }
			}else if (vine2 == true)  //藤蔓2
			{
				//rigid2D.position = new Vector2(13.2f, rigid2D.position.y);
				if (rigid2D.position.y >= 6.5f)
				{
					rigid2D.position = new Vector2(14, 7.5f);
					animator_S.SetBool("climb", false);
					isClimbBtn = false;
					isClimb = false;
					vine2 = false;
				}
			}
			else if (vine3 == true)  //藤蔓3
			{
				//rigid2D.position = new Vector2(13.2f, rigid2D.position.y);
				if (rigid2D.position.y >= 6.5f)
				{
					rigid2D.position = new Vector2(51.5f, 8f);
					animator_S.SetBool("climb", false);
					isClimbBtn = false;
					isClimb = false;
					vine3 = false;
				}
			}
		}

		//----------------------Pick Up--------------------------
		if (CrossPlatformInputManager.GetButtonDown("PickUp"))
		{
			animator_S.SetTrigger("pickUp");

			if (stone1)
			{
				stoneObj1.SetActive(false);
			}
			if (stone2)
			{
				stoneObj2.SetActive(false);
			}
			if (stone3)
			{
				stoneObj3.SetActive(false);
			}
			if (stone4)
			{
				stoneObj4.SetActive(false);
			}
			if (stone5)
			{
				stoneObj5.SetActive(false);
			}
			if (redFairy.activeInHierarchy && isRedFairy) //紅藍精靈
			{
				redFairy.SetActive(false);
				blueFairy.SetActive(true);
				isBlueFairy = false;
				isRedFairy = true;
				//blueFairyCollider.enabled = false;
				npcTask.StatueCollider.enabled = true;
			}
			if (blueFairy.activeInHierarchy && isBlueFairy)
			{
				blueFairy.SetActive(false);
				redFairy.SetActive(true);
				isBlueFairy = true;
				isRedFairy = false;
				//redFairyCollider.enabled = false;
				npcTask.StatueCollider.enabled = true;
			}

			if (redFlower.activeInHierarchy && isRedFlower) //紅藍花
			{
				redFlower.SetActive(false);
				blueFlower.SetActive(true);
				isRedFlower = true;
				isBlueFlower = false;
				//blueFlowerCollider.enabled = false;
				gameManager.Teleportation.SetActive(true);
				npcTask.BobbyCollider.enabled = true;
			}
			if (blueFlower.activeInHierarchy && isBlueFlower)
			{
				blueFlower.SetActive(false);
				redFlower.SetActive(true);
				isRedFlower = false;
				isBlueFlower = true;
				//redFlowerCollider.enabled = false;
				gameManager.Teleportation.SetActive(true);
				npcTask.BobbyCollider.enabled = true;
			}
			isActive = false;
			StartCoroutine("MoveWait");
			PickUpImg.enabled = false;
		}
	}

	//---------------------碰撞-----------------------
	void OnTriggerEnter2D(Collider2D col)  
	{
		if (col.tag == "smallEnemy") //序章-玩家受到小怪物攻擊
		{
			TakeDamage(enemyAtk);
			animator_S.SetTrigger("beaten");
			if (ChapterName == "0")
			{
				animator_B.SetTrigger("beaten");
			}
			healthTextObj.SetActive(true);
			healthText.text = "-" + enemyAtk;
			StartCoroutine("smallbeaten");
		}

		if (col.gameObject.name == "AtkParticle") //序章-玩家受到小BOSS攻擊
		{
			TakeDamage(BossAtk);
			W1_beaten.SetActive(true);
			healthText.text = "-" + BossAtk;
			StartCoroutine("Bossbeaten");
		}

		if (col.tag == "EndPoint")  //序章-結束點
		{
			if (ChapterName == "0")
			{
				dg_GameManager.win();
			}
			else if (ChapterName == "1")
			{
				if (StaticObject.sHE1 == 1)
				{
					gameManager.sHE1.SetActive(true);
					gameManager.win();
				}
				else
				{
					gameManager.sBE1.SetActive(true);
					gameManager.lose();
				}

			}
		}

		if (HealthSlider.value < 100) {
			if (col.gameObject.name == "BloodStation") //補血站
			{
				addBlood = true;
				lineParticle.SetActive(true);
			}

			if (col.tag == "heart")  //補血愛心
			{
				curHealth += 30;
				healthTextObj.SetActive(true);
				healthText.text = "+10";
				StartCoroutine("wait1");
				Destroy(col.gameObject);
			}
		}

		if (col.tag == "trap") //碰到陷阱
		{
			TakeDamage(1);
			animator_S.SetTrigger("beaten");
			healthTextObj.SetActive(true);
			healthText.text = "-" + 1;
			StartCoroutine("smallbeaten");
		}

		if (col.gameObject.name == "vine1") //進入藤蔓1
		{
			isClimb = true;  //是否可以爬
			ClimbImg.enabled = true;  //開啟爬鍵
			vine1 = true; //遇到vine1的藤蔓
			ClimbBtn.transform.SetAsLastSibling();
		}

		if (col.gameObject.name == "vine2") //進入藤蔓2
		{
			isClimb = true;  //是否可以爬
			ClimbImg.enabled = true;  //開啟爬鍵
			vine2 = true; //遇到vine1的藤蔓
			ClimbBtn.transform.SetAsLastSibling();
		}

		if (col.gameObject.name == "vine3") //進入藤蔓3
		{
			isClimb = true;  //是否可以爬
			ClimbImg.enabled = true;  //開啟爬鍵
			vine3 = true; //遇到vine1的藤蔓
			ClimbBtn.transform.SetAsLastSibling();
		}

		if (col.tag == "NPC") //觸碰到NPC
		{
			NPCPoint.SetActive(true);
		}

		if (col.tag == "pickUpObj") //拾取物件
		{
			PickUpImg.enabled = true;  //開啟拾取鍵
			PickUpBtn.transform.SetAsLastSibling();
			if (col.gameObject.name == "stone1") //形石
			{
				stone1 = true;
			}else if(col.gameObject.name == "stone2")
			{
				stone2 = true;
			}else if (col.gameObject.name == "stone3")
			{
				stone3 = true;
			}else if (col.gameObject.name == "stone4")
			{
				stone4 = true;
			}else if (col.gameObject.name == "stone5")
			{
				stone5 = true;
			}
		}

		if (col.gameObject.name=="redFairy") //觸碰到紅精靈
		{
			redFairyParticle.SetActive(true);
			isRedFairy = true;
		}
		if (col.gameObject.name == "blueFairy") //觸碰到藍精靈
		{
			blueFairyParticle.SetActive(true);
			isBlueFairy = true;
		}

		if (col.gameObject.name == "redFlower") //觸碰到紅花
		{
			isRedFlower = true;
		}
		if (col.gameObject.name == "blueFlower") //觸碰到藍花
		{
			isBlueFlower = true;
		}

		if (col.gameObject.name == "Teleportation") //傳送陣
		{
			StartCoroutine("Teleportation");
		}

	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "BloodStation") //補血站
		{
			addBlood = false;
			lineParticle.SetActive(false);
		}

		if (col.gameObject.name == "vine1") //離開藤蔓1
		{
			isClimb = false;
			ClimbImg.enabled = false;
			vine1 = false;
		}
		if (col.gameObject.name == "vine2") //離開藤蔓2
		{
			isClimb = false;
			ClimbImg.enabled = false;
			vine2 = false;
		}
		if (col.gameObject.name == "vine3") //離開藤蔓3
		{
			isClimb = false;
			ClimbImg.enabled = false;
			vine3 = false;
		}

		if (col.tag == "NPC") //觸碰到NPC
		{
			NPCPoint.SetActive(false);
		}

		if (col.tag == "pickUpObj") //離開拾取物件
		{
			PickUpImg.enabled = false;  
		}

		if (col.gameObject.name == "stone1")  //形石
		{
			stone1 = false;
		}
		else if (col.gameObject.name == "stone2")
		{
			stone2 = false;
		}
		else if (col.gameObject.name == "stone3")
		{
			stone3 = false;
		}
		else if (col.gameObject.name == "stone4")
		{
			stone4 = false;
		}
		else if (col.gameObject.name == "stone5")
		{
			stone5 = false;
		}

		if (col.gameObject.name == "redFairy") //離開紅精靈
		{
			redFairyParticle.SetActive(false);
			isRedFairy = false;
		}
		if (col.gameObject.name == "blueFairy") //離開藍精靈
		{
			blueFairyParticle.SetActive(false);
			isBlueFairy = false;
		}

		if (col.gameObject.name == "redFlower") //離開紅花
		{
			isRedFlower = false;
		}
		if (col.gameObject.name == "blueFlower") //離開藍花
		{
			isBlueFlower = false;
		}
	}

	IEnumerator MoveWait()
	{
		yield return new WaitForSeconds(1f);
		isActive = true;
	}

	IEnumerator wait1()
	{
		yield return new WaitForSeconds(1f);
		healthTextObj.SetActive(false);
	}

	IEnumerator smallbeaten()
	{
		for (int i = 0; i < 2; i++)
		{
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			falsh.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(.5f);
		healthTextObj.SetActive(false);
	}

	IEnumerator Bossbeaten()
	{
		yield return new WaitForSeconds(0.4f);
		if (ChapterName == "0")
		{
			animator_B.SetTrigger("beaten");
		}
		animator_S.SetTrigger("beaten");	
		healthTextObj.SetActive(true);
		for (int i = 0; i < 2; i++)
		{
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			falsh.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}	
		yield return new WaitForSeconds(.2f);
		W1_beaten.SetActive(false);
		healthTextObj.SetActive(false);
	}

	IEnumerator Teleportation()
	{
		gameManager.FadeWhite.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		rigid2D.position = new Vector2(.8f, 7.5f);
		yield return new WaitForSeconds(1.5f);
		gameManager.FadeWhite.SetActive(false);
	}

	public void Attack()
	{
		if (ChapterName == "0")
		{	
			animator_B.SetTrigger("attack");
		}
		animator_S.SetTrigger("attack");
		audio.PlayOneShot(AtkSound);
	}


	public void TakeDamage(int amount)
	{
		curHealth -= amount;
		curHealth = Mathf.Clamp(curHealth, 0, maxHealth);

		damaged = true;
		Debug.Log("damaged");
		if (curHealth <= 0 && !isDead)
		{
			Death();
			Debug.Log("DEAD");
		}
	}

	void Death()
	{
		isDead = true;
	}

}

