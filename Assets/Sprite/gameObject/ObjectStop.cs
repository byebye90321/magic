using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ObjectStop : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player") 
		{
			Joystick.isMove = false;
			Destroy(gameObject.GetComponent<ObjectStop>());
		}
	}
}
