using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:c81c738e-f933-4e8b-82f5-c082e2423a8b
	public partial class MaskPanel
	{
		public const string Name = "MaskPanel";
		
		
		private MaskPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public MaskPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		MaskPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MaskPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
