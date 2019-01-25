using UnityEngine;
using System.Collections;

public class background2 : MonoBehaviour
{
    public Vector3 CreatPos;
    public float width;
    Renderer rend;
	public int value;

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
			//CreatPos = new Vector3(Mathf.Floor((CreatPos.x + width*value)*100)/100, CreatPos.y, CreatPos.z);
			CreatPos = new Vector3(CreatPos.x + width * value, CreatPos.y, CreatPos.z);
			Instantiate(gameObject, CreatPos, new Quaternion(0, 0, 0, 0));
		}
    }
}

