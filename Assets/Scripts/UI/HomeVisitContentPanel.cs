using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class HomeVisitContentPanelData : UIPanelData
	{
	}
	public partial class HomeVisitContentPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitContentPanelData ?? new HomeVisitContentPanelData();


			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(() =>
			{
				UIKit.GetPanel<MainPanel>().NextStep();
				UIKit.OpenPanelAsync<HomeVisitFormPanel>().ToAction().Start(this);
				Hide();
			});

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
	}
}
