using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:2fe5d7eb-078d-4dd9-bf60-a280f617f97c
	public partial class ButtonPanel
	{
		public const string Name = "ButtonPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTip;
		[SerializeField]
		public UnityEngine.UI.Button btnSeting;
		[SerializeField]
		public UnityEngine.UI.Button btnKnowledge;
		[SerializeField]
		public UnityEngine.UI.Button btnTestReport;
		[SerializeField]
		public UnityEngine.UI.Button btnTestBrief;
		[SerializeField]
		public UnityEngine.UI.Image imgTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTip;

		KnowledgeExamPanel knowledgeExamPanel = null;
		TestReportPanel testReportPanel = null;

		private ButtonPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTip = null;
			btnSeting = null;
			btnKnowledge = null;
			btnTestReport = null;
			btnTestBrief = null;
			imgTip = null;
			tmpTip = null;
			
			mData = null;
		}
		
		public ButtonPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		ButtonPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ButtonPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
