using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ActiveClimb : MonoBehaviour {

	public string ClimbObjName;
	public GameObject ClimbBtn;
	private Image ClimbImg;
	public bool isClimb = false;  //是否可攀爬
	public float highestPoint;  //Y軸可爬最高點
	public Vector2 targetPoint; //降落點

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			ClimbImg = ClimbBtn.GetComponent<Image>();
			ClimbImg.enabled = true;  //開啟爬鍵
			ClimbBtn.transform.SetAsLastSibling();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			ClimbImg.enabled = false;
		}
	}
}
