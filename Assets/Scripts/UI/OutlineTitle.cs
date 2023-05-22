using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace HomeVisit.UI
{
	public class OutlineTitleData : UIPanelData
	{
		public string strTitleDescribe;
		public List<string> strTogList;
	}
	public partial class OutlineTitle : UIPanel, ITitle
	{
		[SerializeField] GameObject togPrefab;
		List<Toggle> togList = new List<Toggle>();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as OutlineTitleData ?? new OutlineTitleData();

			titleDescribe.text = mData.strTitleDescribe;

			CommunicateOutlinePanel communicateOutlinePanel = UIKit.GetPanel<CommunicateOutlinePanel>();

			for (int i = 0; i < mData.strTogList.Count; i++)
			{
				Toggle newTog = Instantiate(togPrefab).GetComponent<Toggle>();
				newTog.name = "tog" + i;
				int index = i;
				newTog.transform.GetComponentInChildren<TextMeshProUGUI>().text = mData.strTogList[index];
				newTog.transform.SetParent(transform);

				newTog.onValueChanged.AddListener((isOn) =>
				{
					if (isOn)
					{
						if (togList.Count > 3)
							togList[0].isOn = false;
						togList.Add(newTog);
						communicateOutlinePanel.strTogSelected.Add(mData.strTogList[index]);
					}
					else
					{
						togList.Remove(newTog);
						communicateOutlinePanel.strTogSelected.Remove(mData.strTogList[index]);
					}
				});
			}
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		public ScoreReportData GetScoreReportData()
		{
			throw new System.NotImplementedException();
		}
	}
}
