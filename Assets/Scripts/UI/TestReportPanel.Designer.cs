using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:b55c1a7a-df4a-40cb-9fe8-cd7ec4ad1fab
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
		public TMPro.TextMeshProUGUI tmpRoute;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTestEvaluate;
		[SerializeField]
		public UnityEngine.RectTransform objScreenshot;
		[SerializeField]
		public UnityEngine.UI.RawImage imgScreenshot;
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
			tmpRoute = null;
			tmpTestEvaluate = null;
			objScreenshot = null;
			imgScreenshot = null;
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
