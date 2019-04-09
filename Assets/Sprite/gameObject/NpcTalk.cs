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

	public GameObject NPCPoint;  //未完成?
    public GameObject finishPoint; //完成!
	public GameObject TalkBtn;  //對話鍵

	public string whoTask;
	private Image TalkImg;
	public bool isTasting = false; //是否可再開啟任務頁面

	public string startTaskName; //開始任務
	public string endTaskName; //完成任務

	public bool right;
	public bool wrong;

    public void Start()
    {
        NPCBoxcollider = NPC.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			NPCBoxcollider = NPC.GetComponent<BoxCollider2D>();
            if (right || wrong)
            {
                finishPoint.SetActive(true);
            }
            else
            {
                NPCPoint.SetActive(true);
            }
			TalkImg = TalkBtn.GetComponent<Image>();
			TalkImg.enabled = true;
			TalkBtn.transform.SetAsLastSibling();			
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
            if (right || wrong)
            {
                finishPoint.SetActive(false);
            }
            else
            {
                NPCPoint.SetActive(false);
            }
			TalkImg.enabled = false;
		}
	}
}
