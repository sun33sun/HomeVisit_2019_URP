using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:f686966f-a79f-4b81-9643-66633546815f
	public partial class HomeVisitRoutePanel
	{
		public const string Name = "HomeVisitRoutePanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgMid;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public HomeVisit.UI.MyScrollRect svMap;
		[SerializeField]
		public UnityEngine.UI.Image imgRoute;
		[SerializeField]
		public UnityEngine.UI.Button btnDraw;
		[SerializeField]
		public UnityEngine.UI.Button btnErase;
		[SerializeField]
		public UnityEngine.UI.InputField inputTestEvaluate;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Image imgTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTip;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnDialogue;
		[SerializeField]
		public TMPro.TextMeshProUGUI txtDialogue;
		[SerializeField]
		public UnityEngine.UI.Image imgBlank;
		
		private HomeVisitRoutePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgMid = null;
			btnClose = null;
			svMap = null;
			imgRoute = null;
			btnDraw = null;
			btnErase = null;
			inputTestEvaluate = null;
			btnSubmit = null;
			imgTip = null;
			tmpTip = null;
			btnConfirm = null;
			btnDialogue = null;
			txtDialogue = null;
			imgBlank = null;
			
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
