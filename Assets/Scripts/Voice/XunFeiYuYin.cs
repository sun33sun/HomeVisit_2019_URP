using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

#if UNITY_EDITOR
public class XunFeiYuYin : MonoBehaviour
{
    public enum Voice
    {
        xiaoyan,//小燕,甜美女声,普通话
        aisjiuxu,//许久,亲切男声,普通话
        aisxping,//小萍知性女声,普通话
        aisjinger,//小婧,亲切女声,普通话
        aisbabyxu,//许小宝,可爱童声,普通话
        //以上为基础发音人下面的是付费的英语特色发音人,后台没开通是不能用的
        catherine,//Catherine英文女声
        john,//John英文男声
    }
    string APPID = "5c81de59";
    string APISecret = "ea4d5e9b06f8cfb0deae4d5360e7f8a7";
    string APIKey = "94348d7a6d5f3807176cb1f4923efa5c";
    public string 语音评测APIKey = "c6ea43c9e7b14d163bdeb4e51d2e564d";
    public static XunFeiYuYin yuyin;
    public event Action<string> 语音识别完成事件;   //语音识别回调事件
    public AudioClip RecordedClip;
    ClientWebSocket 语音识别WebSocket;
    private void Awake()
    {
        if (yuyin == null)
        {
            yuyin = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public static XunFeiYuYin Init(string appID, string APISecret, string APIKey, string 语音评测APIKey)
    {
        string name = "讯飞语音";
        if (yuyin == null)
        {
            GameObject g = new GameObject(name);
            g.AddComponent<XunFeiYuYin>();
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        yuyin.APPID = appID;
        yuyin.APISecret = APISecret;
        yuyin.APIKey = APIKey;
        yuyin.语音评测APIKey = 语音评测APIKey;
        //if (!yuyin.讯飞语音加)Debug.LogWarning("未安装或正确设置讯飞语音+将使用在线收费版讯飞引擎");

        //yuyin.javaClass.CallStatic("设置语音识别参数", new object[] { "language", "zh_cn" });//设置语音识别为中文
        return yuyin;
    }

    string GetUrl(string uriStr)
    {
        Uri uri = new Uri(uriStr);
        string date = DateTime.Now.ToString("r");
        string signature_origin = string.Format("host: " + uri.Host + "\ndate: " + date + "\nGET " + uri.AbsolutePath + " HTTP/1.1");
        HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(APISecret));
        string signature = Convert.ToBase64String(mac.ComputeHash(Encoding.UTF8.GetBytes(signature_origin)));
        string authorization_origin = string.Format("api_key=\"{0}\",algorithm=\"hmac-sha256\",headers=\"host date request-line\",signature=\"{1}\"", APIKey, signature);
        string authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(authorization_origin));
        string url = string.Format("{0}?authorization={1}&date={2}&host={3}", uri, authorization, date, uri.Host);
        return url;
    }
    #region 语音识别

    public void 开始语音识别()
    {
        if (语音识别WebSocket != null && 语音识别WebSocket.State == WebSocketState.Open)
        {
            Debug.LogWarning("开始语音识别失败！，等待上次识别连接结束");
            return;
        }
        连接语音识别WebSocket();
        RecordedClip = Microphone.Start(null, false, 60, 16000);
    }

    public IEnumerator 停止语音识别()
    {
        Microphone.End(null);
        yield return new WaitUntil(() => 语音识别WebSocket.State != WebSocketState.Open);
        Debug.Log("识别结束，停止录音");
    }



    async void 连接语音识别WebSocket()
    {
        using (语音识别WebSocket = new ClientWebSocket())
        {
            CancellationToken ct = new CancellationToken();
            Uri url = new Uri(GetUrl("wss://iat-api.xfyun.cn/v2/iat"));
            await 语音识别WebSocket.ConnectAsync(url, ct);
            Debug.Log("连接成功");
            StartCoroutine(发送录音数据流(语音识别WebSocket));
            StringBuilder stringBuilder = new StringBuilder();
            while (语音识别WebSocket.State == WebSocketState.Open)
            {
                var result = new byte[4096];
                await 语音识别WebSocket.ReceiveAsync(new ArraySegment<byte>(result), ct);//接受数据
                List<byte> list = new List<byte>(result); while (list[list.Count - 1] == 0x00) list.RemoveAt(list.Count - 1);//去除空字节
                string str = Encoding.UTF8.GetString(list.ToArray());
                Debug.Log("接收消息：" + str);
                stringBuilder.Append(获取识别单词(str));
                JSONNode js = JSONNode.Parse(str);
                JSONNode data = js["data"];
                if (data["status"] == 2)
                {
                    语音识别WebSocket.Abort();
                }
            }
            Debug.LogWarning("断开连接");
            string s = stringBuilder.ToString();

            if (!string.IsNullOrEmpty(s))
            {
                Debug.LogWarning("识别到声音：" + s);
            }
            语音识别完成事件?.Invoke(s);
        }
    }

    string 获取识别单词(string str)
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (!string.IsNullOrEmpty(str))
        {
            JSONNode recivejson = JSONNode.Parse(str);
            JSONNode ws = recivejson["data"]["result"]["ws"];
            foreach (JSONNode item in ws)
            {
                JSONNode cw = item["cw"];
                foreach (JSONNode item1 in cw)
                {
                    stringBuilder.Append((string)item1["w"]);
                }
            }
        }
        return stringBuilder.ToString();
    }

