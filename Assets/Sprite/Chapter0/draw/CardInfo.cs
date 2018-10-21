using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardInfo : ScriptableObject
{
	public string id;
	public string Name;
	public Sprite CardSprite;
	public string Class;
	public int Atk;
	public int CD;
	public Sprite CardPNG;
	public bool isAtk = false;

}
