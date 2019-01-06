﻿using System.Collections;
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

	private ActiveClimb activeClimb;
	private ActivePickUp activePickUp;
	[HideInInspector]
	public NpcTalk npcTalk;
	//------------playerControl----------------------
	public Rigidbody2D rigid2D;
	public Transform graphics;
	public float speed = 3.0f;

	public LayerMask whatIsGround;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public bool grounded = false;
	public float jumpForce = 12f;
	public bool jumping = false; //是否可跳
	public bool isActive = true;  //是否可移動、跳躍
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
	public GameObject redFairyParticle;
	public GameObject blueFairyParticle;
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
			activeClimb = GameObject.Find("vine1").GetComponent<ActiveClimb>();  //防錯誤 爬藤蔓
			activePickUp = GameObject.Find("stone1").GetComponent<ActivePickUp>(); //防錯誤 拾取物品
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
			activeClimb.isClimb = true;
			animator_S.SetBool("climb", true);
		}

		if (activeClimb.isClimb)
		{
			rigid2D.MovePosition(rigid2D.position + Vector2.up * 2 * Time.deltaTime);
			if (rigid2D.position.y >= activeClimb.highestPoint)
			{
				rigid2D.position = activeClimb.targetPoint;
				animator_S.SetBool("climb", false);
				activeClimb.isClimb = false;
			}
		}

		//----------------------Pick Up--------------------------
		if (CrossPlatformInputManager.GetButtonDown("PickUp"))
		{
			animator_S.SetTrigger("pickUp");

			if (activePickUp.task) //如果是任務
			{
				activePickUp.PickUpObj.SetActive(false);
				activePickUp.anotherObj.SetActive(true);

				if (activePickUp.PickUpObjName == "right")
				{
					npcTalk.right = true;
					npcTalk.wrong = false;
					npcTalk.NPCBoxcollider.enabled = true;
					if (npcTalk.whoTask == "BobbyTask")
						gameManager.Teleportation.SetActive(true);
				}
				else if (activePickUp.PickUpObjName == "wrong")
				{
					npcTalk.right = false;
					npcTalk.wrong = true;
					npcTalk.NPCBoxcollider.enabled = true;
					if (npcTalk.whoTask == "BobbyTask")
						gameManager.Teleportation.SetActive(true);
				}				
			}
			else
			{
				if (activePickUp.PickUpObjBool)  //普通拾取物品
				{
					activePickUp.PickUpObj.SetActive(false);
					ActivePickUp.PickUpInt += 1;
					Debug.Log(ActivePickUp.PickUpInt);
				}
			}

			isActive = false;
			StartCoroutine("MoveWait");
			activePickUp.PickUpImg.enabled = false;
		}

		//-----------------------Talk----------------------------
		if (CrossPlatformInputManager.GetButtonDown("Talk"))
		{
			if (npcTalk.gimmick) //機關
			{
				if (ActivePickUp.PickUpInt>=5 && npcTalk.gimmickName=="Stone")
				{
					npcTalk.gimmickObj.SetActive(true);
				}
			}
			else //一般NPC
			{
				if (Mathf.Abs(rigid2D.transform.position.x - npcTalk.NPC.transform.position.x) < 2 && npcTalk.NPCPoint.activeInHierarchy == true && npcTalk.isTasting == false)
				{
					npcTalk.isTasting = true;
					if (npcTalk.right == true || npcTalk.wrong == true)  //完成任務
					{
						Debug.Log("a2");
						npcTask.TaskFinish();
					}
					else  //接任務
					{
						Debug.Log("a1");
						string taskStart = npcTalk.startTaskName;
						npcTask.GetComponent<NPCTask>().Invoke(taskStart, 0f);
					}
					Debug.Log("rr");
				}
			}
		}
	}
	//---------------------碰撞-----------------------
	void OnTriggerEnter2D(Collider2D col)  
	{

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

		if (col.tag == "vine") //進入藤蔓
		{
			activeClimb = col.gameObject.GetComponent<ActiveClimb>();
		}

		if (col.tag == "NPC") //觸碰到NPC
		{
			npcTalk = col.gameObject.GetComponent<NpcTalk>();
		}

		if (col.tag == "pickUpObj") //拾取物件
		{
			activePickUp = col.gameObject.GetComponent<ActivePickUp>();

			if (col.gameObject.name == activePickUp.PickUpObjName) //形石
			{
				activePickUp.PickUpObjBool = true;
			}
		}

		/*if (col.gameObject.name=="redFairy") //觸碰到紅精靈
		{
			redFairyParticle.SetActive(true);
		}
		if (col.gameObject.name == "blueFairy") //觸碰到藍精靈
		{
			blueFairyParticle.SetActive(true);
		}*/

		if (col.gameObject.name == "redFairy" || col.gameObject.name == "blueFairy") //觸碰到紅藍精靈
		{
			npcTalk = GameObject.Find("NPC_Statue").GetComponent<NpcTalk>();
		}

		if (col.gameObject.name == "redFlower"|| col.gameObject.name == "blueFlower") //觸碰到紅藍花
		{
			npcTalk = GameObject.Find("NPC_Bobby").GetComponent<NpcTalk>();
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

		if (col.tag == "pickUpObj") //離開物件
		{
			if (col.gameObject.name == activePickUp.PickUpObjName) //形石
			{
				activePickUp.PickUpObjBool = false;
			}
		}

		if (col.gameObject.name == "redFairy") //離開紅精靈
		{
			redFairyParticle.SetActive(false);

		}
		if (col.gameObject.name == "blueFairy") //離開藍精靈
		{
			blueFairyParticle.SetActive(false);

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

