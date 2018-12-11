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

			if (curHealth >= 100 && !dialogsScript1.teachBlood)
			{
				dialogsScript1.BloodStation();
			}
		}
	}

	public void FixedUpdate()
	{
		//-------------JUMP-----------------------------
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
	}

	/*public void OnLanding()
	{

		if (rigid2D.velocity.y <= 0 || grounded)
		{
			Debug.Log(rigid2D.velocity.y);
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
	}*/


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

