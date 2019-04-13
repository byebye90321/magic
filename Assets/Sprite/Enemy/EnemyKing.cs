using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKing : MonoBehaviour {

    public DG_EnemyController enemyController;
    private int skill;

    public void kingSkill()
    {

        if (enemyController.curHealth < 150)
        {
            //80%
            if(Random.Range(1, 5) % 5 != 1)
            {
                enemyController.enemy1.state.SetAnimation(0, "hit02", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
                Debug.Log("2:補血");
            }
            else //20%
            {
                enemyController.enemy1.state.SetAnimation(0, "hit03", false);
                enemyController.enemy1.state.AddAnimation(0, "idle", true, 0f);
                Debug.Log("3:球");
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
                Debug.Log("1:範圍");
            }

        }

        
    }
}
