using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using ProjectBase;
using UnityEngine;
using UnityEngine.Networking;

namespace HomeVisit
{
	public class NetWorkResult
	{
		public int errno;
		public string errmsg;
		public int data;
	}

	public class NetWorkQuestion
	{
		public int appid;
		public string QuestionText;
		public string grade;
		public string type;
	}

	public class NetWorkConfig
	{
		//应用ID,设置为2
		public int AppId = 2;

		//用户类型，1为管理员，2为教师，3为学生，4为评审专家
		public int UserType = 1;
		public string UserName = "admin";
		public string Pwd = "123";
		public string DomainUrl = "https://homevisit.project.shxyxx.cn";

		//登录接口
		public string LoginUrl => DomainUrl + "/api/admin/record/loginRecord";
		public string SaveStudyUrl => DomainUrl + "/api/admin/record/saveStudy";
		public string SaveStudyScoreUrl => DomainUrl + "/api/admin/record/saveStudyScore";
		public string SaveStudyTotalScoreUrl => DomainUrl + "/api/admin/record/saveStudyTotalScore";
		public string SaveStudyQuestionUrl => DomainUrl + "saveStudyQuestion";
		public string GetSearchStudyQuestionUrl => DomainUrl + "/api/admin/record/getSearchStudyQuestion";


		public string ContentType = "application/json;charset=utf-8";
		public string Auth = "62e93c92644d344a43f6e82315b32c4b";

		public BackendPlaform nowPlaform = BackendPlaform.SelfWeb;
	}
	public enum BackendPlaform
	{
		[Description("学校自己的平台")]
		SelfWeb,
		[Description("实验空间平台")]
		PublicPlaform
	}

	public class NetWorkManager : QFramework.Singleton<NetWorkManager>
	{
		bool IsBusy = false;

		NetWorkConfig config =  new NetWorkConfig();

		private HttpManager HttpMgr { get; }

		private NetWorkManager()
		{
			HttpMgr = HttpManager.GetInstance();
			MonoMgr.GetInstance().StartCoroutine(LoadConfig());
		}

		IEnumerator LoadConfig()
		{
			UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + "/Config.json") as UnityWebRequest;
			yield return request.SendWebRequest();
			string json = request.downloadHandler.text;
			config = JsonConvert.DeserializeObject<NetWorkConfig>(json);
		}

		public IEnumerator LoginRecordAsync()
		{
			if (IsBusy)
				yield break;
			IsBusy = true;

			var tempObj = new { appid = config.AppId, usertype = config.UserType, username = config.UserName, pwd = config.Pwd };
			DownloadHandlerBuffer dhb = new DownloadHandlerBuffer();
			yield return HttpMgr.PostAsync(config.LoginUrl, config.Auth, config.ContentType, tempObj, dhb);

			IsBusy = false;
		}

		public IEnumerator SaveStudyAsync(int newSubID, Action<string> callBack)
		{
			if (IsBusy)
				yield break;
			IsBusy = true;

			var tempObj = new
			{ appid = config.AppId, itemID = 2, subID = newSubID, userAutoid = "", LoginTime = DateTime.Now };
			DownloadHandlerBuffer dhb = new DownloadHandlerBuffer();
			yield return HttpMgr.PostAsync(config.SaveStudyUrl, config.Auth, config.ContentType, tempObj, dhb);
			NetWorkResult result = JsonConvert.DeserializeObject<NetWorkResult>(dhb.text);
			callBack?.Invoke(result.data.ToString());

			IsBusy = false;
		}

		/// <summary>
		/// newScore的最后一位表示小数的位数，其他位数表示得分。例如得分为78.25分，2位小数，那么score的值应为78252，
		/// 如果得分为78.258，3位小数，那么score的值为782583
		/// </summary>
		/// <param name="newStudyrecordAutoid"></param>
		/// <param name="newSecond"></param>
		/// <param name="newScore"></param>
		/// <returns></returns>
		public IEnumerator SaveStudyScoreAsync(string newStudyrecordAutoid, int newSecond, string newScore)
		{
			if (IsBusy)
				yield break;
			IsBusy = true;

			var tempObj = new
			{
				appid = config.AppId,
				studyrecordAutoid = newStudyrecordAutoid,
				Second = newSecond,
				score = newScore
			};
			DownloadHandlerBuffer dhb = new DownloadHandlerBuffer();
			yield return HttpMgr.PostAsync(config.SaveStudyScoreUrl, config.Auth, config.ContentType, tempObj, dhb);

			IsBusy = false;
		}

		public IEnumerator SaveStudyTotalScoreAsync(string newStudyrecordAutoid, string newScore, string newComment)
		{
			if (IsBusy)
				yield break;
			IsBusy = true;

			var tempObj = new
			{
				appid = config.AppId,
				studyrecordAutoid = newStudyrecordAutoid,
				minutes = 0,
				score = newScore,
				comment = newComment,
				ExitTime = DateTime.Now
			};
			DownloadHandlerBuffer dhb = new DownloadHandlerBuffer();
			yield return HttpMgr.PostAsync(config.SaveStudyTotalScoreUrl, config.Auth, config.ContentType, tempObj, dhb);

			IsBusy = false;
		}

		public IEnumerator GetSearchQuestionAsync(int newType, int newPageNum, int newPageSize,
			Action<List<NetWorkQuestion>> callBack)
		{
			if (IsBusy)
				yield break;
			IsBusy = true;

			var tempObj = new
			{
				appid = config.AppId,
				type = newType,
				status = 1,
				keyword = "",
				pageNum = newPageNum,
				pageSize = newPageSize
			};
			DownloadHandlerBuffer dhb = new DownloadHandlerBuffer();
			yield return HttpMgr.PostAsync(config.GetSearchStudyQuestionUrl, config.Auth, config.ContentType, tempObj, dhb);
			Debug.Log(dhb.text);
			// List<NetWorkQuestion> list = JsonConvert.DeserializeObject<List<NetWorkQuestion>>(dhb.text);
			callBack?.Invoke(null);

			IsBusy = false;
		}
	}
}