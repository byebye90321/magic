using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoviePlay3 : MonoBehaviour
{
	AsyncOperation ToCharacter;

	//public int ClickToStart;
	void Start()
	{
		ToCharacter = SceneManager.LoadSceneAsync("Character");
		Handheld.PlayFullScreenMovie("movie3.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
		//Application.LoadLevel("Character");
		ToCharacter.allowSceneActivation = true;

	}


	void Update()
	{

	}

}