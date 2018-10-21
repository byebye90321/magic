using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow1 : MonoBehaviour {

	public GameObject playerobj;

	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	public float offset;

	void Start(){

	}

	void Update(){
		transform.position = playerobj.transform.position;
		transform.position = new Vector3(Mathf.Clamp(playerobj.transform.position.x + offset, xMin,xMax),Mathf.Clamp(playerobj.transform.position.y,yMin,yMax));
	}
}