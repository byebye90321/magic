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
	public Image icon;


	public void OnPointerExit(PointerEventData eventData)
	{
		DownState = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		DownState = true;
		if (gameObject.name == "Skill0")
		{
			info.SetActive(true);
			skills = GameObject.Find("Skill0").GetComponent<Skills>();
			coldDown = skills.skillInfo.coolDown;
			coldDownText.text = coldDown.ToString("#0");
			Atk = skills.skillInfo.Atk;
			AtkText.text = Atk.ToString("#0");
			icon.sprite = skills.skillInfo.SkillSpriteIcon;
		}
		if (gameObject.name == "G1")
		{
			info.SetActive(true);
			skills = GameObject.Find("G1").GetComponent<Skills>();
			coldDown = skills.skillInfo.coolDown;
			coldDownText.text = coldDown.ToString("#0");
			Atk = skills.skillInfo.Atk;
			AtkText.text = Atk.ToString("#0");
			icon.sprite = skills.skillInfo.SkillSpriteIcon;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		DownState = false;
		info.SetActive(false);
	}

}

