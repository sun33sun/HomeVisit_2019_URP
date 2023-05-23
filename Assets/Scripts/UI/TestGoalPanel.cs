using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public class TestGoalPanelData : UIPanelData
	{
	}
	public partial class TestGoalPanel : UIPanel,IActionController
	{
		bool isInit = true;
		List<IActionController> acList = new List<IActionController>();
		public ulong ActionID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public IAction Action { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public bool Paused { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
			if (isInit)
			{
				isInit = false;
				return;
			}

			var sequence = ActionKit.Sequence()
				.Callback(() => { TestGoal.gameObject.SetActive(true); })
				.Condition(() => { return !TestGoal.gameObject.activeInHierarchy; })
				.Delay(1, () => { TestAssistance.gameObject.SetActive(true); })
				.Condition(() => { return !TestAssistance.gameObject.activeInHierarchy; })
				.Delay(1, Hide)
				.Start(this);
			acList.Add(sequence);

		}

		protected override void OnHide()
		{
			for (int i = 0; i < acList.Count; i++)
				acList[i].Deinit();
		}

		protected override void OnClose()
		{
		}

		public void Reset()
		{
			throw new System.NotImplementedException();
		}

		public void Deinit()
		{
			throw new System.NotImplementedException();
		}
	}
}
