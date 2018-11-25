using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

	//public GameObject playerobj;
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

	void Start(){
		
	}

	void FixedUpdate(){

		if (target.position.x > 3 &&count==0) //放大鏡頭
		{
			StartCoroutine(ZoomCamera(5, 2, 5, 100));
		}

		//transform.position = playerobj.transform.position;
		//transform.position = new Vector3(Mathf.Clamp(playerobj.transform.position.x + offset, xMin,xMax),Mathf.Clamp(playerobj.transform.position.y,yMin,yMax));


		Vector3 newPosition = target.position + offest;
		//transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax));
		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);

		if (transform.position.x < 0)
		{
			transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y);
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