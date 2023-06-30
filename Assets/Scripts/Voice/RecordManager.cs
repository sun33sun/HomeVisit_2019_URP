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
		xunFei = XunFeiYuYin.Init("ddeb7aa4", "ODk2OTgzMDMxYjczM2Y3YjE0OWQ3ZTM4", "75ba1be15320ede80d3e86bf6fd17c82", "c6ea43c9e7b14d163bdeb4e51d2e564d");
		xunFei.语音识别完成事件 += OnResult;
#endif
		SignalManager.Instance.OnEndRecord += OnResult;
	}

	public void StartRecord(string[] keywords)
	{
#if UNITY_EDITOR
		keywordDic.Clear();
		for (int i = 0; i < keywords.Length; i++)
		{
			keywordDic.Add(keywords[i], false);
		}
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
				print("识别到：" + keywordDic[item]);
			}
			else
			{
				print("未识别到：" + keywordDic[item]);
			}
		}

		EventCenter.GetInstance().EventTrigger("语音识别结果", keywordDic);
	}

	public void EndRecord()
	{
#if UNITY_EDITOR
		xunFei.停止语音识别();
#else
		SignalManager.Instance.EndRecorderFunc();
#endif
	}
}

