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
		ĸ��, ��ʦ1, ѧ��1, ѧ��2, ����
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
			EventCenter.GetInstance().RemoveEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);
			EventCenter.GetInstance().RemoveEventListener<string>("ʵʱ�������", RealTimeResult);
			EventCenter.GetInstance().RemoveEventListener<int>("���й����������", AddScore);

			EventCenter.GetInstance().AddEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);
			EventCenter.GetInstance().AddEventListener<int>("���й����������", AddScore);
			EventCenter.GetInstance().AddEventListener<string>("ʵʱ�������", RealTimeResult);

			mData = uiData as OnVisitPanelData ?? new OnVisitPanelData();

			btnCancelObserveDetail.onClick.AddListener(() => { imgObserveDetail.gameObject.SetActive(false); });
			btnConfirmObserveDetail.onClick.AddListener(ConfirmObserveDetail);
			btnStartRecord.onClick.AddListener(StartRecord);
			btnEndRecord.onClick.AddListener(EndRecord);
			btnReRecord.onClick.AddListener(ReRecord);
			btnConfirmRecord.onClick.AddListener(ConfirmRecord);

			btnNext.onClick.AddListener(Next);
			btnSubmitOnVisit.onClick.AddListener(SubmitOnVisit);
			//��ʷ�Ի�
			btnSwitchHistoryDialogueList.onClick.AddListener(() => imgHistoryDialogueList.gameObject.SetActive(!imgHistoryDialogueList.gameObject.activeInHierarchy));
			//��ͼ
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

		#region ���й���UI

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
			//�ر��Լ�
			imgObserveDetail.gameObject.SetActive(false);
		}

		#region ��������

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

		//չʾ�ҳ�˵�Ļ�
		public void AddHistoryDialogue(NPCIndex npcSprite, string strWord)
		{
			int index = (int)npcSprite;
			CloseRecord();
			btnDialogue.gameObject.SetActive(true);
			IndexConvert(ref index);
			btnDialogue.sprite = sprites[index];
			txtDialogue.text = strWord;
			string historyWord = $"{npcSprite}��{strWord}";

			TextMeshProUGUI tmpDialogue = Instantiate(dialoguePrefab);
			tmpDialogue.text = historyWord;
			tmpDialogue.transform.SetParent(imgHistoryDialogueList.content);
			tmpDialogue.transform.localScale = Vector3.one;
			tmpDialogue.transform.SetAsLastSibling();
			LayoutRebuilder.ForceRebuildLayoutImmediate(imgHistoryDialogueList.content);
		}

		//չʾ��ʼ¼��UI
		public void ShowRecordUI(string[] keywords)
		{
			this.keywords = keywords;
			btnDialogue.gameObject.SetActive(false);
			CloseRecord();
			imgPreSpeak.gameObject.SetActive(true);
			//����״̬
			isConfirm = false;
			recordState = RecordState.WaitRecord;
		}

		//�����ʼ¼��
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

		//¼���������
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
			if (speakResult.Equals("û����˷�"))
				btnEndRecord.interactable = true;
			tmpSpeakResult.text = speakResult;
			TextMeshProUGUI tmpDialogue = Instantiate(dialoguePrefab, imgHistoryDialogueList.content);
			tmpDialogues.Add(tmpDialogue);
			tmpDialogue.text = "��ʦ : " + speakResult;
			tmpDialogue.transform.localScale = Vector3.one;
			tmpDialogue.transform.SetAsLastSibling();
			LayoutRebuilder.ForceRebuildLayoutImmediate(imgHistoryDialogueList.content);
		}

		//¼�������룬�ٴ�¼��
		void ReRecord()
		{
			CloseRecord();
			imgPreSpeak.gameObject.SetActive(true);
		}

		//����¼��
		void EndRecord()
		{
			CloseRecord();
			RecordManager.Instance.EndRecord();
			imgPostSpeak.gameObject.SetActive(true);
		}

		//¼������
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

		//�ر�����¼��UI
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

		//ȷ��¼����ֱ�ӽ�����һ��
		public void ConfirmRecord()
		{
			CloseRecord();
			//��¼״̬
			//if (recordState == RecordState.HaveResult)
			//    recordState = RecordState.ResultIsRight;
			//else
			//    isConfirm = true;

			recordState = RecordState.ResultIsRight;
			isConfirm = true;
		}

		#endregion

		#region ʵ�鱨��

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
			//���ü�¼����
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
				case "��ͳ":
					TraditionTask.Instance.StartCoroutine(TraditionTask.Instance.StartTask());
					break;
				case "�ຢ":
					MultipleChildrenTask.Instance.StartCoroutine(MultipleChildrenTask.Instance.StartTask());
					break;
				case "����":
					SingleParentTask.Instance.StartCoroutine(SingleParentTask.Instance.StartTask());
					break;
			}

			startTime = DateTime.UtcNow;
			topPanel.ChangeTip("���������ʵ�飬��������Ҫ������Ͻǹؼ���");
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
			//��ʼ��ֵ
			TaskHelper.StartSpeak(selectionSO.npcIndex);
			AddHistoryDialogue(selectionSO.npcIndex, data.str);
			//��������
			await AudioManager.Instance.Play(data.clip, cts.Token);
			//�������֢
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