using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using ProjectBase;
using System;
using Newtonsoft.Json;

namespace HomeVisit.UI
{
	public class TestReportPanelData : UIPanelData
	{
		public List<ScoreReportData> datas = null;
	}
	public partial class TestReportPanel : UIPanel
	{
		[SerializeField] List<ScoreReport> reportList;
		DateTime startTime;

		private void Start()
		{
			startTime = DateTime.Now;
			MonoMgr.GetInstance().AddFixedUpdateListener(() => { tmpDate.text = "当前时间：" + DateTime.Now.ToString("yyyy/MM-dd HH:mm"); });
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestReportPanelData ?? new TestReportPanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(Submit);
		}

		public void InitReport(TestReportPanelData data)
		{
			mData = data;
			if (mData.datas != null)
			{
				List<ScoreReportData> datas = mData.datas;
				for (int i = 0; i < datas.Count; i++)
				{
					reportList[i].Init(datas[i]);
				}
			}
		}

		void Submit()
		{
			int projectScore = 0;
			List<Step> steps = new List<Step>();
			for (int i = 0; i < reportList.Count; i++)
			{
				string strEvaluation;
				float percentage = reportList[i].Data.score / (float)reportList[i].Data.maxScore;
				if (percentage > 0.8)
					strEvaluation = "优";
				else if (percentage > 0.66f)
					strEvaluation = "良";
				else
					strEvaluation = "差";
				Step newStep = new Step()
				{
					seq = 0,
					title = reportList[i].Data.title,
					startTime = reportList[i].Data.startTime.ToString("u"),
					endTime = reportList[i].Data.endTime.ToString("u"),
					timeUsed = (reportList[i].Data.startTime - reportList[i].Data.endTime).ToString(),
					expectTime = reportList[i].Data.expectTime.ToString(),
					maxScore = reportList[i].Data.maxScore,
					score = reportList[i].Data.score,
					repeatCount = 1,
					evaluation = strEvaluation
				};
				projectScore += reportList[i].Data.score;
				steps.Add(newStep);
			}
			ContextJson newContextJson = new ContextJson()
			{
				username = "username",
				title = "基础教育家访",
				status = 1,
				score = projectScore,
				startTime = startTime.ToString(),
				endTime = DateTime.Now.ToString(),
				timeUsed = (startTime - DateTime.Now).ToString(),
				appid = 0,
				originId = 0,
				group_id = 0,
				group_name = null,
				role_in_group = null,
				steps = steps
			};
			SubmitData submitData = new SubmitData()
			{
				customName = "上海师范大学",
				accountNumber = "test",
				contextJson = JsonConvert.SerializeObject(newContextJson)
			};
			HttpManager.GetInstance().Post(submitData);
		}


		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(Content);
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		public void HideRender()
		{
			imgBk.gameObject.SetActive(false);
		}

		public void ShowRender()
		{
			imgBk.gameObject.SetActive(true);
		}

		public void CreateScoreReport(ScoreReportData data)
		{
			ScoreReport report = reportList.Find(r => r.name.Equals(data.title));
			report.Data.startTime = data.startTime;
			report.Data.endTime = data.endTime;
			if (data.maxScore != 0)
				report.Data.score = data.maxScore;
			report.Data.score = data.score;
			report.Init(report.Data);
			//计算总分
			int totalScore = 0;
			foreach (var item in reportList)
			{
				totalScore += item.Data.score;
			}
			tmpTotalScore.text = "总成绩：" + totalScore.ToString();
		}

		public void SetTestEvaluate(string newContent)
		{
			tmpTestEvaluate.text = newContent;
		}
	}
}
