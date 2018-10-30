using UnityEngine;
using System.Collections;
using Spine.Unity;
using Spine;

public class cut : MonoBehaviour
{
    public GameObject obj1, obj2;    //分开后的两边水果
    public GameObject[] wz;          //几种污渍背景

    private BoxCollider2D col;
    private Vector2[] vec = { Vector2.left, Vector2.right };   //切后的半截往两个方向飞出
															   //private GameObject scores;     //放置scoreManager.cs和healthManager.cs脚本的游戏对象
	public SkeletonAnimation enemy;
	public int deathCount;

	void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

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
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))                //鼠标在当前水果2Dcollider内
            {
                CreateHalf(obj1, 0);
                CreateHalf(obj2, 1);
                Createwz();
				//Destroy(this.gameObject);
				StartCoroutine("Death");
				
			}
        }

    }

	IEnumerator Death() {
		enemy.state.SetAnimation(0, "death", false);

		yield return new WaitForSeconds(0.7f);
		deathCount += 1;
		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		/*if (col.gameObject.name == "Player")
		{
			//CreateHalf(obj1, 0);
			//CreateHalf(obj2, 1);
			//Createwz();
			//Destroy(this.gameObject);
			StartCoroutine("Death");
		}*/
	}
}
