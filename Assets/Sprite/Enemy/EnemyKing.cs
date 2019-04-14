using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKing : MonoBehaviour {

    public DG_EnemyController enemyController;
    private int skill;
    public GameObject F1; //球
    public GameObject F2; //範圍
    public GameObject addHealthParticle;
    public GameObject addHealthText; //補血字


    public void kingSkill()
    {

        if (enemyController.curHealth < 150)
        {
            //80%
            if(Random.Range(1, 5) % 5 != 1)
            {
                enemyController.enemy1.state.SetAnimation(0, "hit02", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
                //addHeart.SetActive(true);
                enemyController.curHealth += 100;
                enemyController.Health.value = enemyController.curHealth;
                addHealthParticle.SetActive(true);
                GameObject NEWatkpreft = Instantiate(addHealthText) as GameObject;
                NEWatkpreft.transform.SetParent(enemyController.canvas.transform, false);
                NEWatkpreft.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
                enemyController.healthText = NEWatkpreft.GetComponentInChildren<Text>();
                enemyController.healthText.text = "+" + 100;
                StartCoroutine("CloseAddHeart");
                Debug.Log("2:補血");


            }
            else //20%
            {
                enemyController.enemy1.state.SetAnimation(0, "hit03", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);

                Debug.Log("3:球"); //F1
            }
        }
        else
        {
            //80%
            if (Random.Range(1, 5) % 5 != 1)
            {
                enemyController.enemy1.state.SetAnimation(0, "hit03", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
                Debug.Log("3:球");
            }
            else //20%
            {
                enemyController.enemy1.state.SetAnimation(0, "hit01", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
                F2.SetActive(true);
                StartCoroutine("CloseF2");
                Debug.Log("1:範圍"); //F2
            }

        }
    }

    IEnumerator CloseAddHeart()
    {
        yield return new WaitForSeconds(2);
        addHealthParticle.SetActive(false);
    }

    IEnumerator CloseF1()
    {
        yield return new WaitForSeconds(2);
        F1.SetActive(false);
    }

    IEnumerator CloseF2()
    {
        yield return new WaitForSeconds(2);
        F2.SetActive(false);
    }
}
