using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour {

	public void ToCollect()
	{
		Application.LoadLevel("Collect");
	}

	public void ToMenu()
	{
		Application.LoadLevel("Main");
	}

	public void LoadingToMain() {
		Time.timeScale = 1f;
		Application.LoadLevel("LoadingToMain");
	}

	public void ToExit()
	{
		Application.Quit();
	}

	public void startGame() {
		Application.LoadLevel("Chapter0_1movie");
	}

	public void sisterGame()
	{
		Application.LoadLevel("Loading");
	}

	//魔法日報問題
	public void paper1() {
		Application.OpenURL("https://goo.gl/UWCWqi");
	}

	public void paper2() {
		Application.OpenURL("https://goo.gl/9Sr7Jw");

	}

	public void paper3() {
		Application.OpenURL("https://goo.gl/9fye4W");
	}
}
