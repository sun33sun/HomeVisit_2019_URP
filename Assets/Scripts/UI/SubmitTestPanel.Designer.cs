using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:9404c741-7863-4e16-9ea2-316ca9e804c3
	public partial class SubmitTestPanel
	{
		public const string Name = "SubmitTestPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTIp;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnCancel;
		
		private SubmitTestPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			tmpTIp = null;
			btnConfirm = null;
			btnCancel = null;
			
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
