﻿using UnityEngine;
using QFramework;


namespace HomeVisit.UI
{
    public class Main : MonoBehaviour
    {
        void Start()
        {
            ResKit.InitAsync().ToAction().Start(this,()=> 
            {
                UIKit.OpenPanelAsync<MainPanel>(UILevel.Bg).ToAction().Start(this);
                UIKit.OpenPanelAsync<TestReportPanel>().ToAction().Start(this,()=>
                {
                    UIKit.OpenPanelAsync<KnowledgeExamPanel>().ToAction().Start(this,()=> 
                    {
                        UIKit.OpenPanelAsync<ButtonPanel>(UILevel.PopUI).ToAction().Start(this);
                    });
                });
            });
        }
    }
}


