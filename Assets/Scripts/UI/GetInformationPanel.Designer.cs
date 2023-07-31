using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:09ca052a-60b0-4971-a46a-8412eabab1f0
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
		public RectTransform InformationSecurity;
		[SerializeField]
		public UnityEngine.UI.Button btnConfirmInformationSecurity;
		[SerializeField]
		public TipElement TipElement;
		[SerializeField]
		public StudentInformation StudentInformation;
		[SerializeField]
		public StudentInformation1 StudentInformation1;
		
		private GetInformationPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			EditTeachterInformation = null;
			PolicyList = null;
			SchoolList = null;
			ClassList = null;
			StudentList = null;
			InformationSecurity = null;
			btnConfirmInformationSecurity = null;
			TipElement = null;
			StudentInformation = null;
			StudentInformation1 = null;
			
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
