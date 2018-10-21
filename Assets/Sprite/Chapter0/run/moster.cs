using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moster : MonoBehaviour {
	public static moster Moster;


	Rigidbody2D rigi;
	public float speed;
	public GameObject warmingImage;
	public GameObject warmingText;

	//--------------音效
	public AudioSource audio;
	public AudioClip warmingSound;

	void Start()
	{
		Moster = this;
		
		rigi = GetComponent<Rigidbody2D>();
		//audio = GetComponent<AudioSource>();
		rigi.AddForce(new Vector2(0, 0));
		rigi.velocity = new Vector2(0, 0f);

	}

	// Update is called once per frame
	void Update()
	{
		if (RunGameManager.gameState == GameState.Running)
		{
			Moster.transform.position = new Vector3(Moster.transform.position.x + speed, Moster.transform.position.y, 10);
		}


	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "obstacle")
		{
			Destroy(col.gameObject);
		}

		if (col.gameObject.tag == "MosterSpeedAdd")
		{
			StartCoroutine("DoSomeThingInFaster");
			
		}

		if (col.tag == "Player")
		{
			RunGameManager.Instance.Dead();
			Debug.Log("dead");
		}
	}

	IEnumerator DoSomeThingInFaster()
	{
		warmingText.SetActive(true);
		for (int i = 0; i < 4; i++) {
			warmingImage.SetActive(true);
			audio.PlayOneShot(warmingSound);
			yield return new WaitForSeconds(0.2f);
			warmingImage.SetActive(false);
			yield return new WaitForSeconds(0.2f);
		}
		
		warmingText.SetActive(false);
		if (RunGameManager.gameState == GameState.Running)
		{	
			speed = 0.35f;
			yield return new WaitForSeconds(2.5f);
			speed = 0.1f;

		}
		
	}

}
