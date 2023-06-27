using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase
{
	public class EventManager : SingletonMono<EventManager>
	{
		List<BaseEvent> eventList = new List<BaseEvent>();

		public void AddObjClick(GameObject newObj, UnityAction callBack)
		{
			BaseEvent baseEvent = newObj.GetComponent<BaseEvent>();
			if (baseEvent == null)
				baseEvent = newObj.AddComponent<ObjClickEvent>();
			eventList.Add(baseEvent);
			baseEvent.OnClick += callBack;
		}

		public WaitUntil AddObjClick(GameObject newObj)
		{
			BaseEvent baseEvent = newObj.GetComponent<BaseEvent>();
			if (baseEvent == null)
			{
				baseEvent = newObj.AddComponent<ObjClickEvent>();
			}
			else
			{
				Debug.LogWarning($"{newObj.name} : {newObj.GetInstanceID()}\t\t有重复的回调事件：");
			}
			return new WaitUntil(() => { return baseEvent.isDestroy; });
		}

		public void Register(BaseEvent newEvent)
		{
			Instance.eventList.Add(newEvent);
		}

		public void Unregister(BaseEvent newEvent)
		{
			Instance.eventList.Remove(newEvent);
		}

		public void Clear()
		{
			for (int i = eventList.Count -1; i >= 0; i--)
			{
				Destroy(eventList[i]);
				eventList.RemoveAt(i);
			}
			eventList.Clear();
		}
	}
}
