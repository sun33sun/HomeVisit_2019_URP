using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class SignalManager : MonoBehaviour
{
	#region µ¥ÀýÄ£Ê½
	private static SignalManager instance = null;
	public static SignalManager Instance { get { return instance; } }

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public Action<string> OnEndRecord;
	[SerializeField] AudioSource m_audioSource;

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
#else
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

	public void OnEndRecordTrigger(string newStr)
	{
		OnEndRecord?.Invoke(newStr);
	}
}
