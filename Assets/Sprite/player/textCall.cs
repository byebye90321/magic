using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textCall : MonoBehaviour {

	public DialogsScript2 dialogsScript2;

	public void textCall1()
	{
		dialogsScript2.currentLine = 148;
		dialogsScript2.endAtLine = 153;
		dialogsScript2.NPCAppear();
	}

	public void textCall2()
	{
		dialogsScript2.currentLine = 167;
		dialogsScript2.endAtLine = 170;
		dialogsScript2.NPCAppear();
	}

	public void textCall3()
	{
		dialogsScript2.currentLine = 174;
		dialogsScript2.endAtLine = 175;
		dialogsScript2.NPCAppear();
	}

	public void textCall4() 
	{
		dialogsScript2.currentLine = 182;
		dialogsScript2.endAtLine = 188;
		dialogsScript2.NPCAppear();
	}

	public void textCallK()
	{
		dialogsScript2.currentLine = 176;
		dialogsScript2.endAtLine = 178;
		dialogsScript2.NPCAppear();
	}

	public void textCall5() //戰鬥後對話
	{
		dialogsScript2.currentLine = 200;
		dialogsScript2.endAtLine = 207;
		dialogsScript2.NPCAppear();
	}
}
