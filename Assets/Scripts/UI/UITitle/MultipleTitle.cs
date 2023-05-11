using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Text;

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
				CheckState();
			});
			togB.onValueChanged.AddListener(isOn =>
			{
				isSelected[1] = isOn;
				CheckState();
			});
			togC.onValueChanged.AddListener(isOn =>
			{
				isSelected[2] = isOn;
				CheckState();
			});
			togD.onValueChanged.AddListener(isOn =>
			{
				isSelected[3] = isOn;
				CheckState();
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

		bool CheckState()
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

			if (allRight)
				tmpAnalysis.text = rightTip;
			else if (selectedCount >= rightCount)
				tmpAnalysis.text = errorTip;
			else
				tmpAnalysis.text = "解析：";

			return allRight;
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
			if (CheckState())
				return mData.score;
			else
				return 0;
		}
	}
}
