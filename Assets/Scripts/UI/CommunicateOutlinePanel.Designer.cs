using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:184a75c7-9d6e-4a48-8a82-21ce65aa8362
	public partial class CommunicateOutlinePanel
	{
		public const string Name = "CommunicateOutlinePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private CommunicateOutlinePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnClose = null;
			Content = null;
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
