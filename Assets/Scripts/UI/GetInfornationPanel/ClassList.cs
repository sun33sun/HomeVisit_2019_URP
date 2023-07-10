using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HomeVisit.UI
{
	public class ClassList : MonoBehaviour
	{
		[SerializeField] List<TextMeshProUGUI> tmpClassList;
		[SerializeField] List<Button> btnClassList;

		private void Start()
		{
			TeacherData rightData = UIKit.GetPanel<GetInformationPanel>().nowAdministrator;
			for (int i = 0; i < btnClassList.Count; i++)
			{
				int index = i;
				btnClassList[i].onClick.AddListener(() =>
				{
					gameObject.SetActive(false);
					UIKit.GetPanel<GetInformationPanel>().StudentList.gameObject.SetActive(true);
				});
			}
		}


	}

}
