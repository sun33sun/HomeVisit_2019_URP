using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:e5668c19-da86-4535-a5f0-151dc122a5f5
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
