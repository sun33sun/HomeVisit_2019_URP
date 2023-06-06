using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class KnowledgeExamPanelData : UIPanelData
	{
	}
	public partial class KnowledgeExamPanel : UIPanel
	{
		[SerializeField] GameObject singlePrefab = null;
		[SerializeField] GameObject multiplePrefab = null;

		List<ITitle> titles = new List<ITitle>();

		DateTime startTime;

		void TestExam()
		{
			DateTime startTime = DateTime.Now;
			SingleTitleData singleData = new SingleTitleData()
			{
				rightIndex = 0,
				strDescribe = "单选题",
				strA = "选项A",
				strB = "选项B",
				strC = "选项C",
				strD = "选项D",
				score = 20,

				strModule = "知识考核",
				strStart = startTime
			};
			CreateSingleTitle(singleData);

			MultipleTitleData multipleData = new MultipleTitleData()
			{
				isRights = new bool[4] {true,true,false,false},
				strDescribe = "多选题",
				strA = "选项A",
				strB = "选项B",
				strC = "选项C",
				strD = "选项D",
				score = 30,
				strModule = "知识考核"
			};
			CreateMultipleTitle(multipleData);

			btnSubmit.transform.SetAsLastSibling();
			btnConfirm.transform.SetAsLastSibling();
		}

		GameObject CreateSingleTitle(SingleTitleData data)
		{
			SingleTitle singleTitle = ExamManager.Instance.CreateSingleTitle(data);
			titles.Add(singleTitle);
			singleTitle.transform.SetParent(Content);
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

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgeExamPanelData ?? new KnowledgeExamPanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(()=>
			{
				imgSubmitExam.gameObject.SetActive(true);
				imgExam.gameObject.SetActive(false);
			});
			btnCancel.onClick.AddListener(()=> 
			{
				imgSubmitExam.gameObject.SetActive(false);
				imgExam.gameObject.SetActive(true);
				for (int i = 0; i < titles.Count; i++)
					titles[i].Reset();
			});
			btnConfirm.onClick.AddListener(Confirm);
			btnConfirmSubmit.onClick.AddListener(ConfirmSubmit);

			TestExam();
		}

		void Confirm()
		{
			for (int i = 0; i < titles.Count; i++)
				titles[i].CheckTitle();
			btnSubmit.transform.SetAsLastSibling();
		}

		void ConfirmSubmit()
		{
			imgSubmitExam.gameObject.SetActive(true);
			imgExam.gameObject.SetActive(false);

			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "知识考核",
				strStart = startTime,
				strEnd = DateTime.Now,
				strScore = totalScore.ToString()
			};
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.CreateScoreReport(data);

			UIKit.ShowPanel<TestReportPanel>();
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
