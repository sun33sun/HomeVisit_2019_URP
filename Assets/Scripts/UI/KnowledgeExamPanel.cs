using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class KnowledgeExamPanelData : UIPanelData
	{
	}
	public partial class KnowledgeExamPanel : UIPanel
	{
		[SerializeField] GameObject singlePrefab = null;
		[SerializeField] GameObject multiplePrefab = null;

		List<ITitle> titles = new List<ITitle>();

		void TestExam()
		{
			DateTime startTime = DateTime.Now;
			SingleTitleData singleData = new SingleTitleData()
			{
				rightIndex = 0,
				strDescribe = "��ѡ��",
				strA = "ѡ��A",
				strB = "ѡ��B",
				strC = "ѡ��C",
				strD = "ѡ��D",
				score = 20,

				strModule = "֪ʶ����",
				strStart = startTime
			};
			CreateSingleTitle(singleData);

			MultipleTitleData multipleData = new MultipleTitleData()
			{
				isRights = new bool[4] {true,true,false,false},
				strDescribe = "��ѡ��",
				strA = "ѡ��A",
				strB = "ѡ��B",
				strC = "ѡ��C",
				strD = "ѡ��D",
				score = 30,

				strModule = "֪ʶ����",
				strStart = startTime
			};
			CreateMultipleTitle(multipleData);

			btnSubmit.transform.SetAsLastSibling();
		}

		GameObject CreateSingleTitle(SingleTitleData data)
		{
			GameObject gameObj = Instantiate(singlePrefab);
			gameObj.name = singlePrefab.name;
			SingleTitle singleTitle = gameObj.GetComponent<SingleTitle>();
			titles.Add(singleTitle);
			singleTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			gameObj.transform.SetAsFirstSibling();
			return gameObj;
		}

		GameObject CreateMultipleTitle(MultipleTitleData data)
		{
			GameObject gameObj = Instantiate(multiplePrefab);
			gameObj.name = multiplePrefab.name;
			MultipleTitle multipleTitle = gameObj.GetComponent<MultipleTitle>();
			titles.Add(multipleTitle);
			multipleTitle.Init(data);
			gameObj.transform.SetParent(Content);
			gameObj.transform.localScale = Vector3.one;
			gameObj.transform.SetAsFirstSibling();
			return gameObj;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as KnowledgeExamPanelData ?? new KnowledgeExamPanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(()=> 
			{
				imgSubmitExam.gameObject.SetActive(true);
				imgExam.gameObject.SetActive(false);
				TestReportPanel testReportPanel = UIKit.GetPanel<TestReportPanel>();
				for (int i = 0; i < titles.Count; i++)
					testReportPanel.CreateScoreReport(titles[i].GetScoreReportData());
			});
			btnCancel.onClick.AddListener(()=> 
			{
				imgSubmitExam.gameObject.SetActive(false);
				imgExam.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(()=> 
			{
				UIKit.ShowPanel<TestReportPanel>();
				Hide();
			});

			TestExam();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
			imgExam.gameObject.SetActive(true);
			imgSubmitExam.gameObject.SetActive(false);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
