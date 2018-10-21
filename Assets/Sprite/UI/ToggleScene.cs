using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScene : MonoBehaviour
{

	public Canvas graphic;
	public Toggle high;
	public Toggle low;

	// Use this for initialization
	void Start()
	{
		graphic = graphic.GetComponent<Canvas>();
		high = high.GetComponent<Toggle>();
		low = low.GetComponent<Toggle>();

		if (StaticObject.graphic == 1)
		{
			low.isOn = false;
			high.isOn = true;
		}
		else
		{
			low.isOn = true;
			high.isOn = false;
		}
		
	}


	public void highGraphic()
	{
		QualitySettings.SetQualityLevel(6);//Ultra
		//graphicQuality = 1;
		StaticObject.graphic = 1;
		PlayerPrefs.SetInt("StaticObject.graphic", StaticObject.graphic);
	}

	public void lowGraphic()
	{
		QualitySettings.SetQualityLevel(2);//Medium
		StaticObject.graphic = 0;
		PlayerPrefs.SetInt("StaticObject.graphic", StaticObject.graphic);
	}


}
