using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour {
	//開關
	public GameObject setMenu;
	public GameObject Menu_Button;
	public GameObject blackPanel;


	//---------------------設定
	public void SetOpen()
	{
		setMenu.SetActive(true);
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

    public void FB()
    {
        Application.OpenURL("https://www.facebook.com/Castspellfindyourself/");
    }

    public void IG()
    {
        Application.OpenURL("https://www.instagram.com/castspell_findyourself/");
    }

    public void WEB()
    {
        Application.OpenURL("https://castspell2018.wixsite.com/findyourself");
    }
}
