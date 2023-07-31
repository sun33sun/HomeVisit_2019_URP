using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:0f586768-f1e3-4168-9be8-3126e6719924
	public partial class RecordSheetPanel
	{
		public const string Name = "RecordSheetPanel";
		
		[SerializeField]
		public UnityEngine.UI.InputField inputName;
		[SerializeField]
		public UnityEngine.UI.InputField inputSex;
		[SerializeField]
		public UnityEngine.UI.InputField inputBirth;
		[SerializeField]
		public UnityEngine.UI.InputField inputIdType;
		[SerializeField]
		public UnityEngine.UI.InputField inputId;
		[SerializeField]
		public UnityEngine.UI.InputField inputNationality;
		[SerializeField]
		public UnityEngine.UI.InputField inputNation;
		[SerializeField]
		public UnityEngine.UI.InputField inputResidencePermit;
		[SerializeField]
		public UnityEngine.UI.InputField inputRemarkGuardian;
		[SerializeField]
		public UnityEngine.UI.InputField inputPhone;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianIdType;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianId;
		[SerializeField]
		public UnityEngine.UI.InputField inputRelationship;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianName;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianSex;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianEducation;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianUnit;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianDomicile;
		[SerializeField]
		public UnityEngine.UI.InputField inputGuardianDistrict;
		[SerializeField]
		public UnityEngine.UI.InputField inputStudentCharacteristics;
		[SerializeField]
		public UnityEngine.UI.InputField inputFamilySituation;
		[SerializeField]
		public UnityEngine.UI.Button btnSubmit;
		[SerializeField]
		public UnityEngine.UI.Button btnClose;
		
		private RecordSheetPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			inputName = null;
			inputSex = null;
			inputBirth = null;
			inputIdType = null;
			inputId = null;
			inputNationality = null;
			inputNation = null;
			inputResidencePermit = null;
			inputRemarkGuardian = null;
			inputPhone = null;
			inputGuardianIdType = null;
			inputGuardianId = null;
			inputRelationship = null;
			inputGuardianName = null;
			inputGuardianSex = null;
			inputGuardianEducation = null;
			inputGuardianUnit = null;
			inputGuardianDomicile = null;
			inputGuardianDistrict = null;
			inputStudentCharacteristics = null;
			inputFamilySituation = null;
			btnSubmit = null;
			btnClose = null;
			
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
