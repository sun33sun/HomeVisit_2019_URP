using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:e8bea18c-578a-4d88-b4e4-ede949e5c375
	public partial class TestReportPanel
	{
		public const string Name = "TestReportPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgBk;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpDate;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTotalScore;
		[SerializeField]
		public RectTransform Grid;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTestEvaluate;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		
		private TestReportPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgBk = null;
			btnClose = null;
			tmpDate = null;
			tmpTotalScore = null;
			Grid = null;
			tmpTestEvaluate = null;
			btnSubmit = null;
			
			mData = null;
		}
		
		public TestReportPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TestReportPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TestReportPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
