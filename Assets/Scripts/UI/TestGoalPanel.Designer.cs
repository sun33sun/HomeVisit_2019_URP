using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:b40b2ac9-91ca-40ac-819d-d984d5e915a0
	public partial class TestGoalPanel
	{
		public const string Name = "TestGoalPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTestGoal;
		[SerializeField]
		public UnityEngine.UI.Button btnTestPrinciple;
		[SerializeField]
		public UnityEngine.UI.Button btnTestDemand;
		[SerializeField]
		public UnityEngine.UI.Button btnTestAssistance;
		[SerializeField]
		public UnityEngine.UI.Image TestGoal;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseGoal;
		[SerializeField]
		public UnityEngine.UI.Image TestAssistance;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseAssistance;
		
		private TestGoalPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTestGoal = null;
			btnTestPrinciple = null;
			btnTestDemand = null;
			btnTestAssistance = null;
			TestGoal = null;
			btnCloseGoal = null;
			TestAssistance = null;
			btnCloseAssistance = null;
			
			mData = null;
		}
		
		public TestGoalPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TestGoalPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TestGoalPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
