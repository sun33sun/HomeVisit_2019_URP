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

			btnClose.onClick.AddListener(UnloadOnVisit);
			btnSubmit.onClick.AddListener(UnloadOnVisit);
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

		void UnloadOnVisit()
		{
			StartCoroutine(UnloadOnVisitAsync());
		}

		IEnumerator UnloadOnVisitAsync()
		{
			TopPanel topPanel = UIKit.GetPanel<TopPanel>();
			yield return topPanel.CloseEyeAnim();
			AsyncOperation unload = SceneManager.UnloadSceneAsync(Settings.OldRandomScene);
			yield return unload;
			UIKit.GetPanel<MainPanel>().SetBK(false);
			UIKit.ShowPanel<TestReportPanel>();
			ScoreReportData data = new ScoreReportData()
			{
				title = "·Ãºó¼ÇÂ¼",
				startTime = this.startTime,
				endTime = DateTime.Now,
				score = 2
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
			MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
			mainPanel.SetBK(true);
			mainPanel.ShowCompletedTip();
			Hide();
			topPanel.StartCoroutine(topPanel.OpenEyeAnim());
		}
	}
}
