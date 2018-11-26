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
	public DG_GameManager dg_GameManager;
	//------------playerControl----------------------
	public Rigidbody2D rigid2D;
	public Transform graphics;
	public float speed = 5.0f;
	private bool touchStart = false;
	private Vector2 pointA;
	private Vector2 pointB;
	public Button Active;

	public LayerMask whatIsGround;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public bool grounded = false;
	public float jumpForce = 70f;
	public bool jumping = false;

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
	public GameObject falsh;
	public GameObject damageTextObj;
	private Text damageText;

	//------------------Enemy-----------------------
	public int enemyAtk;
	public int BossAtk;
	//-----------------Particle System---------------
	//public GameObject G1_Skill;
	public GameObject W1_beaten;
	//------------------Audio--------------------
	public AudioSource audio;
	public AudioClip AtkSound;


	void Start()
	{
		rigid2D.velocity = new Vector2(0, 0f);		
		healthCanvas = playerHealth.GetComponent<Transform>();
		damageText = damageTextObj.GetComponent<Text>();
		if (ChapterName == "0")
		{
			Active.interactable = false;
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
	}

	public void FixedUpdate()
	{
		//-------------JUMP-----------------------------
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

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



		if (grounded)
		{
			if (CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				if (ChapterName == "0")
				{
					Debug.Log("0000");
					dg_GameManager.TeachJump = true;
					jumping = true;
					rigid2D.velocity = new Vector2(0, jumpForce);
					animator_S.SetBool("isJump", jumping);
					animator_B.SetBool("isJump", jumping); //test版
				} else if (ChapterName == "1")
				{
					Debug.Log("1111");
					jumping = true;
					rigid2D.velocity = new Vector2(0, jumpForce);
					animator_S.SetBool("isJump", jumping);
				}
			}
			animator_S.SetBool("fall", false);
			//animator_B.SetBool("fall", false);  
		}
		else
		{
			OnLanding();
		}

		//--------------move----------------
		if (moveVec.x > 0)
		{
			graphics.localRotation = Quaternion.Euler(0, 0, 0);
			healthCanvas.localRotation = Quaternion.Euler(0, 0, 0);
			dg_GameManager.TeachMove = true;
		}
		else if (moveVec.x < 0)
		{
			graphics.localRotation = Quaternion.Euler(0, 180, 0);
			healthCanvas.localRotation = Quaternion.Euler(0, 180, 0);
			dg_GameManager.TeachMove = true;
		}


		/*if (dg_GameManager.isRun == true)
		{
			rigid2D.transform.position = new Vector3(rigid2D.transform.position.x + 0.06f, rigid2D.transform.position.y, 10);
			animator_S.SetBool("run", true);
			animator_B.SetBool("run", true);
		}*/
	}

	public void OnLanding() {

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
		  //test版
	}


	//---------------------Damage-----------------------
	void OnTriggerEnter2D(Collider2D col)  
	{
		if (col.tag == "smallEnemy") //玩家受到小怪物攻擊
		{
			TakeDamage(enemyAtk);
			animator_S.SetTrigger("beaten");
			animator_B.SetTrigger("beaten");
			damageTextObj.SetActive(true);
			damageText.text = "-" + enemyAtk;
			StartCoroutine("smallbeaten");
		}

		if (col.gameObject.name == "AtkParticle") //玩家受到小BOSS攻擊
		{
			TakeDamage(BossAtk);
			W1_beaten.SetActive(true);
			damageText.text = "-" + BossAtk;
			StartCoroutine("Bossbeaten");
		}

		if (col.tag == "EndPoint")
		{
			dg_GameManager.win();
		}
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
		damageTextObj.SetActive(false);
	}

	IEnumerator Bossbeaten()
	{
		yield return new WaitForSeconds(0.4f);
		animator_S.SetTrigger("beaten");
		animator_B.SetTrigger("beaten");
		damageTextObj.SetActive(true);
		for (int i = 0; i < 2; i++)
		{
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			falsh.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}	
		yield return new WaitForSeconds(.2f);
		W1_beaten.SetActive(false);
		damageTextObj.SetActive(false);
	}

	public void Attack()
	{
		animator_S.SetTrigger("attack");
		animator_B.SetTrigger("attack");
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

