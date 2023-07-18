using UnityEngine;
using QFramework;
using System.Collections;
using System;
using System.Collections.Generic;
using ProjectBase;
using UnityEngine.SceneManagement;
using HomeVisit.Character;
using HomeVisit.Screenshot;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Threading;

namespace HomeVisit.UI
{
	public class Main : MonoBehaviour
	{
		IEnumerator Start()
		{
			//遮挡层，遮挡其他UI，直到它们加载完成
			yield return UIKit.PreLoadPanelAsync<MaskPanel>(UILevel.PopUI, prefabName: Settings.UI + QAssetBundle.Maskpanel_prefab.MASKPANEL);
			//主要
			yield return UIKit.PreLoadPanelAsync<MainPanel>(UILevel.Bg, prefabName: Settings.UI + QAssetBundle.Mainpanel_prefab.MAINPANEL);
			//获取信息
			yield return UIKit.PreLoadPanelAsync<GetInformationPanel>(prefabName: Settings.UI + QAssetBundle.Getinfornationpanel_prefab.GETINFORMATIONPANEL);
			//家访内容
			yield return UIKit.PreLoadPanelAsync<HomeVisitContentPanel>(prefabName: Settings.UI + QAssetBundle.Homevisitcontentpanel_prefab.HOMEVISITCONTENTPANEL);
			UIKit.HidePanel<HomeVisitContentPanel>();
			//家访形式
			yield return UIKit.PreLoadPanelAsync<HomeVisitFormPanel>(prefabName: Settings.UI + QAssetBundle.Homevisitformpanel_prefab.HOMEVISITFORMPANEL);
			//知识考核
			yield return UIKit.PreLoadPanelAsync<KnowledgeExamPanel>(prefabName: Settings.UI + QAssetBundle.Knowledgeexampanel_prefab.KNOWLEDGEEXAMPANEL);
			UIKit.HidePanel<KnowledgeExamPanel>();
			//实验报告
			TestReportPanelData reportPanelData = new TestReportPanelData();
			yield return WebKit.GetInstance().Read<List<ScoreReportData>>(Application.streamingAssetsPath + "/" + "DefaultReportData.json", d =>
			{
				reportPanelData.datas = d;
			});
			yield return UIKit.PreLoadPanelAsync<TestReportPanel>(UILevel.Common, prefabName: Settings.UI + QAssetBundle.Testreportpanel_prefab.TESTREPORTPANEL);
			UIKit.GetPanel<TestReportPanel>().InitReport(reportPanelData);
			UIKit.HidePanel<TestReportPanel>();
			//实验简介
			yield return UIKit.PreLoadPanelAsync<TestBriefPanel>(prefabName: Settings.UI + QAssetBundle.Testbriefpanel_prefab.TESTBRIEFPANEL);
			//最顶部页面
			yield return UIKit.PreLoadPanelAsync<TopPanel>(UILevel.PopUI, prefabName: Settings.UI + QAssetBundle.Toppanel_prefab.TOPPANEL);
			HomeVisitFormPanel formPanel = UIKit.GetPanel<HomeVisitFormPanel>();
			//关闭MaskPanel
			UIKit.ClosePanel<MaskPanel>();
			//关闭获取信息
			UIKit.HidePanel<GetInformationPanel>();
			//关闭形式
			UIKit.HidePanel<HomeVisitFormPanel>();
		}

		#region 生成数据

