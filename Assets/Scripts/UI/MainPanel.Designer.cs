using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:ff5868b0-d3c9-424f-bc3e-b5835ad4e317
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
