using UnityEngine;
using ProjectBase;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class RecordManager : SingletonMono<RecordManager>
{
    Dictionary<string, bool> keywordDic = new Dictionary<string, bool>();

#if UNITY_EDITOR
    XunFeiYuYin xunFei;
#endif

	protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        xunFei = XunFeiYuYin.Init("ddeb7aa4", "ODk2OTgzMDMxYjczM2Y3YjE0OWQ3ZTM4", "75ba1be15320ede80d3e86bf6fd17c82",
            "c6ea43c9e7b14d163bdeb4e51d2e564d");
        xunFei.语音识别完成事件 += OnResult;
#else
		SignalManager.Instance.OnEndRecord += OnResult;
#endif
    }

    public void StartRecord(string[] keywords)
    {
		keywordDic.Clear();
		for (int i = 0; i < keywords.Length; i++)
		{
			keywordDic.Add(keywords[i], false);
		}
#if UNITY_EDITOR
		xunFei.开始语音识别();
#else
		SignalManager.Instance.StartRecorderFunc();
#endif
    }


    void OnResult(string newStr)
    {
        string[] keys = new string[keywordDic.Count];
        keywordDic.Keys.CopyTo(keys, 0);
        foreach (var item in keys)
        {
            if (newStr.Contains(item))
            {
                keywordDic[item] = true;
                Debug.Log("识别到：" + keywordDic[item]);
            }
            else
            {
				Debug.Log("未识别到：" + keywordDic[item]);
            }
        }
		EventCenter.GetInstance().EventTrigger("实时语音结果", newStr);
        EventCenter.GetInstance().EventTrigger("语音识别结果", keywordDic);
    }

    public void EndRecord()
    {
        Debug.Log("Unity Execute EndRecord");
#if UNITY_EDITOR
        StartCoroutine(xunFei.停止语音识别());
#else
		SignalManager.Instance.EndRecorderFunc();
#endif
    }
}