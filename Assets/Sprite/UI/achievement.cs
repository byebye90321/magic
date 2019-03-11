using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achievement : MonoBehaviour
{
    //成就類型
    public bool collider;

    public string achievementName;
    public Text achievementText;
    public GameObject achievementObj;
    public bool isAchievement;

    public string staticValueName;
    private int staticValueInt;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (collider)
        { 
            if (col.gameObject.name == "Player")
            {
                if (!isAchievement)
                {
                    if (gameObject.name == "water")
                    {
                        StaticObject.a05 = 1; //解鎖
                        PlayerPrefs.SetInt("StaticObject.a05", StaticObject.a05);
                        Debug.Log(StaticObject.a05);
                    }
                    StartCoroutine("Achievement");
                    isAchievement = true;
                }
            }
        }
    }

    public IEnumerator Achievement()
    {
        achievementObj.SetActive(true);
        achievementText.text = achievementName;
        /*staticValueInt = 1;
        PlayerPrefs.SetInt(staticValueName, staticValueInt);
        Debug.Log(StaticObject.a05);
        Debug.Log(staticValueInt);*/
        yield return new WaitForSeconds(2);
        achievementObj.SetActive(false);
    }
}
