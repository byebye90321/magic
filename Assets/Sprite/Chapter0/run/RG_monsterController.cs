using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RG_monsterController : MonoBehaviour {
	public string ChapterName;
	public static RG_monsterController Moster;
	public RG_playerController playerController;
	Rigidbody2D rigi;
	public float speed;
	public GameObject player;
	public bool isStop = false;

	void Start()
	{
		Moster = this;
		rigi = GetComponent<Rigidbody2D>();
		rigi.AddForce(new Vector2(0, 0));
		rigi.velocity = new Vector2(0, 0f);
	}

	void Update()
	{
		if (RunGameManager.gameState == GameState.Running)
		{
			
			Moster.transform.position = new Vector3(Moster.transform.position.x + speed, Moster.transform.position.y, 10);

			if (ChapterName == "0")
			{
				if (playerController.Up == true || playerController.Down)
				{
					speed = 0.01f;
				}
				else if (!isStop)
				{
					if (Mathf.Abs(player.transform.position.x - Moster.transform.position.x) > 15f)
					{
						speed = Random.Range(0.14f, 0.16f);
					}
					else if (Mathf.Abs(player.transform.position.x - Moster.transform.position.x) < 15f && Mathf.Abs(player.transform.position.x - Moster.transform.position.x) > 4f)
					{
						speed = Random.Range(0.12f, 0.13f);
					}
					else if (Mathf.Abs(player.transform.position.x - Moster.transform.position.x) < 4f)
					{
						speed = 0.11f;
					}
				}
				else
				{
					speed = 0.06f;
				}
			}
			else
			{
				if (!isStop)
				{
					//speed = 0.13f;
				}
				else
				{
					speed = 0.06f;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "MosterStop")
		{
			isStop = true;
		}

		/*if (col.gameObject.name == "Player")
		{
			Debug.Log("dead");
			RunGameManager.Instance.Dead();
		}*/
	}

}
