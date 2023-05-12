using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:1272c19c-1ac4-4f1d-a45e-697c8fefbde9
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
			btnReRecord = null;
			btnConfirmRecord = null;
			btnStopRecord = null;
			imgExpressGratitude = null;
			btnRefuse = null;
			btnAccept = null;
			imgNext = null;
			btnNext = null;
			
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
