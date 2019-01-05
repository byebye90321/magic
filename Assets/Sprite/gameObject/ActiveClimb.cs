using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ActiveClimb : MonoBehaviour {

	public static string ClimbObjName;
	public GameObject ClimbBtn;
	private Image ClimbImg;
	public bool isClimb = false;  //是否可攀爬
	public bool isClimbBtn = false;
	public float highestPoint;  //Y軸可爬最高點
	public Vector2 targetPoint; //降落點

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player")
		{
			//scriptName = target.gameObject.GetComponent("MySetComponent");
			ClimbImg = ClimbBtn.GetComponent<Image>();
			//isClimb = true;  //是否可以爬
			ClimbImg.enabled = true;  //開啟爬鍵
			ClimbBtn.transform.SetAsLastSibling();
		}
	}
}
