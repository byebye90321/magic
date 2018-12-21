using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

	public string ChapterName;
	public GameManager gameManager;
	public Camera camera;
	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	//public float offset;

	public Transform target;
	public float smoothSpeed = 0.125f;
	public Vector3 offest;

	Vector3 velocity;

	//放大鏡頭變數
	public int scaleCount = 0;
	//移轉鏡頭變數
	public bool isFollowTarget = true;
	public int moveCount = 0;

	void Start(){
		
	}

	void FixedUpdate(){

		/*if (playController.isTasting && scaleCount == 0) //放大鏡頭
		{
			StartCoroutine(ZoomCamera(2.56f, 2, 0.1f, 50));
		}
		else if(!playController.isTasting && scaleCount == 1)
		{
			StartCoroutine(ZoomCamera(2, 2.56f, 0.1f, 50));
			count = 0;
		}*/


		if (ChapterName == "0")  //序章 跟隨
		{
			transform.position = target.position;
			transform.position = new Vector3(Mathf.Clamp(target.position.x+3f, xMin,xMax),Mathf.Clamp(target.position.y,yMin,yMax));
		}
		if (ChapterName == "1") //正章
		{
			if (isFollowTarget) //正常跟隨玩家狀況
			{
				Vector3 newPosition = target.position + offest;
				transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
			}
			else 
			{
				if (moveCount == 1)  //形石平台移動
				{
					Vector3 newPosition = new Vector3(8.5f, 3.5f, -8);
					transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
					StartCoroutine(gameManager.floorOpen());
				}

				if (moveCount == 2)  //形石機關解完，長藤蔓
				{
					Vector3 newPosition = new Vector3(14, 4.7f, -8);
					transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
					StartCoroutine(gameManager.vineOpen());
				}

				if (moveCount == 3)  //歪斜天平
				{
					Vector3 newPosition = new Vector3(29, 5, -8);
					transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
				}
				if (moveCount == 4)  //發現雕像
				{
					Vector3 newPosition = new Vector3(39, 3, -8);
					transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
				}
			}

			//transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
			//transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax),-8);

			if (transform.position.x < xMin)
			{
				transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y);
			}

			if (transform.position.y >= yMax)
			{
				transform.position = new Vector3(transform.position.x,yMax,-8);
			}

			offest.y = 2f - ((transform.position.y / (yMax - yMin)) * 2f); //offest Y軸偏移
			//Camera.main.orthographicSize = 5-((transform.position.y / (yMax - yMin)) * 4f);
			transform.position = new Vector3(transform.position.x, transform.position.y, -8);
		}
	}

	IEnumerator ZoomCamera(float from, float to, float time, float steps)  //放大鏡頭
	{
		scaleCount = 1;
		float f = 0;

		while (f <= 1)
		{
			Camera.main.orthographicSize = Mathf.Lerp(from, to, f);

			f += 1f / steps;

			yield return new WaitForSeconds(time / steps);
		}
	}

	
}