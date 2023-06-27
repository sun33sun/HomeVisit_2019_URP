using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:824fcf5a-d5e7-476c-b7ed-e8103c901f25
	public partial class HomeVisitFormPanel
	{
		public const string Name = "HomeVisitFormPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgExam;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public RectTransform btnSubmit;
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
			btnSubmit = null;
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
