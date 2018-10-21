using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class distance : MonoBehaviour {

    public Vector3 EndPoint;
    public Vector3 StartPoint;
    public GameObject player;

    public GameObject Tr;
    public float TrStart;
    public float TrEnd;
    

    Image DistanceBar;
    public float TotalLenght;
    public float NowDistance;
	// Use this for initialization
	void Start () {

        DistanceBar= GetComponent<Image>();
        StartPoint =player.transform.position;
        TotalLenght = EndPoint.x - StartPoint.x;

    }
	
	// Update is called once per frame
	void Update () {

        NowDistance = player.transform.position.x - StartPoint.x;


        DistanceBar.fillAmount = NowDistance / TotalLenght;


        Tr.transform.localPosition = new Vector3(TrStart + (NowDistance / TotalLenght) * (TrEnd - TrStart),-5f, 0);


	}
}
