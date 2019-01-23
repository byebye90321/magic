using System.Collections;
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
	//----------------2版------------------

	public DG_EnemyController enemyController;
	public DG_playerController DG_playerController;
	//public PlayerController playerController;
	public Skills skill0;
	public Skills skillG1;
	public Skills skillB1;
	public Skills skillG2;
	public Skills skillB2;
	//----------Particle System--------------
	public GameObject G0_Particle;
	[HideInInspector]
	public ParticleSystem G0_ParticleP;
	public GameObject G1_Particle;
	[HideInInspector]
	public ParticleSystem G1_ParticleP;
	public GameObject G2_Particle;
	[HideInInspector]
	public ParticleSystem G2_ParticleP;
	public GameObject B1_Particle;
	[HideInInspector]
	public ParticleSystem B1_ParticleP;
	public GameObject B2_Particle;
	[HideInInspector]
	public ParticleSystem B2_ParticleP;

	//-------------Audio----------------
	public AudioSource audio;
	public AudioClip G0;
	public AudioClip G1;
	public AudioClip G2;
	public AudioClip B1;
	public AudioClip B2;

	void Start()
	{
		references = referenceRoot.GetComponentsInChildren<GesturePatternDraw>();
		G0_ParticleP = G0_Particle.GetComponent<ParticleSystem>();
		G1_ParticleP = G1_Particle.GetComponent<ParticleSystem>();
		G2_ParticleP = G2_Particle.GetComponent<ParticleSystem>();
		B1_ParticleP = B1_Particle.GetComponent<ParticleSystem>();
		B2_ParticleP = B2_Particle.GetComponent<ParticleSystem>();
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
				/*if (ChapterName == "0")
				{
					enemyController.StartCoroutine("Skill0");
				}*/
				Debug.Log("攻擊0");
				DG_playerController.Attack();
				audio.PlayOneShot(G0);
				textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
				skill0.currentCoolDown = 0;
				G0_Particle.SetActive(true);
				StartCoroutine("close0");
			}
			else
			{
			}
		}

		//技能1
		if (skillG1.attack == true)
		{
			if (result.gesture.id == "G1")
			{
				Debug.Log("G1攻擊");
				DG_playerController.Attack();
				audio.PlayOneShot(G1);
				skillG1.currentCoolDown = 0;
				G1_Particle.SetActive(true);
				StartCoroutine("close1");
			}
			else
			{
			}
		}
		else if(skillB1.attack == true)
		{
			if (result.gesture.id == "B1")
			{
				Debug.Log("B1攻擊");
				DG_playerController.Attack();
				audio.PlayOneShot(B1);
				skillB1.currentCoolDown = 0;
				B1_Particle.SetActive(true);
				StartCoroutine("close1");
			}
			else
			{
			}
		}

		//技能2
		if (skillG2.attack == true)
		{
			if (result.gesture.id == "Circle")
			{
				Debug.Log("G2攻擊");
				DG_playerController.Attack();
				audio.PlayOneShot(G2);
				skillG2.currentCoolDown = 0;
				G2_Particle.SetActive(true);
				StartCoroutine("close2");
			}
			else
			{
			}
		}
		else if (skillB2.attack == true)
		{
			if (result.gesture.id == "B2")
			{
				Debug.Log("B2攻擊");
				DG_playerController.Attack();
				audio.PlayOneShot(B2);
				skillB2.currentCoolDown = 0;
				B2_Particle.SetActive(true);
				StartCoroutine("close2");
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

	IEnumerator close0()
	{
		yield return new WaitForSeconds(1f);
		G0_Particle.SetActive(false);
	}

	IEnumerator close1()
	{
		yield return new WaitForSeconds(1f);
		G1_Particle.SetActive(false);
		B1_Particle.SetActive(false);
	}

	IEnumerator close2()
	{
		yield return new WaitForSeconds(1f);
		G2_Particle.SetActive(false);
		B2_Particle.SetActive(false);
	}
}
