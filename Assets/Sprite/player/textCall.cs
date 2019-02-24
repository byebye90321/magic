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
}
