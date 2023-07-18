using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBase.Anim
{
	public class AnimationManager:Singleton<AnimationManager>
	{
		WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
		WaitForSeconds wait017 = new WaitForSeconds(0.17f);

		public WaitUntil Play(Animation anim,string clipName)
		{
			anim.Play(clipName);
			return new WaitUntil(() => { return !anim.isPlaying; });
		}

		public IEnumerator Play(Animator animator,string clipName)
		{
			animator.Play(clipName);
			yield return waitFrame;
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
					yield return waitFrame;
				}
			}
		}
	}
}
