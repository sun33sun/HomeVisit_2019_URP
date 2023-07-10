using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;

namespace HomeVisit.UI
{
	public class TopPanelData : UIPanelData
	{
	}
	public partial class TopPanel : UIPanel
	{
		KnowledgeExamPanel knowledgeExamPanel = null;
		TestReportPanel testReportPanel = null;
		TestBriefPanel testBriefPanel = null;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TopPanelData ?? new TopPanelData();
			//获取页面
			knowledgeExamPanel = UIKit.GetPanel<KnowledgeExamPanel>();
			testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testBriefPanel = UIKit.GetPanel<TestBriefPanel>();
			//订阅点击事件
			btnSeting.onClick.AddListener(SwitchSettingBtn);
			btnTip.onClick.AddListener(SwitchTip);

			btnTestReport.onClick.AddListener(SwitchTestReportPanel);
			btnKnowledge.onClick.AddListener(SwitchKnowledgePanel);
			btnTestBrief.onClick.AddListener(SwitchTestBriefPanel);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{

		}

		void SwitchTip()
		{
			imgTipAnim.gameObject.SetActive(false);
			imgTip.gameObject.SetActive(!imgTip.gameObject.activeInHierarchy);
		}
		
		public void ChangeTip(string newContent)
		{
			tmpTip.text = newContent;
			imgTipAnim.gameObject.SetActive(true);
		}

		protected override void OnShow()
		{
			tmpTip.text = "请根据文本内容进行详细的操作";
			imgTip.gameObject.SetActive(false);

			btnKnowledge.gameObject.SetActive(false);
			btnTestBrief.gameObject.SetActive(false);
			btnTestReport.gameObject.SetActive(false);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		void SwitchSettingBtn()
		{
			if (btnKnowledge.gameObject.activeInHierarchy)
			{
				btnKnowledge.gameObject.SetActive(false);
				btnTestBrief.gameObject.SetActive(false);
				btnTestReport.gameObject.SetActive(false);
			}
			else
			{
				btnKnowledge.gameObject.SetActive(true);
				btnTestBrief.gameObject.SetActive(true);
				btnTestReport.gameObject.SetActive(true);
			}
		}

		void SwitchKnowledgePanel()
		{
			switch (knowledgeExamPanel.State)
			{
				case PanelState.Opening:
					knowledgeExamPanel.Hide();
					break;
				case PanelState.Hide:
					knowledgeExamPanel.Show();
					knowledgeExamPanel.StartCoroutine(knowledgeExamPanel.LoadKnowledgeExamPaper());
					break;
			}
		}

		void SwitchTestReportPanel()
		{
			switch (testReportPanel.State)
			{
				case PanelState.Opening:
					testReportPanel.Hide();
					break;
				case PanelState.Hide:
					testReportPanel.Show();
					break;
			}
		}

		void SwitchTestBriefPanel()
		{
			switch (testBriefPanel.State)
			{
				case PanelState.Opening:
					testBriefPanel.Hide();
					break;
				case PanelState.Hide:
					testBriefPanel.Show();
					break;
			}
		}

		public IEnumerator CloseEyeAnim()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 1, 1, 1);
			float duration = 0.1f;
			WaitForSeconds wait01 = new WaitForSeconds(0.1f);
			while (vector.y > 0)
			{
				vector.y -= duration;
				mat.SetVector("_Param", vector);
				yield return wait01;
			}
		}

		public IEnumerator OpenEyeAnim()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 0, 1, 1);
			float duration = 0.1f;
			WaitForSeconds wait01 = new WaitForSeconds(0.1f);
			while (vector.y < 1)
			{
				vector.y += duration;
				mat.SetVector("_Param", vector);
				yield return wait01;
			}
			imgBlank.gameObject.SetActive(false);
		}
	}
}
