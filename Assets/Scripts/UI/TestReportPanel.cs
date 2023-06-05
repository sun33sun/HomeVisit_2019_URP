using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public class TestReportPanelData : UIPanelData
	{
	}
	public partial class TestReportPanel : UIPanel
	{
		[SerializeField] GameObject reportPrefab;
		[SerializeField] List<ScoreReport> reportList;
		//¼Ò·ÃÄÚÈÝÒ³Ãæ
		HomeVisitContentPanel homeVisitContentPanel = null;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestReportPanelData ?? new TestReportPanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(() =>
			{
				HomeVisitContentPanel homeVisitContentPanel = UIKit.GetPanel<HomeVisitContentPanel>();
				homeVisitContentPanel.Show();
				homeVisitContentPanel.transform.SetAsLastSibling();
				Hide();
			});
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(Content);
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		public void HideRender()
		{
			imgBk.gameObject.SetActive(false);
		}

		public void ShowRender()
		{
			imgBk.gameObject.SetActive(true);
		}

		public void CreateScoreReport(ScoreReportData data)
		{
			for (int i = 0; i < reportList.Count; i++)
			{
				if (reportList[i].tmpModule.text == data.strModule)
				{
					reportList[i].Init(data);
					return;
				}
			}
			Transform newTrans = Instantiate(reportPrefab).transform;
			newTrans.SetParent(Grid);
			newTrans.localScale = Vector3.one;
			newTrans.SetAsLastSibling();

			ScoreReport newReport = newTrans.GetComponent<ScoreReport>();
			reportList.Add(newReport);
			newReport.Init(data);
			LayoutRebuilder.ForceRebuildLayoutImmediate(Content);
		}

		public void SetTestEvaluate(string newContent)
		{
			tmpTestEvaluate.text = newContent;
		}
	}
}
