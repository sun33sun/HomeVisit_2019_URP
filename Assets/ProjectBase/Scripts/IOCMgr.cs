using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBase
{
    public class IOCMgr : Singleton<IOCMgr>
    {
        //类型与对应实例的字典
        private Dictionary<Type, object> _typeInstanceDic = new Dictionary<Type, object>();

        public T Get<T>() where T : class
        {
            Type t = typeof(T);
            object obj = null;
            _typeInstanceDic.TryGetValue(t, out obj);
            if (obj != null)
                return obj as T;
            else
                return null;
        }

        public void Set(object obj)
        {
            _typeInstanceDic[obj.GetType()] = obj;
        }

        public void Remove<T>() where T : class
        {
            Type t = typeof(T);
            if(_typeInstanceDic.ContainsKey(t))
                _typeInstanceDic.Remove(t);
        }

        public void Clear()
        {
            _typeInstanceDic.Clear();
        }
    }
}