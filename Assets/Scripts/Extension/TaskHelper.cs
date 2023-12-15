using Cysharp.Threading.Tasks;
using HomeVisit.Character;
using HomeVisit.Effect;
using HomeVisit.UI;
using ProjectBase;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.Task
{
	public static class TaskHelper
	{
		public static IEnumerator WaitHighlightClick(this GameObject targetObj, bool haveInnerGlow = true)
		{
			EffectManager.Instance.AddEffectImmediately(targetObj, haveInnerGlow);
			yield return EventManager.Instance.AddObjClick(targetObj);
		}

		public static IEnumerator WaitHighlightEnter(this GameObject obj)
		{
			yield return obj.GetComponent<ObjColliderEvent>().AreaHighlight();
			yield return FemaleTeacher.Instance.PlayAnim("站立", false);
		}

		public static void StartSpeak(NPCIndex npcSprite)
		{
			switch (npcSprite)
			{
				case NPCIndex.母亲:
					StudentMather.Instance.StartCoroutine(StudentMather.Instance.StartSpeak());
					break;
				case NPCIndex.学生1:
				case NPCIndex.学生2:
					StudentController.Instance.StartCoroutine(StudentController.Instance.StartSpeak());
					break;
				case NPCIndex.父亲:
					StudentFather.Instance.StartCoroutine(StudentFather.Instance.StartSpeak());
					break;
			}
		}

		public static void StopSpeak(NPCIndex npcSprite)
		{
			switch (npcSprite)
			{
				case NPCIndex.母亲:
					StudentMather.Instance.StopCoroutine(StudentMather.Instance.StartSpeak());
					break;
				case NPCIndex.学生1:
				case NPCIndex.学生2:
					StudentController.Instance.StopCoroutine(StudentController.Instance.StartSpeak());
					break;
				case NPCIndex.父亲:
					StudentFather.Instance.StopCoroutine(StudentFather.Instance.StartSpeak());
					break;
			}
		}

		public static IEnumerator PlayAudio(string clipName, int spriteIndex, string strWord)
		{
			NPCIndex npcIndex = (NPCIndex)spriteIndex;

			StartSpeak(npcIndex);

			OnVisitPanel onVisitPanel = UIKit.GetPanel<OnVisitPanel>();
			onVisitPanel.AddHistoryDialogue(npcIndex, strWord);
			yield return AudioManager.Instance.Play(clipName);

			StopSpeak(npcIndex);
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
		}
	}
}

