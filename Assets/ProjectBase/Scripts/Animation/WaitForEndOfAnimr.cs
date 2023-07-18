using System;
using System.Collections;
using UnityEngine;

namespace ProjectBase.Anim
{
	public class WaitForEndOfAnimr : IEnumerator
	{
		Animator animator;

		public WaitForEndOfAnimr(Animator animator)
		{
			this.animator = animator;
		}
		public object Current
		{
			get
			{
				return null;
			}
		}
		public bool MoveNext()
		{
			AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
			return state.normalizedTime > 0;
		}
		public void Reset()
		{
		}
	}
}
