using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:9db376d4-2ed9-49e0-a420-4d96e5df24bf
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
		public TMPro.TextMeshProUGUI tmpSpeakText;
		[SerializeField]
		public UnityEngine.UI.Image imgPostSpeak;
		[SerializeField]
		public UnityEngine.UI.Button btnPlayRecord;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpPostText;
		[SerializeField]
		public UnityEngine.UI.Button btnReRecord;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmRecord;
		[SerializeField]
		public UnityEngine.UI.Button btnStopRecord;
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
		public UnityEngine.UI.Image imgBlank;
		[SerializeField]
		public UnityEngine.UI.Image imgInputBk;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpOnVIsitTip;
		[SerializeField]
		public TMPro.TMP_InputField InputAnswer;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmitOnVisit;
		
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
			tmpSpeakText = null;
			imgPostSpeak = null;
			btnPlayRecord = null;
			tmpPostText = null;
			btnReRecord = null;
			btnConfirmRecord = null;
			btnStopRecord = null;
			imgExpressGratitude = null;
			btnRefuse = null;
			btnAccept = null;
			imgNext = null;
			btnNext = null;
			imgBlank = null;
			imgInputBk = null;
			tmpOnVIsitTip = null;
			InputAnswer = null;
			btnSubmitOnVisit = null;
			
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
