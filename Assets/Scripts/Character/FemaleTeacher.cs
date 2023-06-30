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
		public Animation anim;
		public Transform face;
		public CharacterController cc;
		public Transform thirdView;
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

		private void Start()
		{
			EventCenter.GetInstance().AddEventListener("鼠标右键按下", OnMouseRightDown);
			EventCenter.GetInstance().AddEventListener("鼠标右键抬起", OnMouseRightUp);
			EventCenter.GetInstance().AddEventListener<Vector2>("鼠标滑动", OnMouseSliding);
		}
		private void Update()
		{
			if (!canMove || EventSystem.current.IsPointerOverGameObject())
				return;
			if (cc.isGrounded)
			{
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= moveSpeed;
			}
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
			transform.RotateAround(transform.position, Vector3.up, vec2.x * rotateSpeed);
		}

		#endregion

		public WaitUntil PlayAnim(string clipName)
		{
			if (anim[clipName] == null)
			{
				print("播放 : " + clipName);
				return null;
			}
			return AnimationManager.GetInstance().Play(anim, clipName);
		}

		#region 走路
		public WaitUntil Walk(Transform target)
		{
			nowTarget = target;
			agent.isStopped = false;
			agent.SetDestination(target.position);
			return new WaitUntil(OnWalkCompleted);
		}

		bool OnWalkCompleted()
		{
			if (nowTarget == null)
			{
				Debug.LogWarning("StudentMather的nowTarget为null");
				anim.Stop();
				return true;
			}
			if (Vector3.Distance(nowTarget.position, transform.position) < 1.5f)
			{
				if (!isRotating)
				{
					anim.Stop();
					agent.isStopped = true;
					transform.position = nowTarget.position;
					isRotating = true;
					rotationCompleted = false;
					StartCoroutine(RotateTo(nowTarget.eulerAngles));
				}
				return rotationCompleted;
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
			nowTarget = target;
			transform.position = nowTarget.position;
			anim.Play("SitDown");
			return new WaitUntil(OnSitDownCompleted);
		}

		bool OnSitDownCompleted()
		{
			if (!anim.isPlaying)
			{
				if (!isRotating)
				{
					isRotating = true;
					rotationCompleted = false;
					StartCoroutine(RotateTo(nowTarget.eulerAngles));
				}
			}
			return !anim.isPlaying && rotationCompleted;
		}
		#endregion

		#region 慢慢旋转到某个方向
		bool isRotating = false;
		bool rotationCompleted = false;
		public IEnumerator RotateTo(Vector3 targetAngle)
		{
			WaitForEndOfFrame wait = new WaitForEndOfFrame();
			while (Vector3.Distance(transform.eulerAngles, targetAngle) > 3)
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngle), Time.deltaTime);
				yield return wait;
			}
			transform.rotation = Quaternion.Euler(targetAngle);
			rotationCompleted = true;
			isRotating = false;
		}
		#endregion
	}
}

