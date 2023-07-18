using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;
using ProjectBase;

namespace HomeVisit.UI
{
	public class HomeVisitContentPanelData : UIPanelData
	{
	}
	public partial class HomeVisitContentPanel : UIPanel
	{
		public Questionnaire questionnaire;
		DateTime startTime;

		private void Start()
		{
			startTime = DateTime.Now;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitContentPanelData ?? new HomeVisitContentPanelData();

			btnClose.onClick.AddListener(()=> 
			{
				UIKit.ShowPanel<HomeVisitFormPanel>();
				Hide();
			});
			btnConfirm.onClick.AddListener(ShowStudentInformation);
		}

		void Confirm()
		{
			ScoreReportData data = new ScoreReportData()
			{
				title = "确认家访内容",
				startTime = this.startTime,
				endTime = DateTime.Now,
				score = 2
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);

			Hide();
			UIKit.ShowPanel<HomeVisitFormPanel>();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			UIKit.GetPanel<TopPanel>().ChangeTip("双击左键选择家访训练");
		}

		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		public string GetStudentName()
		{
			return questionnaire.SceneName;
		}

		public void ShowStudentInformation()
		{
			GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
			NewStudentData data = panel.StudentList.datas.Find(s => s.name == questionnaire.StudentName);
			panel.StudentInformation.InitData(data);
			panel.gameObject.SetActive(true);
			panel.StudentInformation.gameObject.SetActive(true);
			panel.InformationSecurity.gameObject.SetActive(true);
			Hide();
			ActionKit.Sequence()
				.DelayFrame(1)
				.Condition(() => { return !panel.StudentInformation.gameObject.activeInHierarchy; })
				.DelayFrame(1)
				.Callback(Confirm)
				.Start(this);
		}
	}
}
