using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DG_EnemyController : MonoBehaviour{

	public string enemyName;
	public int enemyInt;
	public ExampleGestureHandler gesture;
    public Canvas drawCanvas;
	public GameManager gameManager;
	public GameObject player;
	//--------------------Health-------------------
	public int curHealth = 100;
	public int maxHealth = 100;
	public GameObject healthSlider;
	public Slider Health;
	public GameObject HealthCanvas;
	bool isDead;
	bool damaged;

	/*public Animator healthAni;
	public GameObject damageTextObj;
	private Text damageText;*/

	private Text healthText;
	public GameObject healthObj;
	public GameObject canvas;
	//--------------attack------
	public string hitSkillName;
	public int attackCount;
	//---------------------CUT--------------------
	//public GameObject obj1, obj2;    //分开后的两边水果
	//public GameObject[] wz;          //几种污渍背景
	//private BoxCollider2D col;
	//private Vector2[] vec = { Vector2.left, Vector2.right };   //切后的半截往两个方向飞出

	public bool isAttack = false; //戰鬥
	private float AtkCount = 0;
	//------------------Animation------------------ 
	public SkeletonAnimation enemy1;
	public SkeletonAnimation enemy2;
	public Transform enemy1Transform;
	public Transform enemy2Transform;

	public Animator Kattack;
	//--------------音效
	public AudioSource audio;
	public AudioClip AtkSound;

	//-------------Particle System-----------------
	public GameObject AtkParticle;
	public GameObject G0_beaten;
	public GameObject G1_beaten;
	public GameObject G2_beaten;
	public GameObject G3_beaten;
	public GameObject G4_beaten;
	public GameObject B1_beaten;
	public GameObject B2_beaten;
	public GameObject B3_beaten;
	public GameObject B4_beaten;

	void Start()
	{
		//col = GetComponent<BoxCollider2D>();

		if (enemyInt==2)
		{
			enemy2.state.SetAnimation(0, "idle", true);
		}
		enemy1.state.SetAnimation(0, "idle", true);

		canvas = transform.Find("Enemy/EnemyHealthCanvas/GameObject").gameObject;
		
	}
	
	void FixedUpdate()
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

		if (isAttack == true && AtkCount == 0)
		{
			StartCoroutine("Atk");
		}
	}

	IEnumerator Atk()
	{
		AtkCount = 1;
		yield return new WaitForSeconds(1);
		if (enemyName == "0") //序章
		{

		}
		else if(enemyName=="K")
		{
			Kattack.SetTrigger("attack");
		}
		else
		{
			InvokeRepeating("AttackTime", 1f, 10f);
		}
	}


	public void AttackTime()
	{
		W1_Particle();
	}

	public void KAttack()
	{
		enemy1.state.SetAnimation(0, hitSkillName, false);
		enemy1.state.AddAnimation(0, "idle", true, 0f);
		audio.PlayOneShot(AtkSound);
		AtkParticle.SetActive(true);
		StartCoroutine("wait");
	}

	//-------------------------序章Attack--------------------------
	public void W1_Particle()
	{
		enemy1.state.SetAnimation(0, hitSkillName, false);
		enemy1.state.AddAnimation(0, "idle", true, 0f);
		if (enemyInt == 2)
		{
			enemy2.state.SetAnimation(0, hitSkillName, false);
			enemy2.state.AddAnimation(0, "idle", true, 0f);
		}
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
		healthText.text = "-" + gesture.skillG2.skillInfo.Atk;
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
		if (enemyInt == 2)
		{
			enemy2.state.TimeScale = .4f;
		}
		if (enemyName == "K") //維吉維克
		{
			Kattack.speed = 0;
		}
		isDead = true;
		yield return new WaitForSeconds(0.7f);	
		healthSlider.SetActive(false);
		yield return new WaitForSeconds(.8f);
		enemy1.state.TimeScale = .1f;
		if (enemyInt == 2)
		{
			enemy2.state.TimeScale = .1f;
		}

		if (enemyName == "BossEnemy")  //歪歪JQ
		{
			StartCoroutine(gameManager.BossAttackWin());
		}
		else if (enemyName == "MonsterEnemy") //維吉維克
		{
			StartCoroutine(gameManager.MonsterAttackWin());
		}
		else if (enemyName == "K") //K
		{
			StartCoroutine(gameManager.KAttackWin());
		}
	}

		//------------------------Cut---------------------------
		/*private void CreateHalf(GameObject obj, int index)       //创建半个水果
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
	}*/

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Blade" && drawCanvas.isActiveAndEnabled && isAttack==true)
		{
			
			TakeDamage(1);
			StartCoroutine("damageActive");
			healthText.text = "-" + 1;		
			StartCoroutine("wait");
		}

        if (isAttack == true)
        {
            if (col.gameObject.name == "G0_Particle") //被G0攻擊
            {
                G0_beaten.SetActive(true);
                TakeDamage(gesture.skill0.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                healthText.text = "-" + (gesture.skill0.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("G0_Close");

            }

            if (col.gameObject.name == "G1_Particle") //被G1攻擊
            {
                G1_beaten.SetActive(true);
                TakeDamage(gesture.skillG1.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G1_Close");
                healthText.text = "-" + (gesture.skillG1.skillInfo.Atk + gesture.AddAttack);
            }

            if (col.gameObject.name == "B1_Particle") //被B1攻擊
            {
                B1_beaten.SetActive(true);
                TakeDamage(gesture.skillB1.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G1_Close");
                healthText.text = "-" + (gesture.skillB1.skillInfo.Atk + gesture.AddAttack);
            }

            if (col.gameObject.name == "G2_Particle") //被G2攻擊
            {
                G2_beaten.SetActive(true);
                TakeDamage(gesture.skillG2.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G2_Close");
                healthText.text = "-" + (gesture.skillG2.skillInfo.Atk + gesture.AddAttack);
            }
            if (col.gameObject.name == "B2_Particle") //被B2攻擊
            {
                B2_beaten.SetActive(true);
                TakeDamage(gesture.skillB2.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G2_Close");
                healthText.text = "-" + (gesture.skillB2.skillInfo.Atk + gesture.AddAttack);
            }

            if (col.gameObject.name == "G3_Particle") //被G3攻擊
            {
                G3_beaten.SetActive(true);
                TakeDamage(gesture.skillG3.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G3_Close");
                healthText.text = "-" + (gesture.skillG3.skillInfo.Atk + gesture.AddAttack);
            }
            if (col.gameObject.name == "B3_Particle") //被B3攻擊
            {
                B3_beaten.SetActive(true);
                TakeDamage(gesture.skillB3.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G3_Close");
                healthText.text = "-" + (gesture.skillB3.skillInfo.Atk + gesture.AddAttack);
            }

            if (col.gameObject.name == "G4_Particle") //被G4攻擊
            {
                G4_beaten.SetActive(true);
                TakeDamage(gesture.skillG4.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G4_Close");
                healthText.text = "-" + (gesture.skillG4.skillInfo.Atk + gesture.AddAttack);
            }
            if (col.gameObject.name == "B4_Particle") //被B4攻擊
            {
                B4_beaten.SetActive(true);
                TakeDamage(gesture.skillB4.skillInfo.Atk + gesture.AddAttack);
                StartCoroutine("damageActive");
                StartCoroutine("G4_Close");
                healthText.text = "-" + (gesture.skillB4.skillInfo.Atk + gesture.AddAttack);
            }
        }

	}

	IEnumerator damageActive()
	{
		enemy1.state.SetAnimation(0, "death", false);
		enemy1.state.AddAnimation(0, "idle", true, 0f);
		if(enemyInt==2)
		{
		enemy2.state.SetAnimation(0, "death", false);
		enemy2.state.AddAnimation(0, "idle", true, 0f);
		}
	
		//healthAni.SetTrigger("hurtText");
		GameObject NEWatkpreft = Instantiate(healthObj) as GameObject;
		NEWatkpreft.transform.SetParent(canvas.transform, false);
		NEWatkpreft.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
		healthText = NEWatkpreft.GetComponentInChildren<Text>();
		//healthText.text = "-" + damageInt;
		yield return new WaitForSeconds(.1f);
		Destroy(NEWatkpreft, .5f);
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds(1f);
		G0_beaten.SetActive(false);
		AtkParticle.SetActive(false);
	}

	IEnumerator G0_Close()
	{
		yield return new WaitForSeconds(1f);
		G0_beaten.SetActive(false);
	}

	IEnumerator G1_Close()
	{
		yield return new WaitForSeconds(.5f);
		G1_beaten.SetActive(false);
		B1_beaten.SetActive(false);
	}

	IEnumerator G2_Close()
	{
		yield return new WaitForSeconds(.5f);
		G2_beaten.SetActive(false);
		B2_beaten.SetActive(false);
	}

	IEnumerator G3_Close()
	{
		yield return new WaitForSeconds(.5f);
		G3_beaten.SetActive(false);
		B3_beaten.SetActive(false);
	}

	IEnumerator G4_Close()
	{
		yield return new WaitForSeconds(.5f);
		G4_beaten.SetActive(false);
		B4_beaten.SetActive(false);
	}
}


