using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class RG_playerController : MonoBehaviour
{
	public string ChapterName;
	public static RG_playerController Player;
	public RunGameManager runGameManager;

	Rigidbody2D rigid2D;
	public GameObject hurt;

	//---------------------------Teach---------------------------
	public bool Up = false;
	public bool Down = false;
	public bool end = false;
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
	public Button jumpBtn;
	public Button slideBtn;
	//--------------------------velocity-------------------------
	public float speed;
	public float hurtSpeed;
	public float VecitySpeed;
	public float MaxSpeed = 0.8f;
	//---------------------------health-------------------------
	public Animator healthAni;
	public GameObject healthTextObj;

	private Text healthText;
	public GameObject healthObj;
	public GameObject canvas;
	//---------------------------Hurt-----------------------------
	public float VecityHurt;
	public float RecoverySpeed;
	public Animator flash;
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
		end = false;
		Player = this;
		rigid2D = GetComponent<Rigidbody2D>();
		rigid2D.AddForce(new Vector2(0, 0));
		rigid2D.velocity = new Vector2(0, 0f);
		VecitySpeed = speed;
		//healthText = healthTextObj.GetComponent<Text>();
		if (ChapterName == "0")
		{
			jumpBtn.interactable = false;
			slideBtn.interactable = false;
		}
	}

	//---------------------------------Jump----------------------------------
	private void FixedUpdate()
	{
		if (RunGameManager.gameState == GameState.Start)
		{
			skeletonAnimation_S.state.TimeScale = 0;
			if (ChapterName == "0")
			{
				skeletonAnimation_B.state.TimeScale = 0;
			}
		}
		else if (RunGameManager.gameState == GameState.Running)
		{
			if (ChapterName == "0")
			{
				if (Up == true || Down == true)
				{
					skeletonAnimation_S.state.TimeScale = 0.1f;
					skeletonAnimation_B.state.TimeScale = 0.1f;
				}
				else
				{
					skeletonAnimation_S.state.TimeScale = 1;
					skeletonAnimation_B.state.TimeScale = 1;
				}
			}
			else if (ChapterName == "1")
			{
				skeletonAnimation_S.state.TimeScale = 1;
			}
			//--------------jump---------------------
			Player.transform.position = new Vector3(Player.transform.position.x + VecitySpeed, Player.transform.position.y, 10);
			var hitObject = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
			grounded = hitObject;

			//rigid2D.velocity = new Vector2(rigid2D.velocity.x + VecitySpeed, rigid2D.velocity.y);
			//-----------------------------------3版-------------------------------------
			if (grounded)
			{
				if (CrossPlatformInputManager.GetButtonDown("Jump"))
				{
					rigid2D.velocity = new Vector2(0, jumpForce);
					sister.SetTrigger("jump");
					if (ChapterName == "0")
					{
						bother.SetTrigger("jump");
					}
					if (Up == true)
					{
						speed = 0.12f;
						VecitySpeed = 0.12f;
						runGameManager.maskGroup.SetActive(false);
						runGameManager.HintAni.SetTrigger("close");
						Up = false;
					}
				}
				else if (CrossPlatformInputManager.GetButtonDown("Slide"))
				{
					sister.SetTrigger("sliding");
					if (ChapterName == "0")
					{
						bother.SetTrigger("sliding");
					}
					SlidingParticle.SetActive(true);
					StartCoroutine("Sliding");
					if (Down == true)
					{
						speed = 0.12f;
						VecitySpeed = 0.12f;
						runGameManager.maskGroup.SetActive(false);
						runGameManager.HintAni.SetTrigger("close");
						Down = false;
					}
				}
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

		//------------------------------------Dead ------------------------------------------ 
		else if (RunGameManager.gameState == GameState.Dead)
		{
			if (!end)
			{
				skeletonAnimation_S.loop = false;
				sister.SetTrigger("death");
				if (ChapterName == "0")
				{
					skeletonAnimation_B.loop = false;
					bother.SetTrigger("death");
				}
				end = true;
			}
		}
		//---------------------------------MonsterCatch-------------------------------------
		else if (RunGameManager.gameState == GameState.MonsterCatch)
		{
			if (!end)
			{
				skeletonAnimation_S.loop = false;
				sister.SetTrigger("death");
				if (ChapterName == "0")
				{
					skeletonAnimation_B.loop = false;
					bother.SetTrigger("death");
				}
				end = true;
			}
		}
		//------------------------------------Win------------------------------------------- 
		else if (RunGameManager.gameState == GameState.Win)
		{
			skeletonAnimation_S.state.TimeScale = 0;
			if (ChapterName == "0")
			{
				skeletonAnimation_B.state.TimeScale = 0;
			}
		}
		else if (RunGameManager.gameState == GameState.Pause)
		{
			skeletonAnimation_S.state.TimeScale = 0;
			if (ChapterName == "0")
			{
				skeletonAnimation_B.state.TimeScale = 0;
			}
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
		if (ChapterName == "0")
		{
			bother.SetTrigger("run");
		}
	}

	public void Hurt()
	{
		VecitySpeed -= VecityHurt;
		runGameManager.playerHealth -= 1;
		runGameManager.HealthSlider.value = runGameManager.playerHealth;
		GameObject NEWatkpreft = Instantiate(healthObj) as GameObject;
		NEWatkpreft.transform.SetParent(canvas.transform, false);
		NEWatkpreft.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
		healthText = NEWatkpreft.GetComponentInChildren<Text>();
		healthText.text = "-" + 1;
		flash.SetTrigger("flash");
		if (runGameManager.playerHealth <= 0)
		{
			RunGameManager.Instance.Dead();
		}
		Destroy(NEWatkpreft, .5f);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Fade1")
		{
			VecitySpeed = 0.07f;
			speed = 0.07f;
		}

		if (col.gameObject.name == "Fade2_End")
		{
			RunGameManager.Instance.Win();
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

		if (col.gameObject.name == "monster")
		{
			RunGameManager.Instance.MonsterCatch();
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
