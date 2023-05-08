using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class ButtonPanelData : UIPanelData
	{
	}
	public partial class ButtonPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ButtonPanelData ?? new ButtonPanelData();

			btnSeting.onClick.AddListener(SwitchSettingBtn);
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

		void SwitchSettingBtn()
		{
			if (btnKnowledge.gameObject.activeInHierarchy)
			{
				btnKnowledge.gameObject.SetActive(false);
				btnTestBrief.gameObject.SetActive(false);
				btnTestReport.gameObject.SetActive(false);
			}
			else
			{
				btnKnowledge.gameObject.SetActive(true);
				btnTestBrief.gameObject.SetActive(true);
				btnTestReport.gameObject.SetActive(true);
			}
		}
	}
}
