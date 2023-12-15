using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectBase;
using HomeVisit.Character;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HomeVisit.Task;
using TMPro;
using HomeVisit.Screenshot;
using System.Threading;

namespace HomeVisit.UI
{
	public enum NPCIndex
	{
		母亲, 老师1, 学生1, 学生2, 父亲
	}

	public enum RecordState
	{
		None, WaitRecord, Recording, HaveResult, ResultIsRight
	}

	public class OnVisitPanelData : UIPanelData
	{
	}

	public partial class OnVisitPanel : UIPanel
	{
		public string[] keywords;
		public RecordState recordState;
		public int score = 0;
		bool isConfirm = false;
		DateTime startTime;
		[SerializeField] TextMeshProUGUI dialoguePrefab;
		[SerializeField] Sprite[] sprites;
		List<TextMeshProUGUI> tmpDialogues = new List<TextMeshProUGUI>();

		CancellationTokenSource cts = null;

		protected override void OnInit(IUIData uiData = null)
		{
			EventCenter.GetInstance().RemoveEventListener<Dictionary<string, bool>>("语音识别结果", OnResult);
			EventCenter.GetInstance().RemoveEventListener<string>("实时语音结果", RealTimeResult);
			EventCenter.GetInstance().RemoveEventListener<int>("访中过程试题完成", AddScore);

			EventCenter.GetInstance().AddEventListener<Dictionary<string, bool>>("语音识别结果", OnResult);
			EventCenter.GetInstance().AddEventListener<int>("访中过程试题完成", AddScore);
			EventCenter.GetInstance().AddEventListener<string>("实时语音结果", RealTimeResult);

			mData = uiData as OnVisitPanelData ?? new OnVisitPanelData();

			btnCancelObserveDetail.onClick.AddListener(() => { imgObserveDetail.gameObject.SetActive(false); });
			btnConfirmObserveDetail.onClick.AddListener(ConfirmObserveDetail);
			btnStartRecord.onClick.AddListener(StartRecord);
			btnEndRecord.onClick.AddListener(EndRecord);
			btnReRecord.onClick.AddListener(ReRecord);
			btnConfirmRecord.onClick.AddListener(ConfirmRecord);

			btnNext.onClick.AddListener(Next);
			btnSubmitOnVisit.onClick.AddListener(SubmitOnVisit);
			//历史对话
			btnSwitchHistoryDialogueList.onClick.AddListener(() => imgHistoryDialogueList.gameObject.SetActive(!imgHistoryDialogueList.gameObject.activeInHierarchy));
			//截图
			btnScreenshot.onClick.AddListener(() =>
			{
				if (WaveChangeInstance != null)
					return;
				ScreenshotWindow.Screenshot(btnShowScreenshot.transform.localPosition);
			});
			btnShowScreenshot.onClick.AddListener(ScreenshotWindow.Show);

			btnCloseHistoryDialogueList.onClick.AddListener(() => imgHistoryDialogueList.gameObject.SetActive(false));

			InitState();

			StartCoroutine(LoadSceneAsync());
		}

		void Next()
		{
			StartCoroutine(UnloadOnVisitAsync());
		}

		IEnumerator UnloadOnVisitAsync()
		{
			MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
			yield return UIKit.OpenPanelAsync<RecordSheetPanel>(
				prefabName: Settings.UI + QAssetBundle.Recordsheetpanel_prefab.RECORDSHEETPANEL);
			Hide();
		}

		public void ShowNext()
		{
			imgNext.gameObject.SetActive(true);
		}

		void AddScore(int addScore)
		{
			score += addScore;
		}

		#region 访中过程UI

		void SubmitOnVisit()
		{
			imgInputBk.gameObject.SetActive(false);
			EventCenter.GetInstance().EventTrigger<string>(tmpOnVIsitTip.text, InputAnswer.text);
			InputAnswer.text = "";
		}

		public void ShowOnVisitInput(string strTip)
		{
			imgInputBk.gameObject.SetActive(true);
			tmpOnVIsitTip.text = strTip;
		}

		#endregion

