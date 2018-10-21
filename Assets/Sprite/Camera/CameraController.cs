using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject cam1, cam2;
	public Canvas canvas;
	public GameObject winFade;

	bool isWin = false;
	Animator fade;
	void Start() {
		fade = winFade.GetComponent<Animator>();
	}

	void Awake()
	{
		cam2.SetActive(false);
		cam1.SetActive(true);
	}

	void Update()
	{
		if (isWin==false)
		{
			cam1.SetActive(true);			
			cam2.SetActive(false);
			
		}
		else if(isWin == true)
		{
			cam2.SetActive(true);		
			cam1.SetActive(false);
			canvas.GetComponent<Canvas>().enabled = false;
		}
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.tag == "Player")
		{
			StartCoroutine("waitEnd");
		}
	}
	IEnumerator waitEnd()
	{
		winFade.SetActive(true);
		fade.SetBool("FIFO", true);
		yield return new WaitForSeconds(1f);
		Debug.Log("win");
		
		yield return new WaitForSeconds(0.1f);
		isWin = true;
		//fade.SetBool("FIFO", false);
	}
}
