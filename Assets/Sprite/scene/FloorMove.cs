using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour {

	public bool X;
	public bool Y;

	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	public float speed;

	void Start () {
		
	}
	
	void Update () {

		if (X)
		{
			transform.localPosition = new Vector3(Mathf.PingPong(speed, xMax), transform.position.y, transform.position.z);
			speed += 0.02f;
		}

		if (Y)
		{
			transform.localPosition = new Vector3(transform.position.x, Mathf.PingPong(speed, yMax), transform.position.z);
			speed += 0.02f;
		}
	}
}
