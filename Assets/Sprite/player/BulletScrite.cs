using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScrite : MonoBehaviour {

    public float time;
    public bool atk=false;
    private void OnEnable()
    {
        //transform.GetComponent<Ri>
        Invoke("hideBullet", time);
    }

    void hideBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (atk)
        {
            if (col.gameObject.name == "Player")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
