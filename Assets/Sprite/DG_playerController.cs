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
	bool jumping = false;

	//--------------SpineAnimation----------------
	public Animator animator_S;
	public Animator animator_B;

	//--------------------Health-------------------
	public int curHealth = 100;
	public int maxHealth = 100;
	public Slider playerHealth;
	bool isDead;
	bool damaged;

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
				jumping = true;
				rigid2D.velocity = new Vector2(0, jumpForce);
				animator_S.SetBool("isJump", jumping);
				//animator_B.SetBool("isJump", jumping);
			}
			animator_S.SetBool("fall", false);
			//animator_B.SetBool("fall", false);
		}
		else {
			OnLanding();
			animator_S.SetBool("isJump", jumping);
			//animator_B.SetBool("isJump", jumping);
		}

		//--------------move----------------
		if (moveVec.x > 0)
		{
			graphics.localRotation = Quaternion.Euler(0, 0, 0);
		}
		else if (moveVec.x < 0)
		{
			graphics.localRotation = Quaternion.Euler(0, 180, 0);
		}

		
	}

	public void OnLanding() {

		if (rigid2D.velocity.y < 0)
		{
			animator_S.SetBool("fall", true);
			//animator_B.SetBool("fall", true);
		}
		animator_S.SetFloat("velocity", rigid2D.velocity.y);
		//animator_B.SetFloat("velocity", rigid2D.velocity.y);
		jumping = false;
	}


	//---------------------Damage-----------------------
	void OnTriggerEnter2D(Collider2D col)  //玩家受到小怪攻擊
	{
		if (col.tag == "monster")
		{
			TakeDamage(5);
		}

		if (col.tag == "BossMonster")
		{
			TakeDamage(15);
		}
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

