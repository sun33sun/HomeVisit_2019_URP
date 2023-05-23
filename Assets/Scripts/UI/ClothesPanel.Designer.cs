using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:bb072dc8-1238-4e51-83b4-9eecc54e4e3e
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
		public RectTransform Content;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirm;
		[SerializeField]
		public UnityEngine.UI.Image imgConfirm;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmSubmit;
		
		private ClothesPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			imgTopBk = null;
			btnMan = null;
			btnWoman = null;
			btnCloseClothes = null;
			imgMidBk = null;
			imgTeacher = null;
			Content = null;
			btnSubmit = null;
			btnConfirm = null;
			imgConfirm = null;
			btnConfirmSubmit = null;
			
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
