using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public class ButtonPanelData : UIPanelData
	{
	}
	public partial class ButtonPanel : UIPanel
	{
		KnowledgeExamPanel knowledgeExamPanel = null;
		TestReportPanel testReportPanel = null;
		TestBriefPanel testBriefPanel = null;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ButtonPanelData ?? new ButtonPanelData();
			//获取页面
			knowledgeExamPanel = UIKit.GetPanel<KnowledgeExamPanel>();
			testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testBriefPanel = UIKit.GetPanel<TestBriefPanel>();
			//订阅点击事件
			btnSeting.onClick.AddListener(SwitchSettingBtn);
			btnTip.onClick.AddListener(() => { imgTip.gameObject.SetActive(!imgTip.gameObject.activeInHierarchy); });

			btnTestReport.onClick.AddListener(SwitchTestReportPanel);
			btnKnowledge.onClick.AddListener(SwitchKnowledgePanel);
			btnTestBrief.onClick.AddListener(SwitchTestBriefPanel);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{

		}
		
		public void ChangeTip(string newContent)
		{
			tmpTip.text = newContent;
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
	}
}
