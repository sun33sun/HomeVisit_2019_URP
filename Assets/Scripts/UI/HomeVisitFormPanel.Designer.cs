using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:e3b38ddc-3e76-4092-9c3c-aff72cbfc6db
	public partial class HomeVisitFormPanel
	{
		public const string Name = "HomeVisitFormPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgExam;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
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
			btnSubmit = null;
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
