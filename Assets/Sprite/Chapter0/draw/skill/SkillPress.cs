using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillPress : MonoBehaviour
,IPointerExitHandler
, IPointerDownHandler
, IPointerUpHandler
, IEventSystemHandler
{
	private bool DownState = false;
	public GameObject info;
	[HideInInspector]
	public Skills skills;
	public Text coldDownText;
	private float coldDown;
	public Text AtkText;
	private int Atk;
	public Image iconFull;


	public void OnPointerExit(PointerEventData eventData)
	{
		DownState = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		DownState = true;
		if (gameObject.name == "G0")
		{
			skills = GameObject.Find("G0").GetComponent<Skills>();
		}
		if (gameObject.name == "G1")
		{
			skills = GameObject.Find("G1").GetComponent<Skills>();
		}
		if (gameObject.name == "G2")
		{
			skills = GameObject.Find("G2").GetComponent<Skills>();
		}
		if (gameObject.name == "G3")
		{
			skills = GameObject.Find("G3").GetComponent<Skills>();
		}
		if (gameObject.name == "G4")
		{
			skills = GameObject.Find("G4").GetComponent<Skills>();
		}
		if (gameObject.name == "G5")
		{
			skills = GameObject.Find("G5").GetComponent<Skills>();
		}
		if (gameObject.name == "G6")
		{
			skills = GameObject.Find("G6").GetComponent<Skills>();
		}
		if (gameObject.name == "B1")
		{
			skills = GameObject.Find("B1").GetComponent<Skills>();
		}
		if (gameObject.name == "B2")
		{
			skills = GameObject.Find("B2").GetComponent<Skills>();
		}
		if (gameObject.name == "B3")
		{
			skills = GameObject.Find("B3").GetComponent<Skills>();
		}
		if (gameObject.name == "B4")
		{
			skills = GameObject.Find("B4").GetComponent<Skills>();
		}
		if (gameObject.name == "B5")
		{
			skills = GameObject.Find("B5").GetComponent<Skills>();
		}
		if (gameObject.name == "B6")
		{
			skills = GameObject.Find("B6").GetComponent<Skills>();
		}
		info.SetActive(true);
		coldDown = skills.skillInfo.coolDown;
		coldDownText.text = coldDown.ToString("#0");
		Atk = skills.skillInfo.Atk;
		AtkText.text = Atk.ToString("#0");
		iconFull.sprite = skills.skillInfo.SkillSpritebg;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		DownState = false;
		info.SetActive(false);
	}

}

