using UnityEditor.UI;
using UnityEngine;
using UnityEditor;

namespace HomeVisit.UI
{
	[UnityEditor.CustomEditor(typeof(MyScrollRect), true)]
	[CanEditMultipleObjects]
	public class MyScrollRectEditor : ScrollRectEditor
	{
		SerializedProperty mouseButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			mouseButton = serializedObject.FindProperty("mouseButton");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			serializedObject.Update();
			EditorGUILayout.PropertyField(mouseButton);
			serializedObject.ApplyModifiedProperties();
		}
	}

}

