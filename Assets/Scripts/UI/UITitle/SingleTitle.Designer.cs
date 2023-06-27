using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using TMPro;

namespace HomeVisit.UI
{
	// Generate Id:bd22b6e4-b144-4de1-9c47-754da4ecd760
	public partial class SingleTitle
	{
		public const string Name = "title";

		public TextMeshProUGUI titleDescribe;
		public List<Toggle> togs;
		public List<TextMeshProUGUI> tmps;
		public TextMeshProUGUI tmpAnalysis;

		string rightTip = "������<color=#00ff00ff>�ش���ȷ</color>";
		string errorTip = "";

		SingleTitleData mData;
	}
}
