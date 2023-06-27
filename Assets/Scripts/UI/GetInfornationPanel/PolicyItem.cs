using ProjectBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PolicyData
{
	public string strDistrict;
	public string strPolicyType;
	public string strBanner;
	public string strPeriod;
	public int DistrictConfirmType;
	public string strPublishStatus;
}

public class PolicyItem : MonoBehaviour
{
	public PolicyData mData;
	public TextMeshProUGUI tmpDistrict;
	public TextMeshProUGUI tmpPolicyType;
	public TextMeshProUGUI tmpBanner;
	public TextMeshProUGUI tmpPeriod;
	public TextMeshProUGUI tmpDistrictConfirmType;
	public TextMeshProUGUI tmpPublishStatus;
	public TextMeshProUGUI tmpOperation;

	public void InitData(PolicyData data)
	{
		mData = data;
		tmpDistrict.text = data.strDistrict;
		tmpPolicyType.text = data.strPolicyType.ToString();
		tmpBanner.text = data.strBanner;
		tmpPeriod.text = data.strPeriod;
		tmpDistrictConfirmType.text = data.DistrictConfirmType == 0 ? "未确认" : "已确认";
		tmpPublishStatus.text = data.strPublishStatus;
	}
}
