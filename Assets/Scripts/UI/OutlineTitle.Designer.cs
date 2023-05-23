using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:dc4e9067-e73f-403c-ab03-24d5f04cbff2
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
