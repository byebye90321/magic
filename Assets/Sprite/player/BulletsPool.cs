using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BulletsPool : MonoBehaviour
{
    private GameObject player;
    public static BulletsPool bulletPool;
    public GameObject bullet;
    public GameObject canvas;
    public int size;
    [HideInInspector]
    public int addDamageInt;
    public string symbol;

    public bool miss = false;

    [HideInInspector]
    public List<GameObject> bulletList;

    void Start()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject objBullet = (GameObject)Instantiate(bullet);
            objBullet.transform.SetParent(canvas.transform, false);
            objBullet.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            objBullet.SetActive(false);
            bulletList.Add(objBullet);
        }
        player = GameObject.Find("Player");
    }

    public void Fire()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].SetActive(true);
                Text healthText = bulletList[i].GetComponentInChildren<Text>();
                if (miss==true)
                {
                    healthText.text = "MISS";
                }
                else
                {
                    healthText.text = symbol + addDamageInt;
                }
                break;
            }
        }
    }

    public void F1Attack()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].activeInHierarchy)
                {
                    bulletList[i].SetActive(true);
                    break;
                }

                bulletList[i].transform.position = Vector3.Lerp(player.transform.position, player.transform.position, Time.time);
            }
        }

        
        StartCoroutine("F12");
    }

    IEnumerator F12()
    {
        
        yield return null;
    }

}
