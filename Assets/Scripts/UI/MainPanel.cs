using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class MainPanelData : UIPanelData
	{
	}
	public partial class MainPanel : UIPanel
	{
		public VisitStepPanel[] visitStepPanels;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as MainPanelData ?? new MainPanelData();

			btnFullScreen.onClick.AddListener(() => { Screen.fullScreen = !Screen.fullScreen; });

			for (int i = 0; i < visitStepPanels.Length; i++)
			{
				visitStepPanels[i].InitState();
			}
		}

		public void NextStep()
		{
			PreVisitPanel.NextStep();
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
