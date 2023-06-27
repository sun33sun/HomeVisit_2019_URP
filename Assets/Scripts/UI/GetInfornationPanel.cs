using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;

namespace HomeVisit.UI
{
	public class GetInfornationPanelData : UIPanelData
	{
	}
	public partial class GetInfornationPanel : UIPanel
	{
		public TeacherData nowAdministrator = null;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GetInfornationPanelData ?? new GetInfornationPanelData();

			StartCoroutine(HideAdministratorListAsync());
		}

		IEnumerator HideAdministratorListAsync()
		{
			yield return new WaitUntil(()=> { return AdministratorList.datas != null; });
			AdministratorList.gameObject.SetActive(false);
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

		public void SaveAdministratorData(TeacherData newData)
		{
			nowAdministrator = newData;
		}
	}
}
