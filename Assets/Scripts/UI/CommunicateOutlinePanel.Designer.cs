using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:b3f72a52-b855-43af-97ce-f71c7a46f01d
	public partial class CommunicateOutlinePanel
	{
		public const string Name = "CommunicateOutlinePanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public TMPro.TextMeshProUGUI titleDescribe;
		[SerializeField]
		public UnityEngine.UI.Toggle togA;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpA;
		[SerializeField]
		public UnityEngine.UI.Toggle togB;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpB;
		[SerializeField]
		public UnityEngine.UI.Toggle togC;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpC;
		[SerializeField]
		public UnityEngine.UI.Toggle togD;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpD;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpAnalysis;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		
		private CommunicateOutlinePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			titleDescribe = null;
			togA = null;
			tmpA = null;
			togB = null;
			tmpB = null;
			togC = null;
			tmpC = null;
			togD = null;
			tmpD = null;
			tmpAnalysis = null;
			btnConfirm = null;
			btnClose = null;
			
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
