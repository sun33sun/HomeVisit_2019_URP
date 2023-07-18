using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using ProjectBase.Anim;

namespace HomeVisit.Character
{
    public class StudentFather : SingletonMono<StudentFather>
    {
        public Animator anim;

		Transform nowTarget;
		public IEnumerator PlayAnim(string clipName,bool once = true)
		{
			yield return AnimationManager.GetInstance().Play(anim, clipName);
			if(once)
				anim.Play("站立");
		}

		#region 坐下
		public IEnumerator SitDown(Transform target)
		{
			nowTarget = target;
			transform.position = nowTarget.position;
			transform.forward = nowTarget.forward;
			yield return PlayAnim("坐下",false);
			transform.position = nowTarget.position;
			transform.forward = nowTarget.forward;
		}
		#endregion

		public void SetTransform(Transform newTrans)
		{
			transform.position = newTrans.position;
			transform.forward = newTrans.forward;
		}
	}
}

