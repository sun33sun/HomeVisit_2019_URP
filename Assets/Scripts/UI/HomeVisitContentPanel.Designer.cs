using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:d4762eba-dd0c-42e4-9b34-167e42c25d2d
	public partial class HomeVisitContentPanel
	{
		public const string Name = "HomeVisitContentPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgScenarioCases;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.ScrollRect btnBigQuestionnaire;
		[SerializeField]
		public UnityEngine.UI.Image imgBuildTip;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseBuildTip;
		
		private HomeVisitContentPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgScenarioCases = null;
			btnClose = null;
			btnConfirm = null;
			btnBigQuestionnaire = null;
			imgBuildTip = null;
			btnCloseBuildTip = null;
			
			mData = null;
		}
		
		public HomeVisitContentPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		HomeVisitContentPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HomeVisitContentPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
