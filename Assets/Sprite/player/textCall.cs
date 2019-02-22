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
}
