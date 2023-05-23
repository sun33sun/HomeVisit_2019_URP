using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class HomeVisitFormPanelData : UIPanelData
	{
	}
	public partial class HomeVisitFormPanel : UIPanel
	{
		[SerializeField] GameObject singlePrefab = null;
		[SerializeField] GameObject multiplePrefab = null;

		List<ITitle> titles = new List<ITitle>();

		DateTime startTime;
		DateTime endTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitFormPanelData ?? new HomeVisitFormPanelData();

			btnConfirm.onClick.AddListener(() =>
			{
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
			});
			btnCancel.onClick.AddListener(() =>
			{
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
			});

			btnClose.onClick.AddListener(Hide);

			btnSubmit.onClick.AddListener(Submit);

			TestExam();
		}

		void Submit()
		{
			UIKit.OpenPanelAsync<HomeVisitRoutePanel>().ToAction().Start(this);
			UIKit.GetPanel<MainPanel>().NextStep();
			Hide();

			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			endTime = DateTime.Now;
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "家访形式",
				strStart = startTime,
				strEnd = endTime,
				strScore = totalScore.ToString()
			};
			testReportPanel.CreateScoreReport(data);
		}

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

				strModule = "家访形式",
				strStart = startTime
			};
			CreateSingleTitle(singleData);

			MultipleTitleData multipleData = new MultipleTitleData()
			{
				isRights = new bool[4] { true, true, false, false },
				strDescribe = "多选题",
				strA = "选项A",
				strB = "选项B",
				strC = "选项C",
				strD = "选项D",
				score = 30,

				strModule = "家访形式"
			};
			CreateMultipleTitle(multipleData);

			btnSubmit.transform.parent.SetAsLastSibling();
		}

		GameObject CreateSingleTitle(SingleTitleData data)
		{
			GameObject gameObj = Instantiate(singlePrefab);
			gameObj.name = singlePrefab.name;
			SingleTitle singleTitle = gameObj.GetComponent<SingleTitle>();
			titles.Add(singleTitle);
			singleTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			return gameObj;
		}

		GameObject CreateMultipleTitle(MultipleTitleData data)
		{
			GameObject gameObj = Instantiate(multiplePrefab);
			gameObj.name = multiplePrefab.name;
			MultipleTitle multipleTitle = gameObj.GetComponent<MultipleTitle>();
			titles.Add(multipleTitle);
			multipleTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			return gameObj;
		}


		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			startTime = DateTime.Now;
			imgExam.gameObject.SetActive(false);
			imgSubmitExam.gameObject.SetActive(true);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
