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
		public bool IsCompleted = false;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitFormPanelData ?? new HomeVisitFormPanelData();

			btnConfirm.onClick.AddListener(() =>
			{
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
				AudioManager.Instance.StopAudio();
			});

			btnClose.onClick.AddListener(Close);

			btnConfirmFrom.onClick.AddListener(ConfirmForm);
			btnSubmitFrom.onClick.AddListener(SubmitForm);


			StartCoroutine(LoadTitleAsync());
		}

		void Close()
		{
			//生成实验报告
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				title = "确认家访形式",
				startTime = startTime,
				endTime = DateTime.Now,
				maxScore = titles.Count,
				score = totalScore
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
			//调整UI
			if (routePanel == null)
				UIKit.OpenPanelAsync<HomeVisitRoutePanel>(prefabName:Settings.UI + QAssetBundle.Homevisitroutepanel_prefab.HOMEVISITROUTEPANEL).ToAction().Start(this, () => { routePanel = UIKit.GetPanel<HomeVisitRoutePanel>(); });
			else
				UIKit.ShowPanel<HomeVisitRoutePanel>();
			Hide();
		}

		void ConfirmForm()
		{
			//生成实验报告
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
			{
				//检测题目对错
				titles[i].CheckTitle();
				totalScore += titles[i].GetScore();
			}
			ScoreReportData data = new ScoreReportData()
			{
				title = "确认家访形式",
				startTime = startTime,
				endTime = DateTime.Now,
				maxScore = titles.Count,
				score = totalScore
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
			btnSubmitFrom.transform.SetAsLastSibling();
		}

		void SubmitForm()
		{
			if (routePanel == null)
				UIKit.OpenPanelAsync<HomeVisitRoutePanel>(prefabName: Settings.UI + QAssetBundle.Homevisitroutepanel_prefab.HOMEVISITROUTEPANEL).ToAction().Start(this, () => { routePanel = UIKit.GetPanel<HomeVisitRoutePanel>(); });
			else
				UIKit.ShowPanel<HomeVisitRoutePanel>();
			Hide();
		}

		IEnumerator LoadTitleAsync()
		{
			yield return WebKit.GetInstance().Read<List<JudgeTitleData>>(Settings.PAPER + "Paper_Form.json", datas =>
			{
				print("Paper_Form.json读取成功，准备加载为JudgeTitle");
				foreach (var item in datas)
				{
					CreateJudgeTitle(item);
				}
				btnSubmit.SetAsLastSibling();
			});
			IsCompleted = true;
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
			UIKit.GetPanel<MainPanel>().NextStep();
			UIKit.GetPanel<TopPanel>().ChangeTip("请在题目出现后进行答题");

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