    public static byte[] 获取音频流片段(int star, int length, AudioClip recordedClip)
    {
        float[] soundata = new float[length];
        recordedClip.GetData(soundata, star);
        int rescaleFactor = 32767;
        byte[] outData = new byte[soundata.Length * 2];
        for (int i = 0; i < soundata.Length; i++)
        {
            short temshort = (short)(soundata[i] * rescaleFactor);
            byte[] temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }
        return outData;
    }

    void 发送数据(byte[] audio, int status, ClientWebSocket socket)
    {
        if (socket.State != WebSocketState.Open)
        {
            return;
        }
        JSONNode jn = new JSONNode
        {
            {
                "common",new JSONNode{{ "app_id",APPID}}},
            {
                "business",new JSONNode{
                    { "language","zh_cn"},//识别语音
                    {  "domain","iat"},
                    {  "accent","mandarin"},
                    { "vad_eos",2000}
                }
            },
            {
                "data",new JSONNode{
                    { "status",0 },
                    { "encoding","raw" },
                    { "format","audio/L16;rate=16000"}
                 }
            }
        };
        JSONNode data = jn["data"];
        if (status < 2)
        {
            data["audio"] = Convert.ToBase64String(audio);
        }
        data["status"] = status;
        Debug.Log("发送消息:" + jn);
        socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jn)), WebSocketMessageType.Binary, true, new CancellationToken()); //发送数据
    }

    IEnumerator 发送录音数据流(ClientWebSocket socket)
    {
        yield return new WaitWhile(() => Microphone.GetPosition(null) <= 0);
        float t = 0;
        int position = Microphone.GetPosition(null);
        const float waitTime = 0.04f;//每隔40ms发送音频
        int status = 0;
        int lastPosition = 0;
        const int Maxlength = 640;//最大发送长度
        while (position < RecordedClip.samples && socket.State == WebSocketState.Open)
        {
            t += waitTime;
            yield return new WaitForSecondsRealtime(waitTime);
            if (Microphone.IsRecording(null))
                position = Microphone.GetPosition(null);
            Debug.Log("录音时长：" + t + "position=" + position + ",lastPosition=" + lastPosition);
            if (position <= lastPosition)
            {
                Debug.LogWarning("字节流发送完毕！强制结束！");
                break;
            }
            int length = position - lastPosition > Maxlength ? Maxlength : position - lastPosition;
            byte[] date = 获取音频流片段(lastPosition, length, RecordedClip);
            发送数据(date, status, socket);
            lastPosition = lastPosition + length;
            status = 1;
        }
        发送数据(null, 2, socket);
        //WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "关闭WebSocket连接",new CancellationToken());
        Microphone.End(null);
    }
    #endregion

    #region 语音合成
    public AudioSource 语音合成流播放器;
    ClientWebSocket 语音合成WebSocket;
    int 语音流总长度 = 0;
    Queue<float> 播放队列 = new Queue<float>();

    /// <summary>
    /// 开始语音合成
    /// </summary>
    /// <param name="s">需要合成的字符串</param>
    /// <param name="voice">发音人，默认是xiaoyan</param>
    /// <param name="speed">语速，范围是0~100，默认是50</param>
    /// <param name="volume">音量，范围是0~100，默认50</param>
    /// <param name="reqId">请求id,默认0</param>
    public IEnumerator 开始语音合成(string text, Voice voice = Voice.xiaoyan, int speed = 50, int volume = 50)
    {
        if (语音合成WebSocket != null)
        {
            语音合成WebSocket.Abort();
        }
        if (语音合成流播放器 == null)
        {
            语音合成流播放器 = gameObject.AddComponent<AudioSource>();
        }
        语音合成流播放器.Stop();
        连接语音合成WebSocket(GetUrl("wss://tts-api.xfyun.cn/v2/tts"), text, voice.ToString(), speed, volume);
        语音合成流播放器.clip = AudioClip.Create("语音合成流", 16000 * 60, 1, 16000, true, OnAudioRead);//播放器最大播放时长一分钟
        语音合成流播放器.Play();//播放语音流
        while (true)
        {
            yield return null;
            if (!语音合成流播放器.isPlaying || 语音合成WebSocket.State == WebSocketState.Aborted && 语音合成流播放器.timeSamples >= 语音流总长度)
            {
                Debug.Log(text + "语音流播放完毕！");
                break;
            }
        }
    }
    void OnAudioRead(float[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (播放队列.Count > 0)
            {
                data[i] = 播放队列.Dequeue();
            }
            else
            {
                if (语音合成WebSocket == null || 语音合成WebSocket.State != WebSocketState.Aborted) 语音流总长度++;
                data[i] = 0;
            }
        }
    }
    public async void 连接语音合成WebSocket(string urlStr, String text, string voice, int speed, int volume)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        using (语音合成WebSocket = new ClientWebSocket())
        {
            CancellationToken ct = new CancellationToken();
            Uri url = new Uri(urlStr);
            await 语音合成WebSocket.ConnectAsync(url, ct);
            text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            JSONNode sendJson = new JSONNode
                         {
                            { "common",new JSONNode{ { "app_id", APPID } } },
                            { "business",new JSONNode{ { "vcn", voice },{ "aue", "raw" },{ "speed", speed },{ "volume", volume },{ "tte", "UTF8" } } },
                            { "data",new JSONNode{ { "status", 2 }, { "text", text } } }
                         };
            Debug.Log("发送消息:" + sendJson);
            Debug.Log("连接成功");
            await 语音合成WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(sendJson)), WebSocketMessageType.Binary, true, ct); //发送数据
            StringBuilder sb = new StringBuilder();
            播放队列.Clear();
            while (语音合成WebSocket.State == WebSocketState.Open)
            {
                var result = new byte[4096];
                await 语音合成WebSocket.ReceiveAsync(new ArraySegment<byte>(result), ct);//接受数据
                List<byte> list = new List<byte>(result); while (list[list.Count - 1] == 0x00) list.RemoveAt(list.Count - 1);//去除空字节  
                var str = Encoding.UTF8.GetString(list.ToArray());
                Debug.Log(str);
                sb.Append(str);
                if (str.EndsWith("}}"))
                {
                    JSONNode json = JSONNode.Parse(sb.ToString());
                    Debug.Log("收到完整json数据：" + json);
                    JSONNode data = json["data"];
                    int status = data["status"];
                    float[] fs = bytesToFloat(Convert.FromBase64String(data["audio"]));
                    语音流总长度 += fs.Length;
                    foreach (float f in fs) 播放队列.Enqueue(f);
                    sb.Clear();
                    if (status == 2)
                    {
                        语音合成WebSocket.Abort();
                        break;
                    }
                }
            }
        }
    }
    public static float[] bytesToFloat(byte[] byteArray)//byte[]数组转化为AudioClip可读取的float[]类型
    {
        float[] sounddata = new float[byteArray.Length / 2];
        for (int i = 0; i < sounddata.Length; i++)
        {
            sounddata[i] = bytesToFloat(byteArray[i * 2], byteArray[i * 2 + 1]);
        }
        return sounddata;
    }

    static float bytesToFloat(byte firstByte, byte secondByte)
    {
        // convert two bytes to one short (little endian)
        //小端和大端顺序要调整
        short s;
        if (BitConverter.IsLittleEndian)
            s = (short)((secondByte << 8) | firstByte);
        else
            s = (short)((firstByte << 8) | secondByte);
        // convert to range from -1 to (just below) 1
        return s / 32768.0F;
    }
    #endregion

    #region 语音评测
    AudioClip 语音评测录音;
    public AudioClip 开始语音评测录音()
    {
        语音评测录音 = Microphone.Start(null, false, 30, 16000);//开始录音最长30秒
        return 语音评测录音;
    }
    public void 停止语音评测查看结果(string txet, Action<string> 评测结果回调, AudioClip clip = null)
    {
        if (clip == null)
        {
            clip = 语音评测录音;
        }
        byte[] data = 获取真实大小录音(clip);
        Microphone.End(null);
        StartCoroutine(语音评测(data, txet, 评测结果回调));
    }
    public IEnumerator 语音评测(byte[] audioData, string 评测文本, Action<string> 评测结果回调)
    {
        string param = "{\"aue\":\"raw\",\"result_level\":\"entirety\",\"language\":\"en_us\",\"category\":\"read_sentence\"}";
        byte[] bytedata = Encoding.UTF8.GetBytes(param);
        string x_param = Convert.ToBase64String(bytedata);
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        string curTime = Convert.ToInt64(ts.TotalSeconds).ToString();
        string checksum = string.Format("{0}{1}{2}", 语音评测APIKey, curTime, x_param);
        string x_checksum = Md5(checksum);
        string cc = Convert.ToBase64String(audioData);
        String Url = "https://api.xfyun.cn/v1/service/v1/ise";
        WWWForm form = new WWWForm();
        form.AddField("audio", cc);
        form.AddField("text", 评测文本);
        UnityWebRequest request = UnityWebRequest.Post(Url, form);
        request.SetRequestHeader("X-Appid", APPID);
        request.SetRequestHeader("X-CurTime", curTime);
        request.SetRequestHeader("X-Param", x_param);
        request.SetRequestHeader("X-Checksum", x_checksum);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("网络出错请检查网络连接, err: " + request.error);
        }
        else
        {
            评测结果回调(request.downloadHandler.text);
        }
    }
    public static byte[] 获取真实大小录音(ref AudioClip recordedClip)
    {
        int position = Microphone.GetPosition(null);
        if (position <= 0 || position > recordedClip.samples)
        {
            position = recordedClip.samples;
        }
        float[] soundata = new float[position * recordedClip.channels];
        recordedClip.GetData(soundata, 0);
        recordedClip = AudioClip.Create(recordedClip.name, position, recordedClip.channels, recordedClip.frequency, false);
        recordedClip.SetData(soundata, 0);
        return 获取音频流片段(0, position * recordedClip.channels, recordedClip);
    }
    public static byte[] 获取真实大小录音(AudioClip recordedClip)
    {
        int position = Microphone.GetPosition(null);
        if (position <= 0 || position > recordedClip.samples)
        {
            position = recordedClip.samples;
        }
        return 获取音频流片段(0, position * recordedClip.channels, recordedClip);
    }
    public static String Md5(string s)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        bytes = md5.ComputeHash(bytes);
        md5.Clear();
        string ret = "";
        for (int i = 0; i < bytes.Length; i++)
        {
            ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
        }
        return ret.PadLeft(32, '0');
    }
    #endregion

}
#endif