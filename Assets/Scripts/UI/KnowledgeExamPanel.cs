using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System.Collections;
using System;
using ProjectBase;

namespace HomeVisit.UI
{
	public enum ExamState
	{
		Knowledge, OnVisit
	}
	public class KnowledgeExamPanelData : UIPanelData
	{
	}
	public partial class KnowledgeExamPanel : UIPanel
	{
		List<ITitle> titles = new List<ITitle>();
		DateTime startTime;
		ExamState examState = ExamState.Knowledge;


		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgeExamPanelData ?? new KnowledgeExamPanelData();

			btnClose.onClick.AddListener(Close);

			btnConfirm.onClick.AddListener(Confirm);
			btnSubmit.onClick.AddListener(Submit);

			btnCancelConfirm.onClick.AddListener(() =>
			{
				imgConfirmConfirm.gameObject.SetActive(false);
			});
			btnConfirmConfirm.onClick.AddListener(ConfirmConfirm);

			StartCoroutine(LoadKnowledgeExamPaper());
		}

		public IEnumerator LoadKnowledgeExamPaper()
		{
			examState = ExamState.Knowledge;
			yield return WebKit.GetInstance().Read<List<SingleTitleData>>(Settings.PAPER + "Paper_Knowledge.json", datas =>
			{
				foreach (var item in datas)
				{
					CreateSingleTitle(item);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(Content);
			});
			btnSubmit.transform.SetAsLastSibling();
			btnConfirm.transform.SetAsLastSibling();
		}

		public void LoadOnVisitPaper(List<SingleTitleData> datas)
		{
			//加载数据
			for (int i = titles.Count - 1; i >= 0; i--)
			{
				Destroy(titles[i].gameObject);
				titles.RemoveAt(i);
			}
			titles.Clear();
			for (int i = 0; i < datas.Count; i++)
				CreateSingleTitle(datas[i]);
			//设置UI
			gameObject.SetActive(true);
			imgExam.gameObject.SetActive(true);
			imgConfirmConfirm.gameObject.SetActive(false);
			btnSubmit.transform.SetAsLastSibling();
			btnConfirm.transform.SetAsLastSibling();
			LayoutRebuilder.ForceRebuildLayoutImmediate(Content);
			//记录状态
			examState = ExamState.OnVisit;
		}

		GameObject CreateSingleTitle(SingleTitleData data)
		{
			SingleTitle singleTitle = ExamManager.Instance.CreateSingleTitle(data);
			titles.Add(singleTitle);
			singleTitle.transform.SetParent(Content);
			singleTitle.transform.localScale = Vector3.one;
			singleTitle.transform.SetAsFirstSibling();
			return singleTitle.gameObject;
		}

		GameObject CreateMultipleTitle(MultipleTitleData data)
		{
			MultipleTitle multipleTitle = ExamManager.Instance.CreateMultipleTitle(data);
			titles.Add(multipleTitle);
			multipleTitle.transform.SetParent(Content);
			multipleTitle.transform.localScale = Vector3.one;
			multipleTitle.transform.SetAsFirstSibling();
			return multipleTitle.gameObject;
		}

		void Confirm()
		{
			switch (examState)
			{
				case ExamState.Knowledge:
					imgConfirmConfirm.gameObject.SetActive(true);
					break;
				case ExamState.OnVisit:
					for (int i = 0; i < titles.Count; i++)
					{
						titles[i].CheckTitle();
						titles[i].SetInteractable(false);
					}
					btnSubmit.transform.SetAsLastSibling();
					break;
				default:
					break;
			}
		}

		void ConfirmConfirm()
		{
			imgConfirmConfirm.gameObject.SetActive(false);
			foreach (var item in titles)
			{
				item.CheckTitle();
				item.SetInteractable(false);
			}
			btnSubmit.transform.SetAsLastSibling();
		}

		void Submit()
		{
			switch (examState)
			{
				case ExamState.Knowledge:
					for (int i = 0; i < titles.Count; i++)
					{
						titles[i].CheckTitle();
					}
					break;
				case ExamState.OnVisit:
					int totalScore1 = 0;
					for (int i = 0; i < titles.Count; i++)
						totalScore1 += titles[i].GetScore();
					EventCenter.GetInstance().EventTrigger<int>("访中过程试题完成", totalScore1);
					Hide();
					break;
				default:
					break;
			}
		}

		void Close()
		{
			switch (examState)
			{
				case ExamState.Knowledge:
					int totalScore = 0;
					for (int i = 0; i < titles.Count; i++)
					{
						totalScore += titles[i].GetScore();
					}
					//创建知识考核的实验报告
					ScoreReportData data = new ScoreReportData()
					{
						seq = 1,
						title = "知识考核",
						startTime = startTime,
						endTime = DateTime.UtcNow,
						score = totalScore
					};
					TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
					testReportPanel.CreateScoreReport(data);
					break;
				case ExamState.OnVisit:
					int totalScore1 = 0;
					for (int i = 0; i < titles.Count; i++)
						totalScore1 += titles[i].GetScore();
					EventCenter.GetInstance().EventTrigger<int>("访中过程试题完成", totalScore1);
					break;
				default:
					break;
			}
			
			Hide();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			UIKit.GetPanel<TopPanel>().ChangeTip("知识考核");


			if(examState == ExamState.Knowledge)
			{
				for (int i = 0; i < titles.Count; i++)
					titles[i].Reset();
			}
			else
			{
				examState = ExamState.Knowledge;
				for (int i = titles.Count - 1; i >= 0; i--)
				{
					Destroy(titles[i].gameObject);
					titles.RemoveAt(i);
				}
				titles.Clear();
				LoadKnowledgeExamPaper();
			}

			startTime = DateTime.UtcNow;
			imgExam.gameObject.SetActive(true);
			imgConfirmConfirm.gameObject.SetActive(false);
			btnConfirm.transform.SetAsLastSibling();
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
