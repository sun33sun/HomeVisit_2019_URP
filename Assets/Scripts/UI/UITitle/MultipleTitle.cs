using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Text;
using System;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public class MultipleTitleData : TitleData
	{
		public MultipleTitleData(string strTitle, List<bool> rightIndexs, List<string> strOptions, int score)
		{
			this.type = TitleType.MultipleTitle;
			this.strTitle = strTitle;
			this.rightIndexs = rightIndexs;
			this.strOptions = strOptions;
			this.score = score;
		}

		public MultipleTitleData()
		{
			this.type = TitleType.MultipleTitle;
		}
	}
	public partial class MultipleTitle : MonoBehaviour, ITitle
	{
		bool[] isSelected = new bool[4];
		int rightCount = 0;

		public int GetScore()
		{
			if (GetExamResult())
				return mData.score;
			else
				return 0;
		}

		public bool GetExamResult()
		{
			bool allRight = true;
			int selectedCount = 0;
			for (int i = 0; i < mData.rightIndexs.Count; i++)
			{
				if (mData.rightIndexs[i] != isSelected[i])
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
			for (int i = 0; i < mData.rightIndexs.Count; i++)
			{
				if (mData.rightIndexs[i] != isSelected[i])
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
			foreach (var item in togs)
			{
				item.isOn = false;
			}
		}

		public void SetInteractable(bool newState)
		{
			foreach (var item in togs)
			{
				item.interactable = newState;
			}
		}

		public bool GetInteractable()
		{
			return togs[0].interactable;
		}

		public void InitData(TitleData titleData)
		{
			mData = titleData as MultipleTitleData;

			titleDescribe.text = mData.strTitle;
			for (int i = 0; i < tmps.Count; i++)
			{
				tmps[i].text = mData.strOptions[i];
			}

			StringBuilder strError = new StringBuilder();
			rightCount = mData.rightIndexs.Count;
			for (int i = 0; i < togs.Count; i++)
			{
				int index = i;
				togs[i].onValueChanged.AddListener(isOn =>
				{
					isSelected[index] = isOn;
					GetExamResult();
				});
			}

			for (int i = 0; i < mData.rightIndexs.Count; i++)
			{
				if (mData.rightIndexs[i])
				{
					int temp = (int)'A' + i;
					strError.Append((char)temp);
				}
			}

			errorTip = $"解析：回答错误，正确答案<color=#FF0000>{strError}</color>";
		}
	}
}
