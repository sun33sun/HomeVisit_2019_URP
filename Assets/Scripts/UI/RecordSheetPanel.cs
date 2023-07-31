using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace HomeVisit.UI
{
	public class RecordSheetPanelData : UIPanelData
	{
	}
	public partial class RecordSheetPanel : UIPanel
	{
		DateTime startTime;
		NewStudentData sd;

		private void Start()
		{
			startTime = DateTime.Now;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			btnClose.onClick.AddListener(Close);
			btnSubmit.onClick.AddListener(Close);
			InitData();
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			UIKit.GetPanel<MainPanel>().NextVisitStepPanel();
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		void Close()
		{
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.Show();
			ScoreReportData data = new ScoreReportData()
			{
				title = "访后记录",
				startTime = this.startTime,
				endTime = DateTime.Now,
				score = 2
			};
			testReportPanel.CreateScoreReport(data);
			UIKit.GetPanel<MainPanel>().ShowCompletedTip();
			Hide();
			UIKit.GetPanel<MainPanel>().SetProgress(false);
		}

		public void InitData()
		{
			sd = UIKit.GetPanel<HomeVisitContentPanel>().studentData;
			inputName.text = "姓名：" + sd.name;
			inputSex.text = "性别：" + sd.sex;
			inputBirth.text = "生日：" + sd.birth;
			inputIdType.text = "身份证：" + sd.idType;
			inputId.text = "身份证号：" + sd.id;
			inputNationality.text = "国籍：" + sd.nationality;
			inputNation.text = "民族：" + sd.nation;
			inputResidencePermit.text = "居住证情况：" + sd.residencePermit;
			inputRemarkGuardian.text = "备注：" + sd.remarkGuardian;
			inputPhone.text = "监护人电话：" + sd.phone;
			inputGuardianIdType.text = "监护人身份证：" + sd.guardianIdType;
			inputGuardianId.text = "监护人身份证号：" + sd.guardianId;
			inputRelationship.text = "监护人身份：" + sd.relationship;
			inputGuardianName.text = "监护人：" + sd.guardianName;
			inputGuardianSex.text = "监护人性别：" + sd.guardianSex;
			inputGuardianEducation.text = "监护人学历：" + sd.guardianEducation;
			inputGuardianUnit.text = "监护人单位：" + sd.guardianUnit;
			inputGuardianDomicile.text = "住址：" + sd.guardianDomicile;
			inputGuardianDistrict.text = "所在区：" + sd.guardianDistrict;
		}
	}
}
