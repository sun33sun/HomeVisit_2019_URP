using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HomeVisit.UI
{
	public class ClassList : MonoBehaviour
	{
		[Header("欢迎老师")]
		public TextMeshProUGUI tmpTeacher;
		[SerializeField] List<TextMeshProUGUI> tmpClassList;
		[SerializeField] List<Button> btnClassList;
		public Button btnPrior;
		public Button btnNext;
		int pageIndex = 0;
		List<string> nowDatas = new List<string>() { "初一一班", "初一二班", "初一三班", "初一四班", "初二一班", "初二二班", "初二三班", "初二四班", "初三一班", "初三二班", "初三三班", "初三四班" };

		private void Start()
		{
			GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
			for (int i = 0; i < btnClassList.Count; i++)
			{
				TextMeshProUGUI tmp = tmpClassList[i];
				int index = i;
				btnClassList[i].onClick.AddListener(() =>
				{
					if (panel.nowTeacherData.classIndex != tmp.text)
					{
						panel.TipElement.Show();
						return;
					}
					gameObject.SetActive(false);
					UIKit.GetPanel<GetInformationPanel>().StudentList.gameObject.SetActive(true);
				});
			}
			btnPrior.onClick.AddListener(Prior);
			btnPrior.interactable = true;
			btnNext.onClick.AddListener(Next);
			btnNext.interactable = true;
			LoadItemsData();
		}

		void Prior()
		{
			if (pageIndex == 0)
				return;
			pageIndex -= 1;
			LoadItemsData();
		}

		void Next()
		{
			//每页10个元素
			if (pageIndex > (nowDatas.Count / (float)btnClassList.Count - 1))
				return;
			pageIndex++;
			LoadItemsData();
		}

		void LoadItemsData()
		{
			for (int i = 0; i < btnClassList.Count; i++)
			{
				int dataIndex = pageIndex * btnClassList.Count + i;
				Button item = btnClassList[i];
				if (dataIndex > nowDatas.Count - 1)
				{
					item.gameObject.SetActive(false);
				}
				else
				{
					item.gameObject.SetActive(true);
					tmpClassList[i].text = nowDatas[dataIndex];
				}
			}
		}
	}

}
