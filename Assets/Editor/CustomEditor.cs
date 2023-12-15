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
	public static void ExecuteScript()
	{
		Transform parent = Selection.activeGameObject.transform;
		Transform[] childs = parent.GetComponentsInChildren<Transform>(true);
		Transform FurnitureParent = childs.First(c => c.name.Equals("FurnitureParent"));
		Transform[] furnitures = childs.Where(c=>c.tag.Equals("Furniture")).ToArray();
		foreach (Transform t in furnitures)
		{
			t.gameObject.SetActive(false);
			t.SetParent(FurnitureParent);
		}
	}
}
