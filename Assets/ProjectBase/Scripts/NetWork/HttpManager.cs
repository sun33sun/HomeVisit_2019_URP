using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using HomeVisit;

namespace ProjectBase
{
    public class HttpManager : Singleton<HttpManager>
    {
        bool _isBusy = false;

        [DllImport("__Internal")]
        public static extern string StringReturnValueFunction();


        public IEnumerator PostAsync(string url, string auth, string contentType, object submitData,
            DownloadHandler downloadHandler)
        {
            if (_isBusy)
            {
                Debug.LogWarning("<color=#ff0000>当前正在请求网络，请稍后再试</color>");
                yield break;
            }

            _isBusy = true;
            string json = JsonConvert.SerializeObject(submitData);
            Debug.Log($"Post预打印json: {json}");
            byte[] postBytes = Encoding.UTF8.GetBytes(json);

            UnityWebRequest request = new UnityWebRequest(url, "Post"); //method传输方式，默认为Get
            request.SetRequestHeader("Auth", auth);
            request.uploadHandler = new UploadHandlerRaw(postBytes) { contentType = contentType }; //实例化上传缓存器
            request.downloadHandler = downloadHandler; //实例化下载存贮器
            request.SetRequestHeader("Content-Type", contentType); //更改内容类型
            yield return request.SendWebRequest(); //发送请求
            Debug.Log("Post Status Code: " + request.responseCode); //获得返回值
            if (request.responseCode == 200) //检验是否成功
            {
                string text = request.downloadHandler.text; //打印获得值
                Debug.Log("Post成功，返回值为：" + text);
            }

            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogWarning(request.error);
            }
            Debug.Log("--------------------Post结束--------------------");

            _isBusy = false;
        }
    }
}