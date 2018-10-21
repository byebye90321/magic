using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoviePlay2 : MonoBehaviour
{
	AsyncOperation ToDraw;

	//public int ClickToStart;
	void Start()
	{
		ToDraw = SceneManager.LoadSceneAsync("DrawGame_chapter0");
		Handheld.PlayFullScreenMovie("movie2.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
		//Application.LoadLevel("DrawGame_chapter0");
		ToDraw.allowSceneActivation = true;
	}


	void Update()
	{
		
	}

}