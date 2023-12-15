/****************************************************************************
 * 2023.7 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;

namespace HomeVisit.UI
{
	public partial class StudentInformation : UIElement
	{
		public TextMeshProUGUI tmpTeacher;
		public NewStudentData mData;

		protected override void OnBeforeDestroy()
		{
		}

		private void Start()
		{
			btnConfirm.onClick.AddListener(() =>
			{
				ScoreReportData data = new ScoreReportData()
				{
					title = "获取信息",
					startTime = UIKit.GetPanel<GetInformationPanel>().startTime,
					endTime = DateTime.UtcNow,
					score = 2
				};
				UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
				UIKit.HidePanel<GetInformationPanel>();
				UIKit.ShowPanel<HomeVisitContentPanel>();
				gameObject.SetActive(false);
			});
			btnBack.onClick.AddListener(() =>
			{
				UIKit.GetPanel<GetInformationPanel>().StudentList.gameObject.SetActive(true);
				gameObject.SetActive(false);
			});
		}

		public void InitData(NewStudentData newData)
		{
			mData = newData;
			tmpName.text = "姓名：" + mData.name;
			tmpSex.text = "性别：" + mData.sex;
			tmpBirth.text = "生日：" + mData.birth;
			tmpIdType.text = "身份证：" + mData.idType;
			tmpId.text = "身份证号：" + mData.id;
			tmpNationality.text = "国籍：" + mData.nationality;
			tmpNation.text = "民族：" + mData.nation;
			tmpResidencePermit.text = "居住证情况：" + mData.residencePermit;
			tmpRemarkGuardian.text = "备注：" + mData.remarkGuardian;
			tmpPhone.text = "监护人电话：" + mData.phone;
			tmpGuardianIdType.text = "监护人身份证：" + mData.guardianIdType;
			tmpGuardianId.text = "监护人身份证号：" + mData.guardianId;
			tmpRelationship.text = "监护人身份：" + mData.relationship;
			tmpGuardianName.text = "监护人：" + mData.guardianName;
			tmpGuardianSex.text = "监护人性别：" + mData.guardianSex;
			tmpGuardianEducation.text = "监护人学历：" + mData.guardianEducation;
			tmpGuardianUnit.text = "监护人单位：" + mData.guardianUnit;
			tmpGuardianDomicile.text = "住址：" + mData.guardianDomicile;
			tmpGuardianDistrict.text = "所在区：" + mData.guardianDistrict;
			//tmpStudentID.text = "学生学号：" + mData.StudentID.ToString();
			//tmpBirth.text = "出生日期：" + mData.birth;
			//tmpPrimarySchool.text = "原小学：" + mData.primarySchool;
			//tmpPrimarySchoolDistrict.text = "原小学所在区：" + mData.primarySchoolDistrict;
			//tmpDistrict.text = "学生出生区：" + mData.district;
			//tmpParentType.text = "家长与学生关系：" + mData.parentType;
			//tmpParentName.text = "家长姓名：" + mData.parentName;
			//tmpParentSex.text = "家长性别：" + mData.parentSex;
			//tmpParentEducation.text = "家长教育水平：" + mData.parentEducation;
		}
	}
}