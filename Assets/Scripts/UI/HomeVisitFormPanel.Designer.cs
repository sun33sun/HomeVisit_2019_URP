using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:33963beb-13f5-4e80-9def-4d4de8362289
	public partial class HomeVisitFormPanel
	{
		public const string Name = "HomeVisitFormPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgExam;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmitFrom;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmFrom;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.UI.Image imgSubmitExam;
		[SerializeField]
		public UnityEngine.UI.Button btnCancel;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private HomeVisitFormPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgExam = null;
			Content = null;
			btnSubmitFrom = null;
			btnConfirmFrom = null;
			btnClose = null;
			imgSubmitExam = null;
			btnCancel = null;
			btnConfirm = null;
			
			mData = null;
		}
		
		public HomeVisitFormPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		HomeVisitFormPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HomeVisitFormPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
