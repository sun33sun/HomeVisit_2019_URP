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
		Transform parent = Selection.activeGameObject.transform;
		GameObject inputPrefab = Resources.Load<GameObject>("input");
		for (int i = 0; i < parent.childCount; i++)
		{
			GameObject newObj = Instantiate(inputPrefab);
			Transform child = parent.GetChild(i);
			newObj.name = "input" + child.name.Replace("img", "");
			child.transform.SetParent(child);
		}
	}
}
