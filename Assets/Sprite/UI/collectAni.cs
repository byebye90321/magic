using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class collectAni : MonoBehaviour {

    //--------------分類Toggle------------
    public Toggle characterToggle;
    public GameObject characterpanel;
    private Animator characterAnimator;
    private Animation characterAnimation;
    bool character = false;

    public Toggle treeToggle;
	public GameObject treepanel;
	private Animator treeAnimator;
	private Animation treeAnimation;
	bool tree = false;

    public Toggle achievementToggle;
    public GameObject achievementpanel;
    private Animator achievementAnimator;
    private Animation achievementAnimation;
    bool achievement = true;

    public GameObject black_bg;
	float r = 0.3019608f, g = 0.2745098f, b = 0.4470588f, a = 1f;

	//解鎖變數
	//------樹狀圖------
	private int s1;
	private int sHE1;
	private int sBE1;

	void Start() {
        achievementAnimator = achievementpanel.GetComponent<Animator>();
        achievementAnimation = achievementpanel.GetComponent<Animation>();
        achievementAnimation.Play();
		treeAnimator = treepanel.GetComponent<Animator>();
		treeAnimation = treepanel.GetComponent<Animation>();
		treeAnimation.Play();
		characterAnimator = characterpanel.GetComponent<Animator>();
		characterAnimation = characterpanel.GetComponent<Animation>();
		characterAnimation.Play();
	}

	public void Update() {
        if (characterToggle.GetComponent<Toggle>().isOn == true)
        {
            characterAnimator.SetBool("character", true);
        }
        else
        {
            tree = false;
            characterAnimator.SetBool("character", false);
        }

        if (treeToggle.GetComponent<Toggle>().isOn == true)
		{
			treeAnimator.SetBool("tree", true);
		}
		else
		{
			tree = false;
			treeAnimator.SetBool("tree", false);
		}

        if (achievementToggle.GetComponent<Toggle>().isOn == true)
        {
            achievementAnimator.SetBool("badge", true);
        }
        else
        {
            achievement = false;
            achievementAnimator.SetBool("badge", false);
        }
    }
	
}
