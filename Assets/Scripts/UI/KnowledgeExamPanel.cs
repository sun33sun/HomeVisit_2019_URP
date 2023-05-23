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
		List<Toggle> togs = new List<Toggle>();

		DateTime startTime;
		DateTime endTime;

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
			GameObject gameObj = Instantiate(singlePrefab);
			gameObj.name = singlePrefab.name;
			togs.Add(gameObj.GetComponent<Toggle>());
			SingleTitle singleTitle = gameObj.GetComponent<SingleTitle>();
			titles.Add(singleTitle);
			singleTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			gameObj.transform.SetAsFirstSibling();
			return gameObj;
		}

		GameObject CreateMultipleTitle(MultipleTitleData data)
		{
			GameObject gameObj = Instantiate(multiplePrefab);
			gameObj.name = multiplePrefab.name;
			togs.Add(gameObj.GetComponent<Toggle>());
			MultipleTitle multipleTitle = gameObj.GetComponent<MultipleTitle>();
			titles.Add(multipleTitle);
			multipleTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			gameObj.transform.SetAsFirstSibling();
			return gameObj;
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
				strEnd = endTime,
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
			endTime = DateTime.Now;
		}
		
		protected override void OnClose()
		{
		}
	}
}
