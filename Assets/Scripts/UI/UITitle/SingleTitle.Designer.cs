using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:bd22b6e4-b144-4de1-9c47-754da4ecd760
	public partial class SingleTitle
	{
		public const string Name = "title";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI titleDescribe;
		[SerializeField]
		public UnityEngine.UI.Toggle togA;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpA;
		[SerializeField]
		public UnityEngine.UI.Toggle togB;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpB;
		[SerializeField]
		public UnityEngine.UI.Toggle togC;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpC;
		[SerializeField]
		public UnityEngine.UI.Toggle togD;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpD;
		[SerializeField]
		public TMPro.TextMeshProUGUI tmpAnalysis;

		string rightTip = "解析：<color=#00ff00ff>回答正确</color>";
		string errorTip = "";

		private SingleTitleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			titleDescribe = null;
			togA = null;
			tmpA = null;
			togB = null;
			tmpB = null;
			togC = null;
			tmpC = null;
			togD = null;
			tmpD = null;
			tmpAnalysis = null;
			
			mData = null;
		}
		
		public SingleTitleData Data
		{
			get
			{
				return mData;
			}
		}
		
		SingleTitleData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new SingleTitleData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
