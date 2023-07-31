using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using UnityEngine.AI;
using ProjectBase.Anim;
using UnityStandardAssets.Characters.FirstPerson;

namespace HomeVisit.Character
{
	public class FemaleTeacher : SingletonMono<FemaleTeacher>
	{
		public Animator anim;
		public Transform face;
		public Transform thirdPerson;
		SkinnedMeshRenderer faceSkin;
		public CharacterController cc;
		public NavMeshAgent agent;

		public bool canMove = false;
		public bool canRotate = false;
		//移动速度
		[SerializeField] float moveSpeed = 6.0F;
		//正在转向
		[SerializeField] bool isRotate = false;
		//旋转速度
		[SerializeField] float rotateSpeed;

		[SerializeField] Transform nowTarget;

		public float gravity = 20.0F;
		Vector3 moveDirection = Vector3.zero;

		//private void OnEnable()
		//{
		//	faceSkin = face.gameObject.GetComponent<SkinnedMeshRenderer>();
		//	StartCoroutine(Speak());
		//}

		//IEnumerator Speak()
		//{
		//	WaitForEndOfFrame wait = new WaitForEndOfFrame();
		//	float count = 1;
		//	while (count > 0)
		//	{
		//		count-=0.01f;
		//		faceSkin.SetBlendShapeWeight(0, count);
		//		yield return wait; 
		//	}
		//}

		private void Start()
		{
			EventCenter.GetInstance().AddEventListener("鼠标右键按下", OnMouseRightDown);
			EventCenter.GetInstance().AddEventListener("鼠标右键抬起", OnMouseRightUp);
			EventCenter.GetInstance().AddEventListener<Vector2>("鼠标滑动", OnMouseSliding);
			MonoMgr.GetInstance().AddUpdateListener(MyUpdate);

			EventCenter.GetInstance().AddEventListener(KeyCode.W + "按下", OnWDown);
			EventCenter.GetInstance().AddEventListener(KeyCode.A + "按下", OnADown);
			EventCenter.GetInstance().AddEventListener(KeyCode.S + "按下", OnSDown);
			EventCenter.GetInstance().AddEventListener(KeyCode.D + "按下", OnDDown);

			EventCenter.GetInstance().AddEventListener(KeyCode.W + "抬起", OnWUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.A + "抬起", OnAUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.S + "抬起", OnSUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.D + "抬起", OnDUp);
		}

