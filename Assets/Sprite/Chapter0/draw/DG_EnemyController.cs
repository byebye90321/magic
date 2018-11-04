using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DG_EnemyController : MonoBehaviour{

	public ExampleGestureHandler gesture;
    public Canvas drawCanvas;

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
					
	//------------------Animation------------------ 
	public SkeletonAnimation enemy1;
	public SkeletonAnimation enemy2;

	//--------------音效
	/*public AudioSource audio;
	public AudioSource countAudio;
	public AudioClip countSound;
	public AudioClip atkSound;
	//結束
	public static bool end = false;*/

	//-------------Particle System-----------------
	public GameObject AtkParticle;
	public GameObject G1_BeatenParticle; //被G1攻擊特效

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
	IEnumerator Skill1()  //被技能1攻擊
	{
		yield return new WaitForSeconds(.3f);
		TakeDamage(gesture.skillAtk1);
		enemy1.state.SetAnimation(0, "stun", true);
		enemy2.state.SetAnimation(0, "stun", true);
		damageTextObj.SetActive(true);
		damageText.text = "-" + gesture.skillAtk1;
		StartCoroutine("wait");
	}

	public void Atk()
	{
		enemy1.state.SetAnimation(0, "hit", false);
		enemy1.state.AddAnimation(0, "idle", true, 0f);
		enemy2.state.SetAnimation(0, "hit", false);
		enemy2.state.AddAnimation(0, "idle", true, 0f);
		AtkParticle.SetActive(true);
		StartCoroutine("wait");
	}

	public void G1_Beaten()
	{
		G1_BeatenParticle.SetActive(true);
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
		isDead = true;
		yield return new WaitForSeconds(0.7f);
		healthSlider.SetActive(false);
		Destroy(this.gameObject);
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
		if (col.gameObject.name == "Blade" && drawCanvas.isActiveAndEnabled)
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
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds(1f);
		damageTextObj.SetActive(false);
		G1_BeatenParticle.SetActive(false);
		AtkParticle.SetActive(false);
	}
}


