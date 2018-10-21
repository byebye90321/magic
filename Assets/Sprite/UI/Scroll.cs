using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Scroll : MonoBehaviour ,IBeginDragHandler,IEndDragHandler{

	private ScrollRect scrollRect;
	private float[] pageArray = new float[] { 0, 0.5f,1 };
	public float smoothing = 5;
	private float targetVerticalPosition=1;
	private bool isDraging = false;
	// Use this for initialization
	void Start () {
		scrollRect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDraging == false)
		{
			scrollRect.verticalNormalizedPosition = Mathf.Lerp(scrollRect.verticalNormalizedPosition, targetVerticalPosition, Time.deltaTime * smoothing);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isDraging = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		/*float temp = scrollRect.verticalNormalizedPosition;
		print(temp);*/
		isDraging = false;
		float posY = scrollRect.verticalNormalizedPosition;
		int index = 0;
		float offset = Mathf.Abs(pageArray[index] - posY);
		for (int i = 1; i < pageArray.Length; i++)
		{
			float offsetTemp = Mathf.Abs(pageArray[i] - posY);
			if (offsetTemp < offset)
			{
				index = i;
				offset = offsetTemp;
			}
		}
		targetVerticalPosition = pageArray[index];
		//scrollRect.verticalNormalizedPosition = pageArray[index];
	}
}
