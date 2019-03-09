using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour {

	public string name;
	AsyncOperation ToGame;

	public void Start()
	{
		if (name == "main")
		{
			ToGame = SceneManager.LoadSceneAsync("Chapter0_1movie");
			ToGame.allowSceneActivation = false;
		}
		
	}

	public void ToContinueGame()
	{
		SceneManager.LoadScene("ChooseChapter");
	}

	public void ToNewGame()
	{
		ToGame.allowSceneActivation = true;
		//Application.LoadLevel("Chapter0_1movie");
	}

	public void ToCollect()
	{
		SceneManager.LoadScene("Collect");
	}

	public void ToMenu()
	{
		SceneManager.LoadScene("Main");
	}

	public void LoadingToMain()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("LoadingToMain");
	}

	public void ToExit()
	{
		Application.Quit();
	}

	public void sisterGame()
	{
        StaticObject.nowClass = 0f;
        PlayerPrefs.SetFloat("StaticObject.nowClass", StaticObject.nowClass);
        SceneManager.LoadScene("Loading");
	}

	public void sisterRun1()
	{
		SceneManager.LoadScene("RunGame_chapter1");
	}

	public void sisterGame2()
	{
        StaticObject.nowClass = 1.5f;
        PlayerPrefs.SetFloat("StaticObject.nowClass", StaticObject.nowClass);
        SceneManager.LoadScene("Loading");
        //SceneManager.LoadScene("Sister_chapter2");
	}


	//魔法日報問題
	/*public void paper1() {
		Application.OpenURL("https://goo.gl/UWCWqi");
	}

	public void paper2() {
		Application.OpenURL("https://goo.gl/9Sr7Jw");

	}

	public void paper3() {
		Application.OpenURL("https://goo.gl/9fye4W");
	}*/
}
