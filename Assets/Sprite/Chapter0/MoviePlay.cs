using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MoviePlay : MonoBehaviour
{
	public VideoPlayer videoplayer;
	AsyncOperation ToBook;
	public string loadSceneAsyncName;

	void Start()
	{
		videoplayer.Play();
		videoplayer.loopPointReached += EndReached;
		ToBook = SceneManager.LoadSceneAsync(loadSceneAsyncName);
		ToBook.allowSceneActivation = false;
	}

	public void Skip()
	{
		ToBook.allowSceneActivation = true;
	}

	void EndReached(VideoPlayer videoplayer)
	{
		Debug.Log("End Reached");
		ToBook.allowSceneActivation = true;
	}

	/*void Update() {
		Debug.Log(videoplayer.frame);
	}*/

}