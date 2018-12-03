using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

	public string ChapterName;
	
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

	//test
	public int count = 0;
	public DG_playerController playController;

	void Start(){
		
	}

	void FixedUpdate(){

		/*if (playController.isTasting && count == 0) //放大鏡頭
		{
			StartCoroutine(ZoomCamera(2.56f, 2, 0.1f, 50));
		}
		else if(!playController.isTasting && count == 1)
		{
			StartCoroutine(ZoomCamera(2, 2.56f, 0.1f, 50));
			count = 0;
		}*/

		//transform.position = playerobj.transform.position;
		//transform.position = new Vector3(Mathf.Clamp(playerobj.transform.position.x + offset, xMin,xMax),Mathf.Clamp(playerobj.transform.position.y,yMin,yMax));

		if (ChapterName == "0")
		{
			transform.position = target.position;
			transform.position = new Vector3(Mathf.Clamp(target.position.x+3f, xMin,xMax),Mathf.Clamp(target.position.y,yMin,yMax));
		}
		if (ChapterName == "1")
		{
			Vector3 newPosition = target.position + offest;
			transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);

			if (transform.position.x < xMin)
			{
				transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y);
			}
			if (transform.position.y >= yMax)
			{
				transform.position = new Vector2(transform.position.x, Mathf.Clamp(target.position.y, yMin, yMax));
			}
			offest.y = 2.3f - ((transform.position.y / (yMax - yMin)) * 2.3f);
			//Camera.main.orthographicSize = 5-((transform.position.y / (yMax - yMin)) * 4f);
		}
	}

	IEnumerator ZoomCamera(float from, float to, float time, float steps)  //放大鏡頭
	{
		count = 1;
		float f = 0;

		while (f <= 1)
		{
			Camera.main.orthographicSize = Mathf.Lerp(from, to, f);

			f += 1f / steps;

			yield return new WaitForSeconds(time / steps);
		}
	}
}