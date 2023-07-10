using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:d032b137-2f40-47e8-9ac0-54ded381af0c
	public partial class ScoreReport
	{
		public const string Name = "imgScoreBk";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpModule;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpStart;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpEnd;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTotalTime;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpScore;
		
		private ScoreReportData mPrivateData = new ScoreReportData();
		
		protected override void ClearUIComponents()
		{
			tmpModule = null;
			tmpStart = null;
			tmpEnd = null;
			tmpTotalTime = null;
			tmpScore = null;
			
			mData = null;
		}
		
		public ScoreReportData Data
		{
			get
			{
				return mData;
			}
		}
		
		ScoreReportData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ScoreReportData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
