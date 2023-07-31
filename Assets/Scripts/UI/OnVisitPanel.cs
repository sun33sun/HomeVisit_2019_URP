using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectBase;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;
using HomeVisit.Character;
using System;
using HomeVisit.Task;
using TMPro;
using HomeVisit.Screenshot;
using Cysharp.Threading.Tasks;

namespace HomeVisit.UI
{
	public enum RecordState
	{
		None, WaitRecord, Recording, HaveResult, ResultIsRight
	}
	public class OnVisitPanelData : UIPanelData
	{

	}
	public partial class OnVisitPanel : UIPanel
	{
		bool isRefuseGift = false;
		public string[] keywords;
		public RecordState recordState;
		public int score = 0;
		bool isConfirm = false;
		DateTime startTime;
		[SerializeField] TextMeshProUGUI dialoguePrefab;
		[SerializeField] Sprite[] sprites;
		List<TextMeshProUGUI> tmpDialogues = new List<TextMeshProUGUI>();

		protected override void OnInit(IUIData uiData = null)
		{
			EventCenter.GetInstance().RemoveEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);
			EventCenter.GetInstance().RemoveEventListener<int>("���й����������", AddScore);

			EventCenter.GetInstance().AddEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);
			EventCenter.GetInstance().AddEventListener<int>("���й����������", AddScore);

			mData = uiData as OnVisitPanelData ?? new OnVisitPanelData();

			btnCancelObserveDetail.onClick.AddListener(() =>
			{
				imgObserveDetail.gameObject.SetActive(false);
			});
			btnConfirmObserveDetail.onClick.AddListener(ConfirmObserveDetail);
			btnStartRecord.onClick.AddListener(StartRecord);
			btnEndRecord.onClick.AddListener(EndRecord);
			btnReRecord.onClick.AddListener(ReRecord);
			btnConfirmRecord.onClick.AddListener(ConfirmRecord);
			btnRefuse.onClick.AddListener(() =>
			{
				imgExpressGratitude.gameObject.SetActive(false);
				isRefuseGift = true;
			});
			btnAccept.onClick.AddListener(() =>
			{
				imgGratitudeTip.gameObject.SetActive(true);
				imgExpressGratitude.gameObject.SetActive(false);
			});
			btnConfirmGratitudeTip.onClick.AddListener(() =>
			{
				imgGratitudeTip.gameObject.SetActive(false);
				imgExpressGratitude.gameObject.SetActive(true);
			});
			btnNext.onClick.AddListener(Next);
			btnSubmitOnVisit.onClick.AddListener(SubmitOnVisit);
			//��ʷ�Ի�
			btnSwitchHistoryDialogueList.onClick.AddListener(() =>
			{ 
				imgHistoryDialogueList.gameObject.SetActive(!imgHistoryDialogueList.gameObject.activeInHierarchy);
			});
			//��ͼ
			btnScreenshot.onClick.AddListener(()=> { ScreenshotManager.Instance.CaptureScreenshot(rawImgScreenshot); });
			btnShowScreenshot.onClick.AddListener(() => { imgScreenshot.gameObject.SetActive(true); });
			btnCloseHistoryDialogueList.onClick.AddListener(() => { imgHistoryDialogueList.gameObject.SetActive(false); });
			btnCloseScreenshot.onClick.AddListener(() => { imgScreenshot.gameObject.SetActive(false); });

			InitState();

			StartCoroutine(LoadSceneAsync());
		}

		void Next()
		{
			StartCoroutine(UnloadOnVisitAsync());
		}

		IEnumerator UnloadOnVisitAsync()
		{
			AsyncOperation unload = SceneManager.UnloadSceneAsync(Settings.OldRandomScene);
			yield return unload;
			MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
			yield return UIKit.OpenPanelAsync<RecordSheetPanel>(prefabName: Settings.UI + QAssetBundle.Recordsheetpanel_prefab.RECORDSHEETPANEL);
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
		//չʾ�ҳ�˵�Ļ�
		public void ShowParentWord(int index, string strWord)
		{
			CloseRecord();
			btnDialogue.gameObject.SetActive(true);
			btnDialogue.sprite = sprites[index];
			txtDialogue.text = strWord;
			string historyWord = "";
			if (index == 0)
				historyWord = "ĸ�ף�" + strWord;
			else if (index == 2)
				historyWord = "ѧ����" + strWord;
			else if (index == 3)
				historyWord = "ѧ����" + strWord;
			else if (index == 4)
				historyWord = "���ף�" + strWord;
			else
				return;
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
			StartCoroutine(WaveChange());
			RecordManager.Instance.StartRecord(keywords);

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
		IEnumerator WaveChange()
		{
			float value = 0;
			WaitForSeconds wait = new WaitForSeconds(0.05f);
			imgFillWave.fillOrigin = 0;

			while (value < 1)
			{
				imgFillWave.fillAmount = value;
				value += 0.1f;
				yield return wait;
			}

			imgFillWave.fillOrigin = 1;
			while (value > 0)
			{
				imgFillWave.fillAmount = value;
				value -= 0.1f;
				yield return wait;
			}
			btnEndRecord.interactable = true;
		}
		//�ر�����¼��UI
		public void CloseRecord()
		{
			imgPreSpeak.gameObject.SetActive(false);
			imgOnSpeak.gameObject.SetActive(false);
			imgPostSpeak.gameObject.SetActive(false);
		}
		//ȷ��¼����ֱ�ӽ�����һ��
		public void ConfirmRecord()
		{
			CloseRecord();
			//��¼״̬
			if (recordState == RecordState.HaveResult)
				recordState = RecordState.ResultIsRight;
			else
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
				endTime = DateTime.Now,
				maxScore = maxScore,
				score = this.score
			};
			//���ü�¼����
			score = 0;
			startTime = DateTime.Now;
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
		}
		#endregion

		IEnumerator LoadSceneAsync()
		{
			TopPanel topPanel = UIKit.GetPanel<TopPanel>();
			yield return topPanel.CloseEyeAnim();
			yield return SceneManager.UnloadSceneAsync("Office");
			AsyncOperation operation = SceneManager.LoadSceneAsync(Settings.RandomScene, LoadSceneMode.Additive);
			yield return operation;
			CameraManager.Instance.SetRoamPos(FemaleTeacher.Instance.transform.position);
			CameraManager.Instance.SetRoamForward(FemaleTeacher.Instance.transform.forward);

			switch (UIKit.GetPanel<HomeVisitContentPanel>().GetStudentName())
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
			startTime = DateTime.Now;
			topPanel.ChangeTip("���������ʵ�飬��������Ҫ������Ͻǹؼ���");
		}

		public void InitState()
		{
			imgObserveDetail.gameObject.SetActive(false);
			CloseRecord();
			imgExpressGratitude.gameObject.SetActive(false);
			imgNext.gameObject.SetActive(false);

			UIKit.GetPanel<MainPanel>().StartTMP();
		}

		public WaitUntil ShowExpressGratitude()
		{
			CloseRecord();
			isRefuseGift = false;
			imgExpressGratitude.gameObject.SetActive(true);
			return new WaitUntil(() => { return isRefuseGift; });
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
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
			for (int i = tmpDialogues.Count - 1; i >=0 ; i--)
			{
				Destroy(tmpDialogues[i].gameObject);
				tmpDialogues.RemoveAt(i);
			}
			tmpDialogues.Clear();
			base.Hide();
		}
	}
}