		//List<string> studentNames = new List<string>()
		//{
		//	"赵仁杰" , "钱好古" , "孙望珠" , "李义府" , "林光美",
		//	"秦彦威","张守光","郑游沪","王归粤","冯如来",
		//	"陈思古","卫去病","蒋相如","沈思庄","韩佐尧",
		//	"杨望台","朱玉盘","秦福金","尤如是","许子君",
		//	"何照容","吕秋瑾","姜暖阳","夏金铄","郝月桂"
		//};
		//List<string> sex = new List<string>()
		//{
		//	"男","男","女","男","女",
		//	"男","女","女","女","男",
		//	"男","男","女","女","男",
		//	"男","女","女","女","女",
		//	"女","女","女","女","女"
		//};
		//List<string> parentNameList = new List<string>()
		//{
		//	"魏庆霞", "赖雅致", "许斯斯", "阎如冰", "曾水云",
		//	"梁嘉宝", "唐国娟", "刘瑞绣", "郭茹云", "王悦淇",
		//	"金英禄", "潘素怀", "姚初柳", "唐雅凡", "卢山芙",
		//	"钱笑晨", "黎叶舞", "陈落妃", "韩雯娟", "顾从周",
		//	"钱玲羽", "曹如云", "邵凝海", "周韶美", "余访风",
		//	"尹婷婷", "李芝芳", "谭思嘉", "孙佳文", "龙忆寒",
		//	"赖语彤", "郝欣玉", "段姣丽", "罗怡木"
		//};
		//List<string> parentSex = new List<string>()
		//{
		//	"女","女","女","女","女",
		//	"男","女","女","女","男",
		//	"女","女","女","女","女",
		//	"男","女","女","女","男",
		//	"女","女","女","女","男"
		//};

		//private void OnEnable()
		//{
		//	List<NewStudentData> datas = new List<NewStudentData>();
		//	for (int i = 0; i < sex.Count; i++)
		//	{
		//		NewStudentData data = new NewStudentData();
		//		data.name = studentNames[i];
		//		data.sex = sex[i];
		//		data.birth = GenerateDate();
		//		data.idType = "居民身份证";
		//		data.id = GenerateId(data.birth);
		//		data.nationality = "中国";
		//		data.nation = GenerateNation();
		//		data.residencePermit = GenerateResidencePermit();
		//		data.phone = GeneratePhone();
		//		data.guardianIdType = "居民身份证";
		//		data.guardianSex = parentSex[i];
		//		data.relationship = GenerateRelationship(data.guardianSex);
		//		data.guardianId = GenerateParentId(data.birth, data.relationship);
		//		data.guardianName = parentNameList[i];
		//		data.guardianUnit = GenerateUnit();
		//		data.guardianDistrict = RandomDistrict();
		//		data.guardianDomicile = data.guardianDistrict + "第五小区";
		//		data.guardianEducation = GenerateEducation();
		//		datas.Add(data);
		//	}
		//	WebKit.GetInstance().Write(Application.streamingAssetsPath + "/" + "NewStudentData.json", datas);
		//}

		//void GenerateDefaultReportData()
		//{
		//	List<ScoreReportData> datas = new List<ScoreReportData>();
		//	ScoreReportData data1 = new ScoreReportData()
		//	{
		//		seq = 0,
		//		title = "家访形式",
		//		startTime = default(DateTime),
		//		endTime = default(DateTime),
		//		maxScore = 4,
		//		expectTime = new TimeSpan(0, 4, 0),
		//		score = 0
		//	};
		//	datas.Add(data1);
		//	ScoreReportData data2 = new ScoreReportData()
		//	{
		//		seq = 1,
		//		title = "知识考核",
		//		startTime = default(DateTime),
		//		endTime = default(DateTime),
		//		maxScore = 20,
		//		expectTime = new TimeSpan(0, 20, 0),
		//		score = 0
		//	};
		//	datas.Add(data2);
		//	ScoreReportData data3 = new ScoreReportData()
		//	{
		//		seq = 2,
		//		title = "交流提纲",
		//		startTime = default(DateTime),
		//		endTime = default(DateTime),
		//		maxScore = 18,
		//		expectTime = new TimeSpan(0, 18, 0),
		//		score = 0
		//	};
		//	datas.Add(data3);
		//	ScoreReportData data4 = new ScoreReportData()
		//	{
		//		seq = 3,
		//		title = "着装准备",
		//		startTime = default(DateTime),
		//		endTime = default(DateTime),
		//		maxScore = 16,
		//		expectTime = new TimeSpan(0, 16, 0),
		//		score = 0
		//	};
		//	datas.Add(data4);
		//	ScoreReportData data5 = new ScoreReportData()
		//	{
		//		seq = 3,
		//		title = "访中过程",
		//		startTime = default(DateTime),
		//		endTime = default(DateTime),
		//		maxScore = 42,
		//		expectTime = new TimeSpan(0, 42, 0),
		//		score = 0
		//	};
		//	datas.Add(data5);
		//	StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/" + "DefaultReportData.json", datas));
		//}

