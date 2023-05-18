using QFramework;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public float volume;//音量

    private const int VOLUME_DATA_LENGTH = 128;    //录制的声音长度
    private const int frequency = 16000; //码率
    private const int lengthSec = 600;   //录制时长
    private const float minVolume = 3;//录音关闭音量值
    private const float maxVolume = 8;//录音开启音量值
    private const int minVolume_Sum = 15;//小音量总和值

    private AudioSource audioSource;  //录制的音频
    private bool isRecord;//录音开关
    private bool isStart;//录音开启的起点
    private int minVolume_Number;//记录的小音量数量
    private int start;//录音起点
    private int end;//录音终点

    private void Start()
    {
        StartMicrophone();
    }

    IEnumerator DoStartMicrophone()
	{
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
		if (Application.HasUserAuthorization(UserAuthorization.Microphone))
		{
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Microphone.Start(null, true, lengthSec, frequency);
        }
    }

    void StartMicrophone()
	{
        StopAllCoroutines();
        StartCoroutine(DoStartMicrophone());
    }

    private void Update()
    {
        volume = GetVolume(audioSource.clip, VOLUME_DATA_LENGTH);
        SwitchRecord();
    }

    /// <summary>
    /// 录音自动开关
    /// </summary>
    private void SwitchRecord()
    {
        //开
        if (GetVolume(audioSource.clip, VOLUME_DATA_LENGTH) >= maxVolume)
        {
            if (!isStart)
            {
                isStart = true;
                start = Microphone.GetPosition(Microphone.devices[0]);
            }
            minVolume_Number = 0;
            isRecord = true;
        }
        //关
        if (isRecord && GetVolume(audioSource.clip, VOLUME_DATA_LENGTH) < minVolume)
        {
            if (minVolume_Number > minVolume_Sum)
            {
                end = Microphone.GetPosition(Microphone.devices[0]);
                minVolume_Number = 0;
                isRecord = false;
                isStart = false;
                byte[] playerClipByte = AudioClipToByte(audioSource.clip, start, end);
                File.WriteAllBytes(Application.streamingAssetsPath + "/audio.wav", playerClipByte);
            }
            minVolume_Number++;
        }
    }

    /// <summary>
    /// 获取音量
    /// </summary>
    /// <param name="clip">音频片段</param>
    /// <param name="lengthVolume">长度</param>
    /// <returns></returns>
    private float GetVolume(AudioClip clip, int lengthVolume)
    {
        if (Microphone.IsRecording(null))
        {
            float maxVolume = 0f;
            //用于储存一段时间内的音频信息
            float[] volumeData = new float[lengthVolume];
            //获取录制的音频的开头位置
            int offset = Microphone.GetPosition(null) - (lengthVolume + 1);
            if (offset < 0)
                return 0f;
            //获取数据
            clip.GetData(volumeData, offset);
            //解析数据
            for (int i = 0; i < lengthVolume; i++)
            {
                float tempVolume = volumeData[i];
                if (tempVolume > maxVolume)
                    maxVolume = tempVolume;
            }
            return maxVolume * 99;
        }
        return 0;
    }

    /// <summary>
    /// clip转byte[]
    /// </summary>
    /// <param name="clip">音频片段</param>
    /// <param name="star">开始点</param>
    /// <param name="end">结束点</param>
    /// <returns></returns>
    public byte[] AudioClipToByte(AudioClip clip, int star, int end)
    {
        float[] data;
        if (end > star)
            data = new float[end - star];
        else
            data = new float[clip.samples - star + end];
        clip.GetData(data, star);
        int rescaleFactor = 32767; //to convert float to Int16
        byte[] outData = new byte[data.Length * 2];
        for (int i = 0; i < data.Length; i++)
        {
            short temshort = (short)(data[i] * rescaleFactor);
            byte[] temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }
        return outData;
    }
}

