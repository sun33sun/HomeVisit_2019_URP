using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

namespace HomeVisit.UI
{
	public class SingleTitleData : UIPanelData
	{
		public int rightIndex;

		public string strDescribe;
		public string strA = "";
		public string strB = "";
		public string strC = "";
		public string strD = "";
		public int score = 0;

		public string strModule = "";
		public DateTime strStart;
	}
	public partial class SingleTitle : UIPanel, ITitle
	{
		bool isRight;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as SingleTitleData;

			titleDescribe.text = mData.strDescribe;
			tmpA.text = mData.strA;
			tmpB.text = mData.strB;
			tmpC.text = mData.strC;
			tmpD.text = mData.strD;


			switch (mData.rightIndex)
			{
				case 0:
					togA.onValueChanged.AddListener(OnRightChange);
					togB.onValueChanged.AddListener(OnErrorTogValueChange);
					togC.onValueChanged.AddListener(OnErrorTogValueChange);
					togD.onValueChanged.AddListener(OnErrorTogValueChange);
					errorTip = "解析：回答错误，正确答案<color=#ff0000ff>A</color>";
					break;
				case 1:
					togA.onValueChanged.AddListener(OnErrorTogValueChange);
					togB.onValueChanged.AddListener(OnRightChange);
					togC.onValueChanged.AddListener(OnErrorTogValueChange);
					togD.onValueChanged.AddListener(OnErrorTogValueChange);
					errorTip = "解析：回答错误，正确答案<color=#ff0000ff>B</color>";
					break;
				case 2:
					togA.onValueChanged.AddListener(OnErrorTogValueChange);
					togB.onValueChanged.AddListener(OnErrorTogValueChange);
					togC.onValueChanged.AddListener(OnRightChange);
					togD.onValueChanged.AddListener(OnErrorTogValueChange);
					errorTip = "解析：回答错误，正确答案<color=#ff0000ff>C</color>";
					break;
				case 3:
					togA.onValueChanged.AddListener(OnErrorTogValueChange);
					togB.onValueChanged.AddListener(OnErrorTogValueChange);
					togC.onValueChanged.AddListener(OnErrorTogValueChange);
					togD.onValueChanged.AddListener(OnRightChange);
					errorTip = "解析：回答错误，正确答案<color=#ff0000ff>D</color>";
					break;
			}
		}

		void OnRightChange(bool isOn)
		{
			if (isOn)
			{
				isRight = true;
			}
			else
			{
				isRight = false;
			}
		}

		void OnErrorTogValueChange(bool isOn)
		{
			isRight = !isOn;
			if (isOn)
			{
				isRight = false;
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
			if (isRight)
				return mData.score;
			else
				return 0;
		}

		public ScoreReportData GetScoreReportData()
		{
			ScoreReportData data = new ScoreReportData()
			{
				strModule = mData.strModule,
				strStart = mData.strStart,
				strEnd = DateTime.Now,
				strScore = (isRight? mData.score : 0).ToString()
			};
			return data;
		}

		public bool GetState()
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
