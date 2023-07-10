using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ZTools;
using QFramework;

namespace HomeVisit.UI
{
    public class EditTeachterInformation : MonoBehaviour
    {
        public InputField inputID;
        public InputField inputName;
        public InputField inputSex;
        public InputField inputPhone;
        public TMP_Dropdown dpDistrict;
        public TMP_Dropdown dpOrganizationType;
        public TMP_Dropdown dpAffiliatedUnit;
        public Toggle[] togRoles;
        public ZCalendar zStart;
        public TextMeshProUGUI tmpStartResult;
        public Button btnStart;
        public ZCalendar zEnd;
        public TextMeshProUGUI tmpEndResult;
        public Button btnEnd;
        public TMP_Dropdown dpUserState;
        public TMP_Dropdown dpClassIndex;
        public Button btnSave;

        void Start()
        {
            btnStart.onClick.AddListener(zStart.Show);
            btnEnd.onClick.AddListener(zEnd.Show);
            zStart.ChoiceDayEvent += item =>
            {
                tmpStartResult.text = item.dateTime.ToString("yyyy-MM-dd");
            };
            zEnd.ChoiceDayEvent += item =>
            {
                tmpEndResult.text = item.dateTime.ToString("yyyy-MM-dd");
            };
            btnSave.onClick.AddListener(Confirm);
        }

        void Confirm()
        {
            TeacherData newData = new TeacherData();
            newData.strDistrict = dpDistrict.options[dpDistrict.value].text;
            newData.id = inputID.text;
            newData.strName = inputName.text;
            newData.strUnit = dpAffiliatedUnit.options[dpAffiliatedUnit.value].text;
            newData.role = togRoles[0].isOn ? 0 : 1;
            newData.strUserState = dpUserState.options[dpUserState.value].text;
            newData.classIndex = dpClassIndex.options[dpClassIndex.value].text;

            GetInformationPanel getInfornationPanel = UIKit.GetPanel<GetInformationPanel>();
            getInfornationPanel.SaveAdministratorData(newData);
            getInfornationPanel.AdministratorList.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

