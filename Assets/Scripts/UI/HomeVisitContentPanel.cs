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

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitContentPanelData ?? new HomeVisitContentPanelData();


			btnClose.onClick.AddListener(Hide);

			btnConfirm.onClick.AddListener(() =>
			{
				for (int i = 0; i < titles.Count; i++)
					titles[i].CheckTitle();
				btnSubmit.transform.SetAsLastSibling();
			});
			btnSubmit.onClick.AddListener(Submit);
		}

		void Submit()
		{
			UIKit.OpenPanelAsync<HomeVisitFormPanel>().ToAction().Start(this);
			Hide();

			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "¼Ò·ÃÄÚÈÝ",
				strStart = startTime,
				strEnd = DateTime.Now,
				strScore = totalScore.ToString()
			};
			testReportPanel.CreateScoreReport(data);
		}

		//GameObject CreateSingleTitle(SingleTitleData data)
		//{
		//	GameObject gameObj = Instantiate(singlePrefab);
		//	gameObj.name = singlePrefab.name;
		//	SingleTitle singleTitle = gameObj.GetComponent<SingleTitle>();
		//	titles.Add(singleTitle);
		//	singleTitle.Init(data);
		//	gameObj.transform.SetParent(Content);
		//	gameObj.transform.localScale = Vector3.one;
		//	return gameObj;
		//}

		//GameObject CreateMultipleTitle(MultipleTitleData data)
		//{
		//	GameObject gameObj = Instantiate(multiplePrefab);
		//	gameObj.name = multiplePrefab.name;
		//	MultipleTitle multipleTitle = gameObj.GetComponent<MultipleTitle>();
		//	titles.Add(multipleTitle);
		//	multipleTitle.Init(data);
		//	gameObj.transform.SetParent(Content);
		//	gameObj.transform.localScale = Vector3.one;
		//	return gameObj;
		//}


		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			startTime = DateTime.Now;
			btnSubmit.transform.SetAsLastSibling();
			btnConfirm.transform.SetAsLastSibling();
		}

		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		private void OnEnable()
		{
			Debug.Log(name + "  Enable");
		}
		private void OnDisable()
		{
			Debug.Log(name + "  Disable");
		}
	}
}
