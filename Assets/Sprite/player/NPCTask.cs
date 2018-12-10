using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTask : MonoBehaviour {

	public string ChapterName;
	public Rigidbody2D rigid2D;
	//----------------NPC Tast------------------------
	public GameObject taskPanel; //任務面板
	public GameObject NPC1Point; //任務1提示!特效
	public bool isTasting = false; //是否可再開啟任務頁面
	public GameObject taskObj; //右邊支線任務面板
	public GameObject bookObj;
	private Animator taskAni;
	public Button bookBtn;
	public int bookCount = 0;

	public GameObject Bobby;
	private BoxCollider2D BobbyCollider;

	// Use this for initialization
	void Start () {
		if (ChapterName == "1")
		{
			BobbyCollider = Bobby.GetComponent<BoxCollider2D>();
			taskAni = bookObj.GetComponent<Animator>();
			taskAni.SetInteger("taskCount", 1);
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
				if (Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x) < 2 && NPC1Point.activeInHierarchy == true && isTasting == false)
				{
					isTasting = true;
					Tast();
				}
			}
		}

	}

	public void Tast()
	{
		Debug.Log(Mathf.Abs(rigid2D.transform.position.x - Bobby.transform.position.x));
		taskPanel.SetActive(true);
	}

	public void Tast_Yes()
	{
		isTasting = false;
		taskPanel.SetActive(false);
		BobbyCollider.enabled = false;
		taskAni.SetInteger("taskCount", 2); //任務1
	}

	public void Tast_NO()
	{
		isTasting = false;
		taskPanel.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "NPC_Bobby") //觸碰到NPC波比
		{
			NPC1Point.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "NPC_Bobby") //離開NPC波比
		{
			NPC1Point.SetActive(false);
		}
	}

	public void BookBtn()
	{
		if (bookCount == 1)
		{
			bookCount = 0;
			taskAni.SetBool("isOpen", true);
			taskObj.SetActive(true);
		}
		else {
			bookCount = 1;
			taskAni.SetBool("isOpen", false);
			taskObj.SetActive(false);
		}
	}
}