		void ConfirmObserveDetail()
		{
			//关闭自己
			imgObserveDetail.gameObject.SetActive(false);
		}

		#region 语音部分

		void IndexConvert(ref int index)
		{
			switch (Settings.OldRandomScene)
			{
				case "ToBeDeveloped":
				case "Developing":
					if (index == 2 || index == 2)
						index = 3;
					break;
				case "Developed":
					if (index == 2 || index == 2)
						index = 2;
					break;
				default:
					break;
			}
		}

		//展示家长说的话
		public void AddHistoryDialogue(NPCIndex npcSprite, string strWord)
		{
			int index = (int)npcSprite;
			CloseRecord();
			btnDialogue.gameObject.SetActive(true);
			IndexConvert(ref index);
			btnDialogue.sprite = sprites[index];
			txtDialogue.text = strWord;
			string historyWord = $"{npcSprite}：{strWord}";

			TextMeshProUGUI tmpDialogue = Instantiate(dialoguePrefab);
			tmpDialogue.text = historyWord;
			tmpDialogue.transform.SetParent(imgHistoryDialogueList.content);
			tmpDialogue.transform.localScale = Vector3.one;
			tmpDialogue.transform.SetAsLastSibling();
			LayoutRebuilder.ForceRebuildLayoutImmediate(imgHistoryDialogueList.content);
		}

		//展示开始录音UI
		public void ShowRecordUI(string[] keywords)
		{
			this.keywords = keywords;
			btnDialogue.gameObject.SetActive(false);
			CloseRecord();
			imgPreSpeak.gameObject.SetActive(true);
			//重置状态
			isConfirm = false;
			recordState = RecordState.WaitRecord;
		}

		//点击后开始录音
		void StartRecord()
		{
			CloseRecord();
			imgOnSpeak.gameObject.SetActive(true);
			btnEndRecord.interactable = false;
			WaveChangeInstance = StartCoroutine(WaveChange());
			RecordManager.Instance.StartRecord(keywords);
			tmpSpeakResult.text = "";
			recordState = RecordState.Recording;
		}

		//录音结果返回
		void OnResult(Dictionary<string, bool> keywordDic)
		{
			bool isAllRight = true;
			foreach (var item in keywordDic.Values)
			{
				if (!item)
				{
					recordState = RecordState.HaveResult;
					isAllRight = false;
				}
				else
				{
					score += 1;
				}
			}

			if (isAllRight || isConfirm)
			{
				CloseRecord();
				recordState = RecordState.ResultIsRight;
			}
		}

		void RealTimeResult(string speakResult)
		{
			if (speakResult.Equals("没有麦克风"))
				btnEndRecord.interactable = true;
			tmpSpeakResult.text = speakResult;
			TextMeshProUGUI tmpDialogue = Instantiate(dialoguePrefab, imgHistoryDialogueList.content);
			tmpDialogues.Add(tmpDialogue);
			tmpDialogue.text = "老师 : " + speakResult;
			tmpDialogue.transform.localScale = Vector3.one;
			tmpDialogue.transform.SetAsLastSibling();
			LayoutRebuilder.ForceRebuildLayoutImmediate(imgHistoryDialogueList.content);
		}

		//录音不理想，再次录音
		void ReRecord()
		{
			CloseRecord();
			imgPreSpeak.gameObject.SetActive(true);
		}

		//结束录音
		void EndRecord()
		{
			CloseRecord();
			RecordManager.Instance.EndRecord();
			imgPostSpeak.gameObject.SetActive(true);
		}

		//录音动画
		private Coroutine WaveChangeInstance = null;
		IEnumerator WaveChange()
		{
			float value = 0;
			WaitForEndOfFrame wait = new WaitForEndOfFrame();
			imgFillWave.fillOrigin = 0;
			float timer = 0;

			bool isAdd = true;
			while (recordState != RecordState.ResultIsRight)
			{
				if (value > 1)
				{
					imgFillWave.fillOrigin = 1;
					isAdd = false;
				}
				else if (value < 0)
				{
					imgFillWave.fillOrigin = 0;
					isAdd = true;
				}

				imgFillWave.fillAmount = value;
				if (isAdd)
					value += Time.deltaTime;
				else
					value -= Time.deltaTime;
				timer += Time.deltaTime;
				yield return wait;
				if (timer > 10)
					btnEndRecord.interactable = true;
			}
		}

