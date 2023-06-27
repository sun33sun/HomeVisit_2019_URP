using QFramework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SignalManager : MonoBehaviour
{
	#region 单例模式
	private static SignalManager instance = null;
	public static SignalManager Instance { get { return instance; } }

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public Action<string> OnEndRecord;
	AudioSource m_audioSource;

	void Start()
	{
		m_audioSource = gameObject.AddComponent<AudioSource>();
	}

	#region UnityToJs
	[DllImport("__Internal")]
	private static extern void StartRecord();
	[DllImport("__Internal")]
	private static extern void StopRecord();
	public void StartRecorderFunc()
	{
#if UNITY_EDITOR
		StartRecord();
#endif
	}
	public void EndRecorderFunc()
	{
#if UNITY_EDITOR
#else
		StopRecord();
#endif
	}
	#endregion

	#region JsToUnity
	#region Data
	/// <summary>
	///需获取数据的数目
	/// </summary>
	private int m_valuePartCount = 0;
	/// <summary>
	/// 获取的数据数目
	/// </summary>
	private int m_getDataLength = 0;
	/// <summary>
	/// 获取的数据长度
	/// </summary>
	private int m_audioLength = 0;
	/// <summary>
	/// 获取的数据
	/// </summary>
	private string[] m_audioData = null;

	/// <summary>
	/// 当前音频
	/// </summary>
	public static AudioClip m_audioClip = null;

	/// <summary>
	/// 音频片段存放列表
	/// </summary>
	private List<byte[]> m_audioClipDataList;

	/// <summary>
	/// 片段结束标记
	/// </summary>
	private string m_currentRecorderSign;
	/// <summary>
	/// 音频频率
	/// </summary>
	private int m_audioFrequency;

	/// <summary>
	/// 单次最大录制时间
	/// </summary>
	private const int maxRecordTime = 30;
	#endregion

	public void OnEndRecordTrigger(string newStr)
	{
		OnEndRecord?.Invoke(newStr);
	}
	#endregion
}
