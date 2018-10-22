using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAnamitionScript : StateMachineBehaviour {
	public string animationName;
	public float speed;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		SkeletonAnimation anim = animator.GetComponent<SkeletonAnimation>();
		anim.state.SetAnimation(0, animationName, true).timeScale = speed;
	}

}
