using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

    public static background Background;

    
    Vector2 offset;
    public float speed;
    public float y;
    float nowPos;
    float StartSpeed;

    public float vecitySpeed;
    public float vecity=0.001f;
    public float maxVecitySpeed;
    public float stop;
    public float hurt;  
   
    

   Renderer rend;

    bool pressdown = false;

    // Use this for initialization
    void Start () {

	    rend= GetComponent<Renderer>();
        
        Background = this;
        StartSpeed = speed;
        
        offset = new Vector2(0,0);
        rend.material.SetTextureOffset("_MainTex",offset);
    }
	
	// Update is called once per frame
	void Update () {

        if (RunGameManager.gameState == GameState.Win || RunGameManager.gameState == GameState.Dead || RunGameManager.gameState == GameState.Start)
        {
            speed = 0;
        }
        else
        {
            if (pressdown)
            {
                if (vecitySpeed < maxVecitySpeed)
                {
                    vecitySpeed += vecity;
                }
                else if (vecitySpeed >= maxVecitySpeed)
                {
                    vecitySpeed = maxVecitySpeed;
                }

                nowPos += vecitySpeed * Time.deltaTime;
                offset = new Vector2(nowPos, 0f);
                rend.material.SetTextureOffset("_MainTex", offset);
            }
            else if (vecitySpeed >= 0)
            {

                vecitySpeed -= stop;


                if (vecitySpeed <= 0)
                {
                    vecitySpeed = 0;

                }

                nowPos += vecitySpeed * Time.deltaTime;
                offset = new Vector2(nowPos, 0f);
                rend.material.SetTextureOffset("_MainTex", offset);
                
            }
                speed = StartSpeed;
                nowPos += speed * Time.deltaTime;
                offset = new Vector2(nowPos, 0f);
                rend.material.SetTextureOffset("_MainTex", offset);
            
        }
        

    }

}
