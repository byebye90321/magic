﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Defensive_sister_forest : MonoBehaviour{

	/*public static Defensive_sister_forest defensive;
	private Draw_sister_forest_Enemy drawEnemyController;
	public GameObject Atk;
	private Vector3 player;
	void Start() {
		drawEnemyController = GameObject.FindGameObjectWithTag("monster").GetComponent<Draw_sister_forest_Enemy>();
		player = new Vector3(-5.3f, -2.3f, 0);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 up = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D coll = Atk.GetComponent<Collider2D>();
			if (drawEnemyController.time_int > 0)
			{
				if (coll.OverlapPoint(up))
				{
					drawEnemyController.defensiveCount += 1;
					Destroy(Atk);
				}
			}
		}

		if (drawEnemyController.Attack == true)
		{
			Atk.transform.position = Vector3.Lerp(Atk.transform.position, player, 0.08f);
			StartCoroutine("wait");
		}
	}

	IEnumerator wait() {
		if (Atk.transform.position.x - player.x < 0.1f)
		{
			drawEnemyController.heartSystem.TakeDamage(drawEnemyController.Atk);
			Destroy(Atk);
		}
	   yield return new WaitForSeconds(2f);
	}*/
}