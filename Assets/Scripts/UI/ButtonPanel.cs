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
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ButtonPanelData ?? new ButtonPanelData();

			//��������ʼ��ҳ��
			knowledgeExamPanel = UIKit.GetPanel<KnowledgeExamPanel>();
			knowledgeExamPanel.Hide();
			testReportPanel = UIKit.GetPanel<TestReportPanel>();
			testReportPanel.Hide();

			//���ĵ���¼�
			btnSeting.onClick.AddListener(SwitchSettingBtn);
			btnKnowledge.onClick.AddListener(SwitchKnowledgePanel);
			btnTestReport.onClick.AddListener(SwitchTestReportPanel);
			btnTip.onClick.AddListener(() => { imgTip.gameObject.SetActive(false); });
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
			tmpTip.text = "������ı����ݽ�����ϸ�Ĳ���";
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
					knowledgeExamPanel.Hide();
					break;
				case PanelState.Hide:
					knowledgeExamPanel.Show();
					break;
			}
		}
	}
}