		private void OnDestroy()
		{
			EventCenter.GetInstance().RemoveEventListener("鼠标右键按下", OnMouseRightDown);
			EventCenter.GetInstance().RemoveEventListener("鼠标右键抬起", OnMouseRightUp);
			EventCenter.GetInstance().RemoveEventListener<Vector2>("鼠标滑动", OnMouseSliding);
			MonoMgr.GetInstance().RemoveUpdateListener(MyUpdate);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.W + "按下", OnWDown);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.A + "按下", OnADown);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.S + "按下", OnSDown);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.D + "按下", OnDDown);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.W + "抬起", OnWUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.A + "抬起", OnAUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.S + "抬起", OnSUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.D + "抬起", OnDUp);
		}


		void MyUpdate()
		{
			if (canMove != cc.enabled)
				cc.enabled = canMove;
			if (!canMove)
				return;
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= moveSpeed;
			moveDirection.y -= gravity * Time.deltaTime;
			cc.Move(moveDirection * Time.deltaTime);
		}

		#region 按键响应事件
		private void OnMouseRightDown()
		{
			if (!canRotate)
			{
				isRotate = false;
				return;
			}
			isRotate = true;
		}
		private void OnMouseRightUp()
		{
			isRotate = false;
		}
		private void OnMouseSliding(Vector2 vec2)
		{
			if (!isRotate || !canRotate)
			{
				isRotate = false;
				return;
			}

			transform.rotation = Quaternion.AngleAxis(vec2.x, transform.up) * transform.rotation;

		}

		List<bool> moveState = new List<bool>() { false, false, false, false };

		void OnWDown()
		{
			if (!canMove)
				return;
			moveState[0] = true;
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("走路"))
			{
				anim.Play("走路");
			}
		}
		void OnADown()
		{
			if (!canMove)
				return;
			moveState[1] = true;
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("走路"))
			{
				anim.Play("走路");
			}
		}
		void OnSDown()
		{
			if (!canMove)
				return;
			moveState[2] = true;
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("走路"))
			{
				anim.Play("走路");
			}
		}
		void OnDDown()
		{
			if (!canMove)
				return;
			moveState[3] = true;
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("走路"))
			{
				anim.Play("走路");
			}
		}

		void OnWUp()
		{
			if (!canMove)
				return;
			moveState[0] = false;
			if (!moveState[0] && !moveState[1] && !moveState[2] && !moveState[3])
				anim.Play("站立");
		}
		void OnAUp()
		{
			if (!canMove)
				return;
			moveState[1] = false;
			if (!moveState[0] && !moveState[1] && !moveState[2] && !moveState[3])
				anim.Play("站立");
		}
		void OnSUp()
		{
			if (!canMove)
				return;
			moveState[2] = false;
			if (!moveState[0] && !moveState[1] && !moveState[2] && !moveState[3])
				anim.Play("站立");
		}
		void OnDUp()
		{
			if (!canMove)
				return;
			moveState[3] = false;
			if (!moveState[0] && !moveState[1] && !moveState[2] && !moveState[3])
				anim.Play("站立");
		}

		#endregion

		public IEnumerator PlayAnim(string clipName,bool once = true)
		{
			yield return AnimMgr.GetInstance().Play(anim, clipName);
			if(once)
				anim.Play("站立");
		}

		#region 走路

		public IEnumerator Walk(Transform target)
		{
			canMove = false;
			yield return null;
			anim.Play("走路");
			nowTarget = target;
			agent.isStopped = false;
			agent.SetDestination(target.position);
			yield return new WaitForDistance(transform, target);
			anim.Play("站立");
			agent.isStopped = true;
			transform.position = nowTarget.position;
			yield return RotateTo(nowTarget.eulerAngles);
			yield return null;
			canMove = true;
		}
		#endregion

		#region 坐下
		public IEnumerator SitDown(Transform target)
		{
			canMove = false;
			canRotate = false;
			nowTarget = target;
			transform.position = nowTarget.position;
			transform.forward = nowTarget.forward;
			yield return new WaitForEndOfFrame();
			yield return AnimMgr.GetInstance().Play(anim, "坐下");
			transform.position = nowTarget.position;
			transform.forward = nowTarget.forward;
			CameraManager.Instance.StartRoam(CameraManager.Instance.thirdPersonC.transform);
		}
		#endregion

		#region 站起
		public IEnumerator StandUp()
		{
			yield return AnimMgr.GetInstance().Play(anim, "站起");
			yield return CameraManager.Instance.ThirdPerson(transform,2);
			CameraManager.Instance.IsEnable = false;
		}
		#endregion

		#region 慢慢旋转到某个方向
		public IEnumerator RotateTo(Vector3 targetAngle)
		{
			WaitForEndOfFrame wait = new WaitForEndOfFrame();
			Quaternion target = Quaternion.Euler(targetAngle);
			while (Vector3.Distance(transform.eulerAngles, targetAngle) > 3)
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime);
				yield return wait;
			}
			transform.rotation = target;
			yield return null;
		}
		#endregion

		public IEnumerator SetTransform(Transform newTrans)
		{
			agent.enabled = false;
			canMove = false;
			canRotate = false;
			yield return null;
			Vector3 temp = newTrans.position;
			temp.y = transform.position.y;
			transform.position = temp;
			transform.forward = newTrans.forward;
			yield return null;
			anim.Play("站立");
			agent.enabled = true;
			canMove = true;
			canRotate = true;
		}
	}
}

