using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCanvas : MonoBehaviour {

	public drag slot1;
	public drag slot2;
	public drag slot3;
	public drag slot4;
	public drag slot5;

	public GameObject stoneCanvas;

	public void FixedUpdate()
	{
		if (slot1.isRight && slot2.isRight && slot3.isRight && slot4.isRight && slot5.isRight)
		{
			Debug.Log("456");
			StartCoroutine("waitClose");
		}
	}

	IEnumerator waitClose()
	{
		yield return new WaitForSeconds(2);
		stoneCanvas.SetActive(false);
	}

	public void Close()
	{
		stoneCanvas.SetActive(false);
	}
}
