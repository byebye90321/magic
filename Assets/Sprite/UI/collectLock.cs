using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class collectLock : MonoBehaviour {

	//--------------分類Toggle------------
	public Toggle paperToggle;
	public GameObject paperpanel;
	private Animator paperAnimator;
	private Animation paperAnimation;
	bool paper = true;
	public Toggle treeToggle;
	public GameObject treepanel;
	private Animator treeAnimator;
	private Animation treeAnimation;
	bool tree = false;
	public Toggle characterToggle;
	public GameObject characterpanel;
	private Animator characterAnimator;
	private Animation characterAnimation;
	bool character = false;

	//----------------日報----------------
	public Button paperB1;
	public Button paperB2;
	public Button paperB3;
	public Button paperB4;
	public Button paperB5;
	public Button paperB6;
	public Button paperB7;
	public Button paperB8;
	public Text paperT1;
	public Text paperT2;
	public Text paperT3;
	public Text paperT4;
	public Text paperT5;
	public Text paperT6;
	public Text paperT7;
	public Text paperT8;
	public GameObject lock1;
	public GameObject lock2;
	public GameObject lock3;
	public GameObject lock4;
	public GameObject lock5;
	public GameObject lock6;
	public GameObject lock7;
	public GameObject lock8;
	public GameObject paperQ1;
	public GameObject paperQ2;
	public GameObject paperQ3;
	public GameObject paperQ4;
	public GameObject paperQ5;
	public GameObject paperQ6;
	public GameObject paperQ7;
	public GameObject paperQ8;
	private Animator paperClose1;
	private Animator paperClose2;
	private Animator paperClose3;
	private Animator paperClose4;
	private Animator paperClose5;
	private Animator paperClose6;
	private Animator paperClose7;
	private Animator paperClose8;
	public GameObject black_bg;
	float r = 0.3019608f, g = 0.2745098f, b = 0.4470588f, a = 1f;

	//----------------------樹狀圖-----------------------
	public GameObject treeS1;
	public GameObject treeSHE1;
	public GameObject treeSBE1;

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
	//-------日報-------
	private int paper1;
	private int paper2;
	private int paper3;

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

	//-----------------------audio--------------------



	void Start() {
		paperAnimator = paperpanel.GetComponent<Animator>();
		paperAnimation = paperpanel.GetComponent<Animation>();
		paperAnimation.Play();
		treeAnimator = treepanel.GetComponent<Animator>();
		treeAnimation = treepanel.GetComponent<Animation>();
		treeAnimation.Play();
		characterAnimator = characterpanel.GetComponent<Animator>();
		characterAnimation = characterpanel.GetComponent<Animation>();
		characterAnimation.Play();

		paperClose1 = paperQ1.GetComponent<Animator>();
		paperClose2 = paperQ2.GetComponent<Animator>();
		paperClose3 = paperQ3.GetComponent<Animator>();
		paperClose4 = paperQ4.GetComponent<Animator>();
		paperClose5 = paperQ5.GetComponent<Animator>();
		paperClose6 = paperQ6.GetComponent<Animator>();
		paperClose7 = paperQ7.GetComponent<Animator>();
		paperClose8 = paperQ8.GetComponent<Animator>();
		//---------------------日報------------------------
		paper1 = PlayerPrefs.GetInt("StaticObject.Paper1");
		paper2 = PlayerPrefs.GetInt("StaticObject.Paper2");
		paper3 = PlayerPrefs.GetInt("StaticObject.Paper3");
	
		if (paper1 == 1)//1
		{
			paperB1.GetComponent<Button>().interactable = true;
			paperT1.color = new Color(r, g, b, a);
			lock1.gameObject.SetActive(false);
		}

		if (paper2 == 1)//2
		{
			paperB2.GetComponent<Button>().interactable = true;
			paperT2.color = new Color(r, g, b, a);
			lock2.gameObject.SetActive(false);
		}

		if (paper3 == 1)//3
		{
			paperB3.GetComponent<Button>().interactable = true;
			paperT3.color = new Color(r, g, b, a);
			lock3.gameObject.SetActive(false);
		}

		if (StaticObject.Paper4 == 1)//4
		{
			paperB4.GetComponent<Button>().interactable = true;
			paperT4.color = new Color(r, g, b, a);
			lock4.gameObject.SetActive(false);
		}

		if (StaticObject.Paper5 == 1)//5
		{
			paperB5.GetComponent<Button>().interactable = true;
			paperT5.color = new Color(r, g, b, a);
			lock5.gameObject.SetActive(false);
		}
		if (StaticObject.Paper6 == 1)//6
		{
			paperB6.GetComponent<Button>().interactable = true;
			paperT6.color = new Color(r, g, b, a);
			lock6.gameObject.SetActive(false);
		}

		if (StaticObject.Paper7 == 1)//7
		{
			paperB7.GetComponent<Button>().interactable = true;
			paperT7.color = new Color(r, g, b, a);
			lock7.gameObject.SetActive(false);
		}

		if (StaticObject.Paper8 == 1)//8
		{
			paperB8.GetComponent<Button>().interactable = true;
			paperT8.color = new Color(r, g, b, a);
			lock8.gameObject.SetActive(false);
		}

		//---------------------結局樹狀圖------------------------
		s1 = PlayerPrefs.GetInt("StaticObject.s1");
		sHE1 = PlayerPrefs.GetInt("StaticObject.sHE1");
		sBE1 = PlayerPrefs.GetInt("StaticObject.sBE1");

		if (s1 == 1)
		{
			treeS1.SetActive(true);
		}
		if (sHE1 == 1)
		{
			treeSHE1.SetActive(true);
		}
		if (sBE1 == 1)
		{
			treeSBE1.SetActive(true);
		}

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
		if (boMom == 1)
		{
			char13.interactable = true;
			clock13.SetActive(false);
		}
		if (boDad == 1)
		{
			char14.interactable = true;
			clock14.SetActive(false);
		}

	}

	public void Update() {
		if (paperToggle.GetComponent<Toggle>().isOn == true)
		{
			paperAnimator.SetBool("paper", true);		
		}
		else
		{
			paper = false;
			paperAnimator.SetBool("paper", false);
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

		if (characterToggle.GetComponent<Toggle>().isOn == true)
		{
			characterAnimator.SetBool("character", true);
		}
		else
		{
			tree = false;
			characterAnimator.SetBool("character", false);
		}
	}


	//第一題
	public void Open1()
	{
		paperQ1.SetActive(true);
		black_bg.SetActive(true);
	}

	//第2題
	public void Open2()
	{
		paperQ2.SetActive(true);
		black_bg.SetActive(true);
	}

	//第3題
	public void Open3()
	{
		paperQ3.SetActive(true);
		black_bg.SetActive(true);
	}

	//第4題
	public void Open4()
	{
		paperQ4.SetActive(true);
		black_bg.SetActive(true);
	}

	//第5題
	public void Open5()
	{
		paperQ5.SetActive(true);
		black_bg.SetActive(true);
	}

	//第6題
	public void Open6()
	{
		paperQ6.SetActive(true);
		black_bg.SetActive(true);
	}

	//第7題
	public void Open7()
	{
		paperQ7.SetActive(true);
		black_bg.SetActive(true);
	}

	//第8題
	public void Open8()
	{
		paperQ8.SetActive(true);
		black_bg.SetActive(true);
	}

	//關閉
	public void Close() {
		paperClose1.SetBool("PaperClose",true);
		paperClose2.SetBool("PaperClose",true);
		paperClose3.SetBool("PaperClose",true);
		paperClose4.SetBool("PaperClose",true);
		paperClose5.SetBool("PaperClose",true);
		paperClose6.SetBool("PaperClose",true);
		paperClose7.SetBool("PaperClose",true);
		paperClose8.SetBool("PaperClose",true);
		StartCoroutine("waitClose");	
	}

	IEnumerator waitClose() {
		yield return new WaitForSeconds(0.2f);
		paperQ1.SetActive(false);
		paperQ2.SetActive(false);
		paperQ3.SetActive(false);
		paperQ4.SetActive(false);
		paperQ5.SetActive(false);
		paperQ6.SetActive(false);
		paperQ7.SetActive(false);
		paperQ8.SetActive(false);
		black_bg.SetActive(false);
	}
}
