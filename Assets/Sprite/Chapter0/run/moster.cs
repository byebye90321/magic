using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moster : MonoBehaviour {
	public static moster Moster;

	Rigidbody2D rigi;
	public float speed;
	public GameObject player;
	public GameObject warningImage;
	public GameObject warningText;
	public bool isStop = false;
	//--------------音效
	public AudioSource audio;
	public AudioClip warmingSound;

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

			if (!isStop)
			{
				if (Mathf.Abs(player.transform.position.x - Moster.transform.position.x) > 15f)
				{
					speed = Random.Range(0.18f, 0.19f);
					//speed = 0.18f;
				}
				else if (Mathf.Abs(player.transform.position.x - Moster.transform.position.x) < 15f && Mathf.Abs(player.transform.position.x - Moster.transform.position.x) > 5f)
				{
					//speed = 0.155f;
					speed = Random.Range(0.14f, 0.17f);
				}
				else if (Mathf.Abs(player.transform.position.x - Moster.transform.position.x) < 5f)
				{
					speed = 0.15f;
					//speed = Random.Range(0.15f, 0.155f);
				}
			}
			else {
				speed = 0.06f;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		/*if (col.tag == "obstacle")
		{
			Destroy(col.gameObject);
		}*/

		if (col.gameObject.tag == "MosterStop")
		{
			isStop = true;
			//StartCoroutine("DoSomeThingInFaster");
		}

		if (col.tag == "Player")
		{
			RunGameManager.Instance.Dead();
			Debug.Log("dead");
		}

	}

	IEnumerator DoSomeThingInFaster()
	{
		warningText.SetActive(true);
		for (int i = 0; i < 4; i++) {
			warningImage.SetActive(true);
			audio.PlayOneShot(warmingSound);
			yield return new WaitForSeconds(0.2f);
			warningImage.SetActive(false);
			yield return new WaitForSeconds(0.2f);
		}
		
		warningText.SetActive(false);
		if (RunGameManager.gameState == GameState.Running)
		{	
			speed = 0.35f;
			yield return new WaitForSeconds(2.5f);
			speed = 0.1f;
		}
		
	}

}
