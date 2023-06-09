using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:624d645e-e675-463f-ae81-51979daeb0d5
	public partial class HomeVisitContentPanel
	{
		public const string Name = "HomeVisitContentPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private HomeVisitContentPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnSubmit = null;
			btnClose = null;
			btnConfirm = null;
			
			mData = null;
		}
		
		public HomeVisitContentPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		HomeVisitContentPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HomeVisitContentPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
