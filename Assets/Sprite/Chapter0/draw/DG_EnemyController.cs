using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DG_EnemyController : MonoBehaviour{

	//--------------------------Animation-------------------------
	//public SkeletonAnimation skeletonAnimation_E1;
	//public SkeletonAnimation skeletonAnimation_E2;

	//--------------------Health-------------------
	public int curHealth = 100;
	public int maxHealth = 100;
	public Slider Health;
	bool isDead;
	bool damaged;

	//--------------音效
	/*public AudioSource audio;
	public AudioSource countAudio;
	public AudioClip countSound;
	public AudioClip atkSound;
	//結束
	public static bool end = false;*/

	void Start()
	{		
		
	}
	
	void Update()
	{
		//----------health------------
		if (damaged)
		{
			if (curHealth < Health.value)
			{
				Health.value -= 1;
			}
			else if (curHealth == Health.value)
			{
				Health.value = curHealth;
			}
		}
	}

	public void TakeDamage(int amount)
	{
		curHealth -= amount;
		curHealth = Mathf.Clamp(curHealth, 0, maxHealth);

		damaged = true;
		Debug.Log("damaged");
		if (curHealth <= 0 && !isDead)
		{
			Death();
			Debug.Log("DEAD");
		}
	}

	void Death()
	{
		isDead = true;
	}
}


