using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:4236bb07-2a24-43bb-ac89-662d47131b2c
	public partial class HomeVisitRoutePanel
	{
		public const string Name = "HomeVisitRoutePanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgRoute;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.UI.ScrollRect svMap;
		[SerializeField]
		public UnityEngine.UI.Button btnDraw;
		[SerializeField]
		public UnityEngine.UI.Button btnErase;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Image imgTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTIp;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDialogue;
		[SerializeField]
		public TMPro.TextMeshProUGUI txtDialogue;
		
		private HomeVisitRoutePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgRoute = null;
			btnClose = null;
			svMap = null;
			btnDraw = null;
			btnErase = null;
			btnSubmit = null;
			imgTip = null;
			tmpTIp = null;
			btnConfirm = null;
			btnDialogue = null;
			txtDialogue = null;
			
			mData = null;
		}
		
		public HomeVisitRoutePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		HomeVisitRoutePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HomeVisitRoutePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
