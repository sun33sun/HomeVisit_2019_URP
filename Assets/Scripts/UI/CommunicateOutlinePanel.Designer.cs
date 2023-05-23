using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:2bdaa2be-6d63-4463-90a6-dab7b4b7b8b2
	public partial class CommunicateOutlinePanel
	{
		public const string Name = "CommunicateOutlinePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private CommunicateOutlinePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnClose = null;
			Content = null;
			btnSubmit = null;
			btnConfirm = null;
			
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
