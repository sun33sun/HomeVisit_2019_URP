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

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitContentPanelData ?? new HomeVisitContentPanelData();

			btnClose.onClick.AddListener(()=> 
			{
				HomeVisitFormPanel homeVisitFormPanel = UIKit.GetPanel<HomeVisitFormPanel>();
				if (homeVisitFormPanel == null)
					UIKit.OpenPanelAsync<HomeVisitFormPanel>().ToAction().Start(this);
				else
					homeVisitFormPanel.Show();
				Hide();
			});

			btnSubmit.onClick.AddListener(Submit);
			btnConfirm.onClick.AddListener(Confirm);
		}

		void Confirm()
		{
			Hide();
			UIKit.OpenPanelAsync<GetInfornationPanel>().ToAction().Start(this);
		}

		void Submit()
		{
			HomeVisitFormPanel homeVisitFormPanel = UIKit.GetPanel<HomeVisitFormPanel>();
			if (homeVisitFormPanel == null)
				UIKit.OpenPanelAsync<HomeVisitFormPanel>().ToAction().Start(this);
			else
				homeVisitFormPanel.Show();
			Hide();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		public string GetStudentName()
		{
			string studentName = "";
			for (int i = 0; i < questionnaire.btns.Count; i++)
			{
				if(questionnaire.btns[i] == questionnaire.midBtn)
				{
					switch (i)
					{
						case 0:
							studentName = "张继光";
							break;
						case 1:
							studentName = "林光美";
							break;
						case 2:
							studentName = "秦彦威";
							break;
					}
				}
			}
			return studentName;
		}

		public WaitUntil ShowSelectedStudentInfo()
		{
			DoubleClickEvent midDoubleClickEvent = questionnaire.midBtn.GetComponent<DoubleClickEvent>();
			midDoubleClickEvent.OnDoubleClick?.Invoke();
			questionnaire.btnBigQuestionnaire.isDoubleClick = false;
			return new WaitUntil(() => { return questionnaire.btnBigQuestionnaire.isDoubleClick; });
		}
	}
}
