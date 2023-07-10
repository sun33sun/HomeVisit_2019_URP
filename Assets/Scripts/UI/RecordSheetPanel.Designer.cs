using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:ad912991-7bee-4fb8-bbf8-899657b3ab08
	public partial class RecordSheetPanel
	{
		public const string Name = "RecordSheetPanel";
		
		[SerializeField]
		public UnityEngine.UI.InputField inputDate;
		[SerializeField]
		public UnityEngine.UI.InputField inputTime;
		[SerializeField]
		public UnityEngine.UI.InputField inputName;
		[SerializeField]
		public UnityEngine.UI.InputField inputSex;
		[SerializeField]
		public UnityEngine.UI.InputField inputAddress;
		[SerializeField]
		public UnityEngine.UI.InputField inputParentState;
		[SerializeField]
		public UnityEngine.UI.InputField inputDescribeStudent;
		[SerializeField]
		public UnityEngine.UI.InputField inputDescribeParent;
		[SerializeField]
		public UnityEngine.UI.InputField inputStudentSpeciality;
		[SerializeField]
		public UnityEngine.UI.InputField inputStudentHigh;
		[SerializeField]
		public UnityEngine.UI.InputField inputStudentDeskmate;
		[SerializeField]
		public UnityEngine.UI.InputField inputClassDuty;
		[SerializeField]
		public UnityEngine.UI.InputField inputOutline;
		[SerializeField]
		public UnityEngine.UI.InputField inputCharacter;
		[SerializeField]
		public UnityEngine.UI.InputField inputInterest;
		[SerializeField]
		public UnityEngine.UI.InputField inputSpeciality;
		[SerializeField]
		public UnityEngine.UI.InputField inputParentDemand;
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
