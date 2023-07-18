using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
    public class EditTeachterInformation : MonoBehaviour
    {
        public InputField inputName;
        public InputField inputPhone;
        public TMP_Dropdown dpAffiliatedUnit;
        public TMP_Dropdown dpClassIndex;
        public Button btnSave;

        void Start()
        {
            btnSave.onClick.AddListener(Confirm);
        }

        void Confirm()
        {
            TeacherData newData = new TeacherData();
            //newData.strDistrict = dpDistrict.options[dpDistrict.value].text;
            newData.strName = inputName.text;
            newData.strUnit = dpAffiliatedUnit.options[dpAffiliatedUnit.value].text;
            string strDistrict = newData.strUnit.Substring(0, 2);
            newData.strDistrict = strDistrict == "浦东" ? "浦东新区" : strDistrict;
            newData.classIndex = dpClassIndex.options[dpClassIndex.value].text;

            GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
            panel.SaveTeacherData(newData);
            gameObject.SetActive(false);
            panel.PolicyList.gameObject.SetActive(true);
        }
    }
}

