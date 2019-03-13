using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartIns : MonoBehaviour {

    public GameObject heart;

    public void Start()
    {
        InvokeRepeating("Heart",1f,20f);
        GameObject NEWatkpreft = Instantiate(heart) as GameObject;
        NEWatkpreft.transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
    }
    
}
