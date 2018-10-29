using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class DG_EnemyController : MonoBehaviour{

	public ExampleGestureHandler gesture;

	//--------------------Health-------------------
	public int curHealth = 100;
	public int maxHealth = 100;
	public GameObject healthSlider;
	public Slider Health;
	bool isDead;
	bool damaged;

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

	void Start()
	{
		col = GetComponent<BoxCollider2D>();
	}
	
	void Update()
	{

		//-----------------CUT------------------------
		if (Input.GetMouseButton(0))
		{
			if (col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))                //鼠标在当前水果2Dcollider内
			{	
				//StartCoroutine("testWait");
				/*CreateHalf(obj1, 0);
				CreateHalf(obj2, 1);
				Createwz();*/
				//Destroy(this.gameObject);
			}
		}

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
	public void Skill1()
	{
		TakeDamage(gesture.skillAtk1);
		enemy1.state.SetAnimation(0, "death", false);
		enemy1.state.AddAnimation(0, "idle", true, 0f);
		enemy2.state.SetAnimation(0, "death", false);
		enemy2.state.AddAnimation(0, "idle", true, 0f);
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
		enemy1.state.SetAnimation(0, "death", false);
		enemy2.state.SetAnimation(0, "death", false);
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
		if (col.gameObject.name == "Blade")
		{
			TakeDamage(1);
		}
	}
}


