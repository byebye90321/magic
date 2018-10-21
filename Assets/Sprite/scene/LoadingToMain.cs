using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingToMain : MonoBehaviour {

	//--------------------------場景-----------------------------
	AsyncOperation ToMain;

	public Slider loadingBar;

	public void Start() {
		ToMain = SceneManager.LoadSceneAsync("Main");
		ToMain.allowSceneActivation = false;
	}

	public void Update() {
		loadingBar.value = ToMain.progress;
		StartCoroutine("wait");
	}

	IEnumerator wait() {
		yield return new WaitForSeconds(1f);
		ToMain.allowSceneActivation = true;
	}

}
