using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class TestReportPanelData : UIPanelData
	{
	}
	public partial class TestReportPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestReportPanelData ?? new TestReportPanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(()=>
			{
				UIKit.OpenPanelAsync<HomeVisitContentPanel>().ToAction().Start(this, ()=> { UIKit.GetPanel<HomeVisitContentPanel>().transform.SetAsLastSibling(); }) ;
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
