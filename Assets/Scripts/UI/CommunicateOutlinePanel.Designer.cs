using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:d5f6e541-9fa9-4a3e-9fb2-133ff48aff11
	public partial class CommunicateOutlinePanel
	{
		public const string Name = "CommunicateOutlinePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Image imgDescribe;
		[SerializeField]
		public UnityEngine.UI.InputField inputTestEvaluate;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private CommunicateOutlinePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnClose = null;
			Content = null;
			imgDescribe = null;
			inputTestEvaluate = null;
			btnSubmit = null;
			btnConfirm = null;
			
			mData = null;
		}
		
		public CommunicateOutlinePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		CommunicateOutlinePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new CommunicateOutlinePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
