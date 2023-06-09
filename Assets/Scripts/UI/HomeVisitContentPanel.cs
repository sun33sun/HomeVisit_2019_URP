using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class HomeVisitContentPanelData : UIPanelData
	{
	}
	public partial class HomeVisitContentPanel : UIPanel
	{
		DateTime startTime;
		[SerializeField]Questionnaire questionnaire;

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
			HomeVisitFormPanel homeVisitFormPanel = UIKit.GetPanel<HomeVisitFormPanel>();
			if (homeVisitFormPanel == null)
				UIKit.OpenPanelAsync<HomeVisitFormPanel>().ToAction().Start(this);
			else
				homeVisitFormPanel.Show();
			Hide();
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
			startTime = DateTime.Now;
		}

		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
