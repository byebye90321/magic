using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardCD_sister_forest : MonoBehaviour
, IPointerExitHandler
, IPointerDownHandler
, IPointerUpHandler
, IEventSystemHandler
{
	public Draw_sister_forest drawPlayerController;
	public ExampleGestureHandler gesture;
	private CardInfo cardInfo;
	public CardData cardData; 
	private bool DownState = false;
	public GameObject bgImage;
	public Image cardImage;
	public Button cardBtn;
	public Text cardName;
	public Text cardClass;
	public Text cardAtk;
	public Image cardPNG;
	public Text CD1;
	public Text CD2;
	public Text CD3;
	public int curCD;
	float r = 0.38f, g = 0.38f, b = 0.38f, a = 1f;

	private bool card00Turn;
	private bool card01Turn;
	private bool card02Turn;
	private bool card05Turn;
	private bool card06Turn;
	private bool card10Turn;
	private bool card13Turn;
	private bool card14Turn;

	public GameObject card1;
	public GameObject card2;
	public GameObject card3;
	//public GameObject card4;

	public CardCD_sister_forest cardCD1;
	public CardCD_sister_forest cardCD2;
	public CardCD_sister_forest cardCD3;
	//public CardCD_sister_forest cardCD4;

	public void Start()
	{
		card00Turn = StaticObject.card00;
		card02Turn = StaticObject.card02;
		card05Turn = StaticObject.card05;
		card06Turn = StaticObject.card06;
		card10Turn = StaticObject.card10;
		card13Turn = StaticObject.card13;
		card14Turn = StaticObject.card14;

		CD1.GetComponent<Text>().enabled = false;
		CD2.GetComponent<Text>().enabled = false;
		CD3.GetComponent<Text>().enabled = false;
		//CD4.GetComponent<Text>().enabled = false;

		//第一張卡
		if (card00Turn == true) {
			cardCD1.cardInfo = cardData.card00;
			cardCD1.curCD = drawPlayerController.cardindex1;
		}
		else{
			card1.SetActive(false);
		}


		//第二張卡
		if (card05Turn == true)
		{
			cardCD2.cardInfo = cardData.card05;
			cardCD2.curCD = drawPlayerController.cardindex2;
		}	
		else if (card13Turn == true)
		{
			cardCD2.cardInfo = cardData.card13;
			cardCD2.curCD = drawPlayerController.cardindex2;
		}
		else
		{
			card2.SetActive(false);
		}


		//第三張卡
		if (card02Turn == true)
		{
			cardCD3.cardInfo = cardData.card02;
			cardCD3.curCD = drawPlayerController.cardindex3;

		}
		else if (card06Turn == true)
		{
			cardCD3.cardInfo = cardData.card06;
			cardCD3.curCD = drawPlayerController.cardindex3;
		}
		else if (card10Turn == true)
		{
			cardCD3.cardInfo = cardData.card10;
			cardCD3.curCD = drawPlayerController.cardindex3;

		}
		else if (card14Turn == true)
		{
			cardCD3.cardInfo = cardData.card14;
			cardCD3.curCD = drawPlayerController.cardindex3;
		}
		else
		{
			card3.SetActive(false);
		}

		/*//第四張卡
		if (card01Turn == true)
		{
			//cardCD4.cardInfo = cardData.card01;
			//curCD = drawPlayerController.cardindex4;
		}
		else {
			card4.SetActive(false);
		}*/

		cardImage.sprite = cardInfo.CardSprite;
		
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		DownState = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		bgImage.SetActive(true);
		cardName.text = cardInfo.Name;
		cardClass.text = cardInfo.Class;
		cardAtk.text = cardInfo.Atk.ToString();
		cardPNG.sprite = cardInfo.CardPNG;

		//第一張
		if (gameObject.name == "card1")
		{
			if (cardCD1.curCD == 0)
			{
				CD1.text = cardCD1.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD1.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD1.GetComponent<Text>().enabled = true;
			}
			else if (cardCD1.curCD >= cardInfo.CD)
			{
				cardCD1.curCD = cardInfo.CD;
				CD1.text = cardCD1.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD1.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD1.GetComponent<Text>().enabled = true;
			}
			else if (cardCD1.curCD < cardInfo.CD)
			{
				cardCD1.curCD = drawPlayerController.cardindex1;
				CD1.text = cardCD1.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD1.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD1.GetComponent<Text>().enabled = true;
			}
		}

		if (gameObject.name == "card2")
		{
			//第二張
			if (cardCD2.curCD == 0)
			{
				CD2.text = cardCD2.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD2.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD2.GetComponent<Text>().enabled = true;
			}
			else if (cardCD2.curCD >= cardInfo.CD)
			{
				cardCD2.curCD = cardInfo.CD;
				CD2.text = cardCD2.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD2.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD2.GetComponent<Text>().enabled = true;

			}
			else if (cardCD2.curCD < cardInfo.CD)
			{
				cardCD2.curCD = drawPlayerController.cardindex2;
				CD2.text = cardCD2.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD2.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD2.GetComponent<Text>().enabled = true;

			}
		}

		if (gameObject.name == "card3")
		{
			//第三張
			if (cardCD3.curCD == 0)
			{
				CD3.text = cardCD3.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD3.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD3.GetComponent<Text>().enabled = true;
			}
			else if (cardCD3.curCD >= cardInfo.CD)
			{
				cardCD3.curCD = cardInfo.CD;
				CD3.text = cardCD3.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD3.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD3.GetComponent<Text>().enabled = true;

			}
			else if (cardCD3.curCD < cardInfo.CD)
			{
				cardCD3.curCD = drawPlayerController.cardindex3;
				CD3.text = cardCD3.curCD.ToString() + "/" + cardInfo.CD.ToString();
				Debug.Log(cardCD3.curCD.ToString() + " / " + cardInfo.CD.ToString());
				CD3.GetComponent<Text>().enabled = true;

			}
		}



		DownState = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		bgImage.SetActive(false);
		DownState = false;
		CD1.GetComponent<Text>().enabled = false;
		CD2.GetComponent<Text>().enabled = false;
		CD3.GetComponent<Text>().enabled = false;
		//CD4.GetComponent<Text>().enabled = false;
	}


	private float timer = 0;
	void Update()
	{
		cardCD1.curCD = drawPlayerController.cardindex1;
		cardCD2.curCD = drawPlayerController.cardindex2;
		cardCD3.curCD = drawPlayerController.cardindex3;
		//cardCD4.curCD = drawPlayerController.cardindex4;
		if (DownState)
			timer += Time.deltaTime;
		else
			timer = 0;


		//第一張卡
		if (card1.activeInHierarchy == true)
		{
			if (cardCD1.curCD >= cardCD1.cardInfo.CD)
			{
				cardCD1.cardInfo.isAtk = true;
				cardCD1.cardImage.color = new Color(1, 1, 1, 1);
				if (ExampleGestureHandler.cardisAtk1 == true)
				{
					drawPlayerController.cardindex1 = 0;
					cardCD1.curCD = drawPlayerController.cardindex1;
					cardCD1.cardImage.color = new Color(r, g, b, a);
					cardCD1.cardInfo.isAtk = false;
					//ExampleGestureHandler.cardisAtk1 = false;
				}
			}
			else if (cardCD1.curCD == 0)
			{
				drawPlayerController.cardindex1 = 0;
				cardCD1.curCD = drawPlayerController.cardindex1;
				cardCD1.cardImage.color = new Color(r, g, b, a);
				cardCD1.cardInfo.isAtk = false;
			}
			else if (cardCD1.curCD < cardCD1.cardInfo.CD)
			{
				cardCD1.curCD = drawPlayerController.cardindex1;
				cardCD1.cardImage.color = new Color(r, g, b, a);
				cardCD1.cardInfo.isAtk = false;
			}
		}

		//第二張
		if (card2.activeInHierarchy == true)
		{
			if (cardCD2.curCD >= cardCD2.cardInfo.CD)
			{
				cardCD2.cardInfo.isAtk = true;
				cardCD2.cardImage.color = new Color(1, 1, 1, 1);
				if (ExampleGestureHandler.cardisAtk2 == true)
				{
					drawPlayerController.cardindex2 = 0;
					cardCD2.curCD = drawPlayerController.cardindex2;
					cardCD2.cardImage.color = new Color(r, g, b, a);
					cardCD2.cardInfo.isAtk = false;
				}
			}
			else if (cardCD2.curCD == 0)
			{
				drawPlayerController.cardindex2 = 0;
				cardCD2.curCD = drawPlayerController.cardindex2;
				cardCD2.cardImage.color = new Color(r, g, b, a);
				cardCD2.cardInfo.isAtk = false;
			}
			else if (cardCD2.curCD < cardCD2.cardInfo.CD)
			{
				cardCD2.curCD = drawPlayerController.cardindex2;
				cardCD2.cardImage.color = new Color(r, g, b, a);
				cardCD2.cardInfo.isAtk = false;
			}
		}


		//第三張
		if (card3.activeInHierarchy ==true)
		{
			if (cardCD3.curCD >= cardCD3.cardInfo.CD)
			{
				cardCD3.cardInfo.isAtk = true;
				cardCD3.cardImage.color = new Color(1, 1, 1, 1);
				if (ExampleGestureHandler.cardisAtk3 == true)
				{
					drawPlayerController.cardindex3 = 0;
					cardCD3.curCD = drawPlayerController.cardindex3;
					cardCD3.cardImage.color = new Color(r, g, b, a);
					cardCD3.cardInfo.isAtk = false;
				}
			}
			else if (cardCD3.curCD == 0)
			{
				drawPlayerController.cardindex3 = 0;
				cardCD3.curCD = drawPlayerController.cardindex3;
				cardCD3.cardImage.color = new Color(r, g, b, a);
				cardCD3.cardInfo.isAtk = false;
			}
			else if (cardCD3.curCD < cardCD3.cardInfo.CD)
			{
				cardCD3.curCD = drawPlayerController.cardindex3;
				cardCD3.cardImage.color = new Color(r, g, b, a);
				cardCD3.cardInfo.isAtk = false;
			}
		}


	}
}

