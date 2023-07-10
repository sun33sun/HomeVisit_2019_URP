using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:2aa99d51-7c9f-483f-ada7-1f1b777266a5
	public partial class TestBriefPanel
	{
		public const string Name = "TestBriefPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnClosePanel;
		
		private TestBriefPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
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
