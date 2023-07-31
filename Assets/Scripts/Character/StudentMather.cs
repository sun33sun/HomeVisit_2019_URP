using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using UnityEngine.AI;
using ProjectBase.Anim;

namespace HomeVisit.Character
{
	public class StudentMather : SingletonMono<StudentMather>
	{
		public Animator anim;
		public NavMeshAgent agent;
		Transform nowTarget;

		public IEnumerator PlayAnim(string clipName,bool once = true)
		{
			yield return AnimMgr.GetInstance().Play(anim, clipName);
			if(once)
				anim.Play("站立");
		}

		#region 走路
		public IEnumerator Walk(Transform target)
		{
			anim.Play("走路");
			nowTarget = target;
			agent.isStopped = false;
			agent.SetDestination(nowTarget.position);
			yield return new WaitForDistance(transform, nowTarget);
			anim.Play("站立");
			agent.isStopped = true;
			transform.forward = nowTarget.forward;
			transform.position = nowTarget.position;
		}
		#endregion

		#region 坐下
		public IEnumerator SitDown(Transform target)
		{
			nowTarget = target;
			transform.position = nowTarget.position;
			transform.rotation = nowTarget.rotation;
			yield return PlayAnim("坐下", false);
			transform.position = nowTarget.position;
			transform.rotation = nowTarget.rotation;
		}
		#endregion

		public void SetTransform(Transform newTrans)
		{
			agent.enabled = false;
			transform.position = newTrans.position;
			transform.forward = newTrans.forward;
			agent.enabled = true;
		}
	}
}

