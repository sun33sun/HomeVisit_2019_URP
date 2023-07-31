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
	public partial class StudentInformation1 : UIElement
	{
		NewStudentData mData;
		private void Awake()
		{
			GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
			btnBack.onClick.AddListener(() =>
			{
				panel.StudentList.gameObject.SetActive(true);
				panel.InformationSecurity.gameObject.SetActive(true);
				gameObject.SetActive(false);
			});
			btnConfirmStudentInformation.onClick.AddListener(() => {
				panel.Hide();
				gameObject.SetActive(false);
				UIKit.ShowPanel<HomeVisitContentPanel>();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}

		public void InitData(NewStudentData newData)
		{
			mData = newData;
			tmpName.text = "������" + mData.name;
			tmpSex.text = "�Ա�" + mData.sex;
			tmpBirth.text = "���գ�" + mData.birth;
			tmpIdType.text = "���֤��" + mData.idType;
			tmpId.text = "���֤�ţ�" + mData.id;
			tmpNationality.text = "������" + mData.nationality;
			tmpNation.text = "���壺" + mData.nation;
			tmpResidencePermit.text = "��ס֤�����" + mData.residencePermit;
			tmpRemarkGuardian.text = "��ע��" + mData.remarkGuardian;
			tmpPhone.text = "�໤�˵绰��" + mData.phone;
			tmpGuardianIdType.text = "�໤�����֤��" + mData.guardianIdType;
			tmpGuardianId.text = "�໤�����֤�ţ�" + mData.guardianId;
			tmpRelationship.text = "�໤����ݣ�" + mData.relationship;
			tmpGuardianName.text = "�໤�ˣ�" + mData.guardianName;
			tmpGuardianSex.text = "�໤���Ա�" + mData.guardianSex;
			tmpGuardianEducation.text = "�໤��ѧ����" + mData.guardianEducation;
			tmpGuardianUnit.text = "�໤�˵�λ��" + mData.guardianUnit;
			tmpGuardianDomicile.text = "סַ��" + mData.guardianDomicile;
			tmpGuardianDistrict.text = "��������" + mData.guardianDistrict;
			//tmpStudentID.text = "ѧ��ѧ�ţ�" + mData.StudentID.ToString();
			//tmpBirth.text = "�������ڣ�" + mData.birth;
			//tmpPrimarySchool.text = "ԭСѧ��" + mData.primarySchool;
			//tmpPrimarySchoolDistrict.text = "ԭСѧ��������" + mData.primarySchoolDistrict;
			//tmpDistrict.text = "ѧ����������" + mData.district;
			//tmpParentType.text = "�ҳ���ѧ����ϵ��" + mData.parentType;
			//tmpParentName.text = "�ҳ�������" + mData.parentName;
			//tmpParentSex.text = "�ҳ��Ա�" + mData.parentSex;
			//tmpParentEducation.text = "�ҳ�����ˮƽ��" + mData.parentEducation;
		}
	}
}