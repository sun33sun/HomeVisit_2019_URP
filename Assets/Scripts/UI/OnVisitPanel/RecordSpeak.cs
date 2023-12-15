/****************************************************************************
 * 2023.11 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using ProjectBase;
using TMPro;

namespace HomeVisit.UI
{
	public partial class RecordSpeak : UIElement
	{
		Coroutine animCor = null;

		public RecordState recordState;
		public int score = 0;
		bool isConfirm = false;

		[HideInInspector] public List<TextMeshProUGUI> tmpDialogues = null;
		[SerializeField] TextMeshProUGUI dialoguePrefab;

		private void Awake()
		{
			EventCenter.GetInstance().RemoveEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);
			EventCenter.GetInstance().RemoveEventListener<string>("ʵʱ�������", RealTimeResult);
			EventCenter.GetInstance().RemoveEventListener<int>("���й����������", AddScore);

			EventCenter.GetInstance().AddEventListener<Dictionary<string, bool>>("����ʶ����", OnResult);
			EventCenter.GetInstance().AddEventListener<int>("���й����������", AddScore);
			EventCenter.GetInstance().AddEventListener<string>("ʵʱ�������", RealTimeResult);

			btnStartRecord.onClick.AddListener(StartRecord);
			btnEndRecord.onClick.AddListener(EndRecord);
			btnReRecord.onClick.AddListener(ReRecord);
			btnConfirmRecord.onClick.AddListener(ConfirmRecord);
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

		void AddScore(int addScore)
		{
			score += addScore;
		}

		//��ʼ¼��
		void StartRecord()
		{
			imgPreSpeak.gameObject.SetActive(false);
			//��¼����UI
			imgOnSpeak.gameObject.SetActive(true);
			//����¼�ƶ���
			if (animCor != null)
				StopCoroutine(animCor);
			animCor = StartCoroutine(RecordAnim());
		}
		//¼�ƶ���
		IEnumerator RecordAnim()
		{
			btnEndRecord.interactable = false;

			float value = 0;
			imgFillWave.fillOrigin = 0;
			float timer = 0;

			bool isAdd = true;
			while (true)
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
				yield return null;
				if (timer > 15)
					btnEndRecord.interactable = true;
			}

			yield return null;
		}
		//����¼��
		void EndRecord()
		{
			StopCoroutine(animCor);
			imgOnSpeak.gameObject.SetActive(false);
			//��¼�ƺ�
			imgPostSpeak.gameObject.SetActive(true);
		}
		//�ر�����¼��UI
		public void CloseRecord()
		{
			tmpSpeakResult.text = "";
			imgPreSpeak.gameObject.SetActive(false);
			imgOnSpeak.gameObject.SetActive(false);
			imgPostSpeak.gameObject.SetActive(false);
		}
		//�ٴ�¼��
		void ReRecord()
		{
			imgPostSpeak.gameObject.SetActive(false);
			imgPreSpeak.gameObject.SetActive(true);
			StartRecord();
		}
		void ConfirmRecord()
		{
			imgPostSpeak.gameObject.SetActive(false);
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}