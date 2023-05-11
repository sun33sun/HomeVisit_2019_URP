using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:43dfbd58-8bdc-4378-9470-73d9f9075947
	public partial class CommunicateOutlinePanel
	{
		public const string Name = "CommunicateOutlinePanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		
		private CommunicateOutlinePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			btnSubmit = null;
			btnClose = null;
			
			mData = null;
		}
		
		public CommunicateOutlinePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		CommunicateOutlinePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new CommunicateOutlinePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
