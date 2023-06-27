using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SchoolData
{
	public string strDistrict;
	public string strSchoolName;
	public int SchoolCode;
	public string strPeriod;
	public int Boarding;
}

public class SchoolItem : MonoBehaviour
{
	public SchoolData mData;
	public TextMeshProUGUI tmpDistrict;
	public TextMeshProUGUI tmpSchoolName;
	public TextMeshProUGUI tmpSchoolCode;
	public TextMeshProUGUI tmpEnrollmentStage;
	public TextMeshProUGUI tmpBoarding;
	public TextMeshProUGUI tmpOperation;

	public void InitData(SchoolData data)
	{
		mData = data;
		tmpDistrict.text = data.strDistrict;
		tmpSchoolName.text = data.strSchoolName.ToString();
		tmpSchoolCode.text = data.SchoolCode.ToString();
		tmpEnrollmentStage.text = data.strPeriod;
		tmpBoarding.text = data.Boarding == 0 ? "寄宿制" : "非寄宿制";
	}
}
