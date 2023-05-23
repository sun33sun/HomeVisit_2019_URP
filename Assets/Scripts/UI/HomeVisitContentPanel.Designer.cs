using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:ffd18e1e-01aa-4a0f-b07e-b65e59555e1b
	public partial class HomeVisitContentPanel
	{
		public const string Name = "HomeVisitContentPanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		
		private HomeVisitContentPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			btnSubmit = null;
			btnClose = null;
			
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
