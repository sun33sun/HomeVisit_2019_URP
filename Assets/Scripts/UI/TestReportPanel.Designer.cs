using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:35c12f60-be92-49f2-ba1b-9fb6468217e1
	public partial class TestReportPanel
	{
		public const string Name = "TestReportPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpDate;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTotalScore;
		[SerializeField]
		public UnityEngine.RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		
		private TestReportPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnClose = null;
			tmpDate = null;
			tmpTotalScore = null;
			Content = null;
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