		//void GeneratePolicy()
		//{
		//	List<PolicyData> datas = new List<PolicyData>();
		//	List<string> districtList = new List<string>() { "黄浦", "徐汇", "长宁", "静安", "普陀", "虹口", "杨浦", "闵行", "宝山", "嘉定", "浦东新区", "金山", "松江", "青浦", "奉贤", "崇明" };
		//	List<string> policyTypeList = new List<string>() { "区招生政策", "幼升小对口", "小升初对口" };
		//	List<string> periodList = new List<string>() { "小学/初中", "小学", "初中" };
		//	List<int> districtConfirmTypeList = new List<int>() { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
		//	List<string> publishStatusList = new List<string>() { "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常", "正常", "异常" };

		//	for (int i = 0; i < districtList.Count; i++)
		//	{
		//		for (int j = 0; j < policyTypeList.Count; j++)
		//		{
		//			PolicyData data = new PolicyData();
		//			data.strDistrict = districtList[i];
		//			data.DistrictConfirmType = districtConfirmTypeList[i];
		//			data.strPublishStatus = publishStatusList[i];

		//			data.strPolicyType = policyTypeList[j];
		//			data.strPeriod = periodList[j];
		//			data.strBanner = GenerateBanner(data.strDistrict, data.strPolicyType);

		//			datas.Add(data);
		//		}
		//	}

		//	StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/PolicyData.json", datas));
		//}

		//string GenerateBanner(string district, string policyType)
		//{
		//	string newStr = "";
		//	switch (policyType)
		//	{
		//		case "区招生政策":
		//			if (district != "浦东新区")
		//				newStr = $"{district}区教育局关于2018年本区义务教育阶段学校招生";
		//			else
		//				newStr = $"{district}教育局关于2018年本区义务教育阶段学校招生";
		//			break;
		//		case "幼升小对口":
		//			if (district != "浦东新区")
		//				newStr = $"2018年{district}区公办小学划区范围New";
		//			else
		//				newStr = $"2018年{district}公办小学划区范围New";
		//			break;
		//		case "小升初对口":
		//			if (district != "浦东新区")
		//				newStr = $"2018年{district}区公办初中划区范围New";
		//			else
		//				newStr = $"2018年{district}公办初中划区范围New";
		//			break;
		//		default:
		//			break;
		//	}
		//	return newStr;
		//}

		//void GenerateSchool()
		//{
		//	List<SchoolData> datas = new List<SchoolData>();
		//	List<string> districtList = new List<string>() { "黄浦", "徐汇", "长宁", "静安", "普陀", "虹口", "杨浦", "闵行", "宝山", "嘉定", "浦东新区", "金山", "松江", "青浦", "奉贤", "崇明" };
		//	for (int i = 0; i < districtList.Count; i++)
		//	{
		//		SchoolData data = new SchoolData();
		//		data.strDistrict = districtList[i];
		//		data.strSchoolName = $"{districtList[i]}一小";
		//		data.SchoolCode = 10000 + i * 100 + 1;
		//		data.strPeriod = "小学";
		//		data.Boarding = 0;
		//		datas.Add(data);
		//		SchoolData data1 = new SchoolData();
		//		data1.strDistrict = districtList[i];
		//		data1.strSchoolName = $"{districtList[i]}一中";
		//		data1.SchoolCode = 20000 + i * 100 + 1;
		//		data1.strPeriod = "初中";
		//		data1.Boarding = 0;
		//		datas.Add(data1);
		//	}


		//	StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/SchoolData.json", datas));
		//}

		//void GenerateStudent()
		//{
		//	List<string> nameList = new List<string>() { "赵仁杰", "钱好古", "孙望珠", "李义府", "林光美", "秦彦威", "张守光", "郑游沪", "王归粤", "冯如来", "陈思古", "卫去病", "蒋相如", "沈思庄", "韩佐尧", "杨望台", "朱玉盘", "秦福金", "尤如是", "许子君", "何照容", "吕秋瑾" };
		//	
		//	int gradeIndex = 0;

