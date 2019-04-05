using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour {

    bool moveRight = true;

	[SerializeField]
	private float xMax;
	[SerializeField]
	private float xMin;
	public float speed;

	
	void Update () {

        //movingPlatform.transform.position = new Vector3(Mathf.PingPong(speed, xMax), movingPlatform.transform.position.y, movingPlatform.transform.position.z);
        //speed += 0.01f;
        if (transform.position.x > xMax)
            moveRight = false;
        else if(transform.position.x < xMin)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        
	}
}
