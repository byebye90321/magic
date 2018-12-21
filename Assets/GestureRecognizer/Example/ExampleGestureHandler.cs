﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;

public class ExampleGestureHandler : MonoBehaviour
{
	//public static ExampleGestureHandler gesture;
	public string ChapterName;
	public Text textResult;
	public Transform referenceRoot;
	GesturePatternDraw[] references;

	//public static bool playerAtk = false;
	//----------------------卡牌----------------------
	/*public CardData cardData;
	public CardCD cardCD1;
	public CardCD cardCD2;
	public CardCD cardCD3;
	public CardCD cardCD4;

	public int cardAtk;
	public int cardCur1 = 1;
	public int cardCur2 = 1;
	public int cardCur3 = 1;
	public int cardCur4 = 1;

	public static bool cardisAtk1 = false;
	public static bool cardisAtk2 = false;
	public static bool cardisAtk3 = false;
	public static bool cardisAtk4 = false;*/

	//----------------2版------------------
	
	public DG_EnemyController enemyController;
	public DG_playerController DG_playerController;
	//public PlayerController playerController;
	public Skills skill0;
	public int skillAtk0;
	public bool isAtk0=false;
	public Skills skillG1;
	public Skills skillB1;
	public int skillAtk1;
	public bool isAtk1 = false;
	public Skills skillG2;
	public Skills skillB2;
	public int skillAtk2;
	public bool isAtk2 = false;
	//----------Particle System--------------
	public GameObject G0_Particle;
	public GameObject G0_beaten;

	void Start()
	{
		references = referenceRoot.GetComponentsInChildren<GesturePatternDraw>();
	}

	void ShowAll()
	{
		for (int i = 0; i < references.Length; i++)
		{
			references[i].gameObject.SetActive(true);
		}
	}

	public void OnRecognize(RecognitionResult result)
	{
		StopAllCoroutines();
		ShowAll();
		/*if (result != RecognitionResult.Empty)
		{
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";  //答對顯示幾%
			StartCoroutine(Blink(result.gesture.id));  //答對閃爍
		}
		else
		{
			textResult.text = "?";
		}*/

		if (result == RecognitionResult.Empty)
			return;

		//技能0
		if (skill0.attack == true)  
		{
			if (result.gesture.id == "M")
			{
				if (ChapterName == "0")
				{
					enemyController.StartCoroutine("Skill1");
				}
				isAtk0 = true;
				Debug.Log("攻擊");
				DG_playerController.Attack();
				textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
				skill0.currentCoolDown = 0;
				G0_Particle.SetActive(true);
				G0_beaten.SetActive(true);
				StartCoroutine("close");
			}
			else {

			}
		}

		//技能1
		if (skillG1.attack == true)
		{
			if (result.gesture.id == "M")
			{
				if (ChapterName == "0")
				{
					enemyController.StartCoroutine("Skill1");
				}
				isAtk1 = true;
				Debug.Log("攻擊");
				DG_playerController.Attack();
				textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
				skillG2.currentCoolDown = 0;
				//G0_Particle.SetActive(true);
				//G0_beaten.SetActive(true);
				StartCoroutine("close");
			}
			else
			{
			}
		}
		else if(skillB2.attack == true)
		{
			if (result.gesture.id == "M")
			{
				if (ChapterName == "0")
				{
					enemyController.StartCoroutine("Skill1");
				}
				isAtk2 = true;
				Debug.Log("攻擊");
				DG_playerController.Attack();
				textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
				skillB2.currentCoolDown = 0;
				//G0_Particle.SetActive(true);
				//G0_beaten.SetActive(true);
				StartCoroutine("close");
			}
			else
			{
			}
		}
	}

	IEnumerator Blink(string id)
	{
		var draw = references.Where(e => e.pattern.id == id).FirstOrDefault();
		if (draw != null)
		{
			var seconds = new WaitForSeconds(0.1f);
			for (int i = 0; i <= 20; i++)
			{
				draw.gameObject.SetActive(i % 2 == 0);
				yield return seconds;
			}
			draw.gameObject.SetActive(true);
		}
	}

	IEnumerator close()
	{
		yield return new WaitForSeconds(1f);
		isAtk1 = false;
		G0_Particle.SetActive(false);
		G0_beaten.SetActive(false);
	}
}
