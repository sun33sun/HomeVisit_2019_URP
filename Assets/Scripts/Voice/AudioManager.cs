using UnityEngine;
using ProjectBase;
using System.Collections.Generic;
using System.Collections;
using Cysharp.Threading.Tasks;
using System.Threading;

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

	public IEnumerator Play(string clipName)
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

			while (source.isPlaying)
			{
				yield return null;
			}
		}

	}

	public async UniTask Play(AudioClip ac,CancellationToken token)
	{
		if (ac == null)
		{
			print($"播放选项语音：Null");
		}
		else
		{
			source.clip = ac;	
			source.Play();
			while (source.isPlaying)
			{
				await UniTask.Yield(token);
			}
		}
	}
}

