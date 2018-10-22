using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;
public class RG_playerController : MonoBehaviour
{
	public static RG_playerController Player;

	Rigidbody2D rigid2D;
	public GameObject hurt;

	//-----------------------ground check------------------------
	public LayerMask whatIsGround;
	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	//-----------------------Player Control----------------------
	//jump
	public float jumpForce = 70f;
	//slide
	public bool sliding = true;
	//up&down
	private Vector2 pointA;
	private Vector2 pointB;
	public bool Touch;

	//--------------------------velocity-------------------------
	public float speed;
	public float hurtSpeed;
	public float VecitySpeed;
	public float MaxSpeed = 0.8f;
	//---------------------------Hurt-----------------------------
	public float VecityHurt;
	public float RecoverySpeed;
	public GameObject falsh;

	//--------------音效
	public AudioSource audio;
	public AudioClip hurtSound;
	//--------------animator
	public Animator sister;
	public Animator bother;
	public SkeletonAnimation skeletonAnimation_S;
	public SkeletonAnimation skeletonAnimation_B;
	void Start()
	{
		Player = this;
		rigid2D = GetComponent<Rigidbody2D>();
		rigid2D.AddForce(new Vector2(0, 0));
		rigid2D.velocity = new Vector2(0, 0f);
		VecitySpeed = speed;
	}

	void Update()
	{	
		if (RunGameManager.gameState == GameState.Start)
		{
			skeletonAnimation_S.state.TimeScale = 0;	
			skeletonAnimation_B.state.TimeScale = 0;

		}
		else if (RunGameManager.gameState == GameState.Running)
		{
			skeletonAnimation_S.state.TimeScale = 1;
			skeletonAnimation_B.state.TimeScale = 1;
			//--------------jump---------------------
			Player.transform.position = new Vector3(Player.transform.position.x + VecitySpeed, Player.transform.position.y, 10);
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
			if (Input.GetMouseButtonUp(0))
			{
				pointB = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
				Touch = true;
			}
			else
			{
				Touch = false;
			}

			if (Input.GetMouseButtonDown(0))
			{
				pointA = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
			}


			if (VecitySpeed < speed)
			{
				VecitySpeed += RecoverySpeed;

				if (VecitySpeed < 0)
				{
					VecitySpeed = 0.02f;
				}

				if (VecitySpeed >= speed)
				{
					VecitySpeed = speed;
				}
				rigid2D.velocity = new Vector2(Time.deltaTime * VecitySpeed, rigid2D.velocity.y);
			}
			rigid2D.velocity = new Vector2(Time.deltaTime * VecitySpeed, rigid2D.velocity.y);
			
		}

	//------------------------------------Dead------------------------------------------ 
		else if (RunGameManager.gameState == GameState.Dead)
		{
			skeletonAnimation_S.loop = false;
			skeletonAnimation_B.loop = false;
			/*skeletonAnimation.AnimationName = "death";*/
			sister.SetTrigger("death");
			bother.SetTrigger("death");
		}
	//------------------------------------Win------------------------------------------- 
		else if (RunGameManager.gameState == GameState.Win)
		{
			skeletonAnimation_S.state.TimeScale = 0;
			skeletonAnimation_B.state.TimeScale = 0;
		}
		else if (RunGameManager.gameState == GameState.Pause)
		{
			skeletonAnimation_S.state.TimeScale = 0;
			skeletonAnimation_B.state.TimeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	
	}

	//---------------------------------Jump----------------------------------
	private void FixedUpdate()
	{
		if (Touch)
		{
			if (grounded)
			{
				/*Debug.Log(pointA);
				Debug.Log(pointB);*/
				Vector2 offset = pointB - pointA;
				if (offset.y > 0)
				{
					//Debug.Log("up");
					sister.SetTrigger("jump");
					bother.SetTrigger("jump");
					rigid2D.velocity = new Vector2(0, jumpForce);
				}
				else if (offset.y < 0 && sliding==true)
				{
					//Debug.Log("down");
					sister.SetTrigger("sliding");
					bother.SetTrigger("sliding");
					StartCoroutine("wait");
				}
				
			}
		}
	}

	public void Hurt()
	{
		VecitySpeed -= VecityHurt;	
		StartCoroutine("DoSomeThingInDelay");
	}
	IEnumerator DoSomeThingInDelay()
	{
		for (int i = 0; i < 3; i++) {
			falsh.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			falsh.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}	
	}

	IEnumerator wait() {
		sliding = false;
		yield return new WaitForSeconds(0.8f);
		sliding = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.tag == "EndPoint")
		{
			VecitySpeed = 0.07f;
			speed = 0.07f;
		}
	}
}
