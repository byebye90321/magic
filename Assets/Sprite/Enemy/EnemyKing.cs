using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKing : MonoBehaviour {

    public DG_EnemyController enemyController;
    public GameObject playerObj;
    private int skill;
    //public GameObject F1; //球
    public GameObject F2; //範圍
    public GameObject addHealthParticle;
    public GameObject addHealthText; //補血字

    public ballPool F1ball;

    public void kingSkill()
    {
        if (enemyController.curHealth < 150)
        {
            //80%
            if(Random.Range(1, 5) % 5 != 1)
            {
                enemyController.enemy1.state.SetAnimation(0, "hit02", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
                enemyController.curHealth += 100;
                enemyController.addBulletsPool.addDamageInt = 100;
                addHealthParticle.SetActive(true);
                enemyController.addBulletsPool.Fire();
                StartCoroutine("CloseAddHeart");
                Debug.Log("2:補血");
            }
            else //20%
            {
                StartCoroutine("F1Ball");
                Debug.Log("3:球"); //F1
            }
        }
        else
        {
            Vector2 enemy = this.transform.position;
            Vector2 player = playerObj.transform.position;

            if (Mathf.Abs(enemy.x - player.x) > 7)
            {
                Debug.Log(">7");
                 StartCoroutine("F1Ball");
                 Debug.Log("3:球");
            }
            else
            {
                //80%
                if (Random.Range(1, 5) % 5 != 1)
                {
                    StartCoroutine("F1Ball");
                    Debug.Log("3:球");
                }
                else //20%
                {
                    enemyController.enemy1.state.SetAnimation(0, "hit01", false);
                    enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);

                    StartCoroutine("CloseF2");
                    Debug.Log("1:範圍"); //F2
                }
            }

        }
    }

    IEnumerator F1Ball()
    {
        F1ball.F1Attack();
        enemyController.enemy1.state.SetAnimation(0, "hit03", false);
        enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
        yield return new WaitForSeconds(2);
    }

    IEnumerator CloseAddHeart()
    {
        yield return new WaitForSeconds(2);
        addHealthParticle.SetActive(false);
    }

    IEnumerator CloseF2()
    {
        yield return new WaitForSeconds(1f);
        F2.SetActive(true);
        yield return new WaitForSeconds(2);
        F2.SetActive(false);
    }
}
