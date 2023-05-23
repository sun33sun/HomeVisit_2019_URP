using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	// Generate Id:a1b0206f-1540-4ae5-9616-9b47d0f195b5
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
