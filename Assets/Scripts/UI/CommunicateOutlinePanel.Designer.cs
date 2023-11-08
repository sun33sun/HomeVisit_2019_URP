using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:b190f5be-9270-46b3-9648-2063ffd97d02
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
		public UnityEngine.UI.InputField inputCommunicateOutline;
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
			inputCommunicateOutline = null;
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
