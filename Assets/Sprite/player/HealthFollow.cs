using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFollow : MonoBehaviour {

	public float xOffset;
	public float yOffset;

	public GameObject health;

	void Update()
	{
		Vector2 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
		health.transform.position = namePos + new Vector2(xOffset, yOffset);
	}

}
