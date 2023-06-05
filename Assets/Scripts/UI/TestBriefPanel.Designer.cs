using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:7da99792-ef35-4ad8-bdb7-e93b604bedce
	public partial class TestBriefPanel
	{
		public const string Name = "TestBriefPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTestGoal;
		[SerializeField]
		public UnityEngine.UI.Button btnTestPrinciple;
		[SerializeField]
		public UnityEngine.UI.Button btnTestDemand;
		[SerializeField]
		public UnityEngine.UI.Button btnTestAssistance;
		[SerializeField]
		public UnityEngine.UI.Image imgTestGoal;
		[SerializeField]
		public UnityEngine.UI.Image imgTestPrinciple;
		[SerializeField]
		public UnityEngine.UI.Image imgTestDemand;
		[SerializeField]
		public UnityEngine.UI.Image imgTestAssistance;
		[SerializeField]
		public UnityEngine.UI.Button btnClosePanel;
		
		private TestBriefPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTestGoal = null;
			btnTestPrinciple = null;
			btnTestDemand = null;
			btnTestAssistance = null;
			imgTestGoal = null;
			imgTestPrinciple = null;
			imgTestDemand = null;
			imgTestAssistance = null;
			btnClosePanel = null;
			
			mData = null;
		}
		
		public TestBriefPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TestBriefPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TestBriefPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
