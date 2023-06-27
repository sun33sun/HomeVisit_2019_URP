using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;
using ProjectBase;
using System.Collections;

namespace HomeVisit.UI
{
	public class CommunicateOutlinePanelData : UIPanelData
	{
		public List<string> strTog = new List<string>();

	}
	public partial class CommunicateOutlinePanel : UIPanel
	{
		[SerializeField] GameObject outlineTitlePrefab;
		List<ITitle> titles = new List<ITitle>();
		public List<string> strTogSelected = new List<string>();

		DateTime startTime;
		DateTime endTime;
		List<OutlineTitleData> datas = null;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as CommunicateOutlinePanelData ?? new CommunicateOutlinePanelData();

			btnClose.onClick.AddListener(Hide);
			btnConfirm.onClick.AddListener(Confirm);
			btnSubmit.onClick.AddListener(Submit);

			StartCoroutine(LoadPaper());
		}

		void Confirm()
		{
			//检查题目对错
			for (int i = 0; i < titles.Count; i++)
				titles[i].CheckTitle();

			//显示提交按钮
			btnConfirm.gameObject.SetActive(false);
			btnSubmit.gameObject.SetActive(true);
		}

		void Submit()
		{
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore = titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "交流提纲",
				strScore = totalScore.ToString(),
			};
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.CreateScoreReport(data);

			//修改进度UI并进入下一个页面
			UIKit.GetPanel<MainPanel>().NextStep();
			UIKit.OpenPanelAsync<ClothesPanel>().ToAction().Start(this);
			Hide();
		}

		IEnumerator LoadPaper()
		{
			DateTime startTime = DateTime.Now;

			yield return WebKit.GetInstance().Read<List<OutlineTitleData>>(Settings.PAPER + "Paper_Outline.json", d => { datas = d; });

			foreach (var item in datas)
			{
				CreateOutlinTitle(item);
			}

			btnConfirm.transform.SetAsLastSibling();
		}

		GameObject CreateOutlinTitle(OutlineTitleData data)
		{
			OutlineTitle outlineTitle = ExamManager.Instance.CreateOutlineTitle(data);
			titles.Add(outlineTitle);
			outlineTitle.gameObject.transform.SetParent(Content);
			outlineTitle.gameObject.transform.localScale = Vector3.one;
			outlineTitle.gameObject.transform.SetAsFirstSibling();
			return outlineTitle.gameObject;
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			startTime = DateTime.Now;
			btnConfirm.gameObject.SetActive(true);
			btnSubmit.gameObject.SetActive(false);
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
