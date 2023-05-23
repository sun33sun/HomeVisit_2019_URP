using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:4da0086e-7e0c-408d-8a22-4d36d7a16991
	public partial class OutlineTitle
	{
		public const string Name = "OutlineTitle";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI titleDescribe;
		
		private OutlineTitleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			titleDescribe = null;
			
			mData = null;
		}
		
		public OutlineTitleData Data
		{
			get
			{
				return mData;
			}
		}
		
		OutlineTitleData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new OutlineTitleData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
