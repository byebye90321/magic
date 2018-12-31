using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
	public static GameObject itemBeingDragged;
	public Image stone;
	public bool isRight;
	public bool full;
	public Vector3 startPosition;
	public Transform startParent;

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
			full = true;
		}
		if ((other.gameObject.name != gameObject.name) && itemBeingDragged == null)
		{
			full = true;
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if ((other.gameObject.name == gameObject.name))
		{
			isRight = false;
			full = false;
		}
		if ((other.gameObject.name != gameObject.name))
		{
			full = false;
		}
	}
}