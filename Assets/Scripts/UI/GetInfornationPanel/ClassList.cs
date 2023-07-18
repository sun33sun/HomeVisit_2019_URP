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
		[Header("老师信息")]
		public TextMeshProUGUI tmpTeacher;

		private void Start()
		{
			GetInformationPanel panel = UIKit.GetPanel<GetInformationPanel>();
			for (int i = 0; i < btnClassList.Count; i++)
			{
				TextMeshProUGUI tmp = tmpClassList[i];
				int index = i;
				btnClassList[i].onClick.AddListener(() =>
				{
					if (panel.nowTeacherData.classIndex != tmp.text)
					{
						panel.TipElement.Show();
						return;
					}
					gameObject.SetActive(false);
					UIKit.GetPanel<GetInformationPanel>().StudentList.gameObject.SetActive(true);
				});
			}
		}


	}

}
