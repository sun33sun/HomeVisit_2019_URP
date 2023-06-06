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

		DateTime startTime;
		DateTime endTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as CommunicateOutlinePanelData ?? new CommunicateOutlinePanelData();

			btnClose.onClick.AddListener(Hide);
			btnConfirm.onClick.AddListener(Confirm);
			btnSubmit.onClick.AddListener(Submit);

			TestExam();
		}

		void Confirm()
		{
			//�����Ŀ�Դ�
			for (int i = 0; i < titles.Count; i++)
				titles[i].CheckTitle();

			//��ʾ�ύ��ť
			btnConfirm.gameObject.SetActive(false);
			btnSubmit.gameObject.SetActive(true);
		}

		void Submit()
		{
			int totalScore = 0;
			for (int i = 0; i < titles.Count; i++)
				totalScore = titles[i].GetScore();
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "�������",
				strScore = totalScore.ToString(),
			};
			TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.CreateScoreReport(data);

			//�޸Ľ���UI��������һ��ҳ��
			UIKit.GetPanel<MainPanel>().NextStep();
			UIKit.OpenPanelAsync<ClothesPanel>().ToAction().Start(this);
			Hide();
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
				},
				rightList = new List<bool>() { true, true, true, true, false, false, false, false },
				score = 10
			};
			CreateOutlinTitle(multipleData);


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
				},
				rightList = new List<bool>() {  false, false, false, true, true, true, true },
				score = 5
			};
			CreateOutlinTitle(multipleData1);

			btnConfirm.transform.SetAsLastSibling();
		}

		GameObject CreateOutlinTitle(OutlineTitleData data)
		{
			OutlineTitle outlineTitle = ExamManager.Instance.CreateOutlineTitle(data);
			titles.Add(outlineTitle);
			outlineTitle.gameObject.transform.SetParent(Content);
			outlineTitle.gameObject.transform.localScale = Vector3.one;
			outlineTitle.gameObject.transform.SetAsFirstSibling();
			return outlineTitle.gameObject;
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			startTime = DateTime.Now;
			btnConfirm.gameObject.SetActive(true);
			btnSubmit.gameObject.SetActive(false);
		}

		protected override void OnHide()
		{
			endTime = DateTime.Now;
		}

		protected override void OnClose()
		{
		}


	}
}
