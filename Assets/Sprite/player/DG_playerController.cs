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
	//------------playerControl----------------------
	public Rigidbody2D rigid2D;
	public Transform graphics;
	public float speed = 3.0f;
	private bool touchStart = false;
	private Vector2 pointA;
	private Vector2 pointB;
	public GameObject ActiveBtn;
	public GameObject ClimbBtn;
	private Image ClimbImg;

	public LayerMask whatIsGround;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public bool grounded = false;
	public float jumpForce = 12f;
	public bool jumping = false;
	public bool isClimb = false;
	public bool isClimbBtn = false;
	//--------------SpineAnimation----------------
	public Animator animator_S;
	public Animator animator_B;

	//--------------------Health-------------------
	public int curHealth = 100;
	public int maxHealth = 100;
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
	//----------------NPC Tast------------------------
	public GameObject tastPanel; //任務面板
	public GameObject NPC1Point; //任務1提示!特效
	public bool isTasting = false; //是否可再開啟任務頁面
	public GameObject otherTast; //右邊支線任務面板
	private Animator otherTastAni;

	public GameObject Bobby;
	private BoxCollider2D BobbyCollider;
	//------------------draw-------------------------
	public Canvas drawCanvas;
	//-----------------Particle System---------------
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
			BobbyCollider = Bobby.GetComponent<BoxCollider2D>();
			otherTastAni = otherTast.GetComponent<Animator>();
			ClimbImg = ClimbBtn.GetComponent<Image>();
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
		}
	}

	public void FixedUpdate()
	{
		//-------------JUMP-----------------------------
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		if (grounded)
		{
			if (CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				if (ChapterName == "0")
				{
					dg_GameManager.TeachJump = true;
					jumping = true;
					rigid2D.velocity = new Vector2(0, jumpForce);
					animator_S.SetBool("isJump", jumping);
					animator_B.SetBool("isJump", jumping); //test版
				}
				else if (ChapterName == "1")
				{
					jumping = true;
					//rigid2D.velocity = new Vector2(0, jumpForce);
					animator_S.SetBool("isJump", jumping);
					player.y += jumpForce * Time.deltaTime;
					if (player.y < -rigid2D.gravityScale * Time.deltaTime * 3f)
					{
						player.y = -rigid2D.gravityScale * Time.deltaTime * 3f;
					}
				}
			}
			animator_S.SetBool("fall", false);
			//animator_B.SetBool("fall", false);  
		}
		else
		{
			OnLanding();
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



		//-----------------------Climb--------------------------

		if (CrossPlatformInputManager.GetButtonDown("Climb"))
		{
			isClimbBtn = true;
			animator_S.SetBool("climb",true);
		}

		if (isClimb && isClimbBtn)
		{
			rigid2D.MovePosition(rigid2D.position + Vector2.up * 2 * Time.deltaTime);
			if (rigid2D.position.y >= 5)
			{
				rigid2D.position = new Vector2(11, 6);
				animator_S.SetBool("climb", false);
				isClimbBtn = false;
				isClimb = false;
			}
		}

		//----------------------NPC tast-------------------------
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

			if (hit.collider == null)
			{
				//Debug.Log("null");
				//Debug.Log(hit.collider.name);
			}else if (hit.collider.name == "NPC_Bobby")
			{
				if (Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x) < 2 && NPC1Point.activeInHierarchy== true && isTasting == false)
				{
					isTasting = true;
					Tast();
				}
			}
		}
	}


	public void Tast()
	{
		Debug.Log(Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x));
		tastPanel.SetActive(true);
	}

	public void Tast_Yes()
	{
		isTasting = false;
		tastPanel.SetActive(false);
		BobbyCollider.enabled = false;
		otherTast.SetActive(true);
	}

	public void Tast_NO()
	{
		isTasting = false;
		tastPanel.SetActive(false);
		drawCanvas.enabled = true;
	}

	public void OnLanding()
	{
		if (rigid2D.velocity.y < 0)
		{
			if (ChapterName == "0")
			{
				animator_S.SetBool("fall", true);
				//animator_B.SetBool("fall", true);
				animator_S.SetBool("isJump", jumping);
				animator_B.SetBool("isJump", false);
			}
			else if (ChapterName == "1")
			{
				animator_S.SetBool("fall", true);
				animator_S.SetBool("isJump", jumping);
			}			
		}
		jumping = false;
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

		if (col.tag == "EndPoint")
		{
			dg_GameManager.win();
		}

		if (HealthSlider.value < 100) {
			if (col.gameObject.name == "BloodStation") //補血站
			{
				addBlood = true;
				lineParticle.SetActive(true);
			}

			if (col.tag == "heart")
			{
				HealthSlider.value += 10;
				healthTextObj.SetActive(true);
				healthText.text = "+10";
				StartCoroutine("wait1");
				Destroy(col.gameObject);
			}
		}

		if (col.tag == "vine") //進入藤蔓
		{
			isClimb = true;
			ClimbImg.enabled = true;
		}

		if (col.gameObject.name == "NPC_Bobby") //觸碰到NPC波比
		{
			NPC1Point.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "BloodStation") //補血站
		{
			addBlood = false;
			lineParticle.SetActive(false);
		}

		if (col.tag == "vine") //離開藤蔓
		{
			isClimb = false;
			ClimbImg.enabled = false;
			ClimbBtn.transform.SetAsLastSibling();
		}

		if (col.gameObject.name == "NPC_Bobby") //離開NPC波比
		{
			NPC1Point.SetActive(false);
		}
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

