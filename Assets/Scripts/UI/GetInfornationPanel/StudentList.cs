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
		[Header("输入的数据")]
		public TMP_Dropdown dpGrade;
		public TMP_InputField inputKeyword;
		public TMP_InputField inputStudentID;
		public TMP_Dropdown dpBoarding;

		[Header("政策列表相关")]
		public List<StudentItem> items = new List<StudentItem>();
		public List<StudentData> datas;
		List<StudentData> nowDatas;
		public int pageIndex;
		[Header("功能按钮")]
		public Button btnFindAll;
		public Button btnPrior;
		public Button btnNext;

		private void Start()
		{
			StartCoroutine(WebKit.GetInstance().Read<List<StudentData>>(Application.streamingAssetsPath + "/StudentData.json", datas =>
			{
				this.datas = datas;
				nowDatas = datas;
				LoadItemsData();
			}));
			btnPrior.onClick.AddListener(Prior);
			btnNext.onClick.AddListener(Next);
			btnFindAll.onClick.AddListener(FindAll);
			for (int i = 0; i < items.Count; i++)
			{
				StudentItem item = items[i];
				item.GetComponent<Button>().onClick.AddListener(() =>
				{
					gameObject.SetActive(false);
					StudentInformation information = UIKit.GetPanel<GetInfornationPanel>().StudentInformation;
					information.gameObject.SetActive(true);
					gameObject.SetActive(false);
					information.InitData(item.mData);
				});
			}
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
				(inputKeyword.text == "" || data.strStudentName.Contains(inputKeyword.text)) &&
				(inputStudentID.text == "" || data.StudentID == int.Parse(inputStudentID.text)) &&
				data.Boarding == dpBoarding.value
			);
			pageIndex = 0;
			LoadItemsData();
		}
	}
}

