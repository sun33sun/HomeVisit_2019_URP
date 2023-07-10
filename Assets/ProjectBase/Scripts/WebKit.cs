using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace ProjectBase
{
	public class WebKit : Singleton<WebKit>
	{
		public IEnumerator Read<T>(string path, Action<T> callBack)
		{
			try
			{

			}
			catch (Exception e)
			{
				Debug.Log("文件读取错误：" + e.Message);
			}
			UnityWebRequest request = UnityWebRequest.Get(path);
			yield return request.SendWebRequest();
			string json = request.downloadHandler.text;
			T t = JsonConvert.DeserializeObject<T>(json);
			callBack(t);
		}

		public IEnumerator Write(string path, object obj)
		{
			string json = JsonConvert.SerializeObject(obj);
			File.WriteAllText(path, json);
			yield return null;
			//UnityWebRequest request = UnityWebRequest(path, json);
			//request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
			//yield return request.SendWebRequest();
			//Debug.Log("Error : " + request.error + "\t\t" + request.responseCode);
		}
	}
}
