using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class RecordSheetPanelData : UIPanelData
	{
	}
	public partial class RecordSheetPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as RecordSheetPanelData ?? new RecordSheetPanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(Hide);
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
