using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class skillInfo : ScriptableObject
{
	public string id;
	public Sprite SkillSprite;
	public int Atk;
	public bool isAtk = false;

}
