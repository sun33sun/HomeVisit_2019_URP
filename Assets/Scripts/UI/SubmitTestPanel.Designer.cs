using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:535ce2ac-39a7-4667-8b3b-eec95b97cff0
	public partial class SubmitTestPanel
	{
		public const string Name = "SubmitTestPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnCancel;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private SubmitTestPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnCancel = null;
			btnConfirm = null;
			
			mData = null;
		}
		
		public SubmitTestPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		SubmitTestPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new SubmitTestPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
