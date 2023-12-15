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

		private void Start()
		{
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
			int totalScore = 0;
            foreach (var report in reportList)
            {
				ScoreReportData data = report.Data;
				if(data == null)
					data = new ScoreReportData();
				//步骤
				WebSendScore.Instance.SetStartTime(data.startTime);
				WebSendScore.Instance.SetEndTime(data.endTime);
				//总分
				totalScore += data.score;
            }

			tmpTotalScore.text = totalScore.ToString();

			WebSendScore.Instance.Submit();
        }


		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(objScreenshot);
			LayoutRebuilder.ForceRebuildLayoutImmediate(Content);
		}

		public void ReloadScreenshot(Texture texture)
		{
			imgScreenshot.texture = texture;
			imgScreenshot.SetNativeSize();
			LayoutRebuilder.ForceRebuildLayoutImmediate(objScreenshot);
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
			tmpTotalScore.text = totalScore.ToString();
		}

		public void SetTestEvaluate(string newContent)
		{
			tmpTestEvaluate.text = newContent;
		}
	}
}
