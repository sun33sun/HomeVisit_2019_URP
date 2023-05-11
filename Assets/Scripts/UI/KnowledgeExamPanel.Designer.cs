using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:d09b5acb-693e-4c46-97a2-100b38e55dfb
	public partial class KnowledgeExamPanel
	{
		public const string Name = "KnowledgeExamPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform imgExam;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpPanelName;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.RectTransform imgSubmitExam;
		[SerializeField]
		public UnityEngine.UI.Button btnCancel;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private KnowledgeExamPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgExam = null;
			tmpPanelName = null;
			Content = null;
			btnSubmit = null;
			btnClose = null;
			imgSubmitExam = null;
			btnCancel = null;
			btnConfirm = null;
			
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
