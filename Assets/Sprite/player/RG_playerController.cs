using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class RG_playerController : MonoBehaviour
{
	public static RG_playerController Player;
	public RunGameManager runGameManager;

	Rigidbody2D rigid2D;
	public GameObject hurt;

	//---------------------------Teach---------------------------
	public bool Up = false;
	public bool Down = false;
	//public GameObject teachObj;
	//public Animator teachAnim;
	bool end = false;
	//-----------------------ground check------------------------
	public LayerMask whatIsGround;
	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.05f;
	//-----------------------Player Control----------------------
	//jump
	public float jumpForce = 70f;
	public bool jumping = false;
	//slide
	public bool sliding = false;
	//up&down
	/*private Vector2 pointA;
	private Vector2 pointB;
	public bool Touch;*/

	public Button jumpBtn;
	public Button slideBtn;
	//--------------------------velocity-------------------------
	public float speed;
	public float hurtSpeed;
	public float VecitySpeed;
	public float MaxSpeed = 0.8f;
	//---------------------------Hurt-----------------------------
	public float VecityHurt;
	public float RecoverySpeed;
	public GameObject flash;
	//---------------------------音效-----------------------------
	public AudioSource audio;
	public AudioClip hurtSound;
	//-------------------------animator---------------------------
	public Animator sister;
	public Animator bother;
	public SkeletonAnimation skeletonAnimation_S;
	public SkeletonAnimation skeletonAnimation_B;
	//------------------------Particle System-------------------
	public GameObject SlidingParticle;



	void Start()
	{
		Player = this;
		rigid2D = GetComponent<Rigidbody2D>();
		rigid2D.AddForce(new Vector2(0, 0));
		rigid2D.velocity = new Vector2(0, 0f);
		VecitySpeed = speed;
		jumpBtn.interactable = false;
		slideBtn.interactable = false;
	}

	//---------------------------------Jump----------------------------------
	private void FixedUpdate()
	{
		if (RunGameManager.gameState == GameState.Start)
		{
			skeletonAnimation_S.state.TimeScale = 0;
			skeletonAnimation_B.state.TimeScale = 0;

		}
		else if (RunGameManager.gameState == GameState.Running)
		{
			if (Up==true || Down==true)
			{
				skeletonAnimation_S.state.TimeScale = 0.1f;
				skeletonAnimation_B.state.TimeScale = 0.1f;
			}
			else
			{
				skeletonAnimation_S.state.TimeScale = 1;
				skeletonAnimation_B.state.TimeScale = 1;
			}
			//--------------jump---------------------
			Player.transform.position = new Vector3(Player.transform.position.x + VecitySpeed, Player.transform.position.y, 10);
            var hitObject = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
            grounded = hitObject;
            
			//rigid2D.velocity = new Vector2(rigid2D.velocity.x + VecitySpeed, rigid2D.velocity.y);
			//-----------------------------------3版-------------------------------------
			if (grounded)
			{
                //Debug.Log(hitObject.name);
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
				{
					rigid2D.velocity = new Vector2(0, jumpForce);
					sister.SetTrigger("jump");
					bother.SetTrigger("jump");
					if (Up == true)
					{
						speed = 0.12f;
						VecitySpeed = 0.12f;
						runGameManager.maskGroup.SetActive(false);
						runGameManager.HintAni.SetTrigger("close");
						//teachObj.SetActive(false);
						Up = false;
					}
				}
				else if (CrossPlatformInputManager.GetButtonDown("Slide"))
				{
					sister.SetTrigger("sliding");
					bother.SetTrigger("sliding");
					SlidingParticle.SetActive(true);
					StartCoroutine("Sliding");
					if (Down == true)
					{
						speed = 0.12f;
						VecitySpeed = 0.12f;
						runGameManager.maskGroup.SetActive(false);
						runGameManager.HintAni.SetTrigger("close");
						//teachObj.SetActive(false);
						Down = false;
					}
				}
			}

			//-----------------------------------2版-------------------------------------------
			if (!end)
			{
				/*if (Input.GetMouseButtonUp(0))
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
				}*/
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


	IEnumerator Sliding()
	{
		yield return new WaitForSeconds(0.8f);
		SlidingParticle.SetActive(false);
		VecitySpeed = 0.12f;
		sister.SetTrigger("run");
		bother.SetTrigger("run");
	}

	public void Hurt()
	{
		VecitySpeed -= VecityHurt;
		runGameManager.HealthSlider.value -= 1;
		runGameManager.damageTextObj.SetActive(true);
		StartCoroutine("HurtDelay");
	}
	IEnumerator HurtDelay()
	{
		for (int i = 0; i < 3; i++) {
			flash.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			flash.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.2f);
		runGameManager.damageTextObj.SetActive(false);
	}

	

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Fade1")
		{
			VecitySpeed = 0.07f;
			speed = 0.07f;
			end = true;
		}

		if (col.gameObject.name == "Fade2_End")
		{
			RunGameManager.Instance.win();
		}

		if (col.gameObject.name == "TeachUp")
		{
			hurt.SetActive(false);
			speed = 0.01f;
			VecitySpeed = 0.01f;
			Up = true;
			jumpBtn.interactable = true;
			runGameManager.HintAni.SetTrigger("HintOpen");
			runGameManager.HintText.text = "遇到下方障礙物，按跳躍鍵";
			runGameManager.maskGroup.SetActive(true);
			runGameManager.mask.GetComponent<RectTransform>().anchoredPosition = new Vector2(1180, 120);
		}

		if (col.gameObject.name == "TeachDown")
		{
			hurt.SetActive(false);
			speed = 0.01f;
			VecitySpeed = 0.01f;
			Down = true;
			slideBtn.interactable = true;
			runGameManager.HintAni.SetTrigger("HintOpen");
			runGameManager.HintText.text = "遇到上方障礙物，按下滑鍵";
			runGameManager.maskGroup.SetActive(true);
			runGameManager.mask.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 120);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "TeachUp")
		{
			speed = 0.12f;
			VecitySpeed = 0.12f;
			Up = false;
			runGameManager.maskGroup.SetActive(false);
			runGameManager.HintAni.SetTrigger("close");
			hurt.SetActive(true);
		}

		if (col.gameObject.name == "TeachDown")
		{
			speed = 0.12f;
			VecitySpeed = 0.12f;
			Down = false;
			runGameManager.maskGroup.SetActive(false);
			runGameManager.HintAni.SetTrigger("close");
			hurt.SetActive(true);
		}
	}
}
