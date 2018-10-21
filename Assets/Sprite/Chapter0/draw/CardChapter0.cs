using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardChapter0 : MonoBehaviour
, IPointerDownHandler
, IPointerUpHandler
, IEventSystemHandler
{

	public GameObject cardPanel;
	public Image cardImage;
	public void OnPointerDown(PointerEventData eventData)
	{
		cardPanel.SetActive(true);
	
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		cardPanel.SetActive(false);
	}
}
