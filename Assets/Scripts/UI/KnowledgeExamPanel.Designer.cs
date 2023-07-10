using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:46b29909-b40a-414d-b4f0-c3f520867f9d
	public partial class KnowledgeExamPanel
	{
		public const string Name = "KnowledgeExamPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform imgExam;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.RectTransform imgConfirmConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmConfirm;
		
		private KnowledgeExamPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgExam = null;
			Content = null;
			btnSubmit = null;
			btnConfirm = null;
			btnClose = null;
			imgConfirmConfirm = null;
			btnCancelConfirm = null;
			btnConfirmConfirm = null;
			
			mData = null;
		}
		
		public KnowledgeExamPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		KnowledgeExamPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new KnowledgeExamPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
