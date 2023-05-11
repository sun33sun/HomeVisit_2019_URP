using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class ClothesPanelData : UIPanelData
	{
	}
	public partial class ClothesPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ClothesPanelData ?? new ClothesPanelData();

			btnMan.onClick.AddListener(() => { imgTeacher.sprite = spriteSex[0]; });
			btnWoman.onClick.AddListener(() => { imgTeacher.sprite = spriteSex[1]; });
			btnCloseClothes.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(() =>
			{
				imgTopBk.gameObject.SetActive(false);
				imgMidBk.gameObject.SetActive(false);
				imgConfirm.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(() =>
			{
				Hide();
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			imgTopBk.gameObject.SetActive(true);
			imgMidBk.gameObject.SetActive(true);
			imgConfirm.gameObject.SetActive(false);
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
