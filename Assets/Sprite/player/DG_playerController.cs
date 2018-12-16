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
	public DialogsScript1 dialogsScript1; //正章1對話
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
	//拾取物件	
	public  GameObject stoneObj1;
	private bool stone1 = false;
	public  GameObject stoneObj2;
	private bool stone2 = false;
	public  GameObject stoneObj3;
	private bool stone3 = false;
	public  GameObject stoneObj4;
	private bool stone4 = false;
	public  GameObject stoneObj5;
	private bool stone5 = false;
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
	//-----------------Particle System---------------
	public GameObject NPCPoint; //NPC驚嘆號
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
		}
	}

	public void Update() {

		curHealth = HealthSlider.value;
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
			if (HealthSlider.value < 100)
			{
				if (addBlood)
				{
					HealthSlider.value += 0.1f;
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
				if (CrossPlatformInputManager.GetButtonDown("Jump"))
				{
					if (ChapterName == "0")
					{
						dg_GameManager.TeachJump = true;
						//jumping = true;
						rigid2D.velocity = new Vector2(0, jumpForce);
						//animator_S.SetBool("isJump", jumping);
						//animator_B.SetBool("isJump", jumping); //test版
					}
					else if (ChapterName == "1")
					{
						jumping = true;
						rigid2D.velocity = new Vector2(0, jumpForce);
						animator_S.SetBool("isJump", jumping);

					}
				}
				//animator_S.SetBool("fall", false);
				//animator_B.SetBool("fall", false);  
			}
			else
			{
				//OnLanding();
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
				if (ChapterName == "0")
				{
					dg_GameManager.TeachMove = true;
				}
			}
			else if (moveVec.x < 0)
			{
				graphics.localRotation = Quaternion.Euler(0, 180, 0);
				healthCanvas.localRotation = Quaternion.Euler(0, 180, 0);
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

			if (vine1==true && rigid2D.position.y >= 5)  //藤蔓1
			{
				rigid2D.position = new Vector2(11, 7);
				animator_S.SetBool("climb", false);
				isClimbBtn = false;
				isClimb = false;
				vine1 = false;
			}else if (vine2 == true && rigid2D.position.y >= 8)  //藤蔓1
			{
				rigid2D.position = new Vector2(23, 9.2f);
				animator_S.SetBool("climb", false);
				isClimbBtn = false;
				isClimb = false;
				vine2 = false;
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
			isActive = false;
			StartCoroutine("MoveWait");
			PickUpImg.enabled = false;
		}
	}

	//---------------------Damage-----------------------
	void OnTriggerEnter2D(Collider2D col)  
	{
		if (col.tag == "smallEnemy") //序章-玩家受到小怪物攻擊
		{
			TakeDamage(enemyAtk);
			animator_S.SetTrigger("beaten");
			animator_B.SetTrigger("beaten");
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
			dg_GameManager.win();
		}

		if (HealthSlider.value < 100) {
			if (col.gameObject.name == "BloodStation") //補血站
			{
				addBlood = true;
				lineParticle.SetActive(true);
			}

			if (col.tag == "heart")  //補血愛心
			{
				HealthSlider.value += 10;
				healthTextObj.SetActive(true);
				healthText.text = "+10";
				StartCoroutine("wait1");
				Destroy(col.gameObject);
			}
		}

		if (col.gameObject.name == "vine1") //進入藤蔓1
		{
			isClimb = true;  //是否可以爬
			ClimbImg.enabled = true;  //開啟爬鍵
			vine1 = true; //遇到vine1的藤蔓
			ClimbBtn.transform.SetAsLastSibling();
		}

		if (col.gameObject.name == "vine2") //進入藤蔓1
		{
			isClimb = true;  //是否可以爬
			ClimbImg.enabled = true;  //開啟爬鍵
			vine2 = true; //遇到vine1的藤蔓
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
		if (col.gameObject.name == "vine2") //離開藤蔓1
		{
			isClimb = false;
			ClimbImg.enabled = false;
			vine2 = false;
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
	}

	IEnumerator MoveWait()
	{
		yield return new WaitForSeconds(2f);
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
		animator_S.SetTrigger("beaten");
		animator_B.SetTrigger("beaten");
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

