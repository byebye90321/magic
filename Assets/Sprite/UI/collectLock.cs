using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class collectLock : MonoBehaviour {

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

	//---------------------人物介紹----------------------
	public Button char1;
	public Button char2;
	public Button char3;
	public Button char4;
	public Button char5;
	public Button char6;
	public Button char7;
	public Button char8;
	public Button char9;
	public Button char10;
	public Button char11;
	public Button char12;
	public Button char13;
	public Button char14;

	public GameObject clock1;
	public GameObject clock2;
	public GameObject clock3;
	public GameObject clock4;
	public GameObject clock5;
	public GameObject clock6;
	public GameObject clock7;
	public GameObject clock8;
	public GameObject clock9;
	public GameObject clock10;
	public GameObject clock11;
	public GameObject clock12;
	public GameObject clock13;
	public GameObject clock14;

	//解鎖變數
	//------樹狀圖------
	private int s1;
	private int sHE1;
	private int sBE1;

	//-----人物介紹-----
	private int sister;
	private int bother;
	private int utopia;
	private int hikari;
	private int book;

	private int EnemyKing;
	private int J;
	private int Q;
	private int K;

	private int bobby;
	private int wiki;
	private int wiko;
	private int boMom;
	private int boDad;

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

		//----------------------解鎖人物介紹------------------------
		sister = PlayerPrefs.GetInt("StaticObject.sister");
		bother = PlayerPrefs.GetInt("StaticObject.bother");
		utopia = PlayerPrefs.GetInt("StaticObject.utopia");
		hikari = PlayerPrefs.GetInt("StaticObject.hikari");	
		book = PlayerPrefs.GetInt("StaticObject.book");

		EnemyKing = PlayerPrefs.GetInt("StaticObject.EnemyKing");
		J = PlayerPrefs.GetInt("StaticObject.J");
		Q = PlayerPrefs.GetInt("StaticObject.Q");
		K = PlayerPrefs.GetInt("StaticObject.K");

		bobby = PlayerPrefs.GetInt("StaticObject.bobby");
		wiki = PlayerPrefs.GetInt("StaticObject.wiki");
		wiko = PlayerPrefs.GetInt("StaticObject.wiko");
		boMom = PlayerPrefs.GetInt("StaticObject.boMom");
		boDad = PlayerPrefs.GetInt("StaticObject.boDad");

		if (sister == 1)
		{
			char1.interactable = true;
			clock1.SetActive(false);
		}
		if (bother == 1)
		{
			char2.interactable = true;
			clock2.SetActive(false);
		}
		if (utopia == 1)
		{
			char3.interactable = true;
			clock3.SetActive(false);
		}
		if (hikari == 1)
		{
			char4.interactable = true;
			clock4.SetActive(false);
		}
		if (book == 1)
		{
			char5.interactable = true;
			clock5.SetActive(false);
		}
		if (EnemyKing == 1)
		{
			char6.interactable = true;
			clock6.SetActive(false);
		}
		if (J == 1)
		{
			char7.interactable = true;
			clock7.SetActive(false);
		}
		if (Q == 1)
		{
			char8.interactable = true;
			clock8.SetActive(false);
		}
		if (K == 1)
		{
			char9.interactable = true;
			clock9.SetActive(false);
		}
		if (bobby == 1)
		{
			char10.interactable = true;
			clock10.SetActive(false);
		}
		if (wiki == 1)
		{
			char11.interactable = true;
			clock11.SetActive(false);
		}
		if (wiko == 1)
		{
			char12.interactable = true;
			clock12.SetActive(false);
		}

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
