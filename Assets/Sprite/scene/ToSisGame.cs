using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToSisGame : MonoBehaviour {

	//--------------------------場景-----------------------------
	AsyncOperation ToGame;

	public Slider loadingBar;

	public void Start() {
		ToGame = SceneManager.LoadSceneAsync("Sister_chapter1");
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
