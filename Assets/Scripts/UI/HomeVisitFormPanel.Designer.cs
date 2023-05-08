using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:5cca3a2e-49ab-4549-8109-dec9d99e5ccd
	public partial class HomeVisitFormPanel
	{
		public const string Name = "HomeVisitFormPanel";
		
		
		private HomeVisitFormPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public HomeVisitFormPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		HomeVisitFormPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HomeVisitFormPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
