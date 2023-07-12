using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using ProjectBase.Anim;

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
				return null;
			}
			return AnimationManager.GetInstance().Play(anim, clipName);
		}

		#region 坐下
		public WaitUntil SitDown(Transform target)
		{
			anim.Play("坐下");
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

