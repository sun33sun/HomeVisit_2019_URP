using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace HomeVisit.UI
{
	public class ScoreReportData : UIPanelData
	{
		public string strModule = "";
		public DateTime strStart;
		public DateTime strEnd;
		public string strScore = "";
	}
	public partial class ScoreReport : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ScoreReportData ?? new ScoreReportData();

			tmpModule.text = mData.strModule;
			tmpStart.text = mData.strStart.ToString("yyyy-MM-dd HH:mm");
			tmpEnd.text = mData.strEnd.ToString("yyyy-MM-dd HH:mm");
			tmpTotalTime.text = (mData.strStart - mData.strEnd).ToString("%m' min.'");
			tmpScore.text = mData.strScore;
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
