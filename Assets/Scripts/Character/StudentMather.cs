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
		public Animation anim;
		public NavMeshAgent agent;
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
			return AnimationManager.GetInstance().Play(anim,clipName);
		}

		#region 走路
		public WaitUntil Walk(Transform target)
		{
			anim.Play("Walk");
			nowTarget = target;
			agent.isStopped = false;
			agent.SetDestination(nowTarget.position);
			return new WaitUntil(OnWalkCompleted);
		}
		bool OnWalkCompleted()
		{
			if (nowTarget == null)
			{
				Debug.LogWarning("StudentMather的nowTarget为null");
				agent.isStopped = true;
				anim.Stop();
				return true;
			}
			float distance = Vector3.Distance(nowTarget.position, transform.position);
			if (distance < 1.5f)
			{
				agent.isStopped = true;
				anim.Stop();
				transform.forward = nowTarget.forward;
				transform.position = nowTarget.position;
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

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
				Vector3 temp = nowTarget.position;
				temp.y = 0;
				transform.position = temp;
				transform.rotation = nowTarget.rotation;
			}
			return anim.isPlaying;
		}
		#endregion
	}
}

