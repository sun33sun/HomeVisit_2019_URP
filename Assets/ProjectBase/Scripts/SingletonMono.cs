using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBase
{
	public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
	{
		#region 单例
		private static T instance;
		public static T Instance { get { return instance; } }
		public bool NeedDestroy = false;
		#endregion

		protected virtual void Awake()
		{
			if (instance == null)
			{
				instance = (T)this;
				if(!NeedDestroy)
					DontDestroyOnLoad(instance);
			}
			else
			{
				Debug.LogWarning("有重复单例：" + typeof(T));
			}
		}

		protected virtual void OnDestory()
		{
			if (instance == this)
				instance = null;
		}
	}
}
