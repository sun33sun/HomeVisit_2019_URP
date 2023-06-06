using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace ProjectBase
{
    /// <summary>
    /// Mono的管理者
    /// 1.声明周期函数
    /// 2.事件 
    /// 3.协程
    /// </summary>
    public class MonoController : MonoBehaviour
    {

        private event UnityAction updateEvent;
        private event UnityAction fixedUpdateEvent;

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (updateEvent != null)
                updateEvent();
        }
		private void FixedUpdate()
		{
            if (fixedUpdateEvent != null)
                fixedUpdateEvent();

        }

		/// <summary>
		/// 给外部提供的 添加帧更新事件的函数
		/// </summary>
		/// <param name="fun"></param>
		public void AddUpdateListener(UnityAction fun)
        {
            updateEvent += fun;
        }

        /// <summary>
        /// 提供给外部 用于移除帧更新事件函数
        /// </summary>
        /// <param name="fun"></param>
        public void RemoveUpdateListener(UnityAction fun)
        {
            updateEvent -= fun;
        }

        public void AddFixedUpdateListener(UnityAction fun)
        {
            fixedUpdateEvent += fun;
        }


        public void RemoveFixedUpdateListener(UnityAction fun)
        {
            fixedUpdateEvent -= fun;
        }

        public void DoDestroy(Object obj)
		{
            Destroy(obj);
		}
    }
}

