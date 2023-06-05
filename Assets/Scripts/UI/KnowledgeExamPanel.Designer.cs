using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:d3a27b0e-8e9f-4d5c-bf19-c350240f0252
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
		public UnityEngine.RectTransform imgSubmitExam;
		[SerializeField]
		public UnityEngine.UI.Button btnCancel;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmSubmit;
		
		private KnowledgeExamPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgExam = null;
			Content = null;
			btnSubmit = null;
			btnConfirm = null;
			btnClose = null;
			imgSubmitExam = null;
			btnCancel = null;
			btnConfirmSubmit = null;
			
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
