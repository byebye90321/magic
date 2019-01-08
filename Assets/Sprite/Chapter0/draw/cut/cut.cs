using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;

public class cut : MonoBehaviour
{
	public DG_playerController playerController;
    private BoxCollider2D col;

	public SkeletonAnimation enemy;
	public static int DeathCount;
	public GameObject DeathEffect;
	public bool isDead;

	/*public bool cutObj;
	public GameObject obj1, obj2, obj3;    //分开后的两边水果
	private Vector2[] vec = { Vector2.left, Vector2.right };   //切后的半截往两个方向飞出*/

	void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

	/*private void CreateHalf(GameObject obj, int index)       //创建半个水果
	{
		obj = Instantiate(obj, transform.position, Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.back)) as GameObject;
		Rigidbody2D rgd = obj.GetComponent<Rigidbody2D>();
		float v = Random.Range(2f, 4f);                        //随机飞出速度
		rgd.velocity = vec[index] * v;
		Destroy(obj, 2f);
	}*/

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) &&playerController.cutting)                //鼠标在当前水果2Dcollider内
            {
				StartCoroutine("Death");
			}
        }
	}

	IEnumerator Death() {
		isDead = true;
		enemy.state.SetAnimation(0, "death", false);
		DeathEffect.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		DeathCount += 1;
		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "G0_Particle") //被G0攻擊
		{
			StartCoroutine("SkillDeath");
		}

		if (col.gameObject.name == "G1_Particle") //被G1攻擊
		{
			StartCoroutine("SkillDeath");
		}

		if (col.gameObject.name == "B1_Particle") //被B1攻擊
		{
			StartCoroutine("SkillDeath");
		}

		if (col.gameObject.name == "G2_Particle") //被G2攻擊
		{
			StartCoroutine("SkillDeath");
		}
		if (col.gameObject.name == "B2_Particle") //被B2攻擊
		{
			StartCoroutine("SkillDeath");
		}
	}

	IEnumerator SkillDeath()
	{
		isDead = true;
		yield return new WaitForSeconds(0.5f);
		enemy.state.SetAnimation(0, "death", false);
		DeathEffect.SetActive(true);
		yield return new WaitForSeconds(0.7f);
		DeathCount += 1;
		Destroy(this.gameObject);
	}
}
