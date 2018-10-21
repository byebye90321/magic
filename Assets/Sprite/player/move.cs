using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Spine.Unity;
using Spine;

public class move : MonoBehaviour
{
	public DialogsScript dialogsScript;
    public float speed;
    public GameObject ob;
    public Vector2 target;
    public Rigidbody2D rigid2D;
	public Transform flowerMonster;
	public Transform graphics;

	public SkeletonAnimation skeletonAnimation;
	public SkeletonAnimation BobbyAnimation;


	public void Start() {

		if (DialogsScript.GameEnd == false)
		{
			target.x = rigid2D.position.x;
		}
		else {
			target = new Vector3(28, -3, 10);
		}
	}

	void Update() {
		if (dialogsScript.follow == true)
		{
			flowerMonster.transform.position = new Vector3(rigid2D.position.x, rigid2D.position.y-0.2f, 10);
			//BobbyAnimation.state.SetAnimation(0, "idle__Multicolor", true);
		}
	}

	void FixedUpdate()
	{	
			walk();		
    }

    public void OnTouch(BaseEventData bData)
    {
        target = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, ob.transform.position.y);		
	}

    public void walk()
    {

        var dir = target - rigid2D.position;
		if (dir.x > 0.1f)
		{
			GetComponent<AudioSource>().UnPause();
			skeletonAnimation.state.SetAnimation(0, "idle", true);
			graphics.localRotation = Quaternion.Euler(0, 0, 0);
			rigid2D.MovePosition(rigid2D.position + Vector2.right * speed * Time.deltaTime); //鋼體用MovePosition來移動 不要用transform
			if (dialogsScript.follow == true) {
				flowerMonster.localRotation = Quaternion.Euler(0, 0, 0);
				rigid2D.MovePosition(rigid2D.position + Vector2.right * speed * Time.deltaTime);
				BobbyAnimation.AnimationName = "walk__Multicolor";
			}
		}
		else if (dir.x < -0.1f)
		{
			GetComponent<AudioSource>().UnPause();
			skeletonAnimation.state.SetAnimation(0, "idle", true);
			graphics.localRotation = Quaternion.Euler(0, 180, 0);
			rigid2D.MovePosition(rigid2D.position + Vector2.left * speed * Time.deltaTime);
			if (dialogsScript.follow == true)
			{
				flowerMonster.localRotation = Quaternion.Euler(0, 180, 0);
				BobbyAnimation.AnimationName = "walk__Multicolor";
			}
		}
		else {
			GetComponent<AudioSource>().Pause();
			skeletonAnimation.state.SetAnimation(1, "walk", true);

			if (dialogsScript.follow == true)
			{
				BobbyAnimation.AnimationName = "idle__Multicolor";
			}
		}
    }

}



