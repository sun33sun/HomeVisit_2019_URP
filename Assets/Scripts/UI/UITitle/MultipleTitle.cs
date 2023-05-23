using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Text;
using System;

namespace HomeVisit.UI
{
	public class MultipleTitleData : UIPanelData
	{
		public bool[] isRights = new bool[4];

		public string strDescribe;
		public string strA = "";
		public string strB = "";
		public string strC = "";
		public string strD = "";
		public int score = 0;

		public string strModule = "";
	}
	public partial class MultipleTitle : UIPanel, ITitle
	{
		bool[] isSelected = new bool[4];
		int rightCount = 0;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as MultipleTitleData ?? new MultipleTitleData();

			titleDescribe.text = mData.strDescribe;
			tmpA.text = mData.strA;
			tmpB.text = mData.strB;
			tmpC.text = mData.strC;
			tmpD.text = mData.strD;

			StringBuilder strError = new StringBuilder();
			for (int i = 0; i < mData.isRights.Length; i++)
			{
				if (mData.isRights[i])
					rightCount++;
			}

			togA.onValueChanged.AddListener(isOn =>
			{
				isSelected[0] = isOn;
				GetState();
			});
			togB.onValueChanged.AddListener(isOn =>
			{
				isSelected[1] = isOn;
				GetState();
			});
			togC.onValueChanged.AddListener(isOn =>
			{
				isSelected[2] = isOn;
				GetState();
			});
			togD.onValueChanged.AddListener(isOn =>
			{
				isSelected[3] = isOn;
				GetState();
			});

			if (mData.isRights[0])
				strError.Append('A');
			if (mData.isRights[1])
				strError.Append('B');
			if (mData.isRights[2])
				strError.Append('C');
			if (mData.isRights[3])
				strError.Append('D');

			errorTip = $"解析：回答错误，正确答案<color=#ff0000ff>{strError}</color>";
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
			bool allRight = true;
			int selectedCount = 0;
			for (int i = 0; i < mData.isRights.Length; i++)
			{
				if (mData.isRights[i] != isSelected[i])
					allRight = false;
				if (isSelected[i])
					selectedCount++;
			}

			return allRight;
		}

		public void CheckTitle()
		{
			bool allRight = true;
			int selectedCount = 0;
			for (int i = 0; i < mData.isRights.Length; i++)
			{
				if (mData.isRights[i] != isSelected[i])
				{
					allRight = false;
					break;
				}
				if (isSelected[i])
					selectedCount++;
			}
			if (allRight)
				tmpAnalysis.text = rightTip;
			else if (selectedCount >= rightCount || !allRight)
				tmpAnalysis.text = errorTip;
			else
				tmpAnalysis.text = "解析：";
		}

		public void Reset()
		{
			SetInteractable(true);
			tmpAnalysis.text = "解析：";
			togA.isOn = false;
			togB.isOn = false;
			togC.isOn = false;
			togD.isOn = false;
		}

		public void SetInteractable(bool newState)
		{
			togA.interactable = newState;
			togB.interactable = newState;
			togC.interactable = newState;
			togD.interactable = newState;
		}

		public bool GetInteractable()
		{
			return togA.interactable;
		}
	}
}
