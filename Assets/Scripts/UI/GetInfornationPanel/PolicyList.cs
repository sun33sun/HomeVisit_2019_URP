using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QFramework;

namespace HomeVisit.UI
{
	public class PolicyList : MonoBehaviour
	{
		[Header("输入的数据")]
		public TMP_Dropdown dpDistrict;
		public TMP_Dropdown dpPolicyType;
		public InputField inputKeyword;
		public TMP_Dropdown dpPeriod;

		[Header("政策列表相关")]
		public List<PolicyItem> items = new List<PolicyItem>();
		public List<PolicyData> datas;
		List<PolicyData> nowDatas;
		public int pageIndex;
		[Header("功能按钮")]
		public Button btnFindAll;
		public Button btnPrior;
		public Button btnNext;

		private void Start()
		{
			StartCoroutine(WebKit.GetInstance().Read<List<PolicyData>>(Application.streamingAssetsPath + "/PolicyData.json", datas =>
			{
				this.datas = datas;
				nowDatas = datas;
				LoadItemsData();
			}));
			btnPrior.onClick.AddListener(Prior);
			btnNext.onClick.AddListener(Next);
			btnFindAll.onClick.AddListener(FindAll);
			GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
			for (int i = 0; i < items.Count; i++)
			{
				PolicyItem item = items[i];
				item.GetComponent<Button>().onClick.AddListener(() =>
				{
					gameObject.SetActive(false);
					panel.SchoolList.gameObject.SetActive(true);
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
				PolicyItem item = items[i];
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
				data.strPolicyType.Equals(dpPolicyType.options[dpPolicyType.value].text) &&
				(data.strBanner.Contains(inputKeyword.text) || inputKeyword.text.Equals("") || data.strBanner.Equals(inputKeyword.text)) &&
				data.strPeriod.Equals(dpPeriod.options[dpPeriod.value].text)
			);
			pageIndex = 0;
			LoadItemsData();
		}
	}
}

