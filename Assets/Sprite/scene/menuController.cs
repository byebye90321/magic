using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour {
	//開關
	public GameObject setMenu;
	public GameObject Menu_Button;
	public GameObject blackPanel;

	void Start () {

		//setMenu.SetActive(false);

	}
	
	void Update () {
		
	}
	//---------------------設定
	public void SetOpen()
	{
		setMenu.SetActive(true);
		Menu_Button.SetActive(false);
		blackPanel.SetActive(true);
	}

	public void SetClose()
	{
		setMenu.SetActive(false);
		Menu_Button.SetActive(true);
		blackPanel.SetActive(false);
	}

	public void Pause() {
		Menu_Button.SetActive(true);
		blackPanel.SetActive(true);
	}

	public void GameContinue(){
		Menu_Button.SetActive(false);
		blackPanel.SetActive(false);
	}

}
