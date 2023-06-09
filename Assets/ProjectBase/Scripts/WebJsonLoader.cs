using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

namespace ProjectBase
{
	public class WebJsonLoader : Singleton<WebJsonLoader>
	{
		public void Load(string path, string json)
		{
			MonoMgr.GetInstance().StartCoroutine(LoadAsync(path,json));
		}
		public IEnumerator LoadAsync(string path,string json)
		{
			UnityWebRequest request = UnityWebRequest.Get(path);
			yield return request.SendWebRequest();
			json = string.Copy(request.downloadHandler.text);
			Debug.Log(json);
		}
	}
}
