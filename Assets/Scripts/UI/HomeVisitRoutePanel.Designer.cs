using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:1ecdc7b2-1165-46f8-b6ce-8c3deb722cf6
	public partial class HomeVisitRoutePanel
	{
		public const string Name = "HomeVisitRoutePanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgRoute;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public HomeVisit.UI.MyScrollRect svMap;
		[SerializeField]
		public UnityEngine.UI.Button btnDraw;
		[SerializeField]
		public UnityEngine.UI.Button btnErase;
		[SerializeField]
		public TMPro.TMP_InputField inputTestEvaluate;
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
			inputTestEvaluate = null;
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
