using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.IO;
using ProjectBase;

namespace HomeVisit.UI
{
	public class HomeVisitFormPanelData : UIPanelData
	{
	}
	public partial class HomeVisitFormPanel : UIPanel
	{
		List<ITitle> titles = new List<ITitle>();
		public ResLoader mLoader = ResLoader.Allocate();
		DateTime startTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitFormPanelData ?? new HomeVisitFormPanelData();

			btnConfirm.onClick.AddListener(() =>
			{
				UIKit.GetPanel<MainPanel>().NextStep();
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
			});

			btnClose.onClick.AddListener(Hide);

			btnConfirmFrom.onClick.AddListener(() =>
			{
				for (int i = 0; i < titles.Count; i++)
					titles[i].CheckTitle();
				btnSubmitFrom.transform.SetAsLastSibling();
			});
			btnSubmitFrom.onClick.AddListener(SubmitForm);

			LoadTitleAsync();
		}

		void SubmitForm()
		{
			//生成实验报告
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "家访形式",
				strStart = startTime,
				strEnd = DateTime.Now,
				strScore = totalScore.ToString()
			};
			testReportPanel.CreateScoreReport(data);

			HomeVisitRoutePanel routePanel = UIKit.GetPanel<HomeVisitRoutePanel>();
			if (routePanel == null)
				UIKit.OpenPanelAsync<HomeVisitRoutePanel>().ToAction().Start(this);
			else
				routePanel.Show();
			UIKit.GetPanel<MainPanel>().NextStep();
			Hide();
		}

		void LoadTitleAsync()
		{
			mLoader.Add2Load<TextAsset>("Title", "Paper_Form",(isCompleted,asset)=> 
			{
				if (isCompleted)
				{
					string json = (asset.Asset as TextAsset).text;
					List<JudgeTitleData> datas = JsonConvert.DeserializeObject<List<JudgeTitleData>>(json);
					for (int i = 0; i < datas.Count; i++)
					{
						CreateJudgeTitle(datas[i]);
					}
					btnConfirmFrom.transform.parent.SetAsLastSibling();
					btnSubmitFrom.transform.SetAsLastSibling();
					btnConfirmFrom.transform.SetAsLastSibling();
				}
			});
			mLoader.LoadAsync();
		}

		GameObject CreateJudgeTitle(JudgeTitleData data)
		{
			JudgeTitle judgeTitle = ExamManager.Instance.CreateJudgeTitle(data);
			titles.Add(judgeTitle);
			judgeTitle.transform.SetParent(Content);
			judgeTitle.transform.localScale = Vector3.one;
			return judgeTitle.gameObject;
		}


		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			startTime = DateTime.Now;
			imgExam.gameObject.SetActive(false);
			imgSubmitExam.gameObject.SetActive(true);

			btnSubmitFrom.transform.SetAsLastSibling();
			btnConfirmFrom.transform.SetAsLastSibling();
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
