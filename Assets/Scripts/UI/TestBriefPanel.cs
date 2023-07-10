using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public class TestBriefPanelData : UIPanelData
	{
	}
	public partial class TestBriefPanel : UIPanel
	{
		bool IsFirst = true;
		[SerializeField] List<Toggle> togs;
		[SerializeField] List<GameObject> objs;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestBriefPanelData ?? new TestBriefPanelData();
			for (int i = 0; i < togs.Count; i++)
			{
				int index = i;
				togs[i].onValueChanged.AddListener(isOn =>{objs[index].SetActive(isOn);});
			}
			btnClosePanel.onClick.AddListener(() =>
			{
				UIKit.HidePanel<TestBriefPanel>();
			});
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			togs[0].isOn = true;
			objs[0].SetActive(true);
			for (int i = 1; i < togs.Count; i++)
			{
				togs[i].isOn = false;
				objs[i].SetActive(false);
			}
		}

		protected override void OnHide()
		{
			if (IsFirst)
			{
				IsFirst = false;
				UIKit.ShowPanel<GetInformationPanel>();
			}
		}

		protected override void OnClose()
		{
		}
	}
}
