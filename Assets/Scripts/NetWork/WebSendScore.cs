using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace HomeVisit
{
	public class WebSendScore : ProjectBase.SingletonMono<WebSendScore>
	{
		//public string GetURL;
		public string SendURL;
		public TextMeshProUGUI _总分;

		public List<string> _开始时间 = new List<string>();
		public List<string> _结束时间 = new List<string>();
		public List<TextMeshProUGUI> _得分;
		public DateTime originTime = new DateTime(1970, 1, 1, 0, 0, 0);

		public class Step
		{
			public string stepScore { get; set; }
			public string StepStartTime { get; set; }
			public string StepEndTime { get; set; }
		}

		public class Root
		{
			//public string eid { get; set; }
			public string TotalScore { get; set; }
			public List<Step> Steps { get; set; }
		}

		public void ClearData()
		{
			_开始时间.Clear();
			_结束时间.Clear();
		}

		public void SetStartTime(DateTime startTime)
		{
			_开始时间.Add(Convert.ToInt64((startTime - originTime).TotalSeconds).ToString());
		}

		public void SetEndTime(DateTime endTime)
		{
			// UnityWebRequest unityWeb = new UnityWebRequest(GetURL, UnityWebRequestType.POST.ToString());
			// Debug.Log(unityWeb.GetRequestHeader("Cookie"));

			_结束时间.Add(Convert.ToInt64((endTime - originTime).TotalSeconds).ToString());
		}

		public void Submit()//用这个
		{
			Root r = new Root()
			{
				//eid = "",//
				TotalScore = _总分.text.ToString(),
			};
			r.Steps = new List<Step>();
			int I = 0;
			for (int i = 0; i < _得分.Count; i++)
			{
				Step s = new Step();
				I = i;
				s.stepScore = _得分[I].text.ToString();
				s.StepStartTime = _开始时间[I].ToString();
				s.StepEndTime = _结束时间[I].ToString();
				r.Steps.Add(s);
			}
			WebRequest(UnityWebRequestType.POST, r);
		}
		private void WebRequest(UnityWebRequestType type, Root root)
		{
			string jsondata = JsonConvert.SerializeObject(root);
			Debug.Log(jsondata);

			StartCoroutine(WebRequest(type, SendURL, jsondata, false, delegate
			{
				Debug.Log("提交失败");
			}, delegate (string arg0)
			{
				Debug.Log("提交成功");
			}
			));
		}
		private IEnumerator WebRequest(UnityWebRequestType type, string url, string jsonData,
			bool getBase64, UnityAction failureCallBack, UnityAction<string> successCallBack, string[] otherHeaderName = null,
			string[] otherHeaderValue = null)
		{
			Debug.Log(type.ToString() + "\t" + url + "\t" + jsonData);
			/*if (sendBase64)
			{
				jsonData = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonData));
			}*/

			byte[] body = Encoding.UTF8.GetBytes(jsonData);

			UnityWebRequest unityWeb = new UnityWebRequest(@url, type.ToString());
			unityWeb.uploadHandler = new UploadHandlerRaw(body);
			unityWeb.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
			//unityWeb.SetRequestHeader("Authorization", "Bearer " + TK);

			if (otherHeaderName != null)
			{
				for (int i = 0; i < otherHeaderName.Length; i++)
				{
					unityWeb.SetRequestHeader(otherHeaderName[i], otherHeaderValue[i]);
				}
			}


			unityWeb.downloadHandler = new DownloadHandlerBuffer();
			yield return unityWeb.SendWebRequest();

			if (unityWeb.isNetworkError || unityWeb.isHttpError)
			{
				Debug.Log("UnityWebRequest 请求失败:" + unityWeb.error);
				failureCallBack?.Invoke();
				yield break;
			}

			if (unityWeb.isDone)
			{
				string result = unityWeb.downloadHandler.text;
				if (getBase64)
				{
					byte[] c = Convert.FromBase64String(result);
					result = Encoding.UTF8.GetString(c);
				}

				Debug.Log("UnityWebRequest 请求成功:" + result);
				successCallBack?.Invoke(result);
			}
		}
		public enum UnityWebRequestType
		{
			POST,
			GET
		}
	}

}
