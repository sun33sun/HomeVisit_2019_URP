using UnityEngine;
using QFramework;


namespace HomeVisit.UI
{
    public class Main : MonoBehaviour
    {
        void Start()
        {
			ResKit.InitAsync().ToAction().Start(this, () =>
			 {
				 //主页面
				 UIKit.OpenPanelAsync<MainPanel>(UILevel.Bg).ToAction().Start(this);
				 //实验简介
				 UIKit.OpenPanelAsync<TestBriefPanel>().ToAction().Start(this);

				 //实验报告
				 UIKit.OpenPanelAsync<TestReportPanel>().ToAction().Start(this, () =>
				 {
					 UIKit.OpenPanelAsync<KnowledgeExamPanel>().ToAction().Start(this, () =>
					 {
						 UIKit.OpenPanelAsync<ButtonPanel>(UILevel.PopUI).ToAction().Start(this);
					 });
				 });

			 });
		}
	}
}


