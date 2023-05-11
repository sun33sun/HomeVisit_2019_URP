using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class SubmitTestPanelData : UIPanelData
	{
	}
	public partial class SubmitTestPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as SubmitTestPanelData ?? new SubmitTestPanelData();

			btnCancel.onClick.AddListener(() => { this.Hide(); });
			btnConfirm.onClick.AddListener(() => { });
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
