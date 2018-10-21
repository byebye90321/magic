using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settle : MonoBehaviour {

	public Text title;
	public Text content;
	//public Image character;
	
	void Start () {
		if (DialogsScript.sHE1 == true)
		{
			title.text = "光明道路上的正義之人";
			content.text = "正確的選擇與過人的勇氣將指引通往平等的道路，帶來更多繽紛世界應有的可能性。";
		}
		else if (DialogsScript.sBE1 == true)
		{
			title.text = "黑暗森林中的迷惘之徒";
			content.text = "偶爾，在黑霧籠罩的森林之中會看見一位步履蹣跚之人，據說那是失去色彩、迷失自我者唯一的歸處。";
		}
	}

	public void NextBtn() {
		SceneManager.LoadScene("tree");
	}

	public void Web() {
		Application.OpenURL("https://goo.gl/JCG7Nr");
	}
}
