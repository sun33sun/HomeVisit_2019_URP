using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class HomeVisitContentPanelData : UIPanelData
	{
	}
	public partial class HomeVisitContentPanel : UIPanel
	{
		[SerializeField] GameObject singlePrefab = null;
		[SerializeField] GameObject multiplePrefab = null;

		List<ITitle> titles = new List<ITitle>();

		DateTime startTime;
		DateTime endTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitContentPanelData ?? new HomeVisitContentPanelData();


			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(Submit);

			TestExam();
		}

		void Submit()
		{
			UIKit.GetPanel<MainPanel>().NextStep();
			UIKit.OpenPanelAsync<HomeVisitFormPanel>().ToAction().Start(this);
			Hide();

			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			endTime = DateTime.Now;
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "�ҷ�����",
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
				strDescribe = "��ѡ��",
				strA = "ѡ��A",
				strB = "ѡ��B",
				strC = "ѡ��C",
				strD = "ѡ��D",
				score = 20,

				strModule = "�ҷ�����",
				strStart = startTime
			};
			CreateSingleTitle(singleData);

			MultipleTitleData multipleData = new MultipleTitleData()
			{
				isRights = new bool[4] { true, true, false, false },
				strDescribe = "��ѡ��",
				strA = "ѡ��A",
				strB = "ѡ��B",
				strC = "ѡ��C",
				strD = "ѡ��D",
				score = 30,
				strModule = "�ҷ�����"
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
		}

		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
