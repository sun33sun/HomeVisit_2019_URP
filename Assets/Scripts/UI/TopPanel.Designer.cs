using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:c422ae46-f755-40cb-a54f-7218beafb401
	public partial class TopPanel
	{
		public const string Name = "TopPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button btnTip;
		[SerializeField]
		public UnityEngine.UI.Image imgTipAnim;
		[SerializeField]
		public UnityEngine.UI.Button btnSeting;
		[SerializeField]
		public UnityEngine.UI.Button btnTestReport;
		[SerializeField]
		public UnityEngine.UI.Button btnKnowledge;
		[SerializeField]
		public UnityEngine.UI.Button btnTestBrief;
		[SerializeField]
		public UnityEngine.UI.Image imgTip;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpTip;
		[SerializeField]
		public UnityEngine.UI.Image imgBlank;
		
		private TopPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnTip = null;
			imgTipAnim = null;
			btnSeting = null;
			btnTestReport = null;
			btnKnowledge = null;
			btnTestBrief = null;
			imgTip = null;
			tmpTip = null;
			imgBlank = null;
			
			mData = null;
		}
		
		public TopPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TopPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TopPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
