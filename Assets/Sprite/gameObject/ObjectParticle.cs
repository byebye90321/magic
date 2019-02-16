using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParticle : MonoBehaviour
{

	public GameObject particle;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player") //觸碰到玩家
		{
			particle.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "Player") //離開玩家
		{
			particle.SetActive(false);
		}
	}
}
