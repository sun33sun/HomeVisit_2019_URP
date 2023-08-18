using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class IOCMgr : IOCContainer
{
    #region 单例
    
    private static IOCMgr mInstance = null;

    public static IOCMgr Instance => mInstance;

    public static void Init()
    {
        mInstance = new IOCMgr();
    }
    #endregion
}
