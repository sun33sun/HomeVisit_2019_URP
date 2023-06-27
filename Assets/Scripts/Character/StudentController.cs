using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ProjectBase;

namespace HomeVisit.Character
{
	public class StudentController : SingletonMono<StudentController>
	{
		public Animation anim;
		public NavMeshAgent agent;
		Transform nowTarget;

		public bool CheckAnim()
		{
			return !anim.isPlaying;
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

		public void StopAnim()
		{
			anim.Stop();
		}

		#region 走路
		public WaitUntil Walk(Transform target)
		{
			anim.Play("Walk");
			nowTarget = target;
			agent.SetDestination(nowTarget.position);
			agent.isStopped = false;
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
			if (Vector3.Distance(nowTarget.position, transform.position) < 1.5f)
			{
				agent.isStopped = true;
				anim.Stop();
				transform.forward = nowTarget.forward;
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
				transform.position = nowTarget.position;
				transform.rotation = nowTarget.rotation;
			}
			return anim.isPlaying;
		}
		#endregion
	}

}
