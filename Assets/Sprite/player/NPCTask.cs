using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTask : MonoBehaviour {

	public string ChapterName;
	public DG_playerController playerController;
	public DialogsScript1 dialogsScript1;
	public CameraFollow cameraFollow;
	//------------------player位置-------------------
	public Rigidbody2D rigid2D;
	//----------------NPC Tast------------------------
	public GameObject taskPanel; //任務面板
	public GameObject BobbyPoint; //任務1提示!特效
	public GameObject StonePoint; //任務1提示!特效
	public bool isTasting = false; //是否可再開啟任務頁面
	public GameObject taskObj; //右邊支線任務面板
	public GameObject bookObj;
	private Animator taskAni;
	public Button bookBtn;
	public int bookCount = 0;
	public bool BobbyTask; //判斷跟誰接任務

	//-------------------NPC---------------------
	public GameObject Bobby;
	private BoxCollider2D BobbyCollider;
	public GameObject Stone;
	private BoxCollider2D StoneCollider;
	//-------------------機關---------------------
	public GameObject StoneCanvas;
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

	// Use this for initialization
	void Start () {
		if (ChapterName == "1")
		{
			BobbyCollider = Bobby.GetComponent<BoxCollider2D>();
			StoneCollider = Stone.GetComponent<BoxCollider2D>();
			taskAni = bookObj.GetComponent<Animator>();
		}	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//----------------------NPC tast-------------------------
		if (Input.GetMouseButtonDown(0))
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
					isTasting = true;
					BobbyTask = true;
					BobbyTast();
				}
			}
			else if (hit.collider.name == "Stone" && !playerController.stoneObj1.activeInHierarchy && !playerController.stoneObj2.activeInHierarchy && !playerController.stoneObj3.activeInHierarchy && !playerController.stoneObj4.activeInHierarchy && !playerController.stoneObj5.activeInHierarchy)
			{
				if (Mathf.Abs(rigid2D.transform.position.x - Stone.transform.position.x) < 2 && StonePoint.activeInHierarchy == true && isTasting == false)
				{
					StoneCanvas.SetActive(true);
				}
			}
		}
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
	}

	//任務1 Bobby
	public void BobbyTast()
	{
		Debug.Log(Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x));

		dialogsScript1.currentLine = 24;
		dialogsScript1.endAtLine = 32;
		dialogsScript1.NPCAppear();
	}

	public void Task_Yes()
	{
		isTasting = false;
		taskPanel.SetActive(false);
		BobbyCollider.enabled = false;
		taskAni.SetInteger("taskCount", 2); //任務1
		bookCount = 0;  //如果右方面板關閉，強制開啟
		taskAni.SetBool("isOpen", true);
		if (BobbyTask == true)
		{
			dialogsScript1.currentLine = 33;
			dialogsScript1.endAtLine = 35;
			dialogsScript1.NPCAppear();
			BobbyTask = false;
		}
	}

	public void Task_NO()
	{
		isTasting = false;
		taskPanel.SetActive(false);
		if (BobbyTask == true)
		{
			dialogsScript1.currentLine = 36;
			dialogsScript1.endAtLine = 36;
			dialogsScript1.NPCAppear();
			BobbyTask = false;
		}
	}

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

	IEnumerator waitClose()
	{
		yield return new WaitForSeconds(3);
		StoneCanvas.SetActive(false);
		StoneCollider.enabled = false;
		yield return new WaitForSeconds(1);
		cameraFollow.isFollowTarget = false;
		cameraFollow.moveCount = 2;
		slot1.isRight = false;
	}

	public void Close()
	{
		StoneCanvas.SetActive(false);
	}
}
