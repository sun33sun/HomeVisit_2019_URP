using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public class SingleTitleData : TitleData
	{
		public SingleTitleData(string strTitle, int rightIndex, List<string> strOptions, int score)
		{
			this.type = TitleType.SingleTitle;
			this.strTitle = strTitle;
			this.rightIndex = rightIndex;
			this.strOptions = strOptions;
			this.score = score;
		}

		public SingleTitleData()
		{
			this.type = TitleType.SingleTitle;
		}
	}
	public partial class SingleTitle : MonoBehaviour, ITitle
	{
		bool isRight;

		public void InitData(TitleData titleData)
		{
			mData = titleData as SingleTitleData;
			titleDescribe.text = mData.strTitle;
			for (int i = 0; i < tmps.Count; i++)
			{
				tmps[i].text = mData.strOptions[i];
			}

			for (int i = 0; i < togs.Count; i++)
			{
				if(i == mData.rightIndex)
				{
					togs[i].onValueChanged.AddListener(OnRightChange);
					int tempInt = (int)'A' + i;
					errorTip = $"解析：回答错误，正确答案<color=#FF0000>{(char)tempInt}</color>";
				}
				else
				{
					togs[i].onValueChanged.AddListener(OnErrorTogValueChange);
				}
			}
		}

		void OnRightChange(bool isOn)
		{
			if (isOn)
				isRight = true;
			else
				isRight = false;
		}

		void OnErrorTogValueChange(bool isOn)
		{
			isRight = !isOn;
			if (isOn)
				isRight = false;
		}

		public int GetScore()
		{
			if (isRight)
				return mData.score;
			else
				return 0;
		}

		public bool GetExamResult()
		{
			return isRight;
		}

		public void CheckTitle()
		{
			if(isRight)
				tmpAnalysis.text = rightTip;
			else
				tmpAnalysis.text = errorTip;
		}

		public void Reset()
		{
			SetInteractable(true);
			isRight = false;
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


	}
}
