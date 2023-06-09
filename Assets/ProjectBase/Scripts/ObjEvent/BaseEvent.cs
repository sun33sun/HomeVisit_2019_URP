using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectBase
{
	public class BaseEvent : MonoBehaviour
	{
		public bool isDestroy = false;
		public UnityAction OnClick;

		protected virtual void Awake()
		{
			EventManager.Instance.Register(this);
		}

		protected virtual void OnDestroy()
		{
			EventManager.Instance.Unregister(this);
		}

		public virtual void SelfDestroy(bool isInvoke = true)
		{
		}
	}
}
