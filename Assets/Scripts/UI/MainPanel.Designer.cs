using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:bd1a37d9-09f3-4415-9432-0421a19654ec
	public partial class MainPanel
	{
		public const string Name = "MainPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgSchool;
		[SerializeField]
		public UnityEngine.UI.Button btnKeyWord;
		[SerializeField]
		public UnityEngine.UI.Button btnFullScreen;
		[SerializeField]
		public UnityEngine.UI.Image imgVisitProgress;
		
		private MainPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgSchool = null;
			btnKeyWord = null;
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
