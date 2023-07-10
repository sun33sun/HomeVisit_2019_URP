using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using System;

namespace HomeVisit.UI
{
	public class GetInformationPanelData : UIPanelData
	{
	}
	public partial class GetInformationPanel : UIPanel
	{
		public TeacherData nowAdministrator = null;
		public DateTime startTime;

		private void Start()
		{
			startTime = DateTime.Now;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GetInformationPanelData ?? new GetInformationPanelData();

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
			UIKit.GetPanel<TopPanel>().ChangeTip("请一步步点击列表，直到看到学生信息");
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
