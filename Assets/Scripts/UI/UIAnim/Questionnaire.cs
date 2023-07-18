using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using ProjectBase;
using QFramework;
using HomeVisit.UI;

namespace HomeVisit
{
	public class Questionnaire : MonoBehaviour
	{
		[SerializeField] Image imgScenarioCases;
		[SerializeField] List<Button> btns;
		[SerializeField] Image[] imgs;
		int selectedIndex = 0;
		List<string> sceneName = new List<string>() { "传统", "多孩", "单亲" };
		List<string> studentName = new List<string>() { "张守光", "林光美", "秦彦威" };
		[SerializeField] Image imgBuildTip;
		[SerializeField] Button btnCloseBuildTip;
		[SerializeField] Sprite[] sprites;

		public string SceneName { get { return sceneName[selectedIndex]; } }
		public string StudentName { get { return studentName[selectedIndex]; } }

		private void OnEnable()
		{
			imgBuildTip.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			UIKit.ShowPanel<TopPanel>();
		}

		void Start()
		{
			//有对应场景的按钮
			for (int i = 0; i < 3; i++)
			{
				int index = i;
				btns[i].GetComponent<DoubleClickEvent>().OnDoubleClick += () =>
				{
					imgs[selectedIndex].sprite = sprites[0];
					selectedIndex = index;
					imgs[selectedIndex].sprite = sprites[1];
				};
			}
			//没有对应场景的按钮
			for (int i = 3; i < btns.Count; i++)
			{
				btns[i].GetComponent<DoubleClickEvent>().OnDoubleClick += () => { imgBuildTip.gameObject.SetActive(true); };
			}
			btnCloseBuildTip.onClick.AddListener(() => { imgBuildTip.gameObject.SetActive(false); });
		}
	}
}


