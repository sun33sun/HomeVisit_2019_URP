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
			yield return AnimMgr.GetInstance().Play(anim, clipName);
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
		
		[SerializeField] private SkinnedMeshRenderer mouse;
		private bool isSpkeaing = false;
		public IEnumerator StartSpeak()
		{
			isSpkeaing = true;
			WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
			float blendWeight = 0;
			bool isAdd = true;
			while (isSpkeaing)
			{
				if (isAdd)
				{
					blendWeight += Time.deltaTime;
				}
				else
				{
					blendWeight -= Time.deltaTime;
				}
				mouse.SetBlendShapeWeight(0,blendWeight);
				yield return waitFrame;
				if (blendWeight > 99 || blendWeight < 1)
					isAdd = !isAdd;
			}
			mouse.SetBlendShapeWeight(0,0);
		}

		public void StopSpeak()
		{
			isSpkeaing = false;
		}
	}
}

