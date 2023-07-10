using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HomeVisit.UI
{
	public class StudentInformation : MonoBehaviour
	{
		[SerializeField] TextMeshProUGUI tmpName;
		[SerializeField] TextMeshProUGUI tmpStudentID;
		[SerializeField] TextMeshProUGUI tmpBirth;
		[SerializeField] TextMeshProUGUI tmpPrimarySchool;
		[SerializeField] TextMeshProUGUI tmpPrimarySchoolDistrict;
		[SerializeField] TextMeshProUGUI tmpDistrict;
		[SerializeField] TextMeshProUGUI tmpParentType;
		[SerializeField] TextMeshProUGUI tmpParentName;
		[SerializeField] TextMeshProUGUI tmpParentSex;
		[SerializeField] TextMeshProUGUI tmpParentEducation;

		[SerializeField] Button btnConfirm;

		StudentData mData;

		private void Start()
		{
			btnConfirm.onClick.AddListener(() =>
			{
				ScoreReportData data = new ScoreReportData()
				{
					title = "获取信息",
					startTime = UIKit.GetPanel<GetInformationPanel>().startTime,
					endTime = DateTime.Now,
					score = 2
				};
				UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);
				UIKit.HidePanel<GetInformationPanel>();
				UIKit.ShowPanel<HomeVisitContentPanel>();
			});
		}

		public void InitData(StudentData newData)
		{
			mData = newData;
			tmpName.text = "学生姓名：" + mData.strStudentName;
			tmpStudentID.text = "学生学号：" + mData.StudentID.ToString();
			tmpBirth.text = "出生日期：" + mData.birth;
			tmpPrimarySchool.text = "原小学：" + mData.primarySchool;
			tmpPrimarySchoolDistrict.text = "原小学所在区：" + mData.primarySchoolDistrict;
			tmpDistrict.text = "学生出生区：" + mData.district;
			tmpParentType.text = "家长与学生关系：" + mData.parentType;
			tmpParentName.text = "家长姓名：" + mData.parentName;
			tmpParentSex.text = "家长性别：" + mData.parentSex;
			tmpParentEducation.text = "家长教育水平：" + mData.parentEducation;
		}
	}
}

