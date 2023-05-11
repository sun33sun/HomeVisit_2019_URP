using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class TestGoalPanelData : UIPanelData
	{
	}
	public partial class TestGoalPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestGoalPanelData ?? new TestGoalPanelData();

			btnTestGoal.onClick.AddListener(() => { TestGoal.gameObject.SetActive(true); });
			btnTestAssistance.onClick.AddListener(() => { TestAssistance.gameObject.SetActive(true); });
			btnCloseGoal.onClick.AddListener(() => { TestGoal.gameObject.SetActive(false); });
			btnCloseAssistance.onClick.AddListener(() => { TestAssistance.gameObject.SetActive(false); });
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
