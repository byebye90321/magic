using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

	public Transform[] backgrounds;
	public float parallaxScale;
	public float parallaxReductionFactor;
	public float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;
	// Use this for initialization

	void Awake()
	{
		cam = Camera.main.transform;

	}

	void Start () {
		previousCamPos = cam.position;
	}
	
	// Update is called once per frame
	void Update () {
		float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;
		float parallay = (previousCamPos.y - cam.position.y) * parallaxScale;

		for (int i = 0; i < backgrounds.Length; i++) 
		{
			float backgroungTargetPosX = backgrounds [i].position.x + parallax * (i * parallaxReductionFactor + 1);
			float backgroungTargetPosY = backgrounds [i].position.y + parallay * (i * parallaxReductionFactor + 1);
			Vector3 backgroundTargetPos = new Vector3 (backgroungTargetPosX,backgroungTargetPosY, backgrounds [i].position.z);
			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		previousCamPos = cam.position;
	}
}
