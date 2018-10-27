using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
	public GameObject bladeTrailPrefab;
	public float minCuttingVelocity = .001f;

	bool isCutting = false;

	Vector2 previousPosition;

	GameObject currentBladeTrail;

	Rigidbody2D rb;
	Camera cam;
	//CircleCollider2D circleCollider;

	public LayerMask layerMask;

	void Start()
	{
		cam = Camera.main;
		rb = GetComponent<Rigidbody2D>();
		//circleCollider = GetComponent<CircleCollider2D>();
	}

	void Update()
	{
		/*if (Input.GetMouseButtonDown(0))
		{
			StartCutting();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			StopCutting();
		}*/

		if (isCutting)
		{
			UpdateCut();
		}

	}

	void UpdateCut()
	{
		Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		rb.position = newPosition;

		float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
		if (velocity > minCuttingVelocity)
		{
			//circleCollider.enabled = true;
		}
		else
		{
			//circleCollider.enabled = false;
		}

		previousPosition = newPosition;
	}

	public void StartCutting()
	{
		/*isCutting = true;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
		previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
		circleCollider.enabled = false;*/

		isCutting = true;
		rb.position = cam.ScreenToWorldPoint(Input.mousePosition);
		transform.position = rb.position;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
	}

	public void StopCutting()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent(null);
		Destroy(currentBladeTrail, 2f);
		//circleCollider.enabled = false;
	}

}
