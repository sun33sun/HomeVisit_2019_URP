using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CustomEditor : Editor
{
	[MenuItem("GameObject/执行代码",false,49)]
	public static void SetName()
	{
		GameObject parent = Selection.activeGameObject;
		//List<Transform> childs = parent.GetComponentsInChildren<Transform>().ToList().FindAll(o=>o.name == "ViewPort");
		//for (int i = childs.Count - 1; i >= 0; i--)
		//{
		//	Debug.Log(childs[i].name);
		//	DestroyImmediate(childs[i].GetComponent<Mask>());
		//}
		//childs.Clear();
		//childs = null;
	}
}
