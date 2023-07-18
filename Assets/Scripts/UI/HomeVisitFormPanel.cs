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
			//����ʵ�鱨��
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore += titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				title = "ȷ�ϼҷ���ʽ",
				startTime = startTime,
				endTime = DateTime.Now,
				maxScore = titles.Count,
				score = totalScore
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
			//����UI
			if (routePanel == null)
				UIKit.OpenPanelAsync<HomeVisitRoutePanel>(prefabName:Settings.UI + QAssetBundle.Homevisitroutepanel_prefab.HOMEVISITROUTEPANEL).ToAction().Start(this, () => { routePanel = UIKit.GetPanel<HomeVisitRoutePanel>(); });
			else
				UIKit.ShowPanel<HomeVisitRoutePanel>();
			Hide();
		}

		void ConfirmForm()
		{
			//����ʵ�鱨��
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
			{
				//�����Ŀ�Դ�
				titles[i].CheckTitle();
				totalScore += titles[i].GetScore();
			}
			ScoreReportData data = new ScoreReportData()
			{
				title = "ȷ�ϼҷ���ʽ",
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
				print("Paper_Form.json��ȡ�ɹ���׼������ΪJudgeTitle");
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
			UIKit.GetPanel<TopPanel>().ChangeTip("������Ŀ���ֺ���д���");

			startTime = DateTime.Now;
			imgExam.gameObject.SetActive(false);
			imgSubmitExam.gameObject.SetActive(true);
			AudioManager.Instance.PlayAudio("1.ȷ�ϼҷ���ʽ");

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
