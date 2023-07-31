using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QFramework;

namespace HomeVisit.UI
{
	public class SchoolList : MonoBehaviour
	{
		[Header("欢迎老师")]
		public TextMeshProUGUI tmpTeacher;
		[Header("输入的数据")]
		public TMP_Dropdown dpDistrict;
		public InputField inputKeyword;
		public TMP_Dropdown dpPeriod;
		public TMP_Dropdown dpBoarding;

		[Header("政策列表相关")]
		public List<SchoolItem> items = new List<SchoolItem>();
		public List<SchoolData> datas;
		List<SchoolData> nowDatas;
		public int pageIndex;
		[Header("功能按钮")]
		public Button btnFindAll;
		public Button btnPrior;
		public Button btnNext;

		private void Start()
		{
			StartCoroutine(WebKit.GetInstance().Read<List<SchoolData>>(Application.streamingAssetsPath + "/SchoolData.json", datas =>
			{
				GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
				this.datas = datas.FindAll(d=>d.strDistrict.Substring(0,2) == panel.nowTeacherData.strUnit.Substring(0,2));
				nowDatas = this.datas;
				LoadItemsData();
				btnPrior.onClick.AddListener(Prior);
				btnNext.onClick.AddListener(Next);
				btnFindAll.onClick.AddListener(FindAll);
				btnFindAll.interactable = true;
				btnNext.interactable = true;
				btnPrior.interactable = true;
			
				for (int i = 0; i < items.Count; i++)
				{
					SchoolItem item = items[i];
					Button btn = item.GetComponent<Button>();
					btn.onClick.AddListener(() =>
					{
						if (panel.nowTeacherData.strUnit != item.tmpSchoolName.text)
						{
							panel.TipElement.Show();
							return;
						}
						gameObject.SetActive(false);
						panel.ClassList.gameObject.SetActive(true);
						panel.InformationSecurity.gameObject.SetActive(true);
					});
					btn.interactable = true;
				}
			}));
			
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
			if (pageIndex > (nowDatas.Count / 10f - 1))
				return;
			pageIndex++;
			LoadItemsData();
		}

		void LoadItemsData()
		{
			for (int i = 0; i < items.Count; i++)
			{
				int dataIndex = pageIndex * 10 + i;
				SchoolItem item = items[i];
				if (dataIndex > nowDatas.Count - 1)
				{
					item.gameObject.SetActive(false);
				}
				else
				{
					item.gameObject.SetActive(true);
					item.InitData(nowDatas[dataIndex]);
				}
			}
		}

		void FindAll()
		{
			nowDatas = datas.FindAll(data =>
				data.strDistrict.Equals(dpDistrict.options[dpDistrict.value].text) &&
				(inputKeyword.text.Equals("") || data.strSchoolName.Contains(inputKeyword.text) || data.strSchoolName.Equals(inputKeyword.text)) &&
				data.strPeriod.Equals(dpPeriod.options[dpPeriod.value].text) &&
				data.Boarding == dpBoarding.value
			);
			pageIndex = 0;
			LoadItemsData();
		}
	}
}

