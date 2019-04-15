using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ballPool : MonoBehaviour
{
    private Transform player;
    public static BulletsPool bulletPool;
    public GameObject bullet;
    public GameObject canvas;
    public int size;
    [HideInInspector]
    public int addDamageInt;
    public string symbol;

    [HideInInspector]
    public List<GameObject> bulletList;

    private Vector2 startPosition; //球初始位置
    private Vector2 dir; //玩家當前位置

    public bool atk;

    void Start()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject objBullet = (GameObject)Instantiate(bullet);
            objBullet.transform.SetParent(canvas.transform, false);
            startPosition = objBullet.transform.position;
            //objBullet.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
            objBullet.SetActive(false);
            bulletList.Add(objBullet);
        }
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void Update()
    {      
        if (atk == true)
        {
            //Vector2 dir = new Vector2(player.transform.position.x, player.transform.position.y);

            bulletList[0].transform.position = Vector2.Lerp(bulletList[0].transform.position, new Vector2(dir.x-2f, dir.y-2), Time.deltaTime * 1.2f);
            bulletList[1].transform.position = Vector2.Lerp(bulletList[1].transform.position, new Vector2(dir.x, dir.y), Time.deltaTime * 1.2f);
            bulletList[2].transform.position = Vector2.Lerp(bulletList[2].transform.position, new Vector2(dir.x-1, dir.y+1), Time.deltaTime * 1.2f);
        }
    }

    public void F1Attack()
    {     
        for (int i = 0; i < bulletList.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!bulletList[i].activeInHierarchy)
                {
                    bulletList[i].SetActive(true);
                    bulletList[i].transform.position = startPosition;
                    dir = new Vector2(player.transform.position.x, player.transform.position.y);
                    break;
                }
            }
        }
        StartCoroutine("F12");
    }

    IEnumerator F12()
    {
        yield return new WaitForSeconds(.5f);
        atk = true;
        yield return new WaitForSeconds(2);
        atk = false;
    }

}
