using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour ,IDropHandler{
	public GameObject item{
		get {
			if (transform.childCount > 0) {
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}
	#region IDroHanlder implementation

	public void OnDrop(PointerEventData eventData)
	{
		if (!item)
		{
			drag.itemBeingDragged.transform.SetParent(transform);
			//ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
		}
	}
	#endregion
}
