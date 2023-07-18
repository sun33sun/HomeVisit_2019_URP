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
		public TeacherData nowTeacherData = null;
		public DateTime startTime;

		private void Start()
		{
			startTime = DateTime.Now;
			btnConfirmInformationSecurity.onClick.AddListener(() => { InformationSecurity.gameObject.SetActive(false); });
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GetInformationPanelData ?? new GetInformationPanelData();

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

		public void SaveTeacherData(TeacherData newData)
		{
			nowTeacherData = newData;
			string strDistrict = newData.strDistrict == "浦东新区" ? "浦东新" : newData.strDistrict;
			string strWelcome = "";
			if (newData.strName == "")
				strWelcome = $"欢迎您：{strDistrict}区{newData.strUnit}老师";
			else
				strWelcome = $"欢迎您：{strDistrict}区{newData.strUnit}的{newData.strName}老师";
			PolicyList.tmpTeacher.text = strWelcome;
			SchoolList.tmpTeacher.text = strWelcome;
			ClassList.tmpTeacher.text = strWelcome;
			StudentList.tmpTeacher.text = strWelcome;
		}
	}
}
