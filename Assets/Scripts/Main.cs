using UnityEngine;
using QFramework;


namespace HomeVisit.UI
{
    public class Main : MonoBehaviour
    {
        void Start()
        {
            ResMgr.Init();
			UIKit.OpenPanelAsync<MainPanel>(UILevel.Bg).ToAction().Start(this);
			UIKit.OpenPanelAsync<ButtonPanel>(UILevel.PopUI).ToAction().Start(this);
		}
    }
}


