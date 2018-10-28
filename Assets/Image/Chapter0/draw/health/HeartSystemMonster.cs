using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystemMonster : MonoBehaviour {

	public int curHealth = 100;
	public int maxHealth = 100;
	public Slider Health;

	bool isDead;
	bool damaged;


	public void Update()
	{
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
		//UpdateHearts();

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
