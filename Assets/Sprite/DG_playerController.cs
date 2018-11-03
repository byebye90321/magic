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
	public Slider playerHealth;
	public Transform healthObj;
	bool isDead;
	bool damaged;
	public GameObject falsh;

	void Start()
	{
		rigid2D.velocity = new Vector2(0, 0f);
		Active.interactable = false;
	}

	public void Update() {
		//----------health------------
		if (damaged)
		{
			if (curHealth < playerHealth.value)
			{
				playerHealth.value -= 1;
			}
			else if (curHealth == playerHealth.value)
			{
				playerHealth.value = curHealth;
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
		animator_S.SetFloat("Speed", Mathf.Abs(moveVec.x));
		animator_B.SetFloat("Speed", Mathf.Abs(moveVec.x));

		if (grounded)
		{
			if (CrossPlatformInputManager.GetButtonDown("Jump"))
			{
				
				dg_GameManager.TeachJump = true;
				jumping = true;
				rigid2D.velocity = new Vector2(0, jumpForce);
				animator_S.SetBool("isJump", jumping);
				animator_B.SetBool("isJump", jumping); //test版
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
			healthObj.localRotation = Quaternion.Euler(0, 0, 0);
			dg_GameManager.TeachMove = true;
		}
		else if (moveVec.x < 0)
		{
			graphics.localRotation = Quaternion.Euler(0, 180, 0);
			healthObj.localRotation = Quaternion.Euler(0, 180, 0);
			dg_GameManager.TeachMove = true;
		}

		if (dg_GameManager.isRun == true)
		{
			rigid2D.transform.position = new Vector3(rigid2D.transform.position.x + 0.05f, rigid2D.transform.position.y, 10);
			animator_S.SetBool("run", true);
			animator_B.SetBool("run", true);
		}
	}

	public void OnLanding() {

		if (rigid2D.velocity.y < 0)
		{
			animator_S.SetBool("fall", true);
			//animator_B.SetBool("fall", true);
			animator_S.SetBool("isJump", jumping);
			animator_B.SetBool("isJump", false);
			
		}
		jumping = false;
		  //test版
	}


	//---------------------Damage-----------------------
	void OnTriggerEnter2D(Collider2D col)  
	{
		if (col.tag == "smallEnemy") //玩家受到怪物攻擊
		{
			TakeDamage(10);
			animator_S.SetTrigger("beaten");
			animator_B.SetTrigger("beaten");
			StartCoroutine("beaten");
		}

		if (col.tag == "BossEnemy")
		{
			TakeDamage(15);
		}

		if (col.tag == "EndPoint")
		{
			dg_GameManager.win();
		}
	}

	IEnumerator beaten()
	{
		for (int i = 0; i < 2; i++)
		{
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			falsh.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void Attack()
	{
		animator_S.SetTrigger("attack");
		animator_B.SetTrigger("attack");
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

