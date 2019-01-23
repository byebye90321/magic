using UnityEngine;
using System.Collections;

public class background2 : MonoBehaviour
{
    public Vector3 CreatPos;
    public float width;
    Renderer rend;
	//public background2 back2;

    void Start()
    {
        rend = GetComponent<Renderer>();
        width = rend.bounds.size.x;
        CreatPos = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
	}

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Instantiate(gameObject, CreatPos, new Quaternion(0, 0, 0, 0));
            CreatPos = new Vector3(CreatPos.x + width, CreatPos.y, CreatPos.z);
		}
    }
}

