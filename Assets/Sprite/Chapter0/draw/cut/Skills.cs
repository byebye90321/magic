using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Skills : MonoBehaviour
{
	// 技能的图标
	public Image icon;
	// 技能的冷却时间
	public float coolDown;
	// 技能名称，用于区分使用了哪个技能的
	public string skillName;
	// 保存当前技能的冷却时间
	public float currentCoolDown;
	// 技能的按钮
	private Button skillButton;

	public bool attack;

	void Start()
	{
		currentCoolDown = 0;
		
	}

	void Update()
	{
		if (DG_GameManager.drawState == DrawState.Game || GameManager.chapterState == ChapterState.Game)
		{
			Debug.Log(ChapterState.Game);
			if (currentCoolDown < coolDown)
			{
				attack = false;
				// 更新冷却
				currentCoolDown += Time.deltaTime;
				// 显示冷却动画
				this.icon.fillAmount = 1 - currentCoolDown / coolDown;
			}

			if (currentCoolDown >= coolDown)
			{
				attack = true;
			}
		}
	}
}