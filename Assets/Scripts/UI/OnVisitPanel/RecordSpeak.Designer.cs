/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class RecordSpeak
	{
		[SerializeField] public UnityEngine.UI.Image imgPreSpeak;
		[SerializeField] public UnityEngine.UI.Button btnStartRecord;
		[SerializeField] public UnityEngine.UI.Image imgOnSpeak;
		[SerializeField] public UnityEngine.UI.Image imgState;
		[SerializeField] public UnityEngine.UI.Image imgFillWave;
		[SerializeField] public UnityEngine.UI.Button btnEndRecord;
		[SerializeField] public UnityEngine.UI.Image imgPostSpeak;
		[SerializeField] public UnityEngine.UI.Button btnReRecord;
		[SerializeField] public UnityEngine.UI.Button btnConfirmRecord;
		[SerializeField] public TMPro.TextMeshProUGUI tmpSpeakResult;
		[SerializeField] public HomeVisit.UI.MyScrollRect imgHistoryDialogueList;
		[SerializeField] public UnityEngine.UI.Button btnCloseHistoryDialogueList;

		public void Clear()
		{
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
			imgHistoryDialogueList = null;
			btnCloseHistoryDialogueList = null;
		}

		public override string ComponentName
		{
			get { return "RecordSpeak";}
		}
	}
}
