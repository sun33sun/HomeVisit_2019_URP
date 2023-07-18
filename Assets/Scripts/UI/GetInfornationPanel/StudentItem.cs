using ProjectBase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StudentData
{
	public string strStudentName;
	public int StudentID;
	public int Boarding;
	public string birth;
	public string primarySchool;
	public string primarySchoolDistrict;
	public string district;
	public string parentName;
	public string parentType;
	public string parentSex;
	public string parentEducation;
}

public class NewStudentData
{
	//学生姓名
	public string name= "";
	//学生性别
	public string sex = "";
	//学生出生日期
	public string birth = "";
	//学生身份证类型
	public string idType = "";
	//学生身份证号
	public string id = "";
	//国籍
	public string nationality = "";
	//民族
	public string nation = "";
	//持上海居住证情况
	public string residencePermit = "";
	//备注家长信息详细内容
	public string remarkGuardian = "";
	//监护人手机号
	public string phone = "";
	//监护人证件类型
	public string guardianIdType = "";
	//监护人证件号码
	public string guardianId = "";
	//与监护人关系
	public string relationship = "";
	//监护人姓名
	public string guardianName = "";
	//监护人性别
	public string guardianSex = "";
	//监护人学历
	public string guardianEducation = "";
	//监护人工作单位
	public string guardianUnit = "";
	//监护人户籍所在地（省）
	public string guardianDomicile = "";
	//监护人户籍所在地（区）
	public string guardianDistrict = "";
}

public class StudentItem : MonoBehaviour
{
	public NewStudentData mData;
	public TextMeshProUGUI tmpStudentName;
	public TextMeshProUGUI tmpStudentID;
	public TextMeshProUGUI tmpBoarding;
	public TextMeshProUGUI tmpOperation;

	public void InitData(NewStudentData data)
	{
		mData = data;
		tmpStudentName.text = data.name;
		tmpStudentID.text = data.id.ToString();
		tmpBoarding.text = "寄宿制";
	}
}
