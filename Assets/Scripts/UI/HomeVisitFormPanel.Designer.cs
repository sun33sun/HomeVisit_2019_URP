using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:0449169a-12a3-4056-9f3a-c86db4686594
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
