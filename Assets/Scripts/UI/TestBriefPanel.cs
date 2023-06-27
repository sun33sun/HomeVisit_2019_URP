using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class TestBriefPanelData : UIPanelData
	{
	}
	public partial class TestBriefPanel : UIPanel
	{
		public bool IsFollow = true;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestBriefPanelData ?? new TestBriefPanelData();
			btnTestGoal.onClick.AddListener(() =>
			{
				imgTestGoal.gameObject.SetActive(true);
				imgTestAssistance.gameObject.SetActive(false);
				imgTestPrinciple.gameObject.SetActive(false);
				imgTestDemand.gameObject.SetActive(false);
			});
			btnTestPrinciple.onClick.AddListener(() =>
			{
				imgTestGoal.gameObject.SetActive(false);
				imgTestPrinciple.gameObject.SetActive(true);
				imgTestDemand.gameObject.SetActive(false);
				imgTestAssistance.gameObject.SetActive(false);
			});
			btnTestDemand.onClick.AddListener(() =>
			{
				imgTestGoal.gameObject.SetActive(false);
				imgTestPrinciple.gameObject.SetActive(false);
				imgTestDemand.gameObject.SetActive(true);
				imgTestAssistance.gameObject.SetActive(false);
			});
			btnTestAssistance.onClick.AddListener(() =>
			{
				imgTestGoal.gameObject.SetActive(false);
				imgTestPrinciple.gameObject.SetActive(false);
				imgTestDemand.gameObject.SetActive(false);
				imgTestAssistance.gameObject.SetActive(true);
			});
			btnClosePanel.onClick.AddListener(() =>
			{
				UIKit.HidePanel<TestBriefPanel>();
				if (IsFollow)
				{
					IsFollow = false;
					UIKit.ShowPanel<HomeVisitContentPanel>();
				}
			});
			UIKit.OpenPanelAsync<HomeVisitContentPanel>().ToAction().Start(this,()=> 
			{
				UIKit.HidePanel<HomeVisitContentPanel>();
			});
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			imgTestGoal.gameObject.SetActive(true);
			imgTestAssistance.gameObject.SetActive(false);
			imgTestPrinciple.gameObject.SetActive(false);
			imgTestDemand.gameObject.SetActive(false);
		}

		protected override void OnHide()
		{

		}

		protected override void OnClose()
		{
		}
	}
}
