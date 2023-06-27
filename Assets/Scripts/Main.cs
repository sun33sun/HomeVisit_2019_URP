using UnityEngine;
using QFramework;
using System.Collections;
using System;
using System.Collections.Generic;
using ProjectBase;
using ZTools;

namespace HomeVisit.UI
{
	public class Main : MonoBehaviour
	{
		void Start()
		{
			//ResKit.InitAsync().ToAction().Start(this, () =>
			// {
			//	 //获取信息页面
			//	 UIKit.OpenPanelAsync<GetInfornationPanel>().ToAction().Start(this);
			//	 //主页面
			//	 UIKit.OpenPanelAsync<MainPanel>(UILevel.Bg).ToAction().Start(this);
			//	 //实验简介
			//	 UIKit.OpenPanelAsync<TestBriefPanel>().ToAction().Start(this);

			//	 //实验报告
			//	 UIKit.OpenPanelAsync<TestReportPanel>().ToAction().Start(this, () =>
			//	 {
			//		 UIKit.OpenPanelAsync<KnowledgeExamPanel>().ToAction().Start(this, () =>
			//		 {
			//			 UIKit.OpenPanelAsync<ButtonPanel>(UILevel.PopUI).ToAction().Start(this);
			//		 });
			//	 });
			// });

			ResKit.InitAsync().ToAction().Start(this, () =>
			 {

				 UIKit.OpenPanelAsync<MainPanel>().ToAction().Start(this, () =>
				 {
					 UIKit.OpenPanelAsync<OnVisitPanel>().ToAction().Start(this);
				 });
				 UIKit.OpenPanelAsync<KnowledgeExamPanel>().ToAction().Start(this, () => { UIKit.HidePanel<KnowledgeExamPanel>(); });
				 UIKit.OpenPanelAsync<TestReportPanel>().ToAction().Start(this, () => { UIKit.HidePanel<TestReportPanel>(); });
				 UIKit.OpenPanelAsync<ButtonPanel>().ToAction().Start(this);
				 UIKit.OpenPanelAsync<HomeVisitContentPanel>().ToAction().Start(this, () => { UIKit.HidePanel<HomeVisitContentPanel>(); });
			 });
		}

