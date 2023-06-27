using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using QFramework;
using ProjectBase;
using UnityEngine.EventSystems;

namespace ProjectBase
{
	public class CameraManager : SingletonMono<CameraManager>, IActionController
	{
		List<IActionController> acList = new List<IActionController>();

		[SerializeField] float horizontalSpeed = 3;
		[SerializeField] float verticalSpeed = 2;
		[SerializeField] float rotateSpeed = 3;
		[SerializeField] float mouseScrollWheelSpeed = 10;
		[SerializeField] [Range(0, 1)] float drag = 0.8f;


		[SerializeField] Camera mainC;
		[SerializeField] CinemachineVirtualCamera roamC = null;
		[SerializeField] CinemachineVirtualCamera followC = null;
		[SerializeField] CinemachineVirtualCamera thirdPersonC = null;

		//��������ĸ���
		Rigidbody roamRig = null;

		Vector3 originPos;
		Vector3 originAngle;
		float originFieldOfView;
		Vector3 nowPos;
		[SerializeField] bool isEnable = false;
		public bool IsEnable
		{
			get
			{
				return isEnable && !EventSystem.current.IsPointerOverGameObject();
			}
			set
			{
				if (value)
					roamRig.constraints = RigidbodyConstraints.FreezeRotation;
				else
					roamRig.constraints = RigidbodyConstraints.FreezeAll;
				isEnable = value;
			}
		}

		public ulong ActionID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public IAction Action { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public bool Paused { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		bool isRotate = false;

		private void Start()
		{
			//�������
			roamRig = roamC.GetComponent<Rigidbody>();
			//��¼��ʼλ��
			originPos = roamC.transform.position;
			originAngle = roamC.transform.rotation.eulerAngles;
			originFieldOfView = roamC.m_Lens.FieldOfView;

			InputMgr.GetInstance().ChangerInput(true);
			EventCenter.GetInstance().AddEventListener(KeyCode.W + "����", OnWState);
			EventCenter.GetInstance().AddEventListener(KeyCode.A + "����", OnAState);
			EventCenter.GetInstance().AddEventListener(KeyCode.S + "����", OnSState);
			EventCenter.GetInstance().AddEventListener(KeyCode.D + "����", OnDState);

			EventCenter.GetInstance().AddEventListener(KeyCode.W + "̧��", OnWUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.A + "̧��", OnAUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.S + "̧��", OnSUp);
			EventCenter.GetInstance().AddEventListener(KeyCode.D + "̧��", OnDUp);

			EventCenter.GetInstance().AddEventListener("����Ҽ�����", OnMouseRightDown);
			EventCenter.GetInstance().AddEventListener("����Ҽ�̧��", OnMouseRightUp);
			EventCenter.GetInstance().AddEventListener<Vector2>("��껬��", OnMouseSliding);

			EventCenter.GetInstance().AddEventListener(KeyCode.LeftControl + "����", OnLeftControlState);

			EventCenter.GetInstance().AddEventListener(KeyCode.Space + "����", OnSpaceState);

			EventCenter.GetInstance().AddEventListener<float>("������", OnMouseScrollWheel);
		}

		public void SetRoamRig(RigidbodyConstraints newState)
		{
			roamRig.constraints = newState;
		}
		public void SetRoamPos(Vector3 newPos)
		{
			roamC.transform.position = newPos;
		}

		public void SetRoamForward(Vector3 newForward)
		{
			roamC.transform.forward = newForward;
		}


		#region ������Ӧ�¼�
		private void OnWState()
		{
			if (!IsEnable)
				return;
			roamRig.velocity = roamC.transform.forward * horizontalSpeed;
		}

		private void OnAState()
		{
			if (!IsEnable)
				return;
			roamRig.velocity = roamC.transform.right * -horizontalSpeed;
		}

		private void OnSState()
		{
			if (!IsEnable)
				return;
			roamRig.velocity = roamC.transform.forward * -horizontalSpeed;
		}

		private void OnDState()
		{
			if (!IsEnable)
				return;
			roamRig.velocity = roamC.transform.right * horizontalSpeed;
		}

		private void OnWUp()
		{
			if (!IsEnable)
				return;
			roamRig.velocity -= roamC.transform.forward * horizontalSpeed * drag;
		}

		private void OnAUp()
		{
			if (!IsEnable)
				return;
			roamRig.velocity -= roamC.transform.right * -horizontalSpeed * drag;
		}

		private void OnSUp()
		{
			if (!IsEnable)
				return;
			roamRig.velocity -= roamC.transform.forward * -horizontalSpeed * drag;
		}

		private void OnDUp()
		{
			if (!IsEnable)
				return;
			roamRig.velocity -= roamC.transform.right * horizontalSpeed * drag;
		}

		private void OnMouseRightDown()
		{
			if (!IsEnable)
				return;
			isRotate = true;
		}
		private void OnMouseRightUp()
		{
			if (!IsEnable)
				return;
			isRotate = false;
		}
		private void OnMouseSliding(Vector2 vec2)
		{
			if (!isRotate || !IsEnable)
				return;
			roamC.transform.RotateAround(roamC.transform.position, Vector3.up, vec2.x * rotateSpeed);
		}
		private void OnLeftControlState()
		{
			if (!IsEnable)
				return;
			nowPos = roamC.transform.localPosition;
			nowPos.y -= verticalSpeed * Time.fixedDeltaTime;
			roamC.transform.localPosition = nowPos;
		}
		private void OnSpaceState()
		{
			if (!IsEnable)
				return;
			nowPos = roamC.transform.localPosition;
			nowPos.y += verticalSpeed * Time.fixedDeltaTime;
			roamC.transform.localPosition = nowPos;
		}
		private void OnMouseScrollWheel(float distance)
		{
			roamC.m_Lens.FieldOfView += distance * mouseScrollWheelSpeed;
			if (roamC.m_Lens.FieldOfView < 1)
				roamC.m_Lens.FieldOfView = 1;
			else if (roamC.m_Lens.FieldOfView > 90)
				roamC.m_Lens.FieldOfView = 90;
		}
		#endregion

		public void BackToOrigin()
		{
			for (int i = acList.Count - 1; i > -1; i--)
			{
				acList[i].Deinit();
				acList.RemoveAt(i);
			}
			StopAllCoroutines();
			StartCoroutine(MoveToAsync(originPos));
			roamC.m_Lens.FieldOfView = originFieldOfView;

			roamC.Priority = 12;
			followC.Priority = 11;
			followC.Follow = null;
		}

		IEnumerator MoveToAsync(Vector3 targetPos)
		{
			WaitForEndOfFrame wait = new WaitForEndOfFrame();
			while (Vector3.Distance(targetPos, roamC.transform.position) > 0.1f)
			{
				Vector3 dir = Vector3.Normalize(targetPos - roamC.transform.position) * Time.deltaTime * 10;
				roamRig.MovePosition(roamC.transform.position + dir);
				yield return wait;
			}
		}

		public void Follow(Transform target)
		{
			if (target != null)
			{
				followC.Follow = target;
				ActionKit.Delay(1, () =>
				 {
					 followC.Priority = 12;
					 roamC.Priority = 11;
				 });
			}
			else
			{
				followC.Follow = null;
				roamC.Priority = 12;
				followC.Priority = 11;
			}
		}


		public void Reset()
		{
		}

		public void Deinit()
		{
			throw new NotImplementedException();
		}

		private void OnDestroy()
		{
			EventCenter.GetInstance().RemoveEventListener(KeyCode.W + "����", OnWState);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.A + "����", OnAState);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.S + "����", OnSState);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.D + "����", OnDState);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.W + "̧��", OnWUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.A + "̧��", OnAUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.S + "̧��", OnSUp);
			EventCenter.GetInstance().RemoveEventListener(KeyCode.D + "̧��", OnDUp);

