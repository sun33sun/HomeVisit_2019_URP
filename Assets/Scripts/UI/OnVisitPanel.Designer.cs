using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:a5ef7075-c733-44c2-add5-797fbccba065
	public partial class OnVisitPanel
	{
		public const string Name = "OnVisitPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image btnDialogue;
		[SerializeField]
		public TMPro.TextMeshProUGUI txtDialogue;
		[SerializeField]
		public UnityEngine.UI.Image imgObserveDetail;
		[SerializeField]
		public UnityEngine.UI.Button btnCancelObserveDetail;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmObserveDetail;
		[SerializeField]
		public UnityEngine.UI.Image imgPreSpeak;
		[SerializeField]
		public UnityEngine.UI.Button btnStartRecord;
		[SerializeField]
		public UnityEngine.UI.Image imgOnSpeak;
		[SerializeField]
		public UnityEngine.UI.Image imgState;
		[SerializeField]
		public UnityEngine.UI.Image imgFillWave;
		[SerializeField]
		public UnityEngine.UI.Button btnEndRecord;
		[SerializeField]
		public UnityEngine.UI.Image imgPostSpeak;
		[SerializeField]
		public UnityEngine.UI.Button btnReRecord;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmRecord;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpSpeakResult;
		[SerializeField]
		public UnityEngine.UI.Image imgExpressGratitude;
		[SerializeField]
		public UnityEngine.UI.Button btnRefuse;
		[SerializeField]
		public UnityEngine.UI.Button btnAccept;
		[SerializeField]
		public UnityEngine.UI.Image imgNext;
		[SerializeField]
		public UnityEngine.UI.Button btnNext;
		[SerializeField]
		public UnityEngine.UI.Image imgInputBk;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpOnVIsitTip;
		[SerializeField]
		public UnityEngine.UI.InputField InputAnswer;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmitOnVisit;
		[SerializeField]
		public UnityEngine.UI.Image imgGratitudeTip;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmGratitudeTip;
		[SerializeField]
		public UnityEngine.UI.Button btnSwitchHistoryDialogueList;
		[SerializeField]
		public UnityEngine.UI.Button btnScreenshot;
		[SerializeField]
		public UnityEngine.UI.Button btnShowScreenshot;
		[SerializeField]
		public HomeVisit.UI.MyScrollRect imgHistoryDialogueList;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseHistoryDialogueList;
		[SerializeField]
		public UnityEngine.UI.Image imgScreenshot;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseScreenshot;
		[SerializeField]
		public UnityEngine.UI.RawImage rawImgScreenshot;
		[SerializeField]
		public UnityEngine.UI.RawImage imgScreenshotEffect;
		
		private OnVisitPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			btnDialogue = null;
			txtDialogue = null;
			imgObserveDetail = null;
			btnCancelObserveDetail = null;
			btnConfirmObserveDetail = null;
			imgPreSpeak = null;
			btnStartRecord = null;
			imgOnSpeak = null;
			imgState = null;
			imgFillWave = null;
			btnEndRecord = null;
			imgPostSpeak = null;
			btnReRecord = null;
			btnConfirmRecord = null;
			tmpSpeakResult = null;
			imgExpressGratitude = null;
			btnRefuse = null;
			btnAccept = null;
			imgNext = null;
			btnNext = null;
			imgInputBk = null;
			tmpOnVIsitTip = null;
			InputAnswer = null;
			btnSubmitOnVisit = null;
			imgGratitudeTip = null;
			btnConfirmGratitudeTip = null;
			btnSwitchHistoryDialogueList = null;
			btnScreenshot = null;
			btnShowScreenshot = null;
			imgHistoryDialogueList = null;
			btnCloseHistoryDialogueList = null;
			imgScreenshot = null;
			btnCloseScreenshot = null;
			rawImgScreenshot = null;
			imgScreenshotEffect = null;
			
			mData = null;
		}
		
		public OnVisitPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		OnVisitPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new OnVisitPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
