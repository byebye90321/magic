using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
	public static GameObject itemBeingDragged;
	public Image stone;
	public bool isRight;
	Vector3 startPosition;
	Transform startParent;

	public void OnBeginDrag(PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == startParent)
		{
			transform.position = startPosition;
		}
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if ((other.gameObject.name == gameObject.name) && itemBeingDragged == null) {
			isRight = true;
			//Debug.Log(isRight);
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if ((other.gameObject.name == gameObject.name))
		{
			isRight = false;
			//Debug.Log(isRight);
		}
	}
}