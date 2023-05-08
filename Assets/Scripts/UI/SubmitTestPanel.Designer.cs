using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:ca320587-6f8b-462f-8012-f86503b439fb
	public partial class SubmitTestPanel
	{
		public const string Name = "SubmitTestPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTIp;
		[SerializeField]
		public UnityEngine.UI.Button btnCancel;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private SubmitTestPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			tmpTIp = null;
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
