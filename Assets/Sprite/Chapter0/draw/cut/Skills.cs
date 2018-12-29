using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Skills : MonoBehaviour
{
	public skillInfo skillInfo;
	// 技能的图标
	public Image Skillbg;
	public Image SkillIcon;
	// 技能的冷却时间
	private float coolDown;
	// 保存当前技能的冷却时间
	public float currentCoolDown;
	// 技能的按钮
	private Button skillButton;

	public bool attack;

	void Start()
	{
		coolDown = skillInfo.coolDown;
		attack = skillInfo.isAtk;
		currentCoolDown = 0;
		SkillIcon.sprite = skillInfo.SkillSpriteIcon;
		Skillbg.sprite = skillInfo.SkillSpritebg;

	}

	void Update()
	{
		if (DG_GameManager.drawState == DrawState.Game || GameManager.chapterState == ChapterState.Game)
		{
			if (currentCoolDown < coolDown)
			{
				attack = false;
				// 更新冷却
				currentCoolDown += Time.deltaTime;
				// 显示冷却动画
				this.Skillbg.fillAmount = 1 - currentCoolDown / coolDown;
			}

			if (currentCoolDown >= coolDown)
			{
				attack = true;
			}
		}
	}
}