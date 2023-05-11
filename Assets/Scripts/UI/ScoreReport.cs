using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class ScoreReportData : UIPanelData
	{
		public string strModule = "";
		public string strStart = "";
		public string strEnd = "";
		public string strTotalTime = "";
		public string strScore = "";
	}
	public partial class ScoreReport : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ScoreReportData ?? new ScoreReportData();

			tmpModule.text = mData.strModule;
			tmpStart.text = mData.strStart;
			tmpEnd.text = mData.strEnd;
			tmpTotalTime.text = mData.strTotalTime;
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
