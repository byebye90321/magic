using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DG_EnemyController : MonoBehaviour{

	public string enemyName;
	public ExampleGestureHandler gesture;
    public Canvas drawCanvas;
	public GameManager gameManager;
	//--------------------Health-------------------
	public int curHealth = 100;
	public int maxHealth = 100;
	public GameObject healthSlider;
	public Slider Health;
	bool isDead;
	bool damaged;

	public GameObject damageTextObj;
	private Text damageText;

	//---------------------CUT--------------------
	public GameObject obj1, obj2;    //分开后的两边水果
	public GameObject[] wz;          //几种污渍背景
	private BoxCollider2D col;
	private Vector2[] vec = { Vector2.left, Vector2.right };   //切后的半截往两个方向飞出

	public static bool isAttack = true;
	//------------------Animation------------------ 
	public SkeletonAnimation enemy1;
	public SkeletonAnimation enemy2;

	//--------------音效
	public AudioSource audio;
	public AudioClip AtkSound;

	//-------------Particle System-----------------
	public GameObject AtkParticle;
	public GameObject G0_beaten;
	public GameObject G1_beaten;
	public GameObject G2_beaten;
	public GameObject B1_beaten;
	public GameObject B2_beaten;

	void Start()
	{
		col = GetComponent<BoxCollider2D>();
		enemy1.state.SetAnimation(0, "idle", true);
		enemy2.state.SetAnimation(0, "idle", true);
		damageText = damageTextObj.GetComponent<Text>();
	}
	
	void Update()
	{
		//------------------health----------------------
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

	//-------------------------Attack--------------------------
	IEnumerator Skill0()  //序章 被技能0攻擊
	{
		yield return new WaitForSeconds(.3f);
		TakeDamage(gesture.skill0.skillInfo.Atk);
		enemy1.state.SetAnimation(0, "stun", true);
		enemy2.state.SetAnimation(0, "stun", true);
		damageTextObj.SetActive(true);
		damageText.text = "-" + gesture.skill0.skillInfo.Atk;
		StartCoroutine("wait");	
	}

	public void W1_Particle()
	{
		enemy1.state.SetAnimation(0, "hit", false);
		enemy1.state.AddAnimation(0, "idle", true, 0f);
		enemy2.state.SetAnimation(0, "hit", false);
		enemy2.state.AddAnimation(0, "idle", true, 0f);
		audio.PlayOneShot(AtkSound);
		AtkParticle.SetActive(true);
		StartCoroutine("wait");
	}

	public void G1_Beaten()
	{
		G1_beaten.SetActive(true);
		StartCoroutine("wait");
	}

	IEnumerator SkillG2()  //被技能G2攻擊
	{
		yield return new WaitForSeconds(.3f);
		TakeDamage(gesture.skillG2.skillInfo.Atk);
		enemy1.state.SetAnimation(0, "death", false);
		enemy2.state.SetAnimation(0, "death", false);
		damageTextObj.SetActive(true);
		damageText.text = "-" + gesture.skillG2.skillInfo.Atk;
		StartCoroutine("wait");
	}

	public void TakeDamage(int amount)
	{
		curHealth -= amount;
		curHealth = Mathf.Clamp(curHealth, 0, maxHealth);

		damaged = true;
		Debug.Log("damaged");
		if (curHealth <= 0 && !isDead)
		{
			StartCoroutine("Death");
			Debug.Log("DEAD");
		}
	}

	IEnumerator Death()
	{
		enemy1.state.TimeScale = .4f;
		enemy2.state.TimeScale = .4f;
		isDead = true;
		yield return new WaitForSeconds(0.7f);	
		healthSlider.SetActive(false);
		yield return new WaitForSeconds(.8f);
		enemy1.state.TimeScale = .1f;
		enemy2.state.TimeScale = .1f;

		if (enemyName == "BossEnemy")  //歪歪KQ
		{
			StartCoroutine(gameManager.BossAttackWin());
		}
		else if (enemyName == "MonsterEnemy") //維吉維克
		{
			StartCoroutine(gameManager.MonsterAttackWin());
		}
		

	}

		//------------------------Cut---------------------------
		private void CreateHalf(GameObject obj, int index)       //创建半个水果
	{
		obj = Instantiate(obj, transform.position, Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.back)) as GameObject;
		Rigidbody2D rgd = obj.GetComponent<Rigidbody2D>();
		float v = Random.Range(2f, 4f);                        //随机飞出速度
		rgd.velocity = vec[index] * v;
		Destroy(obj, 1f);
	}
	private void Createwz()              //切开水果随机创建污渍
	{
		if (wz.Length == 0)              //仓鼠没有水果污渍
			return;
		GameObject obj = Instantiate(wz[Random.Range(0, wz.Length)], transform.position, Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.back)) as GameObject;
		Destroy(obj, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Blade" && drawCanvas.isActiveAndEnabled && isAttack==true)
		{
			TakeDamage(1);
			damageTextObj.SetActive(true);
			damageText.text = "-" + 1;
			enemy1.state.SetAnimation(0, "death", false);
			enemy1.state.AddAnimation(0, "idle", true, 0f);
			enemy2.state.SetAnimation(0, "death", false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
			StartCoroutine("wait");
		}

		if (col.gameObject.name == "G0_Particle") //被G0攻擊
		{
			G0_beaten.SetActive(true);
			TakeDamage(gesture.skill0.skillInfo.Atk);
			enemy1.state.SetAnimation(0, "death", false);
			enemy1.state.AddAnimation(0, "idle", true, 0f);
			enemy2.state.SetAnimation(0, "death", false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
			damageTextObj.SetActive(true);
			StartCoroutine("G0_Close");
			damageText.text = "-" + gesture.skill0.skillInfo.Atk;
		}

		if (col.gameObject.name == "G1_Particle") //被G1攻擊
		{
			G1_beaten.SetActive(true);
			TakeDamage(gesture.skillG1.skillInfo.Atk);
			enemy1.state.SetAnimation(0, "death", false);
			enemy1.state.AddAnimation(0, "idle", true, 0f);
			enemy2.state.SetAnimation(0, "death", false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
			damageTextObj.SetActive(true);
			StartCoroutine("G1_Close");
			damageText.text = "-" + gesture.skillG1.skillInfo.Atk;
		}

		if (col.gameObject.name == "B1_Particle") //被B1攻擊
		{
			B1_beaten.SetActive(true);
			TakeDamage(gesture.skillB1.skillInfo.Atk);
			enemy1.state.SetAnimation(0, "death", false);
			enemy1.state.AddAnimation(0, "idle", true, 0f);
			enemy2.state.SetAnimation(0, "death", false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
			damageTextObj.SetActive(true);
			StartCoroutine("G1_Close");
			damageText.text = "-" + gesture.skillB1.skillInfo.Atk;
		}

		if (col.gameObject.name == "G2_Particle") //被G2攻擊
		{
			G2_beaten.SetActive(true);
			TakeDamage(gesture.skillG2.skillInfo.Atk);
			enemy1.state.SetAnimation(0, "death", false);
			enemy1.state.AddAnimation(0, "idle", true, 0f);
			enemy2.state.SetAnimation(0, "death", false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
			damageTextObj.SetActive(true);
			StartCoroutine("G2_Close");
			damageText.text = "-" + gesture.skillG2.skillInfo.Atk;
		}
		if (col.gameObject.name == "B2_Particle") //被B2攻擊
		{
			B2_beaten.SetActive(true);
			TakeDamage(gesture.skillB2.skillInfo.Atk);
			enemy1.state.SetAnimation(0, "death", false);
			enemy1.state.AddAnimation(0, "idle", true, 0f);
			enemy2.state.SetAnimation(0, "death", false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
			damageTextObj.SetActive(true);
			StartCoroutine("G2_Close");
			damageText.text = "-" + gesture.skillB2.skillInfo.Atk;
		}

	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds(.5f);
		damageTextObj.SetActive(false);
		yield return new WaitForSeconds(.5f);
		G0_beaten.SetActive(false);
		AtkParticle.SetActive(false);
	}

	IEnumerator G0_Close()
	{
		yield return new WaitForSeconds(.5f);
		damageTextObj.SetActive(false);
		yield return new WaitForSeconds(.5f);
		G0_beaten.SetActive(false);
	}

	IEnumerator G1_Close()
	{
		yield return new WaitForSeconds(.5f);
		damageTextObj.SetActive(false);
		yield return new WaitForSeconds(.5f);
		G1_beaten.SetActive(false);
		B1_beaten.SetActive(false);
	}

	IEnumerator G2_Close()
	{
		yield return new WaitForSeconds(.5f);
		damageTextObj.SetActive(false);
		yield return new WaitForSeconds(.5f);
		G2_beaten.SetActive(false);
	}
}


