/****************************************************************************
 * 2023.7 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class StudentInformation
	{
		[SerializeField] public TMPro.TextMeshProUGUI tmpName;
		[SerializeField] public TMPro.TextMeshProUGUI tmpSex;
		[SerializeField] public TMPro.TextMeshProUGUI tmpBirth;
		[SerializeField] public TMPro.TextMeshProUGUI tmpIdType;
		[SerializeField] public TMPro.TextMeshProUGUI tmpId;
		[SerializeField] public TMPro.TextMeshProUGUI tmpNationality;
		[SerializeField] public TMPro.TextMeshProUGUI tmpNation;
		[SerializeField] public TMPro.TextMeshProUGUI tmpResidencePermit;
		[SerializeField] public TMPro.TextMeshProUGUI tmpRemarkGuardian;
		[SerializeField] public TMPro.TextMeshProUGUI tmpPhone;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianIdType;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianId;
		[SerializeField] public TMPro.TextMeshProUGUI tmpRelationship;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianName;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianSex;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianEducation;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianUnit;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianDomicile;
		[SerializeField] public TMPro.TextMeshProUGUI tmpGuardianDistrict;
		[SerializeField] public UnityEngine.UI.Button btnConfirm;
		[SerializeField] public UnityEngine.UI.Button btnBack;

		public void Clear()
		{
			tmpName = null;
			tmpSex = null;
			tmpBirth = null;
			tmpIdType = null;
			tmpId = null;
			tmpNationality = null;
			tmpNation = null;
			tmpResidencePermit = null;
			tmpRemarkGuardian = null;
			tmpPhone = null;
			tmpGuardianIdType = null;
			tmpGuardianId = null;
			tmpRelationship = null;
			tmpGuardianName = null;
			tmpGuardianSex = null;
			tmpGuardianEducation = null;
			tmpGuardianUnit = null;
			tmpGuardianDomicile = null;
			tmpGuardianDistrict = null;
			btnConfirm = null;
			btnBack = null;
		}

		public override string ComponentName
		{
			get { return "StudentInformation";}
		}
	}
}
