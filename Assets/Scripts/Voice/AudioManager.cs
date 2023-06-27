using UnityEngine;
using ProjectBase;
using System.Collections.Generic;

public class AudioManager : SingletonMono<AudioManager>
{
	[SerializeField] AudioSource source;
	[SerializeField] List<AudioClip> audioList;
	Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();

	protected override void Awake()
	{
		base.Awake();
		for (int i = 0; i < audioList.Count; i++)
		{
			audioDic.Add(audioList[i].name, audioList[i]);
		}
	}

	public float PlayAudio(string audioName)
	{
		if (!audioDic.ContainsKey(audioName))
			return -1;
		source.clip = audioDic[audioName];
		source.Play();
		Debug.Log("音频片段长度：" + audioDic[audioName].length);
		return audioDic[audioName].length;
	}

	public void StopAudio()
	{
		source.Stop();
		source.clip = null;
	}

	public WaitUntil Play(string clipName)
	{
		AudioClip ac = audioDic[clipName];
		if (ac == null)
		{
			print($"播放NPC语音：{clipName}");
		}
		else
		{
			source.clip = ac;
			source.Play();
		}
		return new WaitUntil(CheckPlay);
	}

	bool CheckPlay()
	{
		return !source.isPlaying;
	}
}

