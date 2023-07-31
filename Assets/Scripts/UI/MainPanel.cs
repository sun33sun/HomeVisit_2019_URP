using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using ProjectBase;
using UnityEngine.EventSystems;
using HomeVisit.Screenshot;

namespace HomeVisit.UI
{
	public class MainPanelData : UIPanelData
	{
	}
	public partial class MainPanel : UIPanel
	{
		public VisitStepPanel[] visitStepPanels;
		int visitStepIndex = 0;
		[SerializeField] Image imgBk;
		[SerializeField] Keyword keywordPrefab;
		RectTransform rtBtnKeyword;
		List<Keyword> keywords = new List<Keyword>();

		protected override void OnInit(IUIData uiData = null)
		{
			EventCenter.GetInstance().AddEventListener<Dictionary<string, bool>>("语音识别结果", OnResult);

			mData = uiData as MainPanelData ?? new MainPanelData();

			#region 全屏
			EventTrigger.Entry entry = new EventTrigger.Entry() { eventID = EventTriggerType.PointerDown };
			entry.callback.AddListener(data => { Screen.fullScreen = !Screen.fullScreen; });
			togFullScreen.triggers.Add(entry);
			#endregion


			for (int i = 0; i < visitStepPanels.Length - 1; i++)
			{
				visitStepPanels[i].InitState();
			}
			btnKeyWord.onClick.AddListener(() => { imgKeywordsDetail.gameObject.SetActive(!imgKeywordsDetail.gameObject.activeInHierarchy); });

			rtBtnKeyword = btnKeyWord.transform as RectTransform;
		}

		public void NextStep()
		{
			visitStepPanels[visitStepIndex].NextStep();
		}

		public void StartTMP()
		{
			visitStepPanels[visitStepIndex].StartTMP();
		}
		public void NextTmp()
		{
			visitStepPanels[visitStepIndex].NextTmp();
		}

		public void NextVisitStepPanel()
		{
			visitStepPanels[visitStepIndex].InitState();
			visitStepPanels[visitStepIndex].gameObject.SetActive(false);
			visitStepIndex++;
			if (visitStepIndex >= visitStepPanels.Length - 1)
			{
				imgVisitProgress.sprite = visitStepPanels[visitStepIndex].spriteProgress;
			}
			else
			{
				visitStepPanels[visitStepIndex].gameObject.SetActive(true);
				visitStepPanels[visitStepIndex].InitState();

				imgVisitProgress.sprite = visitStepPanels[visitStepIndex].spriteProgress;
			}

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


		public void SetBK(bool isShow)
		{
			imgBk.enabled = isShow;
		}

		public IEnumerator InitKeyWords(string[] keywords)
		{
			for (int i = this.keywords.Count - 1; i >= 0; i--)
			{
				Destroy(this.keywords[i].gameObject);
			}
			this.keywords.Clear();
			float Height = 20 + 20 + keywords.Length * 20 + (keywords.Length - 1) * 15;
			for (int i = 0; i < keywords.Length; i++)
			{
				Keyword tmpKeyword = Instantiate(keywordPrefab);
				this.keywords.Add(tmpKeyword);
				tmpKeyword.transform.SetParent(imgKeywordsDetail.transform, false);
				tmpKeyword.tmpKeyword.text = keywords[i];
			}
			Vector3 newPos = Vector3.zero;
			newPos.y = -rtBtnKeyword.sizeDelta.y - Height / 2;
			imgKeywordsDetail.anchoredPosition = newPos;
			LayoutRebuilder.ForceRebuildLayoutImmediate(imgKeywordsDetail);
			yield return null;
		}

		void OnResult(Dictionary<string, bool> keywordDic)
		{
			int i = 0;
			foreach (var item in keywordDic.Values)
			{
				keywords[i].imgRight.enabled = item;
				i++;
			}
		}

		public void ShowCompletedTip()
		{
			imgCompletedTip.gameObject.SetActive(true);
		}

		public void SetProgress(bool isShow)
		{
			visitStepPanels[visitStepIndex].gameObject.SetActive(isShow);
			imgVisitProgress.gameObject.SetActive(isShow);
		}
	}
}
