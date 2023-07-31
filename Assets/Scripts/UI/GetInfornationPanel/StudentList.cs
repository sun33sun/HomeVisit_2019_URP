using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QFramework;

namespace HomeVisit.UI
{
	public class StudentList : MonoBehaviour
	{
		[Header("欢迎老师")]
		public TextMeshProUGUI tmpTeacher;
		[Header("输入的数据")]
		public InputField inputKeyword;
		public InputField inputStudentID;
		public TMP_Dropdown dpBoarding;

		[Header("政策列表相关")]
		public List<StudentItem> items = new List<StudentItem>();
		public List<NewStudentData> datas;
		List<NewStudentData> nowDatas;
		public int pageIndex;
		[Header("功能按钮")]
		public Button btnFindAll;
		public Button btnPrior;
		public Button btnNext;

		private void Start()
		{
			StartCoroutine(WebKit.GetInstance().Read<List<NewStudentData>>(Application.streamingAssetsPath + "/NewStudentData.json", datas =>
			{
				GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
				this.datas = datas;
				nowDatas = datas;
				LoadItemsData();
				btnPrior.onClick.AddListener(Prior);
				btnNext.onClick.AddListener(Next);
				btnFindAll.onClick.AddListener(FindAll);
				btnFindAll.interactable = true;
				btnNext.interactable = true;
				btnPrior.interactable = true;
				for (int i = 0; i < items.Count; i++)
				{
					StudentItem item = items[i];
					Button btn = item.GetComponent<Button>();
					btn.onClick.AddListener(() =>
					{
						panel.InformationSecurity.gameObject.SetActive(true);
						StudentInformation1 information1 = panel.StudentInformation1;
						information1.gameObject.SetActive(true);
						information1.InitData(item.mData);
						gameObject.SetActive(false);
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
			//每页元素数量
			if (pageIndex > (nowDatas.Count / (float)items.Count - 1))
				return;
			pageIndex++;
			LoadItemsData();
		}

		void LoadItemsData()
		{
			for (int i = 0; i < items.Count; i++)
			{
				int dataIndex = pageIndex * items.Count + i;
				StudentItem item = items[i];
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
				(inputKeyword.text == "" || data.name.Contains(inputKeyword.text)) &&
				(inputStudentID.text == "" || data.id == inputStudentID.text) &&
				dpBoarding.options[dpBoarding.value].text == "寄宿制"
			);
			pageIndex = 0;
			LoadItemsData();
		}
	}
}

