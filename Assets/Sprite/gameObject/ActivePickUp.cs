using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePickUp : MonoBehaviour{

	public GameObject PickUpObj;
	public string PickUpObjName;
	public bool PickUpObjBool;
	public GameObject PickUpBtn;
	[HideInInspector]
	public Image PickUpImg;
	public static int PickUpInt;

	public bool task;  //是否為任務拾取道具
	public GameObject anotherObj;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			PickUpImg = PickUpBtn.GetComponent<Image>();
			PickUpImg.enabled = true;  //開啟拾取鍵
			PickUpBtn.transform.SetAsLastSibling();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			PickUpImg.enabled = false;
		}
	}
}
