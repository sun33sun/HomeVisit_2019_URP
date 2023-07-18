using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ProjectBase;
using ProjectBase.Anim;

namespace HomeVisit.Character
{
	public class MaleStudent : SingletonMono<MaleStudent>
	{
		public Animator anim;
		public NavMeshAgent agent;
		Transform nowTarget;

		public IEnumerator PlayAnim(string clipName, bool once = true)
		{
			yield return AnimationManager.GetInstance().Play(anim, clipName);
			if (once)
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
			transform.forward = nowTarget.forward;
			yield return AnimationManager.GetInstance().Play(anim, "坐下");
			transform.position = nowTarget.position;
			transform.forward = nowTarget.forward;
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
