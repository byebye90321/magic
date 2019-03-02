using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterChoose : MonoBehaviour {

	public GameObject chapter1;

	public void chapter1Open()
	{
		chapter1.SetActive(true);
	}

	public void close()
	{
		chapter1.SetActive(false);
	}


}
