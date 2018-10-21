using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystemMonster : MonoBehaviour {
    private int maxHeartAmount=3;
    public int startHearts = 3;
    public int curHealth;
    private int maxHealth;
	private int healthPerHeart = 1;

    public Image[] healthImages;
    public Sprite[] healthSprites;

	bool isDead;
	bool damaged;

	// Use this for initialization
	void Start ()
    {
        curHealth = startHearts * healthPerHeart;  //3*2
        maxHealth = maxHeartAmount * healthPerHeart;  //3*2
        checkHealthAmount();  //載入時fill

    }

     void checkHealthAmount()
    {
        for (int i = 0; i < maxHeartAmount; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearts();
    }


    public void UpdateHearts()
    {
        bool empty = false;
        int i = 0;
        foreach (Image image in healthImages)
        {
            if (empty)   //死
            {
                image.sprite = healthSprites[0];
            }
            else  //傷害
            {
                i++;
                if (curHealth >= i * healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - curHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;

                }
            }
        }
        
    }


    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        curHealth = Mathf.Clamp(curHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();


		damaged = true;

		if(curHealth <= 0 && !isDead)
		{
			Death ();
		}
    }


	void Death ()
	{
		isDead = true;
	}


/*    public void AddHeartContainer()
    {
        startHearts++;
        startHearts = Mathf.Clamp(startHearts, 0, maxHeartAmount);

        //curHealth = startHearts * healthPerHeart;
        //maxHealth = maxHeartAmount * healthPerHeart;

        checkHealthAmount();
    }
*/

}
