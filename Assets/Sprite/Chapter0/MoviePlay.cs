using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoviePlay : MonoBehaviour
{
	AsyncOperation ToBook;
	//AsyncOperation ToCharacter;

	void Start()
	{
		//ToCharacter = SceneManager.LoadSceneAsync("Character");
		ToBook = SceneManager.LoadSceneAsync("Chapter0_2book");
		
		//ToCharacter.allowSceneActivation = false;
		ToBook.allowSceneActivation = false;	
		
	}

	public void Skip() {
		//ToCharacter.allowSceneActivation = true;	
		Application.LoadLevel("Character");
	}

	public void Movie() {
		Handheld.PlayFullScreenMovie("movie1.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
		//Application.LoadLevel("Chapter0_2book");
		ToBook.allowSceneActivation = true;
	}

}