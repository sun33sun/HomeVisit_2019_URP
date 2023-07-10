using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace HomeVisit.UI
{
	public class OutlineTitleData : TitleData
	{
		public OutlineTitleData(string strTitle, List<bool> rightIndexs, List<string> strOptions, int score)
		{
			this.type = TitleType.OutlineTitle;
			this.strTitle = strTitle;
			this.rightIndexs = rightIndexs;
			this.strOptions = strOptions;
			this.score = score;
		}

		public OutlineTitleData()
		{
			this.type = TitleType.OutlineTitle;
		}
	}

	public partial class OutlineTitle : MonoBehaviour, ITitle
	{
		[SerializeField] GameObject togPrefab;
		List<TogOutline> selectedTogList = new List<TogOutline>();
		[SerializeField]List<TogOutline> togList = new List<TogOutline>();
		CommunicateOutlinePanel communicateOutlinePanel;

		public void InitData(TitleData titleData)
		{
			mData = titleData as OutlineTitleData;

			titleDescribe.text = mData.strTitle;

			communicateOutlinePanel = UIKit.GetPanel<CommunicateOutlinePanel>();

			for (int i = 0; i < mData.strOptions.Count; i++)
			{
				TogOutline newTog = Instantiate(togPrefab).GetComponent<TogOutline>();
				togList.Add(newTog);
				newTog.name = "tog" + i;
				int index = i;
				newTog.transform.GetComponentInChildren<TextMeshProUGUI>().text = mData.strOptions[index];
				newTog.transform.SetParent(transform);

				newTog.tog.onValueChanged.AddListener((isOn) =>
				{
					if (isOn)
					{
						if (selectedTogList.Count > 3)
							selectedTogList[0].tog.isOn = false;
						selectedTogList.Add(newTog);
						communicateOutlinePanel.strTogSelected.Add(mData.strOptions[index]);
					}
					else
					{
						selectedTogList.Remove(newTog);
						communicateOutlinePanel.strTogSelected.Remove(mData.strOptions[index]);
					}
				});
			}
		}

		public int GetScore()
		{
			if (GetExamResult())
				return mData.score;
			else
				return 0;
		}

		public bool GetExamResult()
		{
			bool isRIght = true;
			if (selectedTogList.Count < 1)
			{
				isRIght = false;
			}
			else
			{
				for (int i = 0; i < selectedTogList.Count; i++)
				{
					if (selectedTogList[i].tog.isOn != mData.rightIndexs[i])
					{
						isRIght = false;
						break;
					}
				}
			}

			return isRIght;
		}

		public void CheckTitle()
		{
			for (int i = 0; i < togList.Count; i++)
			{
				if (togList[i].tog.isOn == mData.rightIndexs[i])
					togList[i].ShowState(1);
				else
					togList[i].ShowState(-1);
			}
		}

		public void Reset()
		{
			SetInteractable(true);
			communicateOutlinePanel.strTogSelected.Clear();
			for (int i = 0; i < togList.Count; i++)
				togList[i].tog.isOn = false;
			for (int i = 0; i < togList.Count; i++)
				togList[i].ShowState(0);
		}

		public void SetInteractable(bool newState)
		{
			for (int i = 0; i < togList.Count; i++)
				togList[i].tog.interactable = newState;
		}

		public bool GetInteractable()
		{
			if (togList.Count < 1)
				return false;
			return togList[0].tog.interactable;
		}
	}
}
