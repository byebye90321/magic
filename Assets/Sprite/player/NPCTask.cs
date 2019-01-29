using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public class NPCTask : MonoBehaviour {

	public string ChapterName;
	public GameManager gameManager;
	public DG_playerController playerController;
	public DialogsScript1 dialogsScript1;
	public CameraFollow cameraFollow;
	//------------------player位置-------------------
	public Rigidbody2D rigid2D;
	//----------------NPC Tast------------------------
	public GameObject taskPanel; //任務面板
	public Text taskTitleText;  //任務標題
	public Text taskContentText; //內容文字
	public GameObject taskObj; //右邊支線任務面板
	public GameObject bookObj;
	private Animator taskAni;
	public GameObject otherTitle;  //支線任務title
	public Image Task1StarImage;   //未完成1任務星星
	public Image Task2StarImage;   //未完成2任務星星
	public Sprite TaskFinishImage;  //完成任務打勾
	public GameObject otherTask1; //波比任務
	public GameObject otherTask2; //雕像任務
	public Button bookBtn;
	public int bookCount = 0;
	public bool BobbyTask; //判斷跟誰接任務
	public bool StatueTask; //判斷跟誰接任務

	public GameObject TaskBtn; //任務YES NO按鈕
	public GameObject TaskCloseBtn;
	//-------------------NPC---------------------
	public SkeletonAnimation BobbyAni;
	public GameObject Bobby;
	[HideInInspector]
	public BoxCollider2D BobbyCollider;
	public GameObject Stone;
	private BoxCollider2D StoneCollider;
	public GameObject Statue;
	[HideInInspector]
	public BoxCollider2D StatueCollider;
	private Animator statueAni;
	//-------------------機關---------------------
	public GameObject StoneCanvas;
	public GameObject stoneBefore;
	public GameObject stoneAfter;
	public drag slot1;
	public drag slot2;
	public drag slot3;
	public drag slot4;
	public drag slot5;
	public GameObject StoneParticle1;
	public GameObject StoneParticle2;
	public GameObject StoneParticle3;
	public GameObject StoneParticle4;
	public GameObject StoneParticle5;
	public GameObject stoneFlash;
	public GameObject Fairy;
	public GameObject BigBalance;
	private Animator BigBalanceAni;
	//--------------Audio---------------
	public AudioSource audio;
	public AudioClip stoneWin;
	public AudioClip stoneLose;
	// Use this for initialization
	void Start () {
		if (ChapterName == "1")
		{
			BobbyCollider = Bobby.GetComponent<BoxCollider2D>();
			StoneCollider = Stone.GetComponent<BoxCollider2D>();
			StatueCollider = Statue.GetComponent<BoxCollider2D>();
			taskAni = bookObj.GetComponent<Animator>();
			statueAni = Statue.GetComponent<Animator>();
			BigBalanceAni = BigBalance.GetComponent<Animator>();
		}	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//----------------------NPC tast-------------------------
		/*if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

			if (hit.collider == null)
			{
				//Debug.Log("null");
				//Debug.Log(hit.collider.name);
			}
			else if (hit.collider.name == "NPC_Bobby")
			{
				if (Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x) < 2 && BobbyPoint.activeInHierarchy == true && isTasting == false)
				{
					playerController.npcTalk.isTasting = true;
					if (playerController.isRedFlower || playerController.isBlueFlower)  //完成任務
					{
						TaskFinish();
					}
					else  //接任務
					{
						BobbyTask = true;
						BobbyTast();
					}
				}
			}
			else if (hit.collider.name == "Stone" && !playerController.stoneObj1.activeInHierarchy && !playerController.stoneObj2.activeInHierarchy && !playerController.stoneObj3.activeInHierarchy && !playerController.stoneObj4.activeInHierarchy && !playerController.stoneObj5.activeInHierarchy)
			{
				if (Mathf.Abs(rigid2D.transform.position.x - Stone.transform.position.x) < 2 && StonePoint.activeInHierarchy == true && isTasting == false)
				{
					StoneCanvas.SetActive(true);
				}
			}
			else if (hit.collider.name == "NPC_Statue")
			{
				if (Mathf.Abs(rigid2D.transform.position.x - Statue.transform.position.x) < 2 && StatuePoint.activeInHierarchy == true && isTasting == false)
				{
					playerController.npcTalk.isTasting = true;
					if (playerController.isRedFairy || playerController.isBlueFairy)  //完成任務
					{
						TaskFinish();
					}
					else  //接任務
					{
						StatueTask = true;
						StatueTast();
					}
				}
			}
		}*/
		//-------------------------森林機關-----------------------
		if (slot1.isRight && slot2.isRight && slot3.isRight && slot4.isRight && slot5.isRight) //完成的時候
		{
			StoneParticle1.SetActive(true);
			StoneParticle2.SetActive(true);
			StoneParticle3.SetActive(true);
			StoneParticle4.SetActive(true);
			StoneParticle5.SetActive(true);
			StartCoroutine("waitClose");
		}
		if (slot1.full && slot2.full && slot3.full && slot4.full && slot5.full)
		{
			if (!slot1.isRight || !slot2.isRight || !slot3.isRight || !slot4.isRight || !slot5.isRight)
			{
				StartCoroutine("StoneWrong");
			}
		}
		
	}

	//任務1 Bobby
	public void BobbyTast()
	{
		Debug.Log(Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x));

		dialogsScript1.currentLine = 24;
		dialogsScript1.endAtLine = 33;
		dialogsScript1.NPCAppear();
	}

	public void Task_Yes()
	{
		playerController.npcTalk.isTasting = false;
		taskPanel.SetActive(false);
		bookCount = 0;  //如果右方面板關閉，強制開啟
		taskAni.SetBool("isOpen", true);
		if (playerController.npcTalk.whoTask == "BobbyTask")
		{
			BobbyCollider.enabled = false;
			otherTitle.SetActive(true);
			otherTask1.SetActive(true);  //波比任務
			dialogsScript1.currentLine = 33;
			dialogsScript1.endAtLine = 35;
			dialogsScript1.NPCAppear();
			BobbyTask = false;
		}
		if (playerController.npcTalk.whoTask == "StatueTask")
		{
			StatueCollider.enabled = false;
			otherTitle.SetActive(true);
			otherTask2.SetActive(true); //雕像任務
			dialogsScript1.currentLine = 51;
			dialogsScript1.endAtLine = 51;
			dialogsScript1.NPCAppear();
			statueAni.SetBool("isOpen1", false);
			Fairy.SetActive(true);
			StatueTask = false;
		}
	}

	public void Task_NO()
	{
		playerController.npcTalk.isTasting = false;
		taskPanel.SetActive(false);
		gameManager.balanceValue -= 5;
		if (playerController.npcTalk.whoTask == "BobbyTask")
		{
			BobbyCollider.enabled = false;
			dialogsScript1.currentLine = 36;
			dialogsScript1.endAtLine = 37;
			dialogsScript1.NPCAppear();
			BobbyTask = false;
		}
		if (playerController.npcTalk.whoTask == "StatueTask")
		{
			StatueCollider.enabled = false;
			dialogsScript1.currentLine = 52;
			dialogsScript1.endAtLine = 53;
			dialogsScript1.NPCAppear();
			statueAni.SetBool("isOpen1", false);
			StatueTask = false;
		}
	}

	//任務2 Statue
	public void StatueTast()
	{
		dialogsScript1.currentLine = 48;
		dialogsScript1.endAtLine = 50;
		dialogsScript1.NPCAppear();

		Debug.Log(Mathf.Abs(rigid2D.transform.position.x - Statue.transform.position.x));
		taskTitleText.text = "雕像平衡";
		taskContentText.text = "恢復平衡需要一種重物，我想<color=#ef6c00>紅精靈</color>再適合不過了!牠們就棲息在<color=#ef6c00>荊棘樹幹的樹洞</color>中，幫我抓一隻回來吧!";
	}


	public void TaskFinish()
	{
		StartCoroutine(playerController.npcTalk.endTaskName);
	}

	//任務2完成，獲得技能2
	public IEnumerator StatueTaskFinish()
	{
		playerController.npcTalk.isTasting = false;
		StatueCollider.enabled = false;
		Task2StarImage.sprite = TaskFinishImage;
		if (playerController.npcTalk.right) //假如選到正確的
		{
			statueAni.SetBool("win", true);
			dialogsScript1.currentLine = 54;
			dialogsScript1.currentLine = 54;
			dialogsScript1.NPCAppear();
		}
		else {
			dialogsScript1.currentLine = 55;
			dialogsScript1.currentLine = 55;
			dialogsScript1.NPCAppear();
		}
		yield return new WaitForSeconds(.5f);
		gameManager.eventObj.SetActive(true);
		gameManager.eventText.text = "完成任務二";
		yield return new WaitForSeconds(2f);
		gameManager.eventObj.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		gameManager.eventObj.SetActive(true);
		gameManager.eventText.text = "獲得技能二";
		yield return new WaitForSeconds(0.5f);
		gameManager.ParticleObj2.SetActive(true); //skill Particle
		yield return new WaitForSeconds(0.5f);
		if (playerController.npcTalk.right)
		{
			StaticObject.G2 = 1;
			gameManager.G2.SetActive(true);
			PlayerPrefs.SetInt("StaticObject.G2", StaticObject.G2);
		}
		else
		{
			StaticObject.B2 = 1;
			gameManager.B2.SetActive(true);
			PlayerPrefs.SetInt("StaticObject.B2", StaticObject.B2);
		}
		yield return new WaitForSeconds(1f);
		gameManager.ParticleObj2.SetActive(false);
		gameManager.eventObj.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		cameraFollow.isFollowTarget = false;
		cameraFollow.moveCount = 5;
		yield return new WaitForSeconds(.5f);
		if (playerController.npcTalk.right)
		{
			BigBalanceAni.SetBool("balance", true);
		}		
	}

	//任務1完成，獲得技能1
	public IEnumerator BobbyTaskFinish()
	{
		playerController.npcTalk.isTasting = false;
		Task1StarImage.sprite = TaskFinishImage;
		if (playerController.npcTalk.wrong) //lose
		{
			dialogsScript1.currentLine = 68;
			dialogsScript1.endAtLine = 70;
			dialogsScript1.NPCAppear();
			yield return new WaitUntil(() =>dialogsScript1.currentLine >=70);
		}
		else //win
		{
			BobbyAni.state.SetAnimation(0, "idle__Multicolor", true);
			dialogsScript1.currentLine = 63;
			dialogsScript1.endAtLine = 67;
			dialogsScript1.NPCAppear();
			yield return new WaitUntil(() => dialogsScript1.currentLine >= 67);
		}
		BobbyCollider.enabled = false;
		yield return new WaitForSeconds(0.5f);
		gameManager.eventObj.SetActive(true);
		gameManager.eventText.text = "完成任務一";
		yield return new WaitForSeconds(2f);
		gameManager.eventObj.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		gameManager.eventObj.SetActive(true);
		gameManager.eventText.text = "獲得技能一";
		yield return new WaitForSeconds(0.5f);
		gameManager.ParticleObj1.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		if (playerController.npcTalk.right)
		{
			StaticObject.G1 = 1;
			gameManager.G1.SetActive(true);
			PlayerPrefs.SetInt("StaticObject.G1", StaticObject.G1);
		}
		else
		{
			StaticObject.B1 = 1;
			gameManager.B1.SetActive(true);
			PlayerPrefs.SetInt("StaticObject.B1", StaticObject.B1);
		}
		yield return new WaitForSeconds(1f);
		gameManager.ParticleObj1.SetActive(false);
		gameManager.eventObj.SetActive(false);
		yield return new WaitForSeconds(1.5f);
		gameManager.FadeWhite.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		rigid2D.position = new Vector2(39f, 1f);
		yield return new WaitForSeconds(1.5f);
		gameManager.FadeWhite.SetActive(false);
		yield return new WaitForSeconds(1f);
		gameManager.stoneDoorAni.SetBool("openDoor", true);
	}

	public void OpenOtherTask1()
	{
		taskPanel.SetActive(true);
		TaskBtn.SetActive(false);
		TaskCloseBtn.SetActive(true);
		taskTitleText.text = "波比的花";
		taskContentText.text = "我有一朵很珍惜的<color=#ef6c00>水晶蘭花</color>，現在花被視為異端，歪歪們把牠搶走了!還把我痛毆一頓...請幫助我拿回屬於我的花兒!\n\n<color=#ef6c00>石鎮處</color>的線索會幫助你順利前行!找到歪歪的下落";
	}

	public void OpenOtherTask2()
	{
		taskPanel.SetActive(true);
		TaskBtn.SetActive(false);
		TaskCloseBtn.SetActive(true);
		taskTitleText.text = "雕像平衡";
		taskContentText.text = "恢復平衡需要一種重物，我想<color=#ef6c00>紅精靈</color>再適合不過了!牠們就棲息在<color=#ef6c00>荊棘樹幹的樹洞</color>中，幫我抓一隻回來吧!";
	}

	public void CloseTaskPanel()
	{
		taskPanel.SetActive(false);
		TaskCloseBtn.SetActive(false);
	}

	//---------------------book-------------------------
	public void bookFly()
	{
		bookObj.SetActive(true);
		taskAni.SetInteger("taskCount", 1);
		taskAni.SetBool("isOpen", true);
	}

	public void BookBtn()  
	{
		if (bookCount == 1) //打開
		{
			bookCount = 0;
			taskAni.SetBool("isOpen", true);
			taskObj.SetActive(true);
		}
		else {  //關閉
			bookCount = 1;
			taskAni.SetBool("isOpen", false);
			taskObj.SetActive(false);
		}
	}

	//------------------------石鎮-------------------------

	IEnumerator waitClose()  //關閉石陣機關
	{
		if (!audio.isPlaying)
		{
			audio.PlayOneShot(stoneWin);
		}
		
		yield return new WaitForSeconds(3);
		StoneCanvas.SetActive(false);
		StoneCollider.enabled = false;
		stoneAfter.SetActive(true);
		stoneBefore.SetActive(false);
		yield return new WaitForSeconds(.3f);
		cameraFollow.isFollowTarget = false;
		cameraFollow.moveCount = 2;
		slot1.isRight = false;  //防止循環
	}

	IEnumerator StoneWrong()  //石鎮錯誤
	{
		if (!audio.isPlaying)
		{
			audio.PlayOneShot(stoneLose);
		}
		
		for (int i = 0; i < 1; i++)
		{
			stoneFlash.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			stoneFlash.SetActive(false);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.1f);
		slot1.gameObject.transform.position = slot1.startPosition;
		slot1.gameObject.transform.parent = slot1.startParent;
		slot2.gameObject.transform.position = slot2.startPosition;
		slot2.gameObject.transform.parent = slot2.startParent;
		slot3.gameObject.transform.position = slot3.startPosition;
		slot3.gameObject.transform.parent = slot3.startParent;
		slot4.gameObject.transform.position = slot4.startPosition;
		slot4.gameObject.transform.parent = slot4.startParent;
		slot5.gameObject.transform.position = slot5.startPosition;
		slot5.gameObject.transform.parent = slot5.startParent;
	}
	
	public void Close()  //石鎮解謎關閉
	{
		StoneCanvas.SetActive(false);
	}
}
