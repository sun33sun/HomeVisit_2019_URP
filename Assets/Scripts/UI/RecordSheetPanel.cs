using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace HomeVisit.UI
{
	public class RecordSheetPanelData : UIPanelData
	{
	}
	public partial class RecordSheetPanel : UIPanel
	{
		DateTime startTime;
		private void Start()
		{
			startTime = DateTime.Now;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as RecordSheetPanelData ?? new RecordSheetPanelData();

			btnClose.onClick.AddListener(Close);
			btnSubmit.onClick.AddListener(Close);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			UIKit.GetPanel<MainPanel>().NextVisitStepPanel();
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		void Close()
		{
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.Show();
			ScoreReportData data = new ScoreReportData()
			{
				title = "·Ãºó¼ÇÂ¼",
				startTime = this.startTime,
				endTime = DateTime.Now,
				score = 2
			};
			testReportPanel.CreateScoreReport(data);
			UIKit.GetPanel<MainPanel>().ShowCompletedTip();
			Hide();
		}
	}
}
