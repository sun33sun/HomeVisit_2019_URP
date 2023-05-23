using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:52a0be06-9b5e-4c5d-881c-5965b9f9f967
	public partial class RecordSheetPanel
	{
		public const string Name = "RecordSheetPanel";
		
		[SerializeField]
		public TMPro.TMP_InputField inputDate;
		[SerializeField]
		public TMPro.TMP_InputField inputTime;
		[SerializeField]
		public TMPro.TMP_InputField inputName;
		[SerializeField]
		public TMPro.TMP_InputField inputSex;
		[SerializeField]
		public TMPro.TMP_InputField inputAddress;
		[SerializeField]
		public TMPro.TMP_InputField inputParentState;
		[SerializeField]
		public TMPro.TMP_InputField inputDescribeStudent;
		[SerializeField]
		public TMPro.TMP_InputField inputDescribeParent;
		[SerializeField]
		public TMPro.TMP_InputField inputStudentSpeciality;
		[SerializeField]
		public TMPro.TMP_InputField inputStudentHigh;
		[SerializeField]
		public TMPro.TMP_InputField inputStudentDeskmate;
		[SerializeField]
		public TMPro.TMP_InputField inputClassDuty;
		[SerializeField]
		public TMPro.TMP_InputField inputOutline;
		[SerializeField]
		public TMPro.TMP_InputField inputCharacter;
		[SerializeField]
		public TMPro.TMP_InputField inputInterest;
		[SerializeField]
		public TMPro.TMP_InputField inputSpeciality;
		[SerializeField]
		public TMPro.TMP_InputField inputParentDemand;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		
		private RecordSheetPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			inputDate = null;
			inputTime = null;
			inputName = null;
			inputSex = null;
			inputAddress = null;
			inputParentState = null;
			inputDescribeStudent = null;
			inputDescribeParent = null;
			inputStudentSpeciality = null;
			inputStudentHigh = null;
			inputStudentDeskmate = null;
			inputClassDuty = null;
			inputOutline = null;
			inputCharacter = null;
			inputInterest = null;
			inputSpeciality = null;
			inputParentDemand = null;
			btnClose = null;
			btnSubmit = null;
			
			mData = null;
		}
		
		public RecordSheetPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		RecordSheetPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new RecordSheetPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