		//关闭所有录音UI
		public void CloseRecord()
		{
			if (WaveChangeInstance != null)
			{
				StopCoroutine(WaveChangeInstance);
				WaveChangeInstance = null;
			}
			tmpSpeakResult.text = "";
			imgPreSpeak.gameObject.SetActive(false);
			imgOnSpeak.gameObject.SetActive(false);
			imgPostSpeak.gameObject.SetActive(false);
		}

		//确认录音，直接进入下一步
		public void ConfirmRecord()
		{
			CloseRecord();
			//记录状态
			//if (recordState == RecordState.HaveResult)
			//    recordState = RecordState.ResultIsRight;
			//else
			//    isConfirm = true;

			recordState = RecordState.ResultIsRight;
			isConfirm = true;
		}

		#endregion

		#region 实验报告

		public void GenerateReportData(string title, int maxScore)
		{
			ScoreReportData data = new ScoreReportData()
			{
				title = title,
				startTime = startTime,
				endTime = DateTime.UtcNow,
				maxScore = maxScore,
				score = this.score
			};
			//重置记录数据
			score = 0;
			startTime = DateTime.UtcNow;
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
		}

		#endregion

		IEnumerator LoadSceneAsync()
		{
			TopPanel topPanel = UIKit.GetPanel<TopPanel>();
			yield return topPanel.CloseEyeAnim();
			AsyncOperation operation = SceneManager.LoadSceneAsync(Settings.RandomScene);
			yield return operation;
			CameraManager.Instance.SetRoamPos(FemaleTeacher.Instance.transform.position);
			CameraManager.Instance.SetRoamForward(FemaleTeacher.Instance.transform.forward);

			switch (UIKit.GetPanel<HomeVisitContentPanel>().GetHomeType())
			{
				case "传统":
					TraditionTask.Instance.StartCoroutine(TraditionTask.Instance.StartTask());
					break;
				case "多孩":
					MultipleChildrenTask.Instance.StartCoroutine(MultipleChildrenTask.Instance.StartTask());
					break;
				case "单亲":
					SingleParentTask.Instance.StartCoroutine(SingleParentTask.Instance.StartTask());
					break;
			}

			startTime = DateTime.UtcNow;
			topPanel.ChangeTip("请进行在线实验，过程中需要念出右上角关键字");
		}

		public void InitState()
		{
			imgObserveDetail.gameObject.SetActive(false);
			CloseRecord();
			imgNext.gameObject.SetActive(false);

			UIKit.GetPanel<MainPanel>().StartTMP();
		}

		public IEnumerator ShowExpressGratitude()
		{
			CloseRecord();
			yield return GiftWindow.ShowExpressGratitude();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			tmpSpeakResult.text = "";
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		public void SetMask(bool enable)
		{
			GetComponent<Image>().enabled = enable;
		}

		public override void Hide()
		{
		}

		public async UniTask WaitSelectionOptions(SelectionSO selectionSO)
		{
			SelectionWindow.gameObject.SetActive(true);
			OptionData data = await SelectionWindow.WaitSelectionOptions(selectionSO);
			SelectionWindow.gameObject.SetActive(false);
			//初始化值
			TaskHelper.StartSpeak(selectionSO.npcIndex);
			AddHistoryDialogue(selectionSO.npcIndex, data.str);
			//播放音乐
			await AudioManager.Instance.Play(data.clip, cts.Token);
			//清除后遗症
			TaskHelper.StopSpeak(selectionSO.npcIndex);
			btnDialogue.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			if (cts != null)
			{
				cts.Dispose();
				cts = null;
			}

			cts = new CancellationTokenSource();
		}

		private void OnDisable()
		{
			cts?.Cancel();
		}
	}
}