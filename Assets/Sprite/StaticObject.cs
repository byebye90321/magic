using UnityEngine;
using System.Collections;

public static class StaticObject
{
	//float int string

	//------------------音量音效---------------------
	public static float bgmVolume = 1f;
	public static float sfxVolume = 1f;

	//--------------圖鑑-魔法日報解鎖-----------------
	public static int Paper1 = 0;
	public static int Paper2 = 0;
	public static int Paper3 = 0;
	public static int Paper4 = 0;
	public static int Paper5 = 0;
	public static int Paper6 = 0;
	public static int Paper7 = 0;
	public static int Paper8 = 0;

	//-----------------平衡條-----------------------
	public static float balanceSlider;

	//------------------卡牌------------------------
	//第一張
	public static bool card00 = true;  //00,B,魔法框框

	//第二張
	public static bool card05 = false; //05,B,察覺之心
	//public static bool card07 = false;
	//public static bool card08 = false;
	//public static bool card09 = false;	
	public static bool card13 = false; //13,C,迷途羔羊	
	//public static bool card15 = false;
	//public static bool card16 = false;
	//public static bool card17 = false;

	//第三張
	public static bool card02 = false; //02,A,被擁抱的玫瑰
	//public static bool card03 = false;
	//public static bool card04 = false;
	public static bool card06 = false; //06,B,信念之盾
	public static bool card10 = false; //10,B,腐爛之果
	//public static bool card11 = false;
	//public static bool card12 = false;
	public static bool card14 = false; //14,C,生鏽的茅

	//第四張
	//public static bool card01 = false; //01,A,先知卡

	//-------------------結局(圖鑑)-----------------------
	public static int s1 = 0;
	public static int sHE1 = 0;
	public static int sBE1 = 0;

	//--------------------人物解鎖------------------------
	public static int sister = 0;
	public static int bother = 0;
	public static int utopia = 0;
	public static int hikari = 0;
	public static int book = 0;

	public static int EnemyKing = 0;
	public static int J = 0;
	public static int Q = 0;
	public static int K = 0;

	public static int bobby = 0;
	public static int wiki = 0;
	public static int wiko = 0;
	public static int boMom = 0;
	public static int boDad = 0;

}