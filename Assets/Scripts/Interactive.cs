using HomeVisit.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
	#region 单例模式
	static Interactive instance;
	public static Interactive Instance { get { return instance; } }
	#endregion
	[SerializeField] List<GameObject> objList;

	private void Awake()
	{
		instance = this;
	}

	public static T Get<T>(string objName)
	{
		GameObject obj = instance.objList.Find(o => o.name.Equals(objName));
		return obj.GetComponent<T>();
	}

	public static GameObject Get(string objName)
	{
		return instance.objList.Find(o => o.name.Equals(objName));
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}

