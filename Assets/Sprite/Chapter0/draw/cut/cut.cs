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

	void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

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
