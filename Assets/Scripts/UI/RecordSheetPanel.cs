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
				title = "�ú��¼",
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
			inputName.text = "������" + sd.name;
			inputSex.text = "�Ա�" + sd.sex;
			inputBirth.text = "���գ�" + sd.birth;
			inputIdType.text = "���֤��" + sd.idType;
			inputId.text = "���֤�ţ�" + sd.id;
			inputNationality.text = "������" + sd.nationality;
			inputNation.text = "���壺" + sd.nation;
			inputResidencePermit.text = "��ס֤�����" + sd.residencePermit;
			inputRemarkGuardian.text = "��ע��" + sd.remarkGuardian;
			inputPhone.text = "�໤�˵绰��" + sd.phone;
			inputGuardianIdType.text = "�໤�����֤��" + sd.guardianIdType;
			inputGuardianId.text = "�໤�����֤�ţ�" + sd.guardianId;
			inputRelationship.text = "�໤����ݣ�" + sd.relationship;
			inputGuardianName.text = "�໤�ˣ�" + sd.guardianName;
			inputGuardianSex.text = "�໤���Ա�" + sd.guardianSex;
			inputGuardianEducation.text = "�໤��ѧ����" + sd.guardianEducation;
			inputGuardianUnit.text = "�໤�˵�λ��" + sd.guardianUnit;
			inputGuardianDomicile.text = "סַ��" + sd.guardianDomicile;
			inputGuardianDistrict.text = "��������" + sd.guardianDistrict;
		}
	}
}
