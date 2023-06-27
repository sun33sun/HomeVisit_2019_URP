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
        public TMP_InputField inputID;
        public TMP_InputField inputName;
        public TMP_InputField inputSex;
        public TMP_InputField inputPhone;
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
        [Header("提示用UI")]
        public Image imgTip;
        public TextMeshProUGUI tmpTip;
        public Button btnConfirm;

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
            btnConfirm.onClick.AddListener(() => { imgTip.gameObject.SetActive(false); });
        }

        void Confirm()
        {
            string strTip = "您填写的信息无法进行下个步骤，请";

            if (dpOrganizationType.value != 0)
                strTip += "将机构类型换为学校";
            
            if (!togRoles[0].isOn)
			{
                if (!strTip.Equals("您填写的信息有问题，请"))
                    strTip += ",";
                strTip += "将所属角色换为教师";
			}
            
            if (dpUserState.value != 0)
            {
                if (!strTip.Equals("您填写的信息有问题，请"))
                    strTip += ",";
                strTip += "将用户状态切换为正常";
            }

			if (!dpClassIndex.options[dpClassIndex.value].text.Contains("初"))
			{
                if (!strTip.Equals("您填写的信息有问题，请"))
                    strTip += ",";
                strTip += "将班级切换为初中班级";
            }

            if (!dpAffiliatedUnit.options[dpAffiliatedUnit.value].text.Contains("中"))
            {
                if (!strTip.Equals("您填写的信息有问题，请"))
                    strTip += ",";
                strTip += "将学校切换为初中";
            }

            if (!strTip.Equals("您填写的信息无法进行下个步骤，请"))
			{
                tmpTip.text = strTip;
                imgTip.gameObject.SetActive(true);
                return;
			}

            TeacherData newData = new TeacherData();
            newData.strDistrict = dpDistrict.options[dpDistrict.value].text;
            newData.id = inputID.text;
            newData.strName = inputName.text;
            newData.strUnit = dpAffiliatedUnit.options[dpAffiliatedUnit.value].text;
            newData.role = togRoles[0].isOn ? 0 : 1;
            newData.strUserState = dpUserState.options[dpUserState.value].text;
            newData.classIndex = dpClassIndex.options[dpClassIndex.value].text;

            GetInfornationPanel getInfornationPanel = UIKit.GetPanel<GetInfornationPanel>();
            getInfornationPanel.SaveAdministratorData(newData);
            getInfornationPanel.AdministratorList.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

