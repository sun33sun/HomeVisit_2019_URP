using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace HomeVisit.UI
{
	public class OutlineTitleData : UIPanelData
	{
		public string strTitleDescribe;
		public List<string> strTogList;

		public List<bool> rightList;
		public int score;
	}

	public partial class OutlineTitle : UIPanel, ITitle
	{
		[SerializeField] GameObject togPrefab;
		List<TogOutline> selectedTogList = new List<TogOutline>();
		List<TogOutline> togList = new List<TogOutline>();
		CommunicateOutlinePanel communicateOutlinePanel;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as OutlineTitleData ?? new OutlineTitleData();

			titleDescribe.text = mData.strTitleDescribe;

			communicateOutlinePanel = UIKit.GetPanel<CommunicateOutlinePanel>();

			for (int i = 0; i < mData.strTogList.Count; i++)
			{
				TogOutline newTog = Instantiate(togPrefab).GetComponent<TogOutline>();
				togList.Add(newTog);
				newTog.name = "tog" + i;
				int index = i;
				newTog.transform.GetComponentInChildren<TextMeshProUGUI>().text = mData.strTogList[index];
				newTog.transform.SetParent(transform);

				newTog.tog.onValueChanged.AddListener((isOn) =>
				{
					if (isOn)
					{
						if (selectedTogList.Count > 3)
							selectedTogList[0].tog.isOn = false;
						selectedTogList.Add(newTog);
						communicateOutlinePanel.strTogSelected.Add(mData.strTogList[index]);
					}
					else
					{
						selectedTogList.Remove(newTog);
						communicateOutlinePanel.strTogSelected.Remove(mData.strTogList[index]);
					}
				});
			}
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}

		public int GetScore()
		{
			if (GetState())
				return mData.score;
			else
				return 0;
		}

		public bool GetState()
		{
			bool isRIght = true;
			for (int i = 0; i < selectedTogList.Count; i++)
			{
				if (selectedTogList[i].tog.isOn != mData.rightList[i])
				{
					isRIght = false;
					break;
				}
			}
			return isRIght;
		}

		public void CheckTitle()
		{
			for (int i = 0; i < togList.Count; i++)
			{
				if (togList[i].tog.isOn == mData.rightList[i])
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
