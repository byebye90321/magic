using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;

public class ExampleGestureHandler : MonoBehaviour
{
	public static ExampleGestureHandler gesture;
	public Text textResult;
	public Transform referenceRoot;
	GesturePatternDraw[] references;

	public static bool playerAtk = false;
	//----------------------卡牌----------------------
	public CardData cardData;
	public CardCD cardCD1;
	public CardCD cardCD2;
	public CardCD cardCD3;
	public CardCD cardCD4;

	public int cardAtk;
	public int cardCur1 = 1;
	public int cardCur2 = 1;
	public int cardCur3 = 1;
	public int cardCur4 = 1;

	public static bool cardisAtk1 = false;
	public static bool cardisAtk2 = false;
	public static bool cardisAtk3 = false;
	public static bool cardisAtk4 = false;

	//----------------2------------------
	public Skills skill1;

	//---------------------Animation----------------------
	/*Animation effectAani;
	public GameObject effectA;
	Animation effectBani;
	public GameObject effectB;
	Animation effectCani;
	public GameObject effectC;*/

	void Start()
	{
		references = referenceRoot.GetComponentsInChildren<GesturePatternDraw>();
		/*effectAani = effectA.GetComponent<Animation>();
		effectBani = effectB.GetComponent<Animation>();
		effectCani = effectC.GetComponent<Animation>();*/
	}

	void ShowAll()
	{
		for (int i = 0; i < references.Length; i++)
		{
			references[i].gameObject.SetActive(true);
		}
	}

	public void OnRecognize(RecognitionResult result)
	{
		StopAllCoroutines();
		ShowAll();
		/*if (result != RecognitionResult.Empty)
		{
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";  //答對顯示幾%
			StartCoroutine(Blink(result.gesture.id));  //答對閃爍
		}
		else
		{
			textResult.text = "?";
		}*/
		if (skill1.attack == true)  //B
		{
			if (result.gesture.id == "M")
			{
				Debug.Log("攻擊");
				textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
				skill1.currentCoolDown = 0;
			}
		}
	}

	IEnumerator Blink(string id)
	{
		var draw = references.Where(e => e.pattern.id == id).FirstOrDefault();
		if (draw != null)
		{
			var seconds = new WaitForSeconds(0.1f);
			for (int i = 0; i <= 20; i++)
			{
				draw.gameObject.SetActive(i % 2 == 0);
				yield return seconds;
			}
			draw.gameObject.SetActive(true);
		}
	}
	//第一張
	/*if (cardData.card00.isAtk == true)  //B
	{	
		if (result.gesture.id == "M")
		{
			cardisAtk1 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card00.Atk; //圖形攻擊力
			//effectB.SetActive(true);
			StartCoroutine("wait1");
		}
	}

	//第二張
	if (cardData.card05.isAtk == true)  //B
	{
		if (result.gesture.id == "heart")
		{
			cardisAtk2 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card05.Atk; //圖形攻擊力
			//effectB.SetActive(true);
			StartCoroutine("wait2");
		}
	}

	if (cardData.card13.isAtk == true)  //C
	{
		if (result.gesture.id == "Prawn")
		{
			cardisAtk2 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card13.Atk; //圖形攻擊力
			//effectC.SetActive(true);
			StartCoroutine("wait2");
		}
	}

	//第三張
	if (cardData.card02.isAtk == true)  //A
	{
		if (result.gesture.id == "Circle")
		{
			cardisAtk3 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card02.Atk; //圖形攻擊力
			//effectA.SetActive(true);
			StartCoroutine("wait3");
		}
	}	

	if (cardData.card06.isAtk == true)  //B
	{
		if (result.gesture.id == "Triangle")
		{
			cardisAtk3 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card06.Atk; //圖形攻擊力
			//effectB.SetActive(true);
			StartCoroutine("wait3");
		}
	}

	if (cardData.card10.isAtk == true)  //B
	{
		if (result.gesture.id == "Spiral")
		{
			cardisAtk3 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card10.Atk; //圖形攻擊力
			//effectB.SetActive(true);
			StartCoroutine("wait3");
		}
	}

	if (cardData.card14.isAtk == true)  //C
	{
		if (result.gesture.id == "Diamond")
		{
			cardisAtk3 = true;
			textResult.text = result.gesture.id + "\n" + Mathf.RoundToInt(result.score.score * 100) + "%";
			playerAtk = true;
			cardAtk = cardData.card14.Atk; //圖形攻擊力
			//effectC.SetActive(true);
			StartCoroutine("wait3");
		}
	}*/

}
