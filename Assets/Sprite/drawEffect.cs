using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawEffect : MonoBehaviour {
	public GameObject partical;
	private float Distance = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetMouseButtonDown(0))
		{
			partical.SetActive(true);
			//Debug.Log(transform.position);
		}*/
		if (Input.GetMouseButton(0))
		{
			partical.SetActive(true);
			Ray effect_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 pos = effect_ray.GetPoint(Distance);
			
			transform.position = pos;
		}
		if (Input.GetMouseButtonUp(0))
		{
			StartCoroutine("waitSecond");
		}
		
	}
	IEnumerator waitSecond()
	{
		yield return new WaitForSeconds(0.5f);
		partical.SetActive(false);
	}
}
