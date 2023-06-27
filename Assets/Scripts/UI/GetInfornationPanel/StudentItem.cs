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

public class StudentItem : MonoBehaviour
{
	public StudentData mData;
	public TextMeshProUGUI tmpStudentName;
	public TextMeshProUGUI tmpStudentID;
	public TextMeshProUGUI tmpBoarding;
	public TextMeshProUGUI tmpOperation;

	public void InitData(StudentData data)
	{
		mData = data;
		tmpStudentName.text = data.strStudentName;
		tmpStudentID.text = data.StudentID.ToString();
		tmpBoarding.text = data.Boarding == 0 ? "寄宿制" : "非寄宿制";
	}
}
