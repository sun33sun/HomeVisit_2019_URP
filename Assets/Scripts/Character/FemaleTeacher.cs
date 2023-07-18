using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using ProjectBase.Anim;

namespace HomeVisit.Character
{
	public class FemaleTeacher : SingletonMono<FemaleTeacher>
	{
		public Animator anim;
		public Transform face;
		SkinnedMeshRenderer faceSkin;
		public CharacterController cc;
		//public Transform thirdView;
		public NavMeshAgent agent;

		public bool canMove = false;
		public bool canRotate = false;
		//移动速度
		[SerializeField] float moveSpeed = 6.0F;
		//正在转向
		[SerializeField] bool isRotate = false;
		//旋转速度
		[SerializeField] float rotateSpeed;

		public float gravity = 20.0F;
		Vector3 moveDirection = Vector3.zero;

		[SerializeField] Transform nowTarget;

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

			EventCenter.GetInstance().AddEventListener(KeyCode.W + "按下", OnKeyState);
			EventCenter.GetInstance().AddEventListener(KeyCode.S + "按下", OnKeyState);
			EventCenter.GetInstance().AddEventListener(KeyCode.A + "按下", OnKeyState);
			EventCenter.GetInstance().AddEventListener(KeyCode.D + "按下", OnKeyState);

			EventCenter.GetInstance().AddEventListener(KeyCode.W + "抬起", OnKeyUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.A + "抬起", OnKeyUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.S + "抬起", OnKeyUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.D + "抬起", OnKeyUp);
		}

		private void OnDestroy()
		{
			EventCenter.GetInstance().RemoveEventListener("鼠标右键按下", OnMouseRightDown);
			EventCenter.GetInstance().RemoveEventListener("鼠标右键抬起", OnMouseRightUp);
			EventCenter.GetInstance().RemoveEventListener<Vector2>("鼠标滑动", OnMouseSliding);
			MonoMgr.GetInstance().RemoveUpdateListener(MyUpdate);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.W + "保持", OnKeyState);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.S + "保持", OnKeyState);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.A + "保持", OnKeyState);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.D + "保持", OnKeyState);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.W + "抬起", OnKeyUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.S + "抬起", OnKeyUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.A + "抬起", OnKeyUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.D + "抬起", OnKeyUp);
		}

		void MyUpdate()
		{
			if (canMove != cc.enabled)
				cc.enabled = canMove;
			if (!canMove || EventSystem.current.IsPointerOverGameObject())
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
			if (!canRotate || EventSystem.current.IsPointerOverGameObject())
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
			if (!isRotate || !canRotate || EventSystem.current.IsPointerOverGameObject())
			{
				isRotate = false;
				return;
			}

			transform.rotation = Quaternion.AngleAxis(vec2.x, transform.up) * transform.rotation;
		}

		void OnKeyState()
		{
			if (!canMove)
				return;
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("走路"))
			{
				anim.Play("走路");
			}
		}

		void OnKeyUp()
		{
			if (!canMove)
				return;
			anim.Play("站立");
		}

		#endregion

		public IEnumerator PlayAnim(string clipName,bool once = true)
		{
			yield return AnimationManager.GetInstance().Play(anim, clipName);
			if(once)
				anim.Play("站立");
		}

		#region 走路
		public IEnumerator Walk(Transform target)
		{
			agent.enabled = true;
			anim.Play("走路");
			nowTarget = target;
			agent.isStopped = false;
			agent.SetDestination(target.position);
			yield return new WaitForDistance(transform, nowTarget);
			anim.Play("站立");
			agent.isStopped = true;
			transform.position = nowTarget.position;
			yield return RotateTo(nowTarget.eulerAngles);
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
			cc.enabled = false;
			yield return new WaitForEndOfFrame();
			yield return AnimationManager.GetInstance().Play(anim, "坐下");
			CameraManager.Instance.StartRoam(CameraManager.Instance.thirdPersonC.transform);
		}
		#endregion

		#region 站起
		public IEnumerator StandUp()
		{
			yield return AnimationManager.GetInstance().Play(anim, "站起");
			CameraManager.Instance.ThirdPerson(transform);
			cc.enabled = true;
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

		}
		#endregion

		public IEnumerator SetTransform(Transform newTrans)
		{
			canMove = false;
			canRotate = false;
			cc.enabled = false;
			anim.enabled = false;
			agent.enabled = false;
			yield return null;
			transform.position = newTrans.position;
			transform.forward = newTrans.forward;
			yield return null;
			canMove = true;
			canRotate = true;
			cc.enabled = true;
			anim.enabled = true;
			agent.enabled = true;
			anim.Play("站立");
		}
	}
}