		//	List<StudentData> datas = new List<StudentData>();
		//	for (int i = 0; i < nameList.Count; i++, gradeIndex++)
		//	{
		//		StudentData data = new StudentData();
		//		data.strStudentName = nameList[i];
		//		data.StudentID = UnityEngine.Random.Range(100, 999) + (i + 1) * 1000;
		//		data.Boarding = 0;
		//		data.parentEducation = GenerateEducation();
		//		data.birth = GenerateDate();
		//		data.parentSex = "女";
		//		data.parentType = "母亲";
		//		data.parentName = parentNameList[i];
		//		data.district = RandomDistrict();
		//		datas.Add(data);
		//	}
		//	StartCoroutine(WebKit.GetInstance().Write(Application.streamingAssetsPath + "/StudentData.json", datas));
		//}

		#region 
		//async void Anim(Animator anim,string clipName)
		//{
		//	anim.Play(clipName);
		//	await UniTask.WaitUntil
		//}
		#endregion

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

		List<string> nations = new List<string>() { "汉", "满", "回", "蒙", "壮", "维吾尔", "汉", "汉", "汉", "汉", "汉", "汉", "汉", "汉", "汉" };
		string GenerateNation()
		{
			string rd = nations[UnityEngine.Random.Range(0, nations.Count)];
			return rd;
		}

		string GeneratePhone()
		{
			return UnityEngine.Random.Range(100000, 999999).ToString() + UnityEngine.Random.Range(10000, 99999);
		}

		string GenerateId(string birth)
		{
			string[] births = birth.Split('/');
			if (int.Parse(births[1]) < 10)
				births[1] = "0" + births[1];
			if (int.Parse(births[2]) < 10)
				births[2] = "0" + births[2];
			string id = UnityEngine.Random.Range(100000, 999999) + births[0] + births[1] + births[2] + UnityEngine.Random.Range(1000, 9999);
			return id;
		}

		string GenerateParentId(string birth, string type)
		{
			string[] births = birth.Split('/');
			switch (type)
			{
				case "奶奶":
					births[0] = (int.Parse(births[0]) - 50).ToString();
					break;
				case "婆婆":
					births[0] = (int.Parse(births[0]) - 50).ToString();
					break;
				default:
					births[0] = (int.Parse(births[0]) - 25).ToString();
					break;
			}
			if (int.Parse(births[1]) < 2)
				births[1] = "0" + births[1];
			if (int.Parse(births[1]) < 10)
				births[1] = "0" + (int.Parse(births[1]) - 2);
			else if (int.Parse(births[1]) > 10)
				births[1] = (int.Parse(births[1]) - 1).ToString();

			if (int.Parse(births[2]) < 2)
				print("不正确的数");
			else if (int.Parse(births[2]) < 10)
				births[2] = "0" + (int.Parse(births[2]) - 2);
			else if (int.Parse(births[2]) > 10)
				births[2] = (int.Parse(births[2]) - 1).ToString();

			string id = UnityEngine.Random.Range(100000, 999999) + births[0] + births[1] + births[2] + UnityEngine.Random.Range(1000, 9999);
			return id;
		}

		List<string> residencePermits = new List<string>() { "有", "无", "有", "无", "无", "无", "无", "无" };
		string GenerateResidencePermit()
		{
			string r = residencePermits[UnityEngine.Random.Range(0, residencePermits.Count)];
			return r;
		}

		List<string> Relationship1 = new List<string>() { "母亲", "母亲", "母亲", "母亲", "奶奶", "婆婆" };

		string GenerateRelationship(string sex)
		{
			string r;
			if (sex == "女")
			{
				r = Relationship1[UnityEngine.Random.Range(0, Relationship1.Count)];
			}
			else
			{
				r = "父亲";
			}
			return r;
		}

		List<string> units = new List<string>() { "上海石化", "长江存储", "个体工商", "自由职业", "闵行一小", "宝山一中", "盐津铺子", "卫龙", "牧原食品", "", "金磨坊", "美的", "光明" };
		string GenerateUnit()
		{
			string r = units[UnityEngine.Random.Range(0, units.Count)];
			return r;
		}
		#endregion
	}

}


