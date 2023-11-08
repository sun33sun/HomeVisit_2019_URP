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
		List<OutlineTitleData> datas = null;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as CommunicateOutlinePanelData ?? new CommunicateOutlinePanelData();

			btnClose.onClick.AddListener(Submit);
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
			inputCommunicateOutline.interactable = false;
		}

		void Submit()
		{
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore = titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				seq = 2,
				title = "交流提纲",
				startTime = startTime,
				endTime = DateTime.Now,
				maxScore = titles.Count,
				score = totalScore,
				expectTime = new TimeSpan(0,18,0)
			};
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.CreateScoreReport(data);
			//下一个页面
			UIKit.OpenPanelAsync<ClothesPanel>(prefabName: Settings.UI + QAssetBundle.Uiprefab.CLOTHESPANEL).ToAction().Start(this);
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
			UIKit.GetPanel<MainPanel>().NextStep();

			btnConfirm.gameObject.SetActive(true);
			btnSubmit.gameObject.SetActive(false);
			inputCommunicateOutline.interactable = true;
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}


	}
}
