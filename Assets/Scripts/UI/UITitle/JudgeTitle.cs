using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using TMPro;

namespace HomeVisit.UI
{
	public class JudgeTitleData : TitleData
	{
		public JudgeTitleData(string strTitle, int rightIndex, List<string> strOptions, int score)
		{
			this.type = TitleType.JudgeTitle;
			this.strTitle = strTitle;
			this.rightIndex = rightIndex;
			this.strOptions = strOptions;
			this.score = score;
		}

		public JudgeTitleData()
		{
			this.type = TitleType.JudgeTitle;
		}
	}
	public partial class JudgeTitle : MonoBehaviour, ITitle
	{
		public JudgeTitleData mData;

		public TextMeshProUGUI tmpDescribe;
		public List<Toggle> togs;
		public List<TextMeshProUGUI> tmps;
		public TextMeshProUGUI tmpAnalysis;
		public int selectIndex = 0;

		private void Start()
		{
			for (int i = 0; i < togs.Count; i++)
			{
				int index = i;
				togs[i].onValueChanged.AddListener(isOn =>
				{
					if (isOn)
						selectIndex = index;
				});
			}
		}

		public void CheckTitle()
		{
			if (selectIndex == mData.rightIndex)
			{
				tmpAnalysis.text = "解析：<color=#00FF00>回答正确</color>";
			}
			else
			{
				char rightOption = (char)((int)('A') + mData.rightIndex);
				tmpAnalysis.text = "解析：回答错误，正确答案<color=#FF0000> " + rightOption + " </color>";
			}
		}

		public bool GetExamResult()
		{
			if (mData.rightIndex == selectIndex)
				return true;
			else
				return false;
		}

		public bool GetInteractable()
		{
			return togs[0].interactable;
		}

		public int GetScore()
		{
			if (selectIndex == mData.rightIndex)
				return mData.score;
			else
				return 0;
		}

		public void InitData(TitleData titleData)
		{
			mData = titleData as JudgeTitleData;
			for (int i = 0; i < togs.Count; i++)
			{
				int index = i;
				togs[i].onValueChanged.AddListener(isOn =>
				{
					if (isOn)
						selectIndex = index;
				});
			}
			tmpDescribe.text = mData.strTitle;
			for (int i = 0; i < tmps.Count; i++)
			{
				tmps[i].text = mData.strOptions[i];
			}
		}

		public void Reset()
		{
			if (selectIndex != -1)
			{
				togs[selectIndex].isOn = false;
				selectIndex = -1;
			}
		}

		public void SetInteractable(bool newState)
		{
			for (int i = 0; i < togs.Count; i++)
			{
				togs[i].interactable = newState;
			}
		}
	}
}
