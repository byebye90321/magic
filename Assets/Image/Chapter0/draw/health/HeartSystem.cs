using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour {
    public int curHealth = 100;
    public int maxHealth = 100;
	public Slider playerHealth;

	bool isDead;
	bool damaged;

	void Start()
	{
		//初始 血量全域
	}

	public void Update()
	{
		if (damaged)
		{
			if (curHealth < playerHealth.value)
			{
				playerHealth.value -= 1;
			}
			else if(curHealth == playerHealth.value)
			{
				playerHealth.value = curHealth;
			}
		}

	}


	void OnTriggerEnter2D(Collider2D col)  //玩家受到小怪攻擊
	{
		if (col.tag == "monster")
		{
			TakeDamage(5);
		}

		if (col.tag == "BossMonster")
		{
			TakeDamage(15);
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
