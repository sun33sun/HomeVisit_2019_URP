using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:9e4fe3ab-3123-42c7-9967-7aac9ce1050b
	public partial class ClothesPanel
	{
		public const string Name = "ClothesPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image imgTopBk;
		[SerializeField]
		public UnityEngine.UI.Button btnMan;
		[SerializeField]
		public UnityEngine.UI.Button btnWoman;
		[SerializeField]
		public UnityEngine.UI.Button btnCloseClothes;
		[SerializeField]
		public UnityEngine.UI.Image imgMidBk;
		[SerializeField]
		public UnityEngine.UI.Image imgTeacher;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Image imgConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		
		private ClothesPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgTopBk = null;
			btnMan = null;
			btnWoman = null;
			btnCloseClothes = null;
			imgMidBk = null;
			imgTeacher = null;
			btnSubmit = null;
			imgConfirm = null;
			btnConfirm = null;
			
			mData = null;
		}
		
		public ClothesPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		ClothesPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ClothesPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
