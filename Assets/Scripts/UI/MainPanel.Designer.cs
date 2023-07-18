using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:92f5e4e6-865c-4610-9451-56562260884a
	public partial class MainPanel
	{
		public const string Name = "MainPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgSchool;
		[SerializeField]
		public UnityEngine.UI.Button btnKeyWord;
		[SerializeField]
		public UnityEngine.RectTransform imgKeywordsDetail;
		[SerializeField]
		public UnityEngine.EventSystems.EventTrigger togFullScreen;
		[SerializeField]
		public UnityEngine.UI.Image imgVisitProgress;
		[SerializeField]
		public UnityEngine.UI.Image imgCompletedTip;
		
		private MainPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgSchool = null;
			btnKeyWord = null;
			imgKeywordsDetail = null;
			togFullScreen = null;
			imgVisitProgress = null;
			imgCompletedTip = null;
			
			mData = null;
		}
		
		public MainPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		MainPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MainPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
