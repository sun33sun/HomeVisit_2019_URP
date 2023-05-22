using UnityEngine;
using UnityEngine.UI;
using QFramework;
using HomeVisit.Draw;

namespace HomeVisit.UI
{
	public class HomeVisitRoutePanelData : UIPanelData
	{

	}
	public partial class HomeVisitRoutePanel : UIPanel
	{
		[SerializeField] DrawDriver drawDriver;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitRoutePanelData ?? new HomeVisitRoutePanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(() =>
			{
				UIKit.OpenPanelAsync<CommunicateOutlinePanel>().ToAction().Start(this);
				UIKit.GetPanel<MainPanel>().NextStep();
				Hide();
				UIKit.GetPanel<TestReportPanel>().SetTestEvaluate(inputTestEvaluate.text);
			});

			btnDialogue.onClick.AddListener(() => 
			{
				btnDialogue.gameObject.SetActive(false);
				imgTip.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(() =>
			{
				imgTip.gameObject.SetActive(false);
				svMap.gameObject.SetActive(true);
			});
			btnDraw.onClick.AddListener(() => { drawDriver.isEnable = !drawDriver.isEnable; });
			btnErase.onClick.AddListener(drawDriver.Clear);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			imgTip.gameObject.SetActive(false);
			svMap.gameObject.SetActive(false);
			imgRoute.gameObject.SetActive(false);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
