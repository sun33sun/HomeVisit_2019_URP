using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:b35537ee-b4ed-4076-bb9d-e014a6aa7792
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
		
		private ButtonPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTip = null;
			btnSeting = null;
			btnKnowledge = null;
			btnTestReport = null;
			btnTestBrief = null;
			
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
