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
		public int totalScore = 0;
		bool isConfirm = false;

		protected override void OnInit(IUIData uiData = null)
		{
			EventCenter.GetInstance().AddEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);

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
				imgNext.gameObject.SetActive(true);
				imgExpressGratitude.gameObject.SetActive(false);
				isRefuseGift = true;
			});
			btnAccept.onClick.AddListener(() =>
			{
				imgNext.gameObject.SetActive(true);
				imgExpressGratitude.gameObject.SetActive(false);
			});
			btnNext.onClick.AddListener(() =>
			{
				UIKit.OpenPanelAsync<RecordSheetPanel>().ToAction().Start(this);
				Hide();
			});
			btnSubmitOnVisit.onClick.AddListener(SubmitOnVisit);

			InitState();

			StartCoroutine(LoadSceneAsync());
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
		public void ShowParentWord(Sprite spriteParent, string strWord)
		{
			CloseRecord();
			btnDialogue.gameObject.SetActive(true);
			btnDialogue.sprite = spriteParent;
			txtDialogue.text = strWord;
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
					totalScore += 5;
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
			WaitForSeconds wait = new WaitForSeconds(0.1f);
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


		IEnumerator LoadSceneAsync()
		{
			yield return CloseEyeAnim();

			Settings.BanGongShi = false;
			AsyncOperation operation = SceneManager.LoadSceneAsync("MultipleChildren", LoadSceneMode.Additive);
			yield return new WaitUntil(() => { return operation.isDone; });
			CameraManager.Instance.SetRoamPos(FemaleTeacher.Instance.transform.position);
			CameraManager.Instance.SetRoamForward(FemaleTeacher.Instance.transform.forward);
			yield return OpenEyeAnim();
		}

		IEnumerator CloseEyeAnim()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 1, 1, 1);
			float duration = 0.1f;
			WaitForSeconds wait01 = new WaitForSeconds(0.1f);
			while (vector.y > 0)
			{
				vector.y -= duration;
				mat.SetVector("_Param", vector);
				yield return wait01;
			}
		}

		IEnumerator OpenEyeAnim()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 0, 1, 1);
			float duration = 0.1f;
			WaitForSeconds wait01 = new WaitForSeconds(0.1f);
			while (vector.y < 1)
			{
				vector.y += duration;
				mat.SetVector("_Param", vector);
				yield return wait01;
			}
			imgBlank.gameObject.SetActive(false);
		}




		public void InitState()
		{
			imgObserveDetail.gameObject.SetActive(false);
			CloseRecord();
			imgExpressGratitude.gameObject.SetActive(false);
			imgNext.gameObject.SetActive(false);

			//������
			MainPanel main = UIKit.GetPanel<MainPanel>();
			main.NextVisitStepPanel();

			UIKit.GetPanel<MainPanel>().StartTMP();
		}

		public WaitUntil ShowExpressGratitude()
		{
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
	}
}
