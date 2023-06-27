using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using TMPro;

namespace HomeVisit.UI
{
	public partial class MultipleTitle
	{
		public TextMeshProUGUI titleDescribe;
		public List<Toggle> togs;
		public List<TextMeshProUGUI> tmps;
		public TextMeshProUGUI tmpAnalysis;

		string rightTip = "解析：<color=#00ff00ff>回答正确</color>";
		string errorTip = "";

		public MultipleTitleData mData;
	}
}
