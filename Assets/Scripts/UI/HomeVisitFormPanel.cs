using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class HomeVisitFormPanelData : UIPanelData
	{
	}
	public partial class HomeVisitFormPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitFormPanelData ?? new HomeVisitFormPanelData();

			btnConfirm.onClick.AddListener(() =>
			{
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
			});
			btnCancel.onClick.AddListener(() =>
			{
				imgExam.gameObject.SetActive(true);
				imgSubmitExam.gameObject.SetActive(false);
			});

			btnClose.onClick.AddListener(Hide);

			btnSubmit.onClick.AddListener(() =>
			{
				UIKit.OpenPanelAsync<HomeVisitRoutePanel>().ToAction().Start(this);
				UIKit.GetPanel<MainPanel>().NextStep();
				Hide();
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			imgExam.gameObject.SetActive(false);
			imgSubmitExam.gameObject.SetActive(true);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
