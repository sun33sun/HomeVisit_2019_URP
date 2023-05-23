using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:de1eb9bb-2544-498d-bf7f-55e5013a7295
	public partial class ScoreReport
	{
		public const string Name = "ScoreReport";
		
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
		
		private ScoreReportData mPrivateData = null;
		
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
