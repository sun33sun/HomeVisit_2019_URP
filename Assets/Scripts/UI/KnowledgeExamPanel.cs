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
		Knowledge,OnVisit
	}
	public class KnowledgeExamPanelData : UIPanelData
	{
	}
	public partial class KnowledgeExamPanel : UIPanel
	{
		public bool IsFollow = true;

		List<ITitle> titles = new List<ITitle>();
		DateTime startTime;
		ExamState examState = ExamState.Knowledge;



		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgeExamPanelData ?? new KnowledgeExamPanelData();

			btnClose.onClick.AddListener(Hide);
			
			btnConfirm.onClick.AddListener(Confirm);
			btnSubmit.onClick.AddListener(Submit);

			btnCancel.onClick.AddListener(() =>
			{
				btnConfirm.transform.SetAsLastSibling();
				imgSubmitExam.gameObject.SetActive(false);
				imgExam.gameObject.SetActive(true);
				for (int i = 0; i < titles.Count; i++)
					titles[i].Reset();
			});
			btnConfirmSubmit.onClick.AddListener(ConfirmSubmit);


			StartCoroutine(LoadKnowledgeExamPaper());
		}

		IEnumerator LoadKnowledgeExamPaper()
		{
			examState = ExamState.Knowledge;
			yield return WebKit.GetInstance().Read<List<SingleTitleData>>(Settings.PAPER + "Paper_Knowledge.json", datas =>
			{
				foreach (var item in datas)
				{
					CreateSingleTitle(item);
				}
			});
			btnSubmit.transform.SetAsLastSibling();
			btnConfirm.transform.SetAsLastSibling();
		}

		public void LoadOnVisitPaper(List<SingleTitleData> datas)
		{
			for (int i = 0; i < titles.Count; i++)
			{
				Destroy(titles[i].gameObject);
			}
			titles.Clear();
			gameObject.SetActive(true);
			examState = ExamState.OnVisit;
			for (int i = 0; i < datas.Count; i++)
			{
				CreateSingleTitle(datas[i]);
			}
			btnSubmit.transform.SetAsLastSibling();
			btnConfirm.transform.SetAsLastSibling();
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
			for (int i = 0; i < titles.Count; i++)
				titles[i].CheckTitle();
			btnSubmit.transform.SetAsLastSibling();
		}

		void Submit()
		{
			switch (examState)
			{
				case ExamState.Knowledge:
					imgSubmitExam.gameObject.SetActive(true);
					break;
				case ExamState.OnVisit:
					ConfirmSubmit();
					break;
				default:
					break;
			}
		}


		void ConfirmSubmit()
		{
			imgExam.gameObject.SetActive(false);
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			
			switch (examState)
			{
				case ExamState.Knowledge:
					ScoreReportData data = new ScoreReportData()
					{
						strModule = "知识考核",
						strStart = startTime,
						strEnd = DateTime.Now,
						strScore = totalScore.ToString()
					};
					TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
					testReportPanel.CreateScoreReport(data);

					if (IsFollow)
					{
						IsFollow = false;
						UIKit.ShowPanel<TestReportPanel>();
					}
					break;
				case ExamState.OnVisit:
					EventCenter.GetInstance().EventTrigger<int>("访中过程考核结束", totalScore);
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
			for (int i = 0; i < titles.Count; i++)
				titles[i].Reset();

			startTime = DateTime.Now;
			imgExam.gameObject.SetActive(true);
			imgSubmitExam.gameObject.SetActive(false);

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
