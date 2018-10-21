using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;
public class RG_playerController : MonoBehaviour
{
	public static RG_playerController Player;

	Rigidbody2D rigi;
	/*	AudioSource audio;*/
	public GameObject hurt;

	//-----------------------ground check------------------------
	public LayerMask whatIsGround;
	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	//-----------------------Player Control----------------------
	public float jumpForce = 70f;
	public float speed;
	//2段跳
	//public float jumpForce2 = 10f;
	bool jumping = false;
	private float count = 1;


	public float hurtSpeed;
	//--------------------------velocity-------------------------
	//	bool pressdown=false;
	//	public float Vecity = 0.001f;
	public float stop;
	public float VecitySpeed;
	public float MaxSpeed = 0.8f;
	//---------------------------Hurt-----------------------------
	public float VecityHurt;
	public float RecoverySpeed;
	public GameObject falsh;
	//------------------------FeverTime---------------------------
	/*	public float FeverSpeed;
		public float AnimFeverTime;
		//-------------------------vecity animation-------------------
		public float AnimMaxrunSpeed=5f;
		public float AnimVecity = 0.001f;
		public float AnimStop;
		//--------------------------sound-----------------------------
		public AudioClip JumpSound;*/

	//--------------------------Animation-------------------------
	/*[SpineAnimation]
	public string runAnim;
	[SpineAnimation]
	public string jumpAnim;
	[SpineAnimation]
	public string deathAnim;
	SkeletonAnimation skeletonAnimation;*/

	//--------------音效
	public AudioSource audio;
	public AudioClip hurtSound;

	public Animator anim;

	void Start()
	{
		Player = this;

		rigi = GetComponent<Rigidbody2D>();
		//audio = GetComponent<AudioSource>();
		rigi.AddForce(new Vector2(0, 0));
		rigi.velocity = new Vector2(0, 0f);
		VecitySpeed = speed;
		//rend = GetComponent<SpriteRenderer>();
		/*skeletonAnimation = GetComponent<SkeletonAnimation>();
		skeletonAnimation.state.SetAnimation(0, "run", true); */ //(起始偵,動畫名,loop)

	}

	void Update()
	{
		
		if (RunGameManager.gameState == GameState.Start)
		{
			//skeletonAnimation.state.TimeScale = 0;
		}

		else if (RunGameManager.gameState == GameState.Running)
		{

			//skeletonAnimation.state.TimeScale = 1;
			Player.transform.position = new Vector3(Player.transform.position.x + VecitySpeed, Player.transform.position.y, 10);
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);



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
				rigi.velocity = new Vector2(Time.deltaTime * VecitySpeed, rigi.velocity.y);
			}
			rigi.velocity = new Vector2(Time.deltaTime * VecitySpeed, rigi.velocity.y);
			
		}

	//------------------------------------Dead------------------------------------------ 
		else if (RunGameManager.gameState == GameState.Dead)
		{			
			/*skeletonAnimation.loop = false;
			skeletonAnimation.AnimationName = "death";*/
		}
	//------------------------------------Win------------------------------------------- 
		else if (RunGameManager.gameState == GameState.Win)
		{
			//skeletonAnimation.state.TimeScale = 0;
		}
		else if (RunGameManager.gameState == GameState.Pause)
		{
			//skeletonAnimation.state.TimeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	
	}

	//---------------------------------Jump----------------------------------
	public void jump()
	{
		if (grounded)
		{
			rigi.AddForce(new Vector2(rigi.velocity.x, jumpForce), ForceMode2D.Impulse);
			anim.SetTrigger("jump");
			//skeletonAnimation.state.SetAnimation(0, "jump_RE", false);
			//skeletonAnimation.state.AddAnimation(0, "run", true, 0);
			//audio.PlayOneShot(JumpSound, 0.2f);//---JumpSound---

			jumping = true;

		}
		else if ((!grounded && jumping))
		{
			Debug.Log("NOjump");
			jumping = false;
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

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.tag == "EndPoint")
		{
			VecitySpeed = 0.07f;
			speed = 0.07f;
		}
	}
}
