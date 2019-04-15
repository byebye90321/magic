using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToSisGame : MonoBehaviour {

	//--------------------------場景-----------------------------
	public string NextSceneName;
	AsyncOperation ToGame;

	public Slider loadingBar;

	public void Start() {
        //NextSceneName
        //ToGame = SceneManager.LoadSceneAsync("Sister_chapter1");

        if (StaticObject.nowClass == 0) //序章
            NextSceneName = "Sister_chapter1";
        else if (StaticObject.nowClass == 1) //森林
            NextSceneName = "RunGame_chapter1";
        else if (StaticObject.nowClass == 1.5f) //森林跑酷
            NextSceneName = "Sister_chapter2";
        else if (StaticObject.nowClass == 2) //城鎮
            NextSceneName = "Sister_chapter3";
        else if (StaticObject.nowClass == 3) //圖書館
            NextSceneName = "RunGame_chapter3";
        else if (StaticObject.nowClass == 3.5) //圖書館地下道
            NextSceneName = "Sister_chapter4";
        else if (StaticObject.nowClass == 4) //水晶室
            NextSceneName = "Main";

        ToGame = SceneManager.LoadSceneAsync(NextSceneName);
        ToGame.allowSceneActivation = false;
	}

	public void Update() {
		loadingBar.value = ToGame.progress;
		StartCoroutine("wait");
	}

	IEnumerator wait() {
		yield return new WaitForSeconds(1f);
		ToGame.allowSceneActivation = true;
	}

}
