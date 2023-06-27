using QFramework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SignalManager : MonoBehaviour
{
	#region ����ģʽ
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
	///���ȡ���ݵ���Ŀ
	/// </summary>
	private int m_valuePartCount = 0;
	/// <summary>
	/// ��ȡ��������Ŀ
	/// </summary>
	private int m_getDataLength = 0;
	/// <summary>
	/// ��ȡ�����ݳ���
	/// </summary>
	private int m_audioLength = 0;
	/// <summary>
	/// ��ȡ������
	/// </summary>
	private string[] m_audioData = null;

	/// <summary>
	/// ��ǰ��Ƶ
	/// </summary>
	public static AudioClip m_audioClip = null;

	/// <summary>
	/// ��ƵƬ�δ���б�
	/// </summary>
	private List<byte[]> m_audioClipDataList;

	/// <summary>
	/// Ƭ�ν������
	/// </summary>
	private string m_currentRecorderSign;
	/// <summary>
	/// ��ƵƵ��
	/// </summary>
	private int m_audioFrequency;

	/// <summary>
	/// �������¼��ʱ��
	/// </summary>
	private const int maxRecordTime = 30;
	#endregion

	public void OnEndRecordTrigger(string newStr)
	{
		OnEndRecord?.Invoke(newStr);
	}
	#endregion
}
