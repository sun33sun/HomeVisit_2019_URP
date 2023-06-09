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
		int visitStepIndex = 0;
		[SerializeField] Image imgBk;

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
			visitStepPanels[visitStepIndex].NextStep();
		}

		public void StartTMP()
		{
			visitStepPanels[visitStepIndex].StartTMP();
		}
		public void NextTmp()
		{
			visitStepPanels[visitStepIndex].NextTmp();
		}

		public void NextVisitStepPanel()
		{
			visitStepPanels[visitStepIndex].InitState();
			visitStepPanels[visitStepIndex].gameObject.SetActive(false);
			visitStepIndex++;
			visitStepPanels[visitStepIndex].gameObject.SetActive(true);
			visitStepPanels[visitStepIndex].InitState();

			imgVisitProgress.sprite = visitStepPanels[visitStepIndex].spriteProgress;
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


		public void SetBK(bool isShow)
		{
			imgBk.enabled = isShow;
		}
	}
}
