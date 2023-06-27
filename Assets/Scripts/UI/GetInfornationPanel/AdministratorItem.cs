using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeacherData{
	public string strDistrict;
	public string id;
	public string strName;
	public string strUnit;
	public int role;
	public string strUserState;
	public string school;
	public string classIndex;
}

public class AdministratorItem : MonoBehaviour
{
	public TeacherData mData;
	public TextMeshProUGUI tmpDistrict;
	public TextMeshProUGUI tmpID;
	public TextMeshProUGUI tmpName;
	public TextMeshProUGUI tmpRole;
	public TextMeshProUGUI tmpUserState;
	public TextMeshProUGUI tmpOperation;

	public void InitData(TeacherData data)
	{
		mData = data;
		tmpDistrict.text = data.strDistrict;
		tmpID.text = data.id.ToString();
		tmpName.text = data.strName;
		tmpRole.text = data.role == 0 ? "教师" : "市教育局管理员";
		tmpUserState.text = data.strUserState;
	}
}
