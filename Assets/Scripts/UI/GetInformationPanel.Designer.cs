using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:1f5879a4-e008-4682-9703-bd03bdb7e61e
	public partial class GetInformationPanel
	{
		public const string Name = "GetInformationPanel";
		
		[SerializeField]
		public EditTeachterInformation EditTeachterInformation;
		[SerializeField]
		public PolicyList PolicyList;
		[SerializeField]
		public HomeVisit.UI.SchoolList SchoolList;
		[SerializeField]
		public ClassList ClassList;
		[SerializeField]
		public HomeVisit.UI.StudentList StudentList;
		[SerializeField]
		public StudentInformation StudentInformation;
		[SerializeField]
		public TipElement TipElement;
		[SerializeField]
		public RectTransform InformationSecurity;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmInformationSecurity;
		
		private GetInformationPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			EditTeachterInformation = null;
			PolicyList = null;
			SchoolList = null;
			ClassList = null;
			StudentList = null;
			StudentInformation = null;
			TipElement = null;
			InformationSecurity = null;
			btnConfirmInformationSecurity = null;
			
			mData = null;
		}
		
		public GetInformationPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GetInformationPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GetInformationPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
