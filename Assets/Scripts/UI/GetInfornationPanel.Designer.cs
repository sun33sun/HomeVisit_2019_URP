using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	// Generate Id:95c21738-d333-4d0e-b0da-3145ee19389f
	public partial class GetInfornationPanel
	{
		public const string Name = "GetInfornationPanel";
		
		[SerializeField]
		public EditTeachterInformation EditTeachterInformation;
		[SerializeField]
		public AdministratorList AdministratorList;
		[SerializeField]
		public ProcessOverview ProcessOverview;
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
		
		private GetInfornationPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			EditTeachterInformation = null;
			AdministratorList = null;
			ProcessOverview = null;
			PolicyList = null;
			SchoolList = null;
			ClassList = null;
			StudentList = null;
			StudentInformation = null;
			
			mData = null;
		}
		
		public GetInfornationPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GetInfornationPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GetInfornationPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
