using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class CommunicateOutlinePanelData : UIPanelData
	{
		public List<string> strTog = new List<string>();

	}
	public partial class CommunicateOutlinePanel : UIPanel
	{
		[SerializeField] GameObject outlineTitlePrefab;
		List<ITitle> titles = new List<ITitle>();
		public List<string> strTogSelected = new List<string>();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as CommunicateOutlinePanelData ?? new CommunicateOutlinePanelData();

			btnClose.onClick.AddListener(Hide);
			btnConfirm.onClick.AddListener(() =>
			{
				UIKit.GetPanel<MainPanel>().NextStep();
				UIKit.OpenPanelAsync<ClothesPanel>().ToAction().Start(this);
				Hide();

				TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
				for (int i = 0; i < titles.Count; i++)
				{
					testReportPanel.CreateScoreReport(titles[i].GetScoreReportData());
				}
			});

			TestExam();
		}

		void TestExam()
		{
			DateTime startTime = DateTime.Now;

			OutlineTitleData multipleData = new OutlineTitleData()
			{
				strTitleDescribe = "教师可以询问家长：（多选题，最多选四个）",
				strTogList = new List<string>() 
				{
					"孩子生活能力（如：吃喝拉撒睡……）",
					"孩子自理能力（刷牙洗脸穿衣……）",
					"孩子性格（内向、外向、情绪是否稳定）",
					"孩子病史（过敏、心脏病、癫痫……）",
					"孩子的学习情况",
					"家长期望（孩子哪方面成长……）",
					"需要老师特别注意的方面（座位、注意力、在校吃饭、人际交往……）",
					"教师可以向家长介绍学校理念，让家长有一个初步的了解。"
				}
			};
			CreateMultipleTitle(multipleData);


			OutlineTitleData multipleData1 = new OutlineTitleData()
			{
				strTitleDescribe = "相关谈话内容及问题供参考：（多选题，最多选四个）",
				strTogList = new List<string>()
				{
					"介绍学校办学理念",
					"介绍班主任带班理念",
					"了解学生性格、爱好等个人情况",
					"了解学生假期生活、查阅作业",
					"了解学生对新学校新集体的期待",
					"了解家长家庭教育理念",
					"预测家长和学生可能关心的教育问题"
				}
			};
			CreateMultipleTitle(multipleData1);

			btnConfirm.transform.SetAsLastSibling();
		}

		GameObject CreateMultipleTitle(OutlineTitleData data)
		{
			GameObject gameObj = Instantiate(outlineTitlePrefab);
			gameObj.name = outlineTitlePrefab.name;
			OutlineTitle outlineTitle = gameObj.GetComponent<OutlineTitle>();
			titles.Add(outlineTitle);
			outlineTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			gameObj.transform.SetAsFirstSibling();
			return gameObj;
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}


	}
}
