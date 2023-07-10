using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QFramework;

namespace HomeVisit.UI
{
	public class AdministratorList : MonoBehaviour
	{
		[Header("输入的数据")]
		public TMP_Dropdown dpDistrict;
		public InputField inputName;
		public TMP_Dropdown dpRole;
		public InputField inputID;

		[Header("管理员列表相关")]
		public List<AdministratorItem> items = new List<AdministratorItem>();
		public List<TeacherData> datas;
		List<TeacherData> nowDatas;
		TeacherData rightData;
		public int pageIndex;
		[Header("功能按钮")]
		public Button btnFindAll;
		public Button btnPrior;
		public Button btnNext;

		private void Start()
		{
			StartCoroutine(WebKit.GetInstance().Read<List<TeacherData>>(Application.streamingAssetsPath + "/AdministratorData.json", datas =>
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
				AdministratorItem item = items[i];
				item.GetComponent<Button>().onClick.AddListener(() =>
				{
					gameObject.SetActive(false);
					UIKit.GetPanel<GetInformationPanel>().PolicyList.gameObject.SetActive(true);
				});
			}
		}


		private void OnEnable()
		{
			GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
			if (panel != null && panel.nowAdministrator != null)
			{
				rightData = panel.nowAdministrator;
				if (!datas.Contains(rightData))
					datas.Add(rightData);
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
				AdministratorItem item = items[i];
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
				(data.strName.Contains(inputName.text) || inputName.text.Equals("") || data.strName.Equals(inputName.text)) &&
				data.role == dpRole.value &&
				(data.id.Equals(inputID.text) || inputID.text.Equals(""))
			);
			pageIndex = 0;
			LoadItemsData();
		}

		public void SaveRightData(TeacherData addData)
		{
			rightData = addData;
			datas.Add(addData);
			LoadItemsData();
		}
	}
}

