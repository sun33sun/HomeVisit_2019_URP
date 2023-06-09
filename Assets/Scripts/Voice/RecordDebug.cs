using UnityEngine;
using ProjectBase;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class RecordDebug : SingletonMono<RecordDebug>
{
	Dictionary<string, bool> keywordDic = new Dictionary<string, bool>();
	public Action<string,bool> OnRight;
	XunFeiYuYin xunFei;

	protected override void Awake()
	{
		base.Awake();
		xunFei = XunFeiYuYin.Init("ddeb7aa4", "ODk2OTgzMDMxYjczM2Y3YjE0OWQ3ZTM4", "75ba1be15320ede80d3e86bf6fd17c82", "c6ea43c9e7b14d163bdeb4e51d2e564d");
		xunFei.语音识别完成事件 += OnResult;
	}

	public void StartRecord(string[] keywords,Action<string,bool> callBack)
	{
		keywordDic.Clear();
		OnRight = null;
		OnRight += callBack;
		for (int i = 0; i < keywords.Length; i++)
		{
			keywordDic.Add(keywords[i], false);
		}
		xunFei.开始语音识别();
	}

	void OnResult(string newStr)
	{
		string[] keys = new string[keywordDic.Count];
		keywordDic.Keys.CopyTo(keys,0);
		foreach (var item in keys)
		{
			if (newStr.Contains(item))
				keywordDic[item] = true;
		}

		bool isRight = true;
		foreach (var item in keywordDic.Values)
		{
			if (!item)
				isRight = false;
		}
		OnRight?.Invoke(newStr,isRight);
	}

	public void EndRecord()
	{
		xunFei.停止语音识别();
	}
}

