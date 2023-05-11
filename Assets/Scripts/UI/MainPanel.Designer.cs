using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:9020609e-1002-45ee-9bf1-16d720376139
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