		#region 生成数据
		void GeneratePolicy()
		{
			List<PolicyData> datas = new List<PolicyData>();
			List<string> districtList = new List<string>() { "黄浦", "徐汇", "长宁", "静安", "普陀", "虹口", "杨浦", "闵行", "宝山", "嘉定", "浦东新区", "金山", "松江", "青浦", "奉贤", "崇明" };
			List<string> policyTypeList = new List<string>() { "区招生政策", "幼升小对口", "小升初对口" };
			List<string> periodList = new List<string>() { "小学/初中", "小学", "初中" };
			List<int> districtConfirmTypeList = new List<int>() { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
			List<string> publishStatusList = new List<string>() { "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常" };

			for (int i = 0; i < districtList.Count; i++)
			{
				for (int j = 0; j < policyTypeList.Count; j++)
				{
					PolicyData data = new PolicyData();
					data.strDistrict = districtList[i];
					data.DistrictConfirmType = districtConfirmTypeList[i];
					data.strPublishStatus = publishStatusList[i];

					data.strPolicyType = policyTypeList[j];
					data.strPeriod = periodList[j];
					data.strBanner = GenerateBanner(data.strDistrict, data.strPolicyType);

					datas.Add(data);
				}
			}

			StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/PolicyData.json", datas));
		}

		string GenerateBanner(string district, string policyType)
		{
			string newStr = "";
			switch (policyType)
			{
				case "区招生政策":
					if (district != "浦东新区")
						newStr = $"{district}区教育局关于2018年本区义务教育阶段学校招生";
					else
						newStr = $"{district}教育局关于2018年本区义务教育阶段学校招生";
					break;
				case "幼升小对口":
					if (district != "浦东新区")
						newStr = $"2018年{district}区公办小学划区范围New";
					else
						newStr = $"2018年{district}公办小学划区范围New";
					break;
				case "小升初对口":
					if (district != "浦东新区")
						newStr = $"2018年{district}区公办初中划区范围New";
					else
						newStr = $"2018年{district}公办初中划区范围New";
					break;
				default:
					break;
			}
			return newStr;
		}

		void GenerateSchool()
		{
			List<SchoolData> datas = new List<SchoolData>();
			List<string> districtList = new List<string>() { "黄浦", "徐汇", "长宁", "静安", "普陀", "虹口", "杨浦", "闵行", "宝山", "嘉定", "浦东新区", "金山", "松江", "青浦", "奉贤", "崇明" };
			for (int i = 0; i < districtList.Count; i++)
			{
				SchoolData data = new SchoolData();
				data.strDistrict = districtList[i];
				data.strSchoolName = $"{districtList[i]}一小";
				data.SchoolCode = 10000 + i * 100 + 1;
				data.strPeriod = "小学";
				data.Boarding = 0;
				datas.Add(data);
				SchoolData data1 = new SchoolData();
				data1.strDistrict = districtList[i];
				data1.strSchoolName = $"{districtList[i]}一中";
				data1.SchoolCode = 20000 + i * 100 + 1;
				data1.strPeriod = "初中";
				data1.Boarding = 0;
				datas.Add(data1);
			}


			StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/SchoolData.json", datas));
		}

		void GenerateStudent()
		{
			List<string> nameList = new List<string>() { "赵仁杰", "钱好古", "孙望珠", "李义府", "林光美", "秦彦威", "张守光", "郑游沪", "王归粤", "冯如来", "陈思古", "卫去病", "蒋相如", "沈思庄", "韩佐尧", "杨望台", "朱玉盘", "秦福金", "尤如是", "许子君", "何照容", "吕秋瑾" };
			List<string> parentNameList = new List<string>() { "魏庆霞", "赖雅致", "许斯斯", "阎如冰", "曾水云", "梁嘉宝", "唐国娟", "刘瑞绣", "郭茹云", "王悦淇", "金英禄", "潘素怀", "姚初柳", "唐雅凡", "卢山芙", "钱笑晨", "黎叶舞", "陈落妃", "韩雯娟", "顾从周", "钱玲羽", "曹如云", "邵凝海", "周韶美", "余访风", "尹婷婷", "李芝芳", "谭思嘉", "孙佳文", "龙忆寒", "赖语彤", "郝欣玉", "段姣丽", "罗怡木" };
			int gradeIndex = 0;

			List<StudentData> datas = new List<StudentData>();
			for (int i = 0; i < nameList.Count; i++, gradeIndex++)
			{
				StudentData data = new StudentData();
				data.strStudentName = nameList[i];
				data.StudentID = UnityEngine.Random.Range(100, 999) + (i + 1) * 1000;
				data.Boarding = 0;
				data.parentEducation = GenerateEducation();
				data.birth = GenerateDate();
				data.parentSex = "女";
				data.parentType = "母亲";
				data.parentName = parentNameList[i];
				data.district = RandomDistrict();
				datas.Add(data);
			}
			StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/StudentData.json", datas));
		}

		string GenerateDate()
		{
			int y = UnityEngine.Random.Range(2011, 2014);

			int m = UnityEngine.Random.Range(1, 12);
			int h = UnityEngine.Random.Range(1, 28);
			return $"{y}/{m}/{h}";
		}

		List<string> parentEducation = new List<string>() { "专科", "专科", "专科", "专科", "专科", "专科", "专科", "专科", "本科", "本科", "本科", "本科", "研究生", "研究生", "博士" };
		string GenerateEducation()
		{
			string pe = parentEducation[UnityEngine.Random.Range(0, parentEducation.Count)];
			return pe;
		}

		List<string> districtList = new List<string>() { "黄浦", "徐汇", "长宁", "静安", "普陀", "虹口", "杨浦", "闵行", "宝山", "嘉定", "浦东新区", "金山", "松江", "青浦", "奉贤", "崇明" };
		string RandomDistrict()
		{
			string rd = districtList[UnityEngine.Random.Range(0, districtList.Count)];
			return rd;
		}
		#endregion

	}

}


