using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBase.Anim
{
	public class AnimMgr:Singleton<AnimMgr>
	{
		WaitForEndOfFrame AnimatorWaitFrame = new WaitForEndOfFrame();
		WaitForEndOfFrame AnimationWaitFrame = new WaitForEndOfFrame();
		WaitForSeconds wait017 = new WaitForSeconds(0.17f);

		public IEnumerator Play(Animation anim,string clipName)
		{
			anim.Play(clipName);
			while (anim.isPlaying)
			{
				yield return AnimationWaitFrame;
			}
		}

		public IEnumerator Play(Animator animator,string clipName)
		{
			animator.Play(clipName);
			yield return AnimatorWaitFrame;
			if(clipName == "站起")
			{
				yield return wait017;
			}
			else
			{
				AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
				while (state.normalizedTime < 1 && state.IsName(clipName))
				{
					state = animator.GetCurrentAnimatorStateInfo(0);
					yield return AnimatorWaitFrame;
				}
			}
		}
	}
}
