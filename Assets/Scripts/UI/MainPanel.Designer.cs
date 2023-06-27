using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:f0185f76-e88f-4c85-8c1d-0007f6f957f7
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
		public UnityEngine.UI.Button btnFullScreen;
		[SerializeField]
		public UnityEngine.UI.Image imgVisitProgress;
		
		private MainPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgSchool = null;
			btnKeyWord = null;
			imgKeywordsDetail = null;
			btnFullScreen = null;
			imgVisitProgress = null;
			
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