			EventCenter.GetInstance().RemoveEventListener("����Ҽ�����", OnMouseRightDown);
			EventCenter.GetInstance().RemoveEventListener("����Ҽ�̧��", OnMouseRightUp);
			EventCenter.GetInstance().RemoveEventListener<Vector2>("��껬��", OnMouseSliding);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.LeftControl + "����", OnLeftControlState);

			EventCenter.GetInstance().RemoveEventListener(KeyCode.Space + "����", OnSpaceState);

			EventCenter.GetInstance().RemoveEventListener<float>("������", OnMouseScrollWheel);
		}

		public void ThirdPerson(Transform target)
		{
			if (target == null)
			{
				thirdPersonC.transform.SetParent(transform, false);
				roamC.Priority = 13;
				thirdPersonC.Priority = 12;
				followC.Priority = 11;
			}
			else
			{
				thirdPersonC.transform.SetParent(target, false);
				thirdPersonC.Priority = 13;
				roamC.Priority = 12;
				followC.Priority = 11;
			}
		}

		public WaitForSeconds ThirdPerson(Transform target, float waitTime)
		{
			if (target == null)
			{
				thirdPersonC.transform.SetParent(transform, false);
				roamC.Priority = 13;
				thirdPersonC.Priority = 12;
				followC.Priority = 11;
			}
			else
			{
				thirdPersonC.transform.SetParent(target, false);
				thirdPersonC.Priority = 13;
				roamC.Priority = 12;
				followC.Priority = 11;
			}
			return new WaitForSeconds(waitTime);
		}
	}

}
