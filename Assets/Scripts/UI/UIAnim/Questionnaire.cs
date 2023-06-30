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
		[SerializeField] TextMeshProUGUI tmpBigQuestionnaire;
		public DoubleClickEvent btnBigQuestionnaire;
		[SerializeField] Image imgScenarioCases;
		[SerializeField] List<Button> btns;
		[SerializeField] List<TextMeshProUGUI> tmps;
		int selectedIndex = 0;
		List<string> studentName = new List<string>() { "传统", "多孩", "单亲" };
		[SerializeField] Image imgBuildTip;
		[SerializeField] Button btnCloseBuildTip;
		public string StudentName { get { return studentName[selectedIndex]; } }

		private void OnEnable()
		{
			imgBuildTip.gameObject.SetActive(false);
			btnBigQuestionnaire.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			UIKit.ShowPanel<ButtonPanel>();
		}

		void Start()
		{
			//有对应场景的按钮
			for (int i = 0; i < 3; i++)
			{
				int index = i;
				btns[i].GetComponent<DoubleClickEvent>().OnDoubleClick += () =>
				{
					selectedIndex = index;
					tmpBigQuestionnaire.text = tmps[index].text;
					btnBigQuestionnaire.gameObject.SetActive(true);
					UIKit.HidePanel<ButtonPanel>();
				};
			}
			//没有对应场景的按钮
			for (int i = 3; i < btns.Count; i++)
			{
				btns[i].GetComponent<DoubleClickEvent>().OnDoubleClick += () => { imgBuildTip.gameObject.SetActive(true); };
			}
			btnCloseBuildTip.onClick.AddListener(() => { imgBuildTip.gameObject.SetActive(false); });

			btnBigQuestionnaire.OnDoubleClick += () =>
			{
				UIKit.ShowPanel<ButtonPanel>();
				btnBigQuestionnaire.gameObject.SetActive(false);
			};
		}
	}
}


