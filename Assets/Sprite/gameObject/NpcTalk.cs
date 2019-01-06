using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTalk : MonoBehaviour {

	public bool gimmick;  //是否為機關
	public string gimmickName;
	public GameObject gimmickObj;

	public GameObject NPC;  //NPC物件
	[HideInInspector]
	public BoxCollider2D NPCBoxcollider;  

	public GameObject NPCPoint;  //NPC頭上驚嘆號
	public GameObject TalkBtn;  //對話鍵

	public string whoTask;
	private Image TalkImg;
	public bool isTasting = false; //是否可再開啟任務頁面

	public string startTaskName; //開始任務
	public string endTaskName; //完成任務

	public bool right;
	public bool wrong;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			NPCBoxcollider = NPC.GetComponent<BoxCollider2D>();
			NPCPoint.SetActive(true);
			TalkImg = TalkBtn.GetComponent<Image>();
			TalkImg.enabled = true;
			TalkBtn.transform.SetAsLastSibling();			
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			NPCPoint.SetActive(false);
			TalkImg.enabled = false;
		}
	}
}
