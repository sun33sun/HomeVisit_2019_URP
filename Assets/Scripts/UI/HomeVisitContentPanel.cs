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
		public NewStudentData studentData;

		private void Start()
		{
			startTime = DateTime.UtcNow;
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
			
			btnCloseBuildTip.onClick.AddListener(()=>imgBuildTip.gameObject.SetActive(false));
		}

		void Confirm()
		{
			ScoreReportData data = new ScoreReportData()
			{
				title = "确认家访内容",
				startTime = this.startTime,
				endTime = DateTime.UtcNow,
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

		public string GetHomeType()
		{
			return questionnaire.HomeType;
		}

		public void ShowStudentInformation()
		{
			string studentName = questionnaire.StudentName;
			if (studentName == "")
			{
				imgBuildTip.gameObject.SetActive(true);
			}
			else
			{
				GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
				studentData = panel.StudentList.datas.Find(s => s.name == studentName);
				panel.StudentInformation.InitData(studentData);
				panel.gameObject.SetActive(true);
				panel.StudentInformation.gameObject.SetActive(true);
				panel.InformationSecurity.gameObject.SetActive(true);
				Hide();
				ActionKit.Sequence()
					.DelayFrame(1)
					.Condition(() => { return !panel.StudentInformation1.gameObject.activeInHierarchy && !panel.StudentInformation.gameObject.activeInHierarchy;  })
					.Callback(Confirm)
					.Start(this);
			}

		}
	}
}
