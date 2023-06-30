using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using ProjectBase;

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

			btnFullScreen.onClick.AddListener(() => { Screen.fullScreen = !Screen.fullScreen; });

			for (int i = 0; i < visitStepPanels.Length; i++)
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
			visitStepPanels[visitStepIndex].gameObject.SetActive(true);
			visitStepPanels[visitStepIndex].InitState();

			imgVisitProgress.sprite = visitStepPanels[visitStepIndex].spriteProgress;
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
			for (int i = 0; i < keywords.Length; i++)
			{
				Keyword tmpKeyword = Instantiate(keywordPrefab);
				this.keywords.Add(tmpKeyword);
				tmpKeyword.transform.SetParent(imgKeywordsDetail.transform, false);
				tmpKeyword.tmpKeyword.text = keywords[i];
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(imgKeywordsDetail);
			Vector3 newPos = Vector3.zero;
			newPos.y = -rtBtnKeyword.sizeDelta.y - imgKeywordsDetail.sizeDelta.y / 2;
			imgKeywordsDetail.anchoredPosition = newPos;
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
	}
}
