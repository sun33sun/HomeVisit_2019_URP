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


			ActionKit.Sequence()
				.Callback(() => { TestGoal.gameObject.SetActive(true); })
				.Condition(() => { return !TestGoal.gameObject.activeInHierarchy; })
				.Delay(1, () => { TestAssistance.gameObject.SetActive(true); })
				.Condition(() => { return !TestAssistance.gameObject.activeInHierarchy; })
				.Delay(1, Hide)
				.Start(this);
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
