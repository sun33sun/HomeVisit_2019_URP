using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:5183559f-9537-41f8-a2ac-c307cd799085
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
		public RectTransform Content;
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
			Content = null;
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
