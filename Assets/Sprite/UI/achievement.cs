﻿using System.Collections;
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
                    if (gameObject.name == "beatuyZoomCollider")
                    {
                        StaticObject.a10 = 1; //解鎖
                        PlayerPrefs.SetInt("StaticObject.a10", StaticObject.a10);
                        Debug.Log(StaticObject.a10);
                    }
                    if (gameObject.name == "dragonCollider")
                    {
                        StaticObject.a14 = 1; //解鎖
                        PlayerPrefs.SetInt("StaticObject.a14", StaticObject.a14);
                        Debug.Log(StaticObject.a14);
                    }
                    if (gameObject.name == "cocoCollider")
                    {
                        StaticObject.a11 = 1; //解鎖
                        PlayerPrefs.SetInt("StaticObject.a11", StaticObject.a11);
                        Debug.Log(StaticObject.a11);
                    }
                    if (gameObject.name == "skill6")
                    {
                        StaticObject.a18 = 1; //解鎖
                        PlayerPrefs.SetInt("StaticObject.a18", StaticObject.a18);
                        Debug.Log(StaticObject.a18);
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
        yield return new WaitForSeconds(2);
        achievementObj.SetActive(false);
    }
}
