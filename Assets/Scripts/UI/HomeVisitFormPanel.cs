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
		DateTime startTime;
		HomeVisitRoutePanel routePanel = null;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitFormPanelData ?? new HomeVisitFormPanelData();

			btnConfirm.onClick.AddListener(() =>
			{
				UIKit.GetPanel<MainPanel>().NextStep();
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
				AudioManager.Instance.StopAudio();
			});

			btnClose.onClick.AddListener(Hide);

			btnConfirmFrom.onClick.AddListener(() =>
			{
				for (int i = 0; i < titles.Count; i++)
					titles[i].CheckTitle();
				btnSubmitFrom.transform.SetAsLastSibling();
			});
			btnSubmitFrom.onClick.AddListener(SubmitForm);

			StartCoroutine(LoadTitleAsync());
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

			if (routePanel == null)
				UIKit.OpenPanelAsync<HomeVisitRoutePanel>().ToAction().Start(this, () => { routePanel = UIKit.GetPanel<HomeVisitRoutePanel>(); });
			else
				UIKit.ShowPanel<HomeVisitRoutePanel>();
			UIKit.GetPanel<MainPanel>().NextStep();
			Hide();
		}

		IEnumerator LoadTitleAsync()
		{
			yield return WebKit.GetInstance().Read<List<JudgeTitleData>>(Settings.PAPER + "Paper_Form.json", datas =>
			{
				foreach (var item in datas)
				{
					CreateJudgeTitle(item);
				}
				btnSubmit.SetAsLastSibling();
			});
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
			AudioManager.Instance.PlayAudio("1.确认家访形式");

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
