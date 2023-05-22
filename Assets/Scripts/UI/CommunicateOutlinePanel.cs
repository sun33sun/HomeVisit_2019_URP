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
				strTitleDescribe = "��ʦ����ѯ�ʼҳ�������ѡ�⣬���ѡ�ĸ���",
				strTogList = new List<string>() 
				{
					"���������������磺�Ժ�����˯������",
					"��������������ˢ��ϴ�����¡�����",
					"�����Ը��������������Ƿ��ȶ���",
					"���Ӳ�ʷ�����������ಡ��������",
					"���ӵ�ѧϰ���",
					"�ҳ������������ķ���ɳ�������",
					"��Ҫ��ʦ�ر�ע��ķ��棨��λ��ע��������У�Է����˼ʽ���������",
					"��ʦ������ҳ�����ѧУ����üҳ���һ���������˽⡣"
				}
			};
			CreateMultipleTitle(multipleData);


			OutlineTitleData multipleData1 = new OutlineTitleData()
			{
				strTitleDescribe = "���̸�����ݼ����⹩�ο�������ѡ�⣬���ѡ�ĸ���",
				strTogList = new List<string>()
				{
					"����ѧУ��ѧ����",
					"���ܰ����δ�������",
					"�˽�ѧ���Ը񡢰��õȸ������",
					"�˽�ѧ���������������ҵ",
					"�˽�ѧ������ѧУ�¼�����ڴ�",
					"�˽�ҳ���ͥ��������",
					"Ԥ��ҳ���ѧ�����ܹ��ĵĽ�������"
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
