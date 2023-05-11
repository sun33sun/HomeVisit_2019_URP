using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:193c6ace-be11-407c-8d2b-38f2878d2d37
	public partial class MultipleTitle
	{
		public const string Name = "MultipleTitle";
		
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

		private MultipleTitleData mPrivateData = null;
		
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
		
		public MultipleTitleData Data
		{
			get
			{
				return mData;
			}
		}
		
		MultipleTitleData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MultipleTitleData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
