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
		public DateTime startTime = default(DateTime);
		public DateTime endTime = default(DateTime);
		public TimeSpan expectTime = new TimeSpan(0,5,0);
		public int maxScore = 0;
		public int score = 0;
	}
	public partial class ScoreReport : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ScoreReportData ?? new ScoreReportData();

			tmpModule.text = mData.title;
			tmpStart.text = "¿ªÊ¼£º"+mData.startTime.ToString("MM-dd HH:mm");
			tmpEnd.text = "½áÊø£º"+ mData.endTime.ToString("MM-dd HH:mm");
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
