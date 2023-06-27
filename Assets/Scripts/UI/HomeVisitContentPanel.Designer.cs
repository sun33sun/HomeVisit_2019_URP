using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:18e82631-2cdb-470c-a42a-4fdd0afd0db9
	public partial class HomeVisitContentPanel
	{
		public const string Name = "HomeVisitContentPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgScenarioCases;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.ScrollRect btnBigQuestionnaire;
		
		private HomeVisitContentPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgScenarioCases = null;
			btnSubmit = null;
			btnClose = null;
			btnConfirm = null;
			btnBigQuestionnaire = null;
			
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
