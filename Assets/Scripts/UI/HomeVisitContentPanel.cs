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
				UIKit.ShowPanel<HomeVisitFormPanel>();
				Hide();
			});
			btnConfirm.onClick.AddListener(Confirm);
		}

		void Confirm()
		{
			Hide();
			UIKit.ShowPanel<HomeVisitFormPanel>();
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
			return questionnaire.StudentName;
		}

		public WaitUntil ShowSelectedStudentInfo()
		{
			//TODO:
			questionnaire.btnBigQuestionnaire.gameObject.SetActive(true);
			questionnaire.btnBigQuestionnaire.isDoubleClick = false;
			return new WaitUntil(() => { return questionnaire.btnBigQuestionnaire.isDoubleClick; });
		}
	}
}
