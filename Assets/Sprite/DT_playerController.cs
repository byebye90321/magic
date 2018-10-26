using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class DT_playerController : MonoBehaviour
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
	float dirX;

	//-----------SpineAnimation----------------
	public Animator animator_S;
	public Animator animator_B;

	//------------NPC Obstacle----------------
	public GameObject particle;

	void Start()
	{
		rigid2D.velocity = new Vector2(0, 0f);
		//Active.interactable = false;
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
				animator_S.SetBool("isJump", true);
				//animator_B.SetBool("isJump", true);
			}
			animator_S.SetBool("fall", false);
			//animator_B.SetBool("fall", false);
		}
		else {
			OnLanding();
			animator_S.SetBool("isJump", false);
			//animator_B.SetBool("isJump", false);
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
		//animator.SetBool("isJump", false);
		jumping = false;
	}


	/*void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "flower")
		{
			particle.SetActive(true);
			Active.interactable = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.name == "flower")
		{
			particle.SetActive(false);
			Active.interactable = false;
		}
	}*/

}

