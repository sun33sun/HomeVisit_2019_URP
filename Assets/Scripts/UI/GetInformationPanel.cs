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
			UIKit.GetPanel<TopPanel>().ChangeTip("��һ��������б�ֱ������ѧ����Ϣ");
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
			string strDistrict = newData.strDistrict == "�ֶ�����" ? "�ֶ���" : newData.strDistrict;
			string strWelcome = "";
			if (newData.strName == "")
				strWelcome = $"��ӭ����{strDistrict}��{newData.strUnit}��ʦ";
			else
				strWelcome = $"��ӭ����{strDistrict}��{newData.strUnit}��{newData.strName}��ʦ";
			PolicyList.tmpTeacher.text = strWelcome;
			SchoolList.tmpTeacher.text = strWelcome;
			ClassList.tmpTeacher.text = strWelcome;
			StudentList.tmpTeacher.text = strWelcome;
		}
	}
}
