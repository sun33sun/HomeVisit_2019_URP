using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;

namespace HomeVisit.Character
{
    public class StudentFather : SingletonMono<StudentFather>
    {
        public Animation anim;

		Transform nowTarget;
		public bool CheckAnim()
		{
			return anim.isPlaying;
		}
		public WaitUntil PlayAnim(string clipName)
		{
			if (anim[clipName] == null)
			{
				print("播放 : " + clipName);
			}
			else
			{
				anim.Play(clipName);
			}
			return new WaitUntil(CheckAnim);
		}

		#region 坐下
		public WaitUntil SitDown(Transform target)
		{
			anim.Play("SitDown");
			nowTarget = target;
			return new WaitUntil(OnSitDownCompleted);
		}
		bool OnSitDownCompleted()
		{
			if (!anim.isPlaying)
			{
				transform.position = nowTarget.position;
				transform.rotation = nowTarget.rotation;
			}
			return anim.isPlaying;
		}
		#endregion
	}
}

