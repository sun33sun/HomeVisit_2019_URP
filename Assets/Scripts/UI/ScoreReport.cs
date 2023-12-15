using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace HomeVisit.UI
{
	public class ScoreReportData : UIPanelData
	{
		public int seq = -1;
		public string title = null;
		public DateTime startTime = DateTime.UtcNow;
		public DateTime endTime = DateTime.UtcNow;
		public TimeSpan expectTime = new TimeSpan(0, 5, 0);
		public int maxScore = 0;
		public int score = 0;
	}
	public partial class ScoreReport : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ScoreReportData ?? new ScoreReportData();

			DateTime startTime = mData.startTime;
			DateTime endTime = mData.endTime;
			tmpModule.text = mData.title;
			tmpStart.text = "开始：" + startTime.ToLocalTime().ToString("MM-dd HH:mm");
			tmpEnd.text = "结束：" + endTime.ToLocalTime().ToString("MM-dd HH:mm");
			tmpTotalTime.text = (mData.startTime - mData.endTime).ToString(@"mm\:ss");
			tmpScore.text = mData.score.ToString();
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
	}
}
