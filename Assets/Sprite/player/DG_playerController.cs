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
	public DialogsScript2 dialogsScript2; //正章2對話
	public ExampleGestureHandler gesture;

	private ActiveClimb activeClimb;
	private ActivePickUp activePickUp;
	[HideInInspector]
	public NpcTalk npcTalk;
    //------------playerControl----------------------
    public GameObject playerS; //角色-妹妹
    public GameObject playerB; //角色-哥哥
    public GameObject playerInsPoint; //角色生成位置

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
    //--------------Animation----------------
    public Animator animator_S;
	public Animator animator_B;

	public SkeletonAnimation SpineSister;
	public SkeletonAnimation SpineBother;
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
	public GameObject lineParticle;

	private Text healthText;
	public GameObject healthObj;
	public GameObject addHealthObj;
	public GameObject canvas;
	//------------------Enemy-----------------------
	//public int enemyAtk;
	public int BossAtk;

	//------------------draw-------------------------
	public Canvas drawCanvas;
	public bool cutting; //小怪
	//-----------------Particle System---------------
	public Transform attackParticle;
	public GameObject W1_beaten;
	//------------------Audio--------------------
	public new AudioSource audio;
	public AudioClip pickUp;

	public Vector2 player;

	public int pickUpInt = 1;
	public Image climb;

    void Awake()
    {
        if (ChapterName != "0")
        {
            StaticObject.whoCharacter = 2;
            if (StaticObject.whoCharacter == 1) //哥哥
            {
                GameObject playS = Instantiate(playerB) as GameObject;
                playS.transform.SetParent(playerInsPoint.transform, false);
                playS = GameObject.FindWithTag("Player");
                animator_S = playS.GetComponent<Animator>();
                SpineSister = playS.GetComponent<SkeletonAnimation>();
                healthCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }
            else if (StaticObject.whoCharacter == 2) //妹妹
            {
                GameObject playS = Instantiate(playerS) as GameObject;
                playS.transform.SetParent(playerInsPoint.transform, false);
                playS = GameObject.FindWithTag("Player");
                animator_S = playS.GetComponent<Animator>();
                SpineSister = playS.GetComponent<SkeletonAnimation>();
                healthCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -0.86f, 0);
            }
        }
    }

	void Start()
	{
		rigid2D.velocity = new Vector2(0, 0f);		
		healthCanvas = playerHealth.GetComponent<Transform>();

        if (ChapterName == "1")
		{
			activeClimb = GameObject.Find("vine1").GetComponent<ActiveClimb>();  //防錯誤 爬藤蔓
			activePickUp = GameObject.Find("stone1").GetComponent<ActivePickUp>(); //防錯誤 拾取物品
			ActivePickUp.PickUpInt = 0;
		}else if (ChapterName == "2")
		{
			activeClimb = GameObject.Find("ladder").GetComponent<ActiveClimb>();  //防錯誤 爬藤蔓
			activePickUp = GameObject.Find("Card").GetComponent<ActivePickUp>(); //防錯誤 拾取物品
			ActivePickUp.PickUpInt = 0;
		}
        else if (ChapterName == "3")
        {
            activeClimb = GameObject.Find("ladder").GetComponent<ActiveClimb>();  //防錯誤 爬藤蔓
            //activePickUp = GameObject.Find("Card").GetComponent<ActivePickUp>(); //防錯誤 拾取物品
            //ActivePickUp.PickUpInt = 0;
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

		if (ChapterName == "1" || ChapterName == "2" || ChapterName == "3")
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

			if (curHealth >= 100) //補血站教學
			{
				if (ChapterName == "1" && !dialogsScript1.teachBlood)
				{
					dialogsScript1.BloodStation();
				}else if(ChapterName == "2" && !dialogsScript2.teachBlood)
					{
						dialogsScript2.BloodStation();
					}
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
				animator_B.SetBool("ground", grounded);

			if (grounded)
			{
				jumping = false;
				animator_S.SetBool("isJump", jumping);
                if (ChapterName == "0")
                    animator_B.SetBool("isJump", jumping);
                if (CrossPlatformInputManager.GetButtonDown("Jump")/*||Input.GetKeyDown(KeyCode.B)*/)
				{
					jumping = true;
					rigid2D.velocity = new Vector2(0, jumpForce);
					animator_S.SetBool("isJump", jumping);
                    if (ChapterName == "0")
                        animator_B.SetBool("isJump", jumping);
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
			else 
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
				var G3 = gesture.G3_ParticleP.textureSheetAnimation;
				G3.flipU = 0;
				var G4 = gesture.G4_ParticleP.textureSheetAnimation;
				G4.flipU = 0;
				var B1 = gesture.B1_ParticleP.textureSheetAnimation;
				B1.flipU = 0;
				var B2 = gesture.B2_ParticleP.textureSheetAnimation;
				B2.flipU = 0;
				var B3 = gesture.B3_ParticleP.textureSheetAnimation;
				B3.flipU = 0;
				var B4 = gesture.B4_ParticleP.textureSheetAnimation;
				B4.flipU = 0;
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
				var G3 = gesture.G3_ParticleP.textureSheetAnimation;
				G3.flipU = 1;
				var G4 = gesture.G4_ParticleP.textureSheetAnimation;
				G4.flipU = 1;
				var B1 = gesture.B1_ParticleP.textureSheetAnimation;
				B1.flipU = 1;
				var B2 = gesture.B2_ParticleP.textureSheetAnimation;
				B2.flipU = 1;
				var B3 = gesture.B3_ParticleP.textureSheetAnimation;
				B3.flipU = 1;
				var B4 = gesture.B4_ParticleP.textureSheetAnimation;
				B4.flipU = 1;
				if (ChapterName == "0")
				{
					dg_GameManager.TeachMove = true;
				}
			}
		}
		//-----------------------Climb--------------------------
		if (ChapterName != "0")
		{
			if (CrossPlatformInputManager.GetButtonDown("Climb"))
			{
				activeClimb.isClimb = true;
				animator_S.SetBool("climb", true);
				if (ChapterName == "1")
				{
					dialogsScript1.MaskGroup.SetActive(false);
					Joystick.isMove = true;
				}
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
		}

		//----------------------Pick Up--------------------------
		if (CrossPlatformInputManager.GetButtonDown("PickUp"))
		{
			animator_S.SetTrigger("pickUp");

			if (!audio.isPlaying)
			{
				audio.PlayOneShot(pickUp);
			}

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
					{
						gameManager.Teleportation.SetActive(true);
						if (pickUpInt == 1)
						{
							dialogsScript1.teleportation();
						}
					}
				}
				else if (activePickUp.PickUpObjName == "wrong")
				{
					npcTalk.right = false;
					npcTalk.wrong = true;
					npcTalk.NPCBoxcollider.enabled = true;
					if (npcTalk.whoTask == "BobbyTask")
					{
						gameManager.Teleportation.SetActive(true);
						if (pickUpInt == 1)
						{
							dialogsScript1.teleportation();
						}
					}
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
			Debug.Log(activePickUp.PickUpObjName);
			isActive = false;
			StartCoroutine("MoveWait");
			activePickUp.PickUpImg.enabled = false;
		}

		//-----------------------Talk----------------------------
		if (CrossPlatformInputManager.GetButtonDown("Talk"))
		{
			if (npcTalk.gimmick) //機關
			{
				if (npcTalk.gimmickName == "Stone") {
					if (ActivePickUp.PickUpInt >= 5)
					{
						npcTalk.gimmickObj.SetActive(true);
					}
					else
					{
						//尚未收集完畢
						gameManager.downHintAni.SetTrigger("whereHint");
						gameManager.downHintText.text = "形石尚未收集完畢";
					}
				}

				if (npcTalk.gimmickName == "Mirror")
				{
					if (ActivePickUp.PickUpInt >= 1)
					{
						dialogsScript2.Mirror = true;
						npcTask.TaskFinish();
					}
					else
					{
						string taskStart = npcTalk.startTaskName;
						npcTask.GetComponent<NPCTask>().Invoke(taskStart, 0f);
						//尚未收集完畢
						gameManager.downHintAni.SetTrigger("whereHint");
						gameManager.downHintText.text = "尚未取得入場券";
					}
				}			
			}
			else //一般NPC
			{
				if (Mathf.Abs(rigid2D.transform.position.x - npcTalk.NPC.transform.position.x) < 2 && npcTalk.NPCPoint.activeInHierarchy == true && npcTalk.isTasting == false)
				{
					npcTalk.isTasting = true;
					if (npcTalk.right == true || npcTalk.wrong == true)  //完成任務
					{
						npcTask.TaskFinish();
					}
					else  //接任務
					{
						string taskStart = npcTalk.startTaskName;
						npcTask.GetComponent<NPCTask>().Invoke(taskStart, 0f);
					}
				}
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if (hit.collider == null)
			{

			}
			else if (hit.collider.name == "NPC_Bobby" || hit.collider.name == "NPC_Statue" /*|| hit.collider.name == "Stone"*/ || hit.collider.name == "NPC_Dida" || hit.collider.name == "NPC_Coco" || hit.collider.name == "NPC_Dragon" || hit.collider.name == "Mirror" && Mathf.Abs(rigid2D.transform.position.x - npcTalk.NPC.transform.position.x) < 2 && npcTalk.NPCPoint.activeInHierarchy == true && npcTalk.isTasting == false)
			{
				npcTalk = hit.collider.GetComponent<NpcTalk>();
				if (npcTalk.gimmick) //機關
				{
                    if (npcTalk.gimmickName == "Stone")
                    {
                        if (ActivePickUp.PickUpInt >= 5)
                        {
                            npcTalk.gimmickObj.SetActive(true);
                        }
                        else
                        {
                            gameManager.downHintAni.SetTrigger("whereHint");
                            gameManager.downHintText.text = "形石尚未收集完畢";
                        }
                    }

                    if (npcTalk.gimmickName == "Mirror")
                    {
                        if (ActivePickUp.PickUpInt >= 1)
                        {
                            dialogsScript2.Mirror = true;
                            npcTask.TaskFinish();
                            
                        }
                        else
                        {
                            string taskStart = npcTalk.startTaskName;
                            npcTask.GetComponent<NPCTask>().Invoke(taskStart, 0f);
                            gameManager.downHintAni.SetTrigger("whereHint");
                            gameManager.downHintText.text = "尚未取得入場券";
                        }
                        
                    }
                    /*if (ActivePickUp.PickUpInt >= 5 && npcTalk.gimmickName == "Stone")
                    {
                        npcTalk.gimmickObj.SetActive(true);
                    }
                    else
                    {
                        //尚未收集完畢
                        gameManager.downHintAni.SetTrigger("whereHint");
                        gameManager.downHintText.text = "形石尚未收集完畢";
                    }*/
                }
				else //一般NPC
				{
					if (Mathf.Abs(rigid2D.transform.position.x - npcTalk.NPC.transform.position.x) < 2 && npcTalk.NPCPoint.activeInHierarchy == true && npcTalk.isTasting == false)
					{
						npcTalk.isTasting = true;
						if (npcTalk.right == true || npcTalk.wrong == true)  //完成任務
						{
							npcTask.TaskFinish();
						}
						else  //接任務
						{
							string taskStart = npcTalk.startTaskName;
							npcTask.GetComponent<NPCTask>().Invoke(taskStart, 0f);
						}
					}
				}
			}
		}
	}
	//---------------------碰撞-----------------------
	void OnTriggerEnter2D(Collider2D col)  
	{
		if (col.gameObject.name == "BossEnemy") //觸碰到敵人
		{
			TakeDamage(BossAtk);
			W1_beaten.SetActive(true);
			StartCoroutine("Bossbeaten");
		}

		if (col.gameObject.name == "AtkParticle") //序章-玩家受到小BOSS攻擊
		{
			TakeDamage(BossAtk);
			W1_beaten.SetActive(true);
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
			else if (ChapterName == "2")
			{
				if (StaticObject.sHE2 == 1)
				{
					gameManager.win();
				}
				else
				{
					gameManager.sBE2.SetActive(true);
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
				curHealth += 10;
				GameObject NEWatkpreft = Instantiate(addHealthObj) as GameObject;
				NEWatkpreft.transform.SetParent(canvas.transform, false);
				NEWatkpreft.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
				healthText = NEWatkpreft.GetComponentInChildren<Text>();
				healthText.text = "+" + 10;
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

		if (col.gameObject.name == "redFairy" || col.gameObject.name == "blueFairy") //觸碰到紅藍精靈
		{
			npcTalk = GameObject.Find("NPC_Statue").GetComponent<NpcTalk>();
		}

		if (col.gameObject.name == "redFlower"|| col.gameObject.name == "blueFlower") //觸碰到紅藍花
		{
			npcTalk = GameObject.Find("NPC_Bobby").GetComponent<NpcTalk>();
		}

		if (col.gameObject.name == "rightClock" || col.gameObject.name == "falseClock") //觸碰到紅藍花
		{
			npcTalk = GameObject.Find("NPC_Dida").GetComponent<NpcTalk>();
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
	}

	IEnumerator MoveWait()
	{
		yield return new WaitForSeconds(1f);
		isActive = true;
	}


	IEnumerator Bossbeaten()
	{	
		if (ChapterName == "0")
		{
			yield return new WaitForSeconds(0.4f);
			animator_B.SetTrigger("beaten");
			SpineBother.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
		}
		animator_S.SetTrigger("beaten");		
		SpineSister.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0.5f);
		GameObject NEWatkpreft = Instantiate(healthObj) as GameObject;
		NEWatkpreft.transform.SetParent(canvas.transform, false);
		NEWatkpreft.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
		healthText = NEWatkpreft.GetComponentInChildren<Text>();
		healthText.text = "-" + BossAtk;
		yield return new WaitForSeconds(0.7f);
		SpineSister.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);
		if (ChapterName == "0")
			SpineBother.GetComponent<Renderer>().material.SetFloat("_FillPhase", 0f);

		yield return new WaitForSeconds(.2f);
		W1_beaten.SetActive(false);
		Destroy(NEWatkpreft, .5f);
	}

	IEnumerator Teleportation()
	{
		gameManager.FadeWhite.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		rigid2D.position = new Vector2(-3.32f, 2.9f);
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

